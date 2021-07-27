
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
            var client = new RestClient(serviceurl);
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            return response.Content;
        }
        public string RequestPostWithId(string serviceurl,int id)
        {
            JObject jobject = new JObject();
            jobject.Add("Id", id);
            var client = new RestClient(serviceurl);
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddParameter("application/json", jobject, ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            //request.AddParameter(new Parameter("Id",id,ParameterType.GetOrPost,true);
            IRestResponse response = client.Execute(request);
            return response.Content;
        }
    }
}
