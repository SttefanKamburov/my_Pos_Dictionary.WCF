using System;
using System.Collections.Generic;
using System.Text;

namespace Dictionary_WCF.Models
{
    public class PhoneNumberModel:BaseModel
    {
        public string Number { get; set; }
        public bool IsHome { get; set; }
        public int PersonId { get; set; }
    }
}