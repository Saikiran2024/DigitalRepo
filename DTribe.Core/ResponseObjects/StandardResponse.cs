using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTribe.Core.ResponseObjects
{
    public enum ResponseStatus
    {
        Success,
        Error,
        Fatal
    }

    public class ErrorDetail
    {
        public string? Code { get; set; }
        public string? Description { get; set; }

        public static ErrorDetail Create(FrequentErrors errorType, params string[] args)
        {
            return errorType switch
            {
                FrequentErrors.InternalServerError => new ErrorDetail { Code = "INTERNAL_SERVER_ERROR", Description = "Internal Server Error." },

            };
        }
    }
    public enum FrequentErrors
    {
        InternalServerError,

        Forbidden,
        UserNotFound
    }
    public class StandardResponse<T>
    {
        public ResponseStatus Status { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
        public List<ErrorDetail> Errors { get; set; } = new List<ErrorDetail>();

        public StandardResponse()
        {
            // You might want to set default values here if needed
        }

        public StandardResponse(ResponseStatus status, string message, T data, List<ErrorDetail> errors)
        {
            Status = status;
            Message = message;
            Data = data;
            Errors = errors;
        }

        public void AddError(string code, string description)
        {
            Errors.Add(new ErrorDetail { Code = code, Description = description });
        }

        public void AddError(string description)
        {
            Errors.Add(new ErrorDetail { Description = description });
        }

        public void AddError(FrequentErrors frequentErrors, params string[] args)
        {
            Errors.Add(ErrorDetail.Create(frequentErrors, args));
        }
    }
}
