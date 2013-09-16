﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18051
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by Microsoft.VSDesigner, Version 4.0.30319.18051.
// 
#pragma warning disable 1591

namespace Inedo.BuildMasterExtensions.Axosoft.AxoRelease {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.Xml.Serialization;
    using System.ComponentModel;
    using System.Data;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="ReleaseHandlerSoap", Namespace="http://axosoft.com/ontime/webservices/")]
    public partial class ReleaseHandler : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback GetAllReleasesOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetByReleaseIdOperationCompleted;
        
        private System.Threading.SendOrPostCallback AddReleaseOperationCompleted;
        
        private System.Threading.SendOrPostCallback UpdateReleaseOperationCompleted;
        
        private System.Threading.SendOrPostCallback DeleteReleaseOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public ReleaseHandler() {
            this.Url = "http://localhost:8080/ReleaseService.asmx";
            if ((this.IsLocalFileSystemWebService(this.Url) == true)) {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        public new string Url {
            get {
                return base.Url;
            }
            set {
                if ((((this.IsLocalFileSystemWebService(base.Url) == true) 
                            && (this.useDefaultCredentialsSetExplicitly == false)) 
                            && (this.IsLocalFileSystemWebService(value) == false))) {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }
        
        public new bool UseDefaultCredentials {
            get {
                return base.UseDefaultCredentials;
            }
            set {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        /// <remarks/>
        public event GetAllReleasesCompletedEventHandler GetAllReleasesCompleted;
        
        /// <remarks/>
        public event GetByReleaseIdCompletedEventHandler GetByReleaseIdCompleted;
        
        /// <remarks/>
        public event AddReleaseCompletedEventHandler AddReleaseCompleted;
        
        /// <remarks/>
        public event UpdateReleaseCompletedEventHandler UpdateReleaseCompleted;
        
        /// <remarks/>
        public event DeleteReleaseCompletedEventHandler DeleteReleaseCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://axosoft.com/ontime/webservices/GetAllReleases", RequestNamespace="http://axosoft.com/ontime/webservices/", ResponseNamespace="http://axosoft.com/ontime/webservices/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Data.DataSet GetAllReleases(System.Guid securityToken) {
            object[] results = this.Invoke("GetAllReleases", new object[] {
                        securityToken});
            return ((System.Data.DataSet)(results[0]));
        }
        
        /// <remarks/>
        public void GetAllReleasesAsync(System.Guid securityToken) {
            this.GetAllReleasesAsync(securityToken, null);
        }
        
        /// <remarks/>
        public void GetAllReleasesAsync(System.Guid securityToken, object userState) {
            if ((this.GetAllReleasesOperationCompleted == null)) {
                this.GetAllReleasesOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetAllReleasesOperationCompleted);
            }
            this.InvokeAsync("GetAllReleases", new object[] {
                        securityToken}, this.GetAllReleasesOperationCompleted, userState);
        }
        
        private void OnGetAllReleasesOperationCompleted(object arg) {
            if ((this.GetAllReleasesCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetAllReleasesCompleted(this, new GetAllReleasesCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://axosoft.com/ontime/webservices/GetByReleaseId", RequestNamespace="http://axosoft.com/ontime/webservices/", ResponseNamespace="http://axosoft.com/ontime/webservices/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public Release GetByReleaseId(System.Guid securityToken, int releaseId) {
            object[] results = this.Invoke("GetByReleaseId", new object[] {
                        securityToken,
                        releaseId});
            return ((Release)(results[0]));
        }
        
        /// <remarks/>
        public void GetByReleaseIdAsync(System.Guid securityToken, int releaseId) {
            this.GetByReleaseIdAsync(securityToken, releaseId, null);
        }
        
        /// <remarks/>
        public void GetByReleaseIdAsync(System.Guid securityToken, int releaseId, object userState) {
            if ((this.GetByReleaseIdOperationCompleted == null)) {
                this.GetByReleaseIdOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetByReleaseIdOperationCompleted);
            }
            this.InvokeAsync("GetByReleaseId", new object[] {
                        securityToken,
                        releaseId}, this.GetByReleaseIdOperationCompleted, userState);
        }
        
        private void OnGetByReleaseIdOperationCompleted(object arg) {
            if ((this.GetByReleaseIdCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetByReleaseIdCompleted(this, new GetByReleaseIdCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://axosoft.com/ontime/webservices/AddRelease", RequestNamespace="http://axosoft.com/ontime/webservices/", ResponseNamespace="http://axosoft.com/ontime/webservices/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public int AddRelease(System.Guid securityToken, Release release) {
            object[] results = this.Invoke("AddRelease", new object[] {
                        securityToken,
                        release});
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        public void AddReleaseAsync(System.Guid securityToken, Release release) {
            this.AddReleaseAsync(securityToken, release, null);
        }
        
        /// <remarks/>
        public void AddReleaseAsync(System.Guid securityToken, Release release, object userState) {
            if ((this.AddReleaseOperationCompleted == null)) {
                this.AddReleaseOperationCompleted = new System.Threading.SendOrPostCallback(this.OnAddReleaseOperationCompleted);
            }
            this.InvokeAsync("AddRelease", new object[] {
                        securityToken,
                        release}, this.AddReleaseOperationCompleted, userState);
        }
        
        private void OnAddReleaseOperationCompleted(object arg) {
            if ((this.AddReleaseCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.AddReleaseCompleted(this, new AddReleaseCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://axosoft.com/ontime/webservices/UpdateRelease", RequestNamespace="http://axosoft.com/ontime/webservices/", ResponseNamespace="http://axosoft.com/ontime/webservices/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public void UpdateRelease(System.Guid securityToken, Release release) {
            this.Invoke("UpdateRelease", new object[] {
                        securityToken,
                        release});
        }
        
        /// <remarks/>
        public void UpdateReleaseAsync(System.Guid securityToken, Release release) {
            this.UpdateReleaseAsync(securityToken, release, null);
        }
        
        /// <remarks/>
        public void UpdateReleaseAsync(System.Guid securityToken, Release release, object userState) {
            if ((this.UpdateReleaseOperationCompleted == null)) {
                this.UpdateReleaseOperationCompleted = new System.Threading.SendOrPostCallback(this.OnUpdateReleaseOperationCompleted);
            }
            this.InvokeAsync("UpdateRelease", new object[] {
                        securityToken,
                        release}, this.UpdateReleaseOperationCompleted, userState);
        }
        
        private void OnUpdateReleaseOperationCompleted(object arg) {
            if ((this.UpdateReleaseCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.UpdateReleaseCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://axosoft.com/ontime/webservices/DeleteRelease", RequestNamespace="http://axosoft.com/ontime/webservices/", ResponseNamespace="http://axosoft.com/ontime/webservices/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public void DeleteRelease(System.Guid securityToken, int releaseId) {
            this.Invoke("DeleteRelease", new object[] {
                        securityToken,
                        releaseId});
        }
        
        /// <remarks/>
        public void DeleteReleaseAsync(System.Guid securityToken, int releaseId) {
            this.DeleteReleaseAsync(securityToken, releaseId, null);
        }
        
        /// <remarks/>
        public void DeleteReleaseAsync(System.Guid securityToken, int releaseId, object userState) {
            if ((this.DeleteReleaseOperationCompleted == null)) {
                this.DeleteReleaseOperationCompleted = new System.Threading.SendOrPostCallback(this.OnDeleteReleaseOperationCompleted);
            }
            this.InvokeAsync("DeleteRelease", new object[] {
                        securityToken,
                        releaseId}, this.DeleteReleaseOperationCompleted, userState);
        }
        
        private void OnDeleteReleaseOperationCompleted(object arg) {
            if ((this.DeleteReleaseCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.DeleteReleaseCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
        
        private bool IsLocalFileSystemWebService(string url) {
            if (((url == null) 
                        || (url == string.Empty))) {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024) 
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0))) {
                return true;
            }
            return false;
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.18058")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://axosoft.com/ontime/webservices/")]
    public partial class Release {
        
        private int releaseIdField;
        
        private int parentReleaseIdField;
        
        private int releaseTypeIdField;
        
        private string nameField;
        
        private System.DateTime startDateField;
        
        private System.DateTime dueDateField;
        
        private System.DateTime velocityStartDateField;
        
        private int releaseStatusTypeIdField;
        
        private string pathField;
        
        private bool isActiveField;
        
        private string releaseNotesField;
        
        private int[] projectsField;
        
        private bool hasChildrenField;
        
        /// <remarks/>
        public int ReleaseId {
            get {
                return this.releaseIdField;
            }
            set {
                this.releaseIdField = value;
            }
        }
        
        /// <remarks/>
        public int ParentReleaseId {
            get {
                return this.parentReleaseIdField;
            }
            set {
                this.parentReleaseIdField = value;
            }
        }
        
        /// <remarks/>
        public int ReleaseTypeId {
            get {
                return this.releaseTypeIdField;
            }
            set {
                this.releaseTypeIdField = value;
            }
        }
        
        /// <remarks/>
        public string Name {
            get {
                return this.nameField;
            }
            set {
                this.nameField = value;
            }
        }
        
        /// <remarks/>
        public System.DateTime StartDate {
            get {
                return this.startDateField;
            }
            set {
                this.startDateField = value;
            }
        }
        
        /// <remarks/>
        public System.DateTime DueDate {
            get {
                return this.dueDateField;
            }
            set {
                this.dueDateField = value;
            }
        }
        
        /// <remarks/>
        public System.DateTime VelocityStartDate {
            get {
                return this.velocityStartDateField;
            }
            set {
                this.velocityStartDateField = value;
            }
        }
        
        /// <remarks/>
        public int ReleaseStatusTypeId {
            get {
                return this.releaseStatusTypeIdField;
            }
            set {
                this.releaseStatusTypeIdField = value;
            }
        }
        
        /// <remarks/>
        public string Path {
            get {
                return this.pathField;
            }
            set {
                this.pathField = value;
            }
        }
        
        /// <remarks/>
        public bool IsActive {
            get {
                return this.isActiveField;
            }
            set {
                this.isActiveField = value;
            }
        }
        
        /// <remarks/>
        public string ReleaseNotes {
            get {
                return this.releaseNotesField;
            }
            set {
                this.releaseNotesField = value;
            }
        }
        
        /// <remarks/>
        public int[] Projects {
            get {
                return this.projectsField;
            }
            set {
                this.projectsField = value;
            }
        }
        
        /// <remarks/>
        public bool HasChildren {
            get {
                return this.hasChildrenField;
            }
            set {
                this.hasChildrenField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    public delegate void GetAllReleasesCompletedEventHandler(object sender, GetAllReleasesCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetAllReleasesCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetAllReleasesCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public System.Data.DataSet Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((System.Data.DataSet)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    public delegate void GetByReleaseIdCompletedEventHandler(object sender, GetByReleaseIdCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetByReleaseIdCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetByReleaseIdCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public Release Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((Release)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    public delegate void AddReleaseCompletedEventHandler(object sender, AddReleaseCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class AddReleaseCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal AddReleaseCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public int Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((int)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    public delegate void UpdateReleaseCompletedEventHandler(object sender, System.ComponentModel.AsyncCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    public delegate void DeleteReleaseCompletedEventHandler(object sender, System.ComponentModel.AsyncCompletedEventArgs e);
}

#pragma warning restore 1591