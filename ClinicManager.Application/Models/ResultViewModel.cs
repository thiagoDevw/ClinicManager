namespace ClinicManager.Application.Models
{
    public class ResultViewModel
    {
        public ResultViewModel(bool isSucess = true, string message = "")
        {
            IsSucess = isSucess;
            Message = message;
        }

        public bool IsSucess { get; set; }
        public string Message { get; set; }

        public static ResultViewModel Error(string message)
            => new(false, message);

        public static ResultViewModel Success()
            => new();
    }

    public class ResultViewModel<T> : ResultViewModel
    {
        public ResultViewModel(T? data, bool isSucess = true, string message = "")
            : base(isSucess, message)
        {
            Data = data;
        }
        public T? Data { get; private set; }

        public static ResultViewModel<T> Success(T data) 
            => new(data);

        public static ResultViewModel<T> Error(string message) 
            => new(default, false, message);
    }
}
