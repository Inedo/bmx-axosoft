using System;
using System.Collections.Generic;
using System.Data;
using Inedo.BuildMaster;
using Inedo.BuildMaster.Extensibility.Providers;
using Inedo.BuildMaster.Extensibility.Providers.IssueTracking;
using Inedo.BuildMaster.Web;
using Inedo.BuildMasterExtensions.Axosoft.Data;

namespace Inedo.BuildMasterExtensions.Axosoft
{
    /// <summary>
    /// Issue tracker provider for connecting to an Axosoft OnTime issue tracking system.
    /// </summary>
    [ProviderProperties(
      "Axosoft OnTime",
      "Supports Axosoft 10.0 and later; requires that the OnTime SDK is installed.")]
    [CustomEditor(typeof(OnTimeTrackerProviderEditor))]
    public sealed class OnTimeTrackerProvider : IssueTrackingProviderBase, ICategoryFilterable, IUpdatingProvider
    {
        private Guid securityToken;
        private AxoProject.ProjectHandler projectHandler;
        private AxoDefect.DefectHandler defectHandler;
        private AxoFeature.FeatureHandler featureHandler;
        private AxoRelease.ReleaseHandler releaseHandler;
        private AxoTypes.TypeHandler typeHandler;
        private string[] categoryNames;

        private const string DefectUrl = "defects/viewdefect.aspx?defectid={0}";
        private const string FeatureUrl = "features/viewfeature.aspx?featureid={0}";

        //http://localhost/OnTime2010Web/defects/viewdefect.aspx?defectid=36

        /// <summary>
        /// Initializes a new instance of the <see cref="OnTimeTrackerProvider"/> class.
        /// </summary>
        public OnTimeTrackerProvider()
        {
        }

        /// <summary>
        /// Gets or sets the URL of the issue tracker web service.
        /// </summary>
        [Persistent]
        public string BaseUrl { get; set; }

        /// <summary>
        /// Gets or sets the URL of the OnTimeWeb site.
        /// </summary>
        [Persistent]
        public string OnTimeWebUrl { get; set; }

        /// <summary>
        /// Gets or sets the issue tracker web service security token. This must be a GUID.
        /// </summary>
        [Persistent]
        public string SecurityToken
        {
            get { return this.securityToken.ToString(); }
            set
            {
                try
                {
                    this.securityToken = new Guid(value);
                }
                catch
                {
                    this.securityToken = Guid.Empty;
                }
            }
        }

        public string[] CategoryIdFilter { get; set; }
        public string[] CategoryTypeNames
        {
            get
            {
                if (this.categoryNames == null)
                {
                    int maxLevel = GetMaximumNestingLevel();
                    if (maxLevel == 0)
                        this.categoryNames = new string[0];
                    else if (maxLevel == 1)
                        this.categoryNames = new string[1] { "Project" };
                    else
                    {
                        this.categoryNames = new string[maxLevel];
                        this.categoryNames[0] = "Project";
                        for (int i = 1; i < this.categoryNames.Length; i++)
                            this.categoryNames[i] = "Subproject";
                    }
                }

                return this.categoryNames;
            }
        }
        public bool CanAppendIssueDescriptions
        {
            get { return true; }
        }
        public bool CanChangeIssueStatuses
        {
            get { return true; }
        }
        public bool CanCloseIssues
        {
            get { return true; }
        }

        private AxoProject.ProjectHandler ProjectHandler
        {
            get { return this.projectHandler ?? (this.projectHandler = new AxoProject.ProjectHandler() { Url = CombinePaths(this.BaseUrl, "ProjectService.asmx") }); }
        }
        private AxoDefect.DefectHandler DefectHandler
        {
            get { return this.defectHandler ?? (this.defectHandler = new AxoDefect.DefectHandler() { Url = CombinePaths(this.BaseUrl, "DefectService.asmx") }); }
        }
        private AxoFeature.FeatureHandler FeatureHandler
        {
            get { return this.featureHandler ?? (this.featureHandler = new AxoFeature.FeatureHandler() { Url = CombinePaths(this.BaseUrl, "FeatureService.asmx") }); }
        }
        private AxoRelease.ReleaseHandler ReleaseHandler
        {
            get { return this.releaseHandler ?? (this.releaseHandler = new AxoRelease.ReleaseHandler() { Url = CombinePaths(this.BaseUrl, "ReleaseService.asmx") }); }
        }
        private AxoTypes.TypeHandler TypeHandler
        {
            get { return this.typeHandler ?? (this.typeHandler = new AxoTypes.TypeHandler() { Url = CombinePaths(this.BaseUrl, "TypeService.asmx") }); }
        }

