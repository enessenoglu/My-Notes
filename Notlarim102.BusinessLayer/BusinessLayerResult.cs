using Notlarim102.Entity.Massages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notlarim102.BusinessLayer
{
  public  class BusinessLayerResult<T> where T:class
    {
        //public List<KeyValuePair<ErrorMessageCode,string>> Error { get; set; }
        public List<ErrorMessageObject> Error { get; set; }
        public T Result { get; set; }
        public BusinessLayerResult()
        {
            //Error = new List<KeyValuePair<ErrorMessageCode, string>>();
            Error = new List<ErrorMessageObject>();
        }
        public void AddError(ErrorMessageCode code, string message)
        {
            Error.Add(new ErrorMessageObject{Code=code,Message=message });
        }
    }
}
