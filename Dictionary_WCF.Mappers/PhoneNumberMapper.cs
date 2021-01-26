using Dictionary_WCF.Shared.ModelsDTO;
using Dictionary_WCF.Shared.OutputModels;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Dictionary_WCF.Mappers
{
    public class PhoneNumberMapper
    {
        public static PhoneNumberOutputModel SelectPhoneOutputModelFromPhoneDTO(PhoneNumberDTO phoneNumberDTO)
        {
            PhoneNumberOutputModel model = new PhoneNumberOutputModel();
            model.PhoneNumber = phoneNumberDTO.Number;
            return model;
        }
    }
}
