using Microsoft.AspNetCore.Mvc;

public interface IResponse<T> : IActionResult
{
    string ResponseCode { get; set; }
    string ErrorMessage { get; set; }
    T Data { get; set; }
}