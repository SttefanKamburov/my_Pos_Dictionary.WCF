using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using Dictionary_WCF.AppConstanst;

namespace Dictionary_WCF.Shared.ModelsDTO
{
    public class AddressDTO : BaseDTO
    {
        [Required]
        [StringLength(AppConstants.NAME_MAX_LRNGHT, MinimumLength = AppConstants.NAME_MIN_LENGHT, ErrorMessage = "Name must be between 2 and 150 characters")]
        public string Address { get; set; }
        [Required]
        public bool IsHomeAddress { get; set; }
        [Required]
        public int PersonId { get; set; }
    }
}
