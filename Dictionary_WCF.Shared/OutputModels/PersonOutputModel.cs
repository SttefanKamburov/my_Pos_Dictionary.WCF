using System;
using System.Collections.Generic;
using System.Text;

namespace Dictionary_WCF.Shared.OutputModels
{
    public class PersonOutputModel
    {
        public string Name { get; set; }
        public List<AddressOutputModel> OfficeAdresses { get; set; }
        public List<PhoneNumberOutputModel> OfficePhoneNumbers { get; set; }
        public List<AddressOutputModel> HomeAddresses { get; set; }
        public List<PhoneNumberOutputModel> HomePhoneNumbers { get; set; }
    }
}
