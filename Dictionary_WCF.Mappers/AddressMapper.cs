using Dictionary_WCF.Models;
using Dictionary_WCF.Shared.ModelsDTO;
using Dictionary_WCF.Shared.OutputModels;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Dictionary_WCF.Mappers
{
    public class AddressMapper
    { 
        public static AddressOutputModel SelectAddressDtoFromAddress (AddressDTO addressDTO)
        {
            AddressOutputModel model = new AddressOutputModel();
            model.Address = addressDTO.Address;
            return model;
        }
    }
}
