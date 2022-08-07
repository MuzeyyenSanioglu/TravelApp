namespace TravelApp.Domain.Model
{
    public class Result
    {
        public string ErrorMessage { get; set; }
        public bool IsSuccess { get; set; }
        public string ObjectId { get; set; }
        public bool AlreadyExist { get; set; }
        public void SetSuccess()
        {
            this.IsSuccess = true;
        }
        public void SetFailure(string errorMessage = "")
        {
            this.IsSuccess = false;
            this.ErrorMessage = errorMessage;
        }

        public void SetFailure(Exception ex, string errorMessage = "")
        {
            this.ErrorMessage = errorMessage;
            if (ex != null)
            {
                this.ErrorMessage += ex.Message + ex.StackTrace;
                if (ex.InnerException != null)
                    this.ErrorMessage += ex.InnerException.Message + ex.InnerException.StackTrace;
            }


            this.IsSuccess = false;
        }

    }
    public class Result<T> : Result
    {
        public T Data { get; set; }

        public void SetData(T data)
        {
            this.Data = data;
            this.IsSuccess = true;
        }
    }
}
