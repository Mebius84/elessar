using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace elessar.Http
{
    public class HttpWrapper
    {
        /// <summary>
        /// UserAgent to be used on the requests
        /// </summary>
        public string UserAgent =
            @"Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.22 (KHTML, like Gecko) Chrome/25.0.1364.172 Safari/537.22";

        /// <summary>
        /// Cookie Container that will handle all the cookies.
        /// </summary>
        public CookieContainer Cookies { get; set; }

        /// <summary>
        /// Performs a basic HTTP GET request.
        /// </summary>
        /// <param name="url">The URL of the request.</param>
        /// <returns>HTML Content of the response.</returns>
        public string HttpGet(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.CookieContainer = Cookies;
            request.UserAgent = UserAgent;
            request.KeepAlive = false;
            request.Method = "GET";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader sr = new StreamReader(response.GetResponseStream());
            return sr.ReadToEnd();
        }

        /// <summary>
        /// Performs a basic HTTP POST request
        /// </summary>
        /// <param name="url">The URL of the request.</param>
        /// <param name="post">POST Data</param>
        /// <param name="referer">Referrer of the request</param>
        /// <returns>HTML Content of the response as string.</returns>
        public string HttpPost(string url, string post, string referer = "")
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.CookieContainer = Cookies;
            request.UserAgent = UserAgent;
            request.KeepAlive = false;
            request.Method = "POST";
            request.Referer = referer;

            byte[] postBytes = Encoding.UTF8.GetBytes(post);
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = postBytes.Length;

            Stream requestStream = request.GetRequestStream();
            requestStream.Write(postBytes, 0, postBytes.Length);
            requestStream.Close();

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader sr = new StreamReader(response.GetResponseStream());

            return sr.ReadToEnd();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Http"/> class.
        /// </summary>
        public HttpWrapper()
        {
            Cookies = new CookieContainer();
        }

        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="Http"/> is reclaimed by garbage collection.
        /// </summary>
        ~HttpWrapper()
        {
            // Nothing here
        }
    }
}
