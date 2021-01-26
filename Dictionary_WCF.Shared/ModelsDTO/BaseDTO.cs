using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Dictionary_WCF.Shared.ModelsDTO
{
    public class BaseDTO
    {
        [Required]
        public int Id { get; set; }
    }
}