        /// <summary>
        /// Gets a URL to the specified issue.
        /// </summary>
        /// <param name="issue">The issue whose URL is returned.</param>
        /// <returns>
        /// The URL of the specified issue if applicable; otherwise null.
        /// </returns>
        public override string GetIssueUrl(IssueTrackerIssue issue)
        {
            if (issue == null)
                throw new ArgumentNullException("issue");

            if (string.IsNullOrEmpty(this.OnTimeWebUrl))
                return null;

            var onTimeIssue = (OnTimeTrackerIssue)issue;
            string urlFormat;
            if (onTimeIssue.IssueType == IssueType.Defect)
                urlFormat = DefectUrl;
            else if (onTimeIssue.IssueType == IssueType.Feature)
                urlFormat = FeatureUrl;
            else
                throw new ArgumentException("Unsupported issue type.");

            return CombinePaths(this.OnTimeWebUrl, string.Format(urlFormat, onTimeIssue.Id));
        }

        public override IssueTrackerIssue[] GetIssues(string releaseNumber)
        {
            var projectId = GetProjectFilter();
            if (projectId == null)
                return new IssueTrackerIssue[0];

            var releases = new Dictionary<int, DataRow>();
            var releaseRows = this.ReleaseHandler.GetAllReleases(this.securityToken).Tables[0].Rows;
            foreach (DataRow release in releaseRows)
                releases[(int)release[TableDefs.Releases.ReleaseId]] = release;

            var statusRows = this.TypeHandler.GetStatusTypes(this.securityToken).Tables[0].Rows;
            var statuses = new Dictionary<int, string>();
            foreach (DataRow status in statusRows)
                statuses[(int)status[TableDefs.StatusTypes.StatusTypeId]] = (status[TableDefs.StatusTypes.Name] ?? "").ToString();

            var issues = new List<OnTimeTrackerIssue>();

            var defectRows = this.DefectHandler.GetDefectsByProject(this.securityToken, (int)projectId).Tables[0].Rows;
            foreach (DataRow defect in defectRows)
            {
                if (defect[TableDefs.Defects.ReleaseId] is int)
                {
                    if(IsInRelease(releaseNumber, (int)defect[TableDefs.Defects.ReleaseId], releases))
                        issues.Add(OnTimeTrackerIssue.FromDefect(defect, releaseNumber, statuses));
                }
            }

            var featureRows = this.FeatureHandler.GetFeaturesByProject(this.securityToken, (int)projectId).Tables[0].Rows;
            foreach (DataRow feature in featureRows)
            {
                if (feature[TableDefs.Features.ReleaseId] is int)
                {
                    if(IsInRelease(releaseNumber, (int)feature[TableDefs.Features.ReleaseId], releases))
                        issues.Add(OnTimeTrackerIssue.FromFeature(feature, releaseNumber, statuses));
                }
            }

            return issues.ToArray();
        }
        public override bool IsIssueClosed(IssueTrackerIssue issue)
        {
            if (issue == null)
                throw new ArgumentNullException("issue");

            var statusRows = this.TypeHandler.GetStatusTypes(this.securityToken).Tables[0].Rows;
            var statuses = new Dictionary<int, string>();
            foreach (DataRow status in statusRows)
                statuses[(int)status[TableDefs.StatusTypes.StatusTypeId]] = (status[TableDefs.StatusTypes.Name] ?? "").ToString();

            string statusText = "";

            var onTimeIssue = (OnTimeTrackerIssue)issue;
            if (onTimeIssue.IssueType == IssueType.Defect)
            {
                var defect = this.DefectHandler.GetByDefectId(this.securityToken, onTimeIssue.Id);
                statuses.TryGetValue(defect.StatusTypeId, out statusText);
            }
            else
            {
                var feature = this.FeatureHandler.GetByFeatureId(this.securityToken, onTimeIssue.Id);
                statuses.TryGetValue(feature.StatusTypeId, out statusText);
            }

            return string.Equals(statusText ?? "", "Closed", StringComparison.InvariantCultureIgnoreCase);
        }
        public override bool IsAvailable()
        {
            return true;
        }
        public override void ValidateConnection()
        {
            try
            {
                this.ProjectHandler.GetAllProjects(this.securityToken);
            }
            catch (Exception ex)
            {
                var message = "Verify that the OnTime SDK is installed and configured on the OnTime server. Full error message: " + ex.Message;
                throw new NotAvailableException(message, ex);
            }
        }
        public override string ToString()
        {
            return "Connects to the issue tracking system of Axosoft OnTime.";
        }
        public IssueTrackerCategory[] GetCategories()
        {
            var allProjects = GetAllProjects();
            return allProjects
                .FindAll(r => object.Equals(r[TableDefs.Projects.ParentId], 0))
                .ConvertAll(r => GetProject(allProjects, r)).ToArray();
        }
        public void AppendIssueDescription(string issueId, string textToAppend)
        {
            if (string.IsNullOrEmpty(issueId))
                throw new ArgumentNullException("issueId");

            int id;
            if (!int.TryParse(issueId.Substring(0, issueId.Length - 1), out id))
                throw new ArgumentException("Invalid issue ID.");

            if (string.IsNullOrEmpty(textToAppend))
                return;

            if (issueId.EndsWith("d", StringComparison.InvariantCultureIgnoreCase))
            {
                var defect = this.DefectHandler.GetByDefectId(this.securityToken, id);
                defect.Description = defect.Description ?? "";
                defect.Description += textToAppend;
                this.DefectHandler.UpdateDefect(this.securityToken, defect);
            }
            else if (issueId.EndsWith("f", StringComparison.InvariantCultureIgnoreCase))
            {
                var feature = this.FeatureHandler.GetByFeatureId(this.securityToken, id);
                feature.Description = feature.Description ?? "";
                feature.Description += textToAppend;
                this.FeatureHandler.UpdateFeature(this.securityToken, feature);
            }
            else
                throw new ArgumentException("Invalid issue ID.");
        }
        public void ChangeIssueStatus(string issueId, string newStatus)
        {
            if (string.IsNullOrEmpty(issueId))
                throw new ArgumentNullException("issueId");
            if (string.IsNullOrEmpty(newStatus))
                throw new ArgumentNullException("newStatus");

            int? statusId = null;
            var statusRows = this.TypeHandler.GetStatusTypes(this.securityToken).Tables[0].Rows;
            foreach(DataRow row in statusRows)
            {
                if (string.Equals((row[TableDefs.StatusTypes.Name] ?? "").ToString(), newStatus, StringComparison.CurrentCultureIgnoreCase))
                {
                    statusId = (int)row[TableDefs.StatusTypes.StatusTypeId];
                    break;
                }
            }

            if (statusId == null)
                throw new ArgumentException("Invalid status.");

            int id;
            if (!int.TryParse(issueId.Substring(0, issueId.Length - 1), out id))
                throw new ArgumentException("Invalid issue ID.");

            if (issueId.EndsWith("d", StringComparison.InvariantCultureIgnoreCase))
            {
                var defect = this.DefectHandler.GetByDefectId(this.securityToken, id);
                defect.StatusTypeId = (int)statusId;
                this.DefectHandler.UpdateDefect(this.securityToken, defect);
            }
            else if (issueId.EndsWith("f", StringComparison.InvariantCultureIgnoreCase))
            {
                var feature = this.FeatureHandler.GetByFeatureId(this.securityToken, id);
                feature.StatusTypeId = (int)statusId;
                this.FeatureHandler.UpdateFeature(this.securityToken, feature);
            }
            else
                throw new ArgumentException("Invalid issue ID.");
        }
        public void CloseIssue(string issueId)
        {
            if (string.IsNullOrEmpty(issueId))
                throw new ArgumentNullException("issueId");

            ChangeIssueStatus(issueId, "Closed");
        }

