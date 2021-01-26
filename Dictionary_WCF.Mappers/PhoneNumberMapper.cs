using Dictionary_WCF.Models;
using Dictionary_WCF.Shared.ModelsDTO;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Dictionary_WCF.Mappers
{
    public class PhoneNumberMapper
    {
        public static readonly Expression<Func<PhoneNumberModel, PhoneNumberDTO>> SelectPhoneNumberDtoFromPhoneNumberModel = (PhoneNumberModel) => new PhoneNumberDTO()
        {
            Id = PhoneNumberModel.Id,
            PersonId = PhoneNumberModel.PersonId,
            IsHome = PhoneNumberModel.IsHome,
            Number = PhoneNumberModel.Number
        };
    }
}
