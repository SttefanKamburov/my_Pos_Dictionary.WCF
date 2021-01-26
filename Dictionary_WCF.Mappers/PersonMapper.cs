using Dictionary_WCF.Shared.ModelsDTO;
using Dictionary_WCF.Shared.OutputModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Dictionary_WCF.Mappers
{
    public class PersonMapper
    {
        public static PersonOutputModel SelectPersonOutputModelFromPersonDTO(PersonDTO personDTO)
        {
            PersonOutputModel model = new PersonOutputModel();
            model.Name = personDTO.Surname + ", " + personDTO.Name + " " + personDTO.MiddleName;

            model.OfficeAdresses = personDTO.Addresses
                .Where(p => p.IsHomeAddress == false)
                .Select(i => new AddressOutputModel() { Address = i.Address }).ToList();

            model.OfficePhoneNumbers = personDTO.PhoneNumbers
                .Where(p => p.IsHome == false)
                .Select(i => new PhoneNumberOutputModel() { PhoneNumber = i.Number }).ToList();

            model.HomeAddresses = personDTO.Addresses
                .Where(p => p.IsHomeAddress == true)
                .Select(i => new AddressOutputModel() { Address = i.Address }).ToList();

            model.HomePhoneNumbers = personDTO.PhoneNumbers
                .Where(p => p.IsHome == true)
                .Select(i => new PhoneNumberOutputModel() { PhoneNumber = i.Number }).ToList();

            return model;
        }
    }
}
