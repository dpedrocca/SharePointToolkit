using SharePointToolkit.CSOM;

namespace SharePointToolkit
{
    public partial class MainPage : Shell
    {
        public MainPage()
        {
            InitializeComponent();
        }

        //private async void OnCounterClicked(object sender, EventArgs e)
        //{
        //    CSOMCConfig cfg = new CSOMCConfig();
        //    cfg.TenantId = "3f8dbf35-fe8e-4b5d-9a76-19d32506b729";
        //    cfg.SPOTenantName = "2h8d1h.sharepoint.com";
        //    cfg.Site = "/sites/devsite1";
        //    cfg.ClientId = "f79ac065-abc1-4afc-8ff5-2bae75711396";
        //    cfg.ClientSecret = "lr08Q~fr03eGCILexY-JXyotONaWv0U4zUnyVc4l";
        //    cfg.CertificateThumbprint = "029F0FC625E78E2B753888D20D15EA5488695CC5";
        //    cfg.CertificatePassword = "Password;1";

        //    SharePointToolkit.CSOM.CSOMUtility x = new SharePointToolkit.CSOM.CSOMUtility();
        //    var cancelToken = new CancellationTokenSource();
        //    await x.Test(cfg, cancelToken.Token);
        //    cancelToken.Cancel();
        //}

        //private async void TestConnection(object sender, EventArgs e)
        //{
        //    CSOMCConfig cfg = new CSOMCConfig();
        //    cfg.TenantId = "3f8dbf35-fe8e-4b5d-9a76-19d32506b729";
        //    cfg.SPOTenantName = "2h8d1h.sharepoint.com";
        //    cfg.Site = "/sites/devsite1";
        //    cfg.ClientId = "f79ac065-abc1-4afc-8ff5-2bae75711396";
        //    cfg.ClientSecret = "lr08Q~fr03eGCILexY-JXyotONaWv0U4zUnyVc4l";
        //    cfg.CertificateThumbprint = "029F0FC625E78E2B753888D20D15EA5488695CC5";
        //    cfg.CertificatePassword = "Password;1";
        //    SharePointToolkit.CSOM.CSOMUtility x = new SharePointToolkit.CSOM.CSOMUtility();
        //    var cancelToken = new CancellationTokenSource();
        //    await x.TestConnection(cfg, cancelToken.Token);
        //    cancelToken.Cancel();
        //}
    }

}
