namespace PizzaOrderAPI.Models.ApiResponses
{
    public class Response<T>
    {
        public string Message { get; set; } = null!;
        public T? Data { get; set; }
        public bool Progress { get; set; }

    }
}
