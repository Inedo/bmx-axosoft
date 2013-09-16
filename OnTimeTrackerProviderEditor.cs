using System;
using System.Web.UI.WebControls;
using Inedo.BuildMaster.Extensibility.Providers;
using Inedo.BuildMaster.Web.Controls;
using Inedo.BuildMaster.Web.Controls.Extensions;
using Inedo.Web.Controls;

namespace Inedo.BuildMasterExtensions.Axosoft
{
    /// <summary>
    /// Custom editor for the OnTime Tracker Provider.
    /// </summary>
    public sealed class OnTimeTrackerProviderEditor : ProviderEditorBase
    {
        private ValidatingTextBox txtBaseUrl;
        private TextBox txtOnTimeWebUrl;
        private ValidatingTextBox txtSecurityToken;

        /// <summary>
        /// Initializes a new instance of the <see cref="OnTimeTrackerProviderEditor"/> class.
        /// </summary>
        public OnTimeTrackerProviderEditor()
        {
        }

        public override void BindToForm(ProviderBase extension)
        {
            EnsureChildControls();

            var provider = (OnTimeTrackerProvider)extension;
            this.txtBaseUrl.Text = provider.BaseUrl ?? "";
            this.txtOnTimeWebUrl.Text = provider.OnTimeWebUrl ?? "";
            this.txtSecurityToken.Text = provider.SecurityToken ?? "";
        }

        public override ProviderBase CreateFromForm()
        {
            EnsureChildControls();

            return new OnTimeTrackerProvider()
            {
                BaseUrl = this.txtBaseUrl.Text,
                OnTimeWebUrl = this.txtOnTimeWebUrl.Text,
                SecurityToken = this.txtSecurityToken.Text
            };
        }

        protected override void CreateChildControls()
        {
            txtBaseUrl = new ValidatingTextBox()
            {
                Width = 300,
                Required = true
            };

            txtOnTimeWebUrl = new TextBox()
            {
                Width = 300
            };

            txtSecurityToken = new ValidatingTextBox()
            {
                Width = 300,
                Required = true
            };

            CUtil.Add(this,
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

            base.CreateChildControls();
        }

        /// <summary>
        /// Raises the <see cref="E:ValidateBeforeSave"/> event.
        /// </summary>
        /// <param name="e">The <see cref="Inedo.BuildMaster.Web.Controls.Extensions.ValidationEventArgs&lt;Inedo.BuildMaster.Extensibility.Providers.ProviderBase&gt;"/> instance containing the event data.</param>
        protected override void OnValidateBeforeSave(ValidationEventArgs<ProviderBase> e)
        {
            base.OnValidateBeforeSave(e);
            if (e.ValidLevel == ValidationLevels.Valid)
                return;

            try
            {
                new Guid(this.txtSecurityToken.Text);
            }
            catch
            {
                e.Message = "Security token must be a GUID.";
                e.ValidLevel = ValidationLevels.Error;
            }
        }
    }
}
