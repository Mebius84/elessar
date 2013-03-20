using System;
using System.Collections.Generic;

namespace elessar.Users
{
    public class UsersProvider
    {
        private Client _Client;

        public UsersProvider(Client client)
        {
            _Client = client;
        }

        public List<User> Get(string name_case = "", Field Fields = Field.none, params string[] Uids)
        {
            try
            {
                if (Uids.Length < 1000)
                {
                    string uids = "";
                    if (Uids.Length > 1)
                    {
                        uids = uids + Uids[0];
                        for (int i = 1; i < Uids.Length; i++)
                        {
                            uids = uids + "," + Uids[i];
                        }
                        
                    }
                    else
                    {
                        uids = Uids[0];
                    }

                    string parameters = "uids=" + uids;
                    if (!Fields.Equals(Field.none))
                    {
                        parameters = parameters + "&fields=" + Fields;
                    }
                    if (!name_case.Equals(""))
                    {
                        parameters = parameters + "&name_case=" + name_case;
                    }
                    //string parameters = String.Format("uids={0}&fields={1}&name_case={2}", uids, Fields, name_case);
                    string result_request = _Client.ApiRequest("users.get", parameters);
                    elessar.JsonClasses.Users.Get users_get = elessar.JsonClasses.Users.Get.FromJson(result_request);
                    return new List<User>(users_get.Response);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<User> Search(string Query, ref int rCount, int offset = 0, Field Fields = Field.none, int count = 20)
        {
            try
            {
                if (!Query.Equals(""))
                {
                    string parameters = "q="+Query;
                    if (!offset.Equals(0))
                    {
                        parameters = parameters + "&offset=" + offset.ToString();
                    }
                    if (!Fields.Equals(Field.none))
                    {
                        parameters = parameters + "&fields=" + Fields;
                    }
                    parameters = parameters + "&" + count.ToString();
                    string result_request = _Client.ApiRequest("users.search", parameters);
                    elessar.JsonClasses.Users.Search users_search =
                        elessar.JsonClasses.Users.Search.FromJson(result_request);
                    List<User> result = new List<User>();
                    rCount = Convert.ToInt32(users_search.Response[0]);
                    for (int i = 1; i < users_search.Response.Length; i++)
                    {
                        result.Add(elessar.JsonClasses.Users.Search.ReadUsers(users_search.Response[i].ToString()));
                    }
                    return result;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }

    public class User
    {
        public int Uid;
        public string First_name;
        public string Last_name;
        public string Screen_name;
        public int Sex;
        public string Bdate;
        public string City;
        public string Country;
        public int Timezone;
        public string Photo;
        public string Photo_medium;
        public string Photo_big;
        public int Has_mobile;
        public string Mobile_phone;
        public string Home_phone;
        public int University;
        public string University_name;
        public int Faculty;
        public string Faculty_name;
        public int Graduation;
        public int Online;
        public ModelCounters Counters;
        //Empty Constructor
        public User()
        {
        }

        public class ModelCounters
        {

            public int Albums;
            public int Videos;
            public int Audios;
            public int Notes;
            public int Photos;
            public int Friends;
            public int Online_friends;
            public int Mutual_friends;
            public int User_photos;
            public int User_videos;
            public int Followers;
            public int Subscriptions;

            //Empty Constructor
            public ModelCounters() { }

        }
    }

    [Flags]
    public enum Field
    {
        none = 0,
        uid = 1,
        first_name = 2,
        last_name = 4,
        nickname = 8,
        screen_name = 16,
        sex = 32,
        birthdate = 64,
        city = 128,
        country = 256,
        timezone = 512,
        photo = 1024,
        photo_medium = 2048,
        photo_big = 4096,
        has_mobile = 8192,
        rate = 16384,
        contacts = 32768,
        education = 65536,
        online = 131072,
        counters = 262144,
    }
}
