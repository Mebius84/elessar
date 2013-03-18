using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Web;
using System.Windows.Forms;
using System.Drawing;

namespace elessar
{
    public class Client
    {
        public OAuthConnection Connection { get; set; }

        private Form OAuthForm;
        private WebBrowser browser;

        public Client()
        {
        }

        public void SetConnection(OAuthConnection connection)
        {
            Connection = connection;
            CreateOAuthPopup();
        }

        private void CreateOAuthPopup()
        {
            OAuthForm = new Form();
            OAuthForm.Width = 640;
            OAuthForm.Height = 480;
            OAuthForm.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            OAuthForm.StartPosition = FormStartPosition.CenterScreen;
            browser = new WebBrowser();
            browser.Parent = OAuthForm;
            browser.Width = 635;
            browser.Height = 458;
            browser.ScrollBarsEnabled = true;
            browser.ScriptErrorsSuppressed = true;
            browser.Navigated += new WebBrowserNavigatedEventHandler(browser_Navigated);
            OAuthForm.Shown += new EventHandler(OAuthForm_Shown);
            OAuthForm.ShowDialog();
        }

        private void OAuthForm_Shown(object sender, EventArgs e)
        {
            if (browser != null)
            {
                //https://oauth.vk.com/authorize?client_id=APP_ID&scope=SETTINGS&redirect_uri=REDIRECT_URI&display=DISPLAY&response_type=token 
                browser.Navigate(String.Format("{0}client_id={1}&scope={2}&redirect_uri={3}&display=popup&response_type=token", Connection.OAuthUrl,Connection.AppID,Connection.Scopes.ToString().Replace(" ",""),Connection.RedirectUri));
            }
        }

        private void browser_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            if (OAuthForm != null)
            {
                OAuthForm.Text = browser.DocumentTitle;
                if (e.Url.AbsolutePath.Equals("/blank.html"))
                {
                    NameValueCollection response = HttpUtility.ParseQueryString(e.Url.Fragment.Replace("#",""));
                    if (response.Count == 3)
                    {
                        for (int i = 0; i < response.Count; i++)
                        {
                            Console.WriteLine(response.Keys[i] + " = " + response.Get(i));
                        }
                        OAuthForm.Close();
                    }
                }
            }
        }
    }
}
