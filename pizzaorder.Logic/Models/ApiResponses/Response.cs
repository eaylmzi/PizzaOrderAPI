﻿namespace PizzaOrderAPI.Logic.Models.ApiResponses
{
    public class Response<T>
    {
        public string Message { get; set; } = null!;
        public T? Data { get; set; }
        public bool Progress { get; set; }

        public static Response<T> CreateSuccessMessage(T data, string message)
        {
            return new Response<T>
            {
                Data = data,
                Message = message,
                Progress = true
            };
        }

        public static Response<T> CreateFailureMessage(string message)
        {
            return new Response<T>
            {
                Data = default(T),
                Message = message,
                Progress = false
            };
        }

    }
}
