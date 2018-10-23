using IpProvider.AspNetAdapt;
using System.Web;

[assembly: PreApplicationStartMethod(typeof(Initializer), nameof(Initializer.Initialize))]

/// <summary>
/// 作者：晏耀
/// </summary>


namespace IpProvider.AspNetAdapt
{
    public class Initializer
    {
        public static void Initialize()
        {
        }
    }
}