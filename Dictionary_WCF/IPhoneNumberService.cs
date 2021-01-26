using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using Dictionary_WCF.Shared.ModelsDTO;
using System.Text;

namespace Dictionary_WCF
{
    [ServiceContract]
    public interface IPhoneNumberService
    {
        [OperationContract]
        [WebInvoke(Method = "GET",
             RequestFormat = WebMessageFormat.Json,
             ResponseFormat = WebMessageFormat.Json,
             BodyStyle = WebMessageBodyStyle.Wrapped,
             UriTemplate = "Phones")]
        IEnumerable<PhoneNumberDTO> GetAll();

        [OperationContract]
        [WebInvoke(Method = "GET",
             RequestFormat = WebMessageFormat.Json,
             ResponseFormat = WebMessageFormat.Json,
             BodyStyle = WebMessageBodyStyle.Wrapped,
             UriTemplate = "Phones/{id}")]
        PhoneNumberDTO GetPhoneNumberById(string id);

        [OperationContract]
        [WebInvoke(Method = "DELETE",
             RequestFormat = WebMessageFormat.Json,
             ResponseFormat = WebMessageFormat.Json,
             BodyStyle = WebMessageBodyStyle.Wrapped,
             UriTemplate = "Phones/{id}")]
        void DeletePhoneNumber(string id);

        [OperationContract]
        [WebInvoke(Method = "POST",
           RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json,
           BodyStyle = WebMessageBodyStyle.Bare,
           UriTemplate = "AddPhone")]
        PhoneNumberDTO AddPhone(PhoneNumberDTO newPhone);

        [OperationContract]
        [WebInvoke(Method = "PUT",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "UpdatePhone")]
        PhoneNumberDTO UpdatePhone(PhoneNumberDTO newPhone);
    }
}
