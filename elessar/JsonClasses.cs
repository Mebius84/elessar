using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using elessar.Users;

namespace elessar.JsonClasses
{
    namespace Account
    {
        public class setOnline
        {

            public int Response;

            public setOnline()
            {
            }

            public string Serialize()
            {
                return JsonConvert.SerializeObject(this);
            }

            public static setOnline FromJson(string json)
            {
                return JsonConvert.DeserializeObject<setOnline>(json);
            }
        }
    }

    namespace Users
    {
        public class isAppUser
        {

            public string Response;

            public isAppUser()
            {
            }

            public string Serialize()
            {
                return JsonConvert.SerializeObject(this);
            }

            public static isAppUser FromJson(string json)
            {
                return JsonConvert.DeserializeObject<isAppUser>(json);
            }
        }

        public class Get
        {

            public User[] Response;
            public Get()
            {
            }

            public string Serialize()
            {
                return JsonConvert.SerializeObject(this);
            }

            public static Get FromJson(string json)
            {
                return JsonConvert.DeserializeObject<Get>(json);
            }
        }

        public class Search
        {

            public object[] Response;

            //Empty Constructor
            public Search() { }

            public string Serialize()
            {
                return JsonConvert.SerializeObject(this);
            }
            public static Search FromJson(string json)
            {
                return JsonConvert.DeserializeObject<Search>(json);
            }

            public static User ReadUsers(string json)
            {
                return JsonConvert.DeserializeObject<User>(json);
            }
        }
    }
}