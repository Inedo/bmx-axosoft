using System;
using System.Web.UI.WebControls;
using Inedo.BuildMaster.Extensibility.Providers;
using Inedo.BuildMaster.Web.Controls;
using Inedo.BuildMaster.Web.Controls.Extensions;
using Inedo.Web.Controls;

namespace Inedo.BuildMasterExtensions.Axosoft
{
    internal sealed class OnTimeTrackerProviderEditor : ProviderEditorBase
    {
        private ValidatingTextBox txtBaseUrl;
        private TextBox txtOnTimeWebUrl;
        private ValidatingTextBox txtSecurityToken;

        public override void BindToForm(ProviderBase extension)
        {
            this.EnsureChildControls();

            var provider = (OnTimeTrackerProvider)extension;
            this.txtBaseUrl.Text = provider.BaseUrl ?? "";
            this.txtOnTimeWebUrl.Text = provider.OnTimeWebUrl ?? "";
            this.txtSecurityToken.Text = provider.SecurityToken ?? "";
        }

        public override ProviderBase CreateFromForm()
        {
            this.EnsureChildControls();

            return new OnTimeTrackerProvider()
            {
                BaseUrl = this.txtBaseUrl.Text,
                OnTimeWebUrl = this.txtOnTimeWebUrl.Text,
                SecurityToken = this.txtSecurityToken.Text
            };
        }

        protected override void CreateChildControls()
        {
            this.txtBaseUrl = new ValidatingTextBox
            {
                Required = true
            };

            this.txtOnTimeWebUrl = new ValidatingTextBox();

            this.txtSecurityToken = new ValidatingTextBox
            {
                Required = true
            };

            this.Controls.Add(
                new FormFieldGroup("OnTime Server",
                    "The URL's of the Axosoft OnTime web service and OnTime Web, for example: http://ontime:8080",
                    false,
                    new StandardFormField(
                        "Web Service URL (Path to installed OnTime SDK):",
                        txtBaseUrl),
                    new StandardFormField(
                        "OnTime Web URL (optional):",
                        txtOnTimeWebUrl)
                ),
                new FormFieldGroup("Configuration",
                    "The security token GUID specified in the OnTime web service web.config file.",
                    false,
                    new StandardFormField(
                        "Security Token:",
                        txtSecurityToken)
                )
            );
        }

        protected override void OnValidateBeforeSave(ValidationEventArgs<ProviderBase> e)
        {
            base.OnValidateBeforeSave(e);
            if (e.ValidLevel == ValidationLevel.Valid)
                return;

            try
            {
                new Guid(this.txtSecurityToken.Text);
            }
            catch
            {
                e.Message = "Security token must be a GUID.";
                e.ValidLevel = ValidationLevel.Error;
            }
        }
    }
}
