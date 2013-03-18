using System;
using System.Collections.Generic;
using System.Text;
using elessar;

namespace AuthSample
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            OAuthConnection connection = new OAuthConnection("3465377",Scope.messages,Scope.groups,Scope.friends);
            Client client = new Client();
            client.SetConnection(connection);
            Console.ReadLine();
        }
    }
}
