

namespace Core.Application.Interfaces.Dtos
{
    public interface IResultDataDto<T> : IResultDto
    {
        public T Data { get; set; }
        public bool IsSuccess { get; set; }
        public string Error { get; set; }
        public string Message { get; set; }
        public Dictionary<string, string> Items { get; set; }

        /// <summary>
        /// Data prop value ekler
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public IResultDataDto<T> SetData(T data);
        /// <summary>
        /// IsSucess prop value ekler,boş gönderilirse true olur
        /// </summary>
        /// <param name="statusValue"></param>
        /// <returns></returns>
        public IResultDataDto<T> SetStatus(bool statusValue = true);
        /// <summary>
        /// Err prop value ekler
        /// </summary>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public IResultDataDto<T> SetErrorMessage(string errorMessage);
        /// <summary>
        /// Message prop value ekler
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public IResultDataDto<T> SetMessage(string message);

        /// <summary>
        /// Using for return id
        /// </summary>
        /// <returns></returns>
        IResultDataDto<T> ReturnGuid(Guid Id);
      
    }
}
