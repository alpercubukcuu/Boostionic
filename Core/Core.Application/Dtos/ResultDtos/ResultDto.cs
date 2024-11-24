using Core.Application.Interfaces.Dtos;

namespace Core.Application.Dtos.ResultDtos
{
    public class ResultDto : IResultDto
    {
        public bool IsSuccess { get; set; }
        public string Err { get; set; }
        public string Message { get; set; }
        public Guid Id { get; set; }

        public IResultDto SetErrorMessage(string errorMessage)
        {
            this.Err = errorMessage;
            return this;
        }

        public IResultDto SetMessage(string message)
        {
            this.Message = message;
            return this;
        }

        public IResultDto SetStatus(bool statusValue = true)
        {
            this.IsSuccess = statusValue;
            return this;
        }
    }
}
