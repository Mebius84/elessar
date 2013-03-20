using System;
using System.Collections.Generic;
using System.Text;
using elessar;
using elessar.Users;

namespace Sample
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            OAuthConnection connection = new OAuthConnection("3465377",Scope.messages,Scope.groups,Scope.friends, Scope.audio,Scope.status,Scope.stats);
            Client client = new Client();
            client.SetConnection(connection);

            UsersProvider Users = new UsersProvider(client);
            List<User> user = Users.Get("",Field.uid | Field.first_name | Field.last_name | Field.nickname | Field.screen_name | Field.sex | Field.birthdate | Field.city | Field.country | Field.timezone | Field.photo | Field.photo_medium | Field.photo_big | Field.has_mobile | Field.rate | Field.contacts | Field.education | Field.online | Field.counters, "vyrin", "naoko_kato");
            foreach (User user1 in user)
            {
                Console.WriteLine(String.Format("{0} : {1} : {2}",user1.Uid,user1.First_name, user1.Last_name));
            }
            
            Console.WriteLine("");
            int Count = 0;
            List<User> SearchUser = Users.Search("mebius",ref Count);

            foreach (User user1 in SearchUser)
            {
                Console.WriteLine(String.Format("{0} : {1} : {2}", user1.Uid, user1.First_name, user1.Last_name));
            }
            Console.ReadLine();
        }
    }
}
