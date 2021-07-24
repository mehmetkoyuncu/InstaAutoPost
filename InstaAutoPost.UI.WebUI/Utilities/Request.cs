using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InstaAutoPost.UI.WebUI.Utilities
{
    public class Request
    {
        public string RequestGet(string serviceurl)
        {
            var client = new RestClient("https://localhost:44338/source/getallsources");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            return response.Content;
        }
    }
}
