using Dictionary_WCF.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Dictionary_WCF.AppConstanst;


namespace Dictionary_WCF.Shared.ModelsDTO
{
    public class PersonDTO:BaseDTO
    {
        [Required]
        [StringLength(AppConstants.NAME_MAX_LRNGHT, MinimumLength = AppConstants.NAME_MIN_LENGHT, ErrorMessage = "Name must be between 2 and 150 characters")]
        public string Name { get; set; }
        [Required]
        [StringLength(AppConstants.NAME_MAX_LRNGHT, MinimumLength = AppConstants.NAME_MIN_LENGHT, ErrorMessage = "Name must be between 2 and 150 characters")]
        public string MiddleName { get; set; }
        [Required]
        [StringLength(AppConstants.NAME_MAX_LRNGHT, MinimumLength = AppConstants.NAME_MIN_LENGHT, ErrorMessage = "Name must be between 2 and 150 characters")]
        public string Surname { get; set; }
        public ICollection<PhoneNumberDTO> PhoneNumbers { get; set; }
        public ICollection<AddressDTO> Addresses { get; set; }
    }
}
