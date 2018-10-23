using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web;

namespace IpProvider.AspNetAdapt
{
    internal class ArbitrationModule : IHttpModule
    {
        public void Dispose()
        {
        }

        public void Init(HttpApplication context) => context.BeginRequest += BeginRequest;

        private void BeginRequest(object sender, EventArgs e)
        {
            var context = HttpContext.Current;
            var request = context.Request;
            var response = context.Response;

            if (request.Path != "/" ||
                "true" == ConfigurationManager.AppSettings["AuthEnable"]
                && request.QueryString["AuthCode"] != ConfigurationManager.AppSettings["AuthCode"])
            {
                response.Close();
                return;
            }

            response.AddOnSendingHeaders(ctx =>
            {
                ctx.Response.Headers.Remove("Server");
                ctx.Response.Headers.Remove("X-AspNet-Version");
            });

            var dictionary = new Dictionary<string, string>();

            dictionary["REMOTE_ADDR"] = request.UserHostAddress;
            dictionary["SERVER_TIME_UTC"] = DateTime.Now.ToUniversalTime().ToString("yyyy-MM-dd HH:mm:ss");

            foreach (string headerKey in request.Headers.AllKeys)
                dictionary[headerKey] = request.Headers[headerKey];

            response.StrResult(JsonConvert.SerializeObject(dictionary, Formatting.Indented), "application/json");
            context.ApplicationInstance.CompleteRequest();
        }
    }
}