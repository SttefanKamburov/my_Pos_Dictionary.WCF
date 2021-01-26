using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using Dictionary_WCF.AppConstanst;

namespace Dictionary_WCF.Shared.ModelsDTO
{
    public class PhoneNumberDTO : BaseDTO
    {
        [Required]
        [StringLength(AppConstants.NAME_MAX_LRNGHT, MinimumLength = AppConstants.NAME_MIN_LENGHT, ErrorMessage = "Name must be between 2 and 150 characters")]
        public string Number { get; set; }
        [Required]
        public bool IsHome { get; set; }
        [Required]
        public int PersonId { get; set; }
    }
}
