namespace FourTwenty.IoT.Connect.Models
{
    public class Loading<T>
    {
        public bool IsLoading { get; set; }
        public T Data { get; set; }

        public Loading(T data, bool isLoading = false)
        {
            Data = data;
            IsLoading = isLoading;
        }

        public Loading()
        {
            IsLoading = true;
        }
    }
}