        /// <summary>
        /// Combines two URL's.
        /// </summary>
        /// <param name="baseUrl">Root URL element.</param>
        /// <param name="relativeUrl">Relative URL element.</param>
        /// <returns>Combined URL.</returns>
        internal static string CombinePaths(string baseUrl, string relativeUrl)
        {
            if (baseUrl.EndsWith("/"))
            {
                return relativeUrl.StartsWith("/")
                    ? baseUrl + relativeUrl.Substring(1, relativeUrl.Length - 1)
                    : baseUrl + relativeUrl;
            }
            else
            {
                return relativeUrl.StartsWith("/")
                    ? baseUrl + relativeUrl
                    : baseUrl + "/" + relativeUrl;
            }
        }

        private static bool IsInRelease(string filterName, int releaseId, Dictionary<int, DataRow> allReleases)
        {
            DataRow row;
            if (!allReleases.TryGetValue(releaseId, out row))
                return false;

            var releaseName = (row[TableDefs.Releases.Name] ?? "").ToString();
            if (string.Equals(filterName, releaseName, StringComparison.CurrentCultureIgnoreCase))
                return true;

            if (!(row[TableDefs.Releases.ParentReleaseId] is int))
                return false;

            return IsInRelease(filterName, (int)row[TableDefs.Releases.ParentReleaseId], allReleases);
        }
        private static void GetMaximumNestingLevel(List<DataRow> allProjects, int parentId, ref int level, ref int maxLevel)
        {
            var matches = allProjects.FindAll(r => object.Equals(r[TableDefs.Projects.ParentId], parentId));
            if (matches.Count > 0)
            {
                level++;
                if (level > maxLevel)
                    maxLevel = level;

                foreach (var project in matches)
                    GetMaximumNestingLevel(allProjects, (int)project[TableDefs.Projects.ProjectId], ref level, ref maxLevel);

                level--;
            }
        }
        private static OnTimeTrackerCategory GetProject(List<DataRow> allProjects, DataRow projectRow)
        {
            var children = allProjects
                .FindAll(r => object.Equals(r[TableDefs.Projects.ParentId], projectRow[TableDefs.Projects.ProjectId]))
                .ConvertAll(r => GetProject(allProjects, r));
            return new OnTimeTrackerCategory(projectRow[TableDefs.Projects.ProjectId].ToString(), projectRow[TableDefs.Projects.Name].ToString(), children);
        }

        private int GetMaximumNestingLevel()
        {
            int level = 0;
            int maxLevel = 0;
            var allProjects = GetAllProjects();
            GetMaximumNestingLevel(allProjects, 0, ref level, ref maxLevel);
            return maxLevel;
        }
        private List<DataRow> GetAllProjects()
        {
            var allProjects = this.ProjectHandler.GetAllProjects(this.securityToken).Tables[0].Rows;
            var projectArray = new DataRow[allProjects.Count];
            allProjects.CopyTo(projectArray, 0);
            return new List<DataRow>(projectArray);
        }
        private int? GetProjectFilter()
        {
            if (this.CategoryIdFilter == null || this.CategoryIdFilter.Length == 0)
                return null;

            for (int i = this.CategoryIdFilter.Length - 1; i >= 0; i--)
            {
                if (!string.IsNullOrEmpty(this.CategoryIdFilter[i]))
                {
                    var value = Util.Int.ParseN(this.CategoryIdFilter[i]);
                    if (value != null)
                        return (int)value;
                }
            }

            return null;
        }
    }
}
