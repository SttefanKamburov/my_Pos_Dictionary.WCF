using System;
using System.Collections.Generic;
using System.Text;

namespace Dictionary_WCF.Models
{
    public class AddressModel : BaseModel
    {
        public string Address { get; set; }
        public bool IsHomeAddress { get; set; }
        public int PersonId { get; set; }
    }
}
