using Newtonsoft.Json;

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

}
