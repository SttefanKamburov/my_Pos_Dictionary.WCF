using System;
using System.Collections.Generic;
using System.Text;

namespace Dictionary_WCF.Shared.ModelsDTO
{
    public class PersonCriteriaInputModel
    {
        public string Name { get; set; }
        public string MiddleName { get; set; }
        public string Surname { get; set; }
        public long? PersonId { get; set; }
        public long? PhoneNumberId { get; set; }
        public long? AddressId { get; set; }
    }
}
