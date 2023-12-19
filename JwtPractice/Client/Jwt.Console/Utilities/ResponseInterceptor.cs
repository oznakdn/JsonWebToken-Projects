using System.Net;

namespace Jwt.Console.Utilities;

public class ResponseInterceptor
{
    public static string CheckResponseStatus(HttpResponseMessage httpResponse)
    {
        switch (httpResponse.StatusCode)
        {

            // Burada Refresh api metodu çağrılarak yeniden login yaptırılacak
            case HttpStatusCode.Unauthorized: return "";
            // Burada yetkisiz kullanıcı uyarısı verecek
            case HttpStatusCode.Forbidden: return "";
        }
        return "";
    }
}

