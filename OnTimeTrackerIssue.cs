using System;
using System.Collections.Generic;
using System.Data;
using Inedo.BuildMaster.Extensibility.Providers.IssueTracking;
using Inedo.BuildMasterExtensions.Axosoft.Data;

namespace Inedo.BuildMasterExtensions.Axosoft
{
    [Serializable]
    public sealed class OnTimeTrackerIssue : IssueTrackerIssue
    {
        private OnTimeTrackerIssue(int id, string title, string description, string status, string release, IssueType type)
            : base(id.ToString() + ((type == IssueType.Defect) ? "d" : "f"), status, string.Format("{0}: {1}", type, title), description, release)
        {
            this.Id = id;
            this.IssueType = type;
        }

        public int Id { get; private set; }
        public IssueType IssueType { get; private set; }

        internal static OnTimeTrackerIssue FromDefect(DataRow defectRow, string release, Dictionary<int, string> statuses)
        {
            var id = (int)defectRow[TableDefs.Defects.DefectId];
            var title = (defectRow[TableDefs.Defects.Name] ?? "").ToString();
            var description = (defectRow[TableDefs.Defects.Description] ?? "").ToString();
            string status = string.Empty;

            if (defectRow[TableDefs.Defects.StatusTypeId] is int)
            {
                if (!statuses.TryGetValue((int)defectRow[TableDefs.Defects.StatusTypeId], out status))
                    status = string.Empty;
            }

            return new OnTimeTrackerIssue(id, title, description, status, release, IssueType.Defect);
        }

        internal static OnTimeTrackerIssue FromFeature(DataRow featureRow, string release, Dictionary<int, string> statuses)
        {
            var id = (int)featureRow[TableDefs.Features.FeatureId];
            var title = (featureRow[TableDefs.Features.Name] ?? "").ToString();
            var description = (featureRow[TableDefs.Features.Description] ?? "").ToString();
            string status = string.Empty;

            if (featureRow[TableDefs.Features.StatusTypeId] is int)
            {
                if (!statuses.TryGetValue((int)featureRow[TableDefs.Features.StatusTypeId], out status))
                    status = string.Empty;
            }

            return new OnTimeTrackerIssue(id, title, description, status, release, IssueType.Feature);
        }
    }

    public enum IssueType
    {
        Defect,
        Feature
    }
}
