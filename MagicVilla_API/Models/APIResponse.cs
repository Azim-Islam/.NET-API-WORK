using System.Net;

namespace MagicVilla_API.Models;

public class APIResponse
{
    public HttpStatusCode StatusCode { get; set; }
    public bool IsSuccessStatusCode { get; set; }
    public List<string> ErrorMessages { get; set; }
    public object Result { get; set; }
}