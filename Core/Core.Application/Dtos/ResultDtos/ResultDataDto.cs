using Core.Application.Interfaces.Dtos;

namespace Core.Application.Dtos.ResultDtos
{
    public class ResultDataDto<T> : ResultDto, IResultDataDto<T>
    {
        public bool IsSuccess { get; set; } = true;
        public string Error { get; set; } = "Error";
        public string Message { get; set; } = "Success";
        public Guid Id { get; set; }
        public T Data { get; set; }
        public Dictionary<string, string> Items { get; set; }

        public IResultDataDto<T> SetData(T data)
        {
            this.Data = data;
            return this;
        }

        public IResultDataDto<T> SetErrorMessage(string errorMessage)
        {
            this.Error = errorMessage;
            return this;
        }

        public IResultDataDto<T> SetMessage(string message)
        {
            this.Message = message;
            return this;
        }

        public IResultDataDto<T> SetStatus(bool statusValue = true)
        {
            this.IsSuccess = statusValue;
            return this;
        }

        public IResultDataDto<T> ReturnGuid(Guid Id)
        {
            this.Id = Id;
            return this;
        }
    }
}
