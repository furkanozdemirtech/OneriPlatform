using OneriPlatform.Entities.Messages;
using System.Collections.Generic;

namespace OneriPlatform.BusinessLayer.Results
{
    public class BusinessLayerResultcs<T> where T : class
    {
        public List<ErrorMessageObj> Errors { get; set; }
        public T Result { get; set; }
        public BusinessLayerResultcs()
        {
            Errors = new List<ErrorMessageObj>();
        }
        public void AddError(ErrorMessageCode code, string message)
        {
            Errors.Add(new ErrorMessageObj() { Code = code, Message = message });
        }
    }
}
