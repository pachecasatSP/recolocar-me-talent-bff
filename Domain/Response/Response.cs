using Microsoft.AspNetCore.Mvc;

public class Response<T> :  IResponse<T>
{
    public string ResponseCode { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public string ErrorMessage { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public T Data { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public static Response<T> CreateSuccessResult<TInput>(T data, string responseCode){
        return new Response<T>{
             Data = data,
             ResponseCode = responseCode
        };
    }

    public static Response<T> CreateErrorResult<TInput>(string message, string responseCode){
        return new Response<T>{
             ErrorMessage = message,
             ResponseCode = responseCode
        };
    }
    public Task ExecuteResultAsync(ActionContext context)
    {
        throw new NotImplementedException();
    }
}