using System.Net;

namespace Domein.Responses;

public class Response<T>
{
    public int StatusCode { get; set; }
    public T? Date { get; set; }
    public string Massage { get; set; }

    public Response(T date)
    {
        Date = date;
        StatusCode = 200;
        
    }

    public Response(HttpStatusCode statusCode, string massage)
    {
        StatusCode = (int)statusCode;
        Massage = massage;
        Date = default;
    }
}