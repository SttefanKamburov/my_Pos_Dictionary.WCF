using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Dictionary_WCF.Models
{
    public class PersonModel:BaseModel
    {
        public string Name { get; set; }
        public string MiddleName { get; set; }
        public string  Surname { get; set; }
        public ICollection<PhoneNumberModel> PhoneNumbers { get; set; }
        public ICollection<AddressModel> Addresses { get; set; }
    }
}
