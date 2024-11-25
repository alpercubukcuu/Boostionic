

namespace Core.Application.Interfaces.Dtos
{
    public interface IResultDto
    {
        public bool IsSuccess { get; set; }
        public string Error { get; set; }
        public string Message { get; set; }
        public Guid Id { get; set; }

        /// <summary>
        /// IsSucess prop value ekler,boş gönderilirse true olur
        /// </summary>
        /// <param name="statusValue"></param>
        /// <returns></returns>
        public IResultDto SetStatus(bool statusValue = true);
        /// <summary>
        /// Error prop value ekler
        /// </summary>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public IResultDto SetErrorMessage(string errorMessage);
        /// <summary>
        /// Message prop value ekler
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public IResultDto SetMessage(string message);
    }
}
