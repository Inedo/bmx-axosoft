using System.Web.UI.WebControls;
using Inedo.BuildMaster.Extensibility.Providers;
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
            var provider = (OnTimeTrackerProvider)extension;
            this.txtBaseUrl.Text = provider.BaseUrl ?? "";
            this.txtOnTimeWebUrl.Text = provider.OnTimeWebUrl ?? "";
            this.txtSecurityToken.Text = provider.SecurityToken ?? "";
        }

        public override ProviderBase CreateFromForm()
        {
            return new OnTimeTrackerProvider
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

            this.txtOnTimeWebUrl = new ValidatingTextBox
            {
                DefaultText = "not specified"
            };

            this.txtSecurityToken = new ValidatingTextBox
            {
                Required = true,
                ValidationExpression = @"^\{?[0-9A-Fa-f]{8}-[0-9A-Fa-f]{4}-[0-9A-Fa-f]{4}-[0-9A-Fa-f]{4}-[0-9A-Fa-f]{12}\}$"
            };

            this.Controls.Add(
                new SlimFormField("Web service URL:", this.txtBaseUrl),
                new SlimFormField("OnTime web URL:", this.txtOnTimeWebUrl),
                new SlimFormField("Security token:", this.txtSecurityToken)
                {
                    HelpText = "Provide the security token GUID specified in the OnTime web service web.config file."
                }
            );
        }
    }
}
