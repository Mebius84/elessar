using System;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web;
using System.Windows.Forms;
using elessar.JsonClasses.Account;

namespace elessar
{
    public class Client
    {
        public OAuthConnection Connection { get; set; }

        private Form OAuthForm;
        private WebBrowser browser;

        private HttpWrapper http = new HttpWrapper();

        private NameValueCollection data;

        public Client()
        {
        }

        public Client(OAuthConnection connection)
        {
            SetConnection(connection);
        }

        public void SetConnection(OAuthConnection connection)
        {
            Connection = connection;
            if (!LoadAuthData())
            {
                CreateOAuthPopup();
            }
            else
            {
                string expires_in = data.Get("expires_in");
                if (expires_in == null || Convert.ToInt32(expires_in) != 0)
                {
                    CreateOAuthPopup();
                }
                else
                {
                    if (!setOnline())
                    {
                        CreateOAuthPopup();
                    }
                    else
                    {
                        Debug.WriteLine("UserID: " + UserID());
                        Debug.WriteLine("AccessToken" + AccessToken());
                    }
                }
            }
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
                string requestUrl =
                    String.Format("{0}client_id={1}&scope={2}&redirect_uri={3}&display=popup&response_type=token",
                                  Connection.OAuthUrl, Connection.AppID, Connection.Scopes.ToString().Replace(" ", ""),
                                  Connection.RedirectUri);
                browser.Navigate(requestUrl);
            }
        }

        private void browser_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            if (OAuthForm != null)
            {
                OAuthForm.Text = browser.DocumentTitle;
                if (e.Url.AbsolutePath.Equals("/blank.html"))
                {
                    data = HttpUtility.ParseQueryString(e.Url.Fragment.Replace("#", ""));
                    if (data.Count == 3)
                    {
                        for (int i = 0; i < data.Count; i++)
                        {
                            Debug.WriteLine(data.Keys[i] + " = " + data.Get(i));
                        }
                        SaveAuthData();
                        OAuthForm.Close();
                    }
                    else
                    {
                        for (int i = 0; i < data.Count; i++)
                        {
                            Debug.WriteLine(data.Keys[i] + " = " + data.Get(i));
                        }
                        OAuthForm.Close();
                    }
                    
                }
            }
        }

        private void SaveAuthData()
        {
            try
            {
                Stream FileStream = File.Create("data.bin");
                BinaryFormatter serializer = new BinaryFormatter();
                serializer.Serialize(FileStream, data);
                FileStream.Close();
            }
            catch (Exception e)
            {
                throw e;
            }
            
        }

        private bool LoadAuthData()
        {
            try
            {
                if (File.Exists("data.bin"))
                {
                    Stream FileStream = File.OpenRead("data.bin");
                    BinaryFormatter deserializer = new BinaryFormatter();
                    data = (NameValueCollection)deserializer.Deserialize(FileStream);
                    FileStream.Close();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public string UserID()
        {
            return data.Get("user_id");
        }

        public string AccessToken()
        {
            return data.Get("access_token");
        }

        public bool setOnline()
        {
            string request = String.Format(Connection.ApiUrl + @"account.setOnline?uid={0}&access_token={1}", UserID(),AccessToken());
            string response = http.HttpGet(request); 
            setOnline setOnline = setOnline.FromJson(response);
            return Convert.ToBoolean(setOnline.Response);
        }
    }
}
