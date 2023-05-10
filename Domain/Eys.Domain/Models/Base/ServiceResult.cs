using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using System.ServiceModel;
using System.Xml.Linq;

namespace Eys.Domain.Models.Base
{
    [Serializable]
    public class ServiceResult<T>
    {
        public string Message { get; set; }
		public string ErrorCode { get; set; }
		public int StatusCode { get; set; }
        public bool IsSuccess { get; set; } = false;
        public bool HasError { get; set; } = false;
        public object Data { get; set; }
        public T Result { get; set; }
        public List<ValidationResponse> ValidErrors { get; set; } = new List<ValidationResponse>();
        public bool IsValid { get; set; } = true;
        public int RecordsTotal { get; set; }
        public int RecordsFiltered { get; set; }
        public void Success(object data)
        {
            IsSuccess = true;
            StatusCode = 200;
            this.Message = "İşlem Başarılı";
            Data = data;
        }
        public void Error(string message, int statusCode = 400)
        {
            IsSuccess = false;
            StatusCode = statusCode;
            if (!string.IsNullOrWhiteSpace(message))
                Message = message;

            else
                this.Message = "İşlem Başarısız";
        }
        public bool Validation(ValidationResult valid)
        {
            this.IsValid = valid.IsValid;
            if (!IsValid)
            {
                this.IsSuccess = false;
                foreach (var error in valid.Errors)
                {
                    ValidErrors.Add(new ValidationResponse
                    {
                        Name = error.PropertyName,
                        Message = error.ErrorMessage
                    });
                }
            }
            return IsValid;
        }
        public ObjectResult HttpGetResponse()
        {
            if (!this.IsSuccess)
            {
                if (!this.IsValid)
                {
                    return GetResponse(400, this);
                }
            }

            //if (this.Data == null) // Get asla null olamaz
            //{
            //    return GetResponse(404, "");
            //}

            return GetResponse(200, this.Result);
        }

        public ObjectResult HttpPostResponse()
        {
            if (!this.IsSuccess)
            {
                if (!this.IsValid)
                {
                    return GetResponse(400, this);
                }
                else if (this.Result == null)
                {
                    return GetResponse(404, "");
                }
            }

            return GetResponse(200, this.Result);
        }
        private ObjectResult GetResponse(int statusCode, object response)
        {
            var result = new ObjectResult(response);
            result.StatusCode = statusCode;
            return result;
        }

		public void SetException(Exception exception)
		{
			var faultException = exception?.InnerException as FaultException;
			if (faultException == null)
			{
				SetError(exception.Message);
			}
			else
			{
				try
				{
					var errorElement = XElement.Parse(faultException.CreateMessageFault().GetReaderAtDetailContents().ReadOuterXml());
					var errorDictionary = errorElement.Elements().ToDictionary(key => key.Name.LocalName, val => val.Value);

					string code = String.Empty;
					string message = String.Empty;

					if (errorDictionary.TryGetValue("Code", out code)
						&& errorDictionary.TryGetValue("Message", out message))
					{
						this.SetError(message, code);
					}
					else
					{
						SetError(exception.Message);
					}
				}
				catch (Exception e)
				{
					SetError(faultException.Reason.GetMatchingTranslation().Text);

				}

			}
		}
		public void SetError(string message, string errorCode = "")
		{
			IsSuccess = false;
			HasError = true;
			Message = message;
			ErrorCode = errorCode;
		}
	}
}
public class ValidationResponse
{
    public string Name { get; set; }
    public string Message { get; set; }
}

