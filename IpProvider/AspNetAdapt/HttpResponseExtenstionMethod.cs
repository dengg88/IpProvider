using System.Web;

namespace IpProvider.AspNetAdapt
{
    internal static class HttpResponseExtenstionMethod
    {
        public static void Result(this HttpResponse response, string contentType = "application/octet-stream", byte[] data = null, int statusCode = 200)
        {
            if (null == data)
            {
                response.StatusCode = 404;
                response.ContentType = "text/plain";
                response.Write("404 Not Found");
            }
            else
            {
                response.StatusCode = statusCode;
                response.ContentType = contentType;
                response.BinaryWrite(data);
            }
        }

        public static void StrResult(this HttpResponse response, string data = null, string contentType = "text/plain", int statusCode = 200)
        {
            if (null == data)
            {
                response.StatusCode = 404;
                response.ContentType = "text/plain";
                response.Write("404 Not Found");
            }
            else
            {
                response.StatusCode = statusCode;
                response.ContentType = contentType;
                response.Write(data);
            }
        }
    }
}