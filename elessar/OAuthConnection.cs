using System;

namespace elessar
{
    public class OAuthConnection
    {
        public string AppID { get; set; }
        public string OAuthUrl { get; set;  }
        public string RedirectUri { get; set; }

        public Scope Scopes { get; set; }

        private bool _saveAuthData;

        public bool SaveAuthData
        {
            get { return _saveAuthData; }
            set
            {
                _saveAuthData = value;
                if (value.Equals(true))
                    Scopes = Scopes | Scope.offline;
                else
                    Scopes = Scopes ^ Scope.offline;
            }
        }

        public OAuthConnection(string appID, params Scope[] scopes)
        {
            AppID = appID;
            OAuthUrl = "https://oauth.vk.com/authorize?";
            RedirectUri = "https://oauth.vk.com/blank.html";
            Scopes = Scope.none;
            foreach (var scope in scopes)
            {
                Scopes = Scopes | scope;
            }
            SaveAuthData = false;
        }


        public void AddScope(Scope scope)
        {
            Scopes = Scopes | scope;
        }

        public void RemoveScope(Scope scope)
        {
            Scopes = Scopes ^ scope;
        }

    }

    [Flags]
    public enum Scope
    {
        none = 0,
        notify = 1,
        friends = 2,
        photos = 4,
        audio = 8,
        video = 16,
        docs = 32,
        notes = 64,
        pages = 128,
        status = 256,
        offers = 512,
        questions = 1024,
        wall = 2048,
        groups = 4096,
        messages = 8192,
        notifications = 16384,
        stats = 32768,
        ads = 65536,
        offline = 131072,
        nohttps = 262144,
    }
}
