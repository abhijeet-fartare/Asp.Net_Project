using ServiceContracts.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.helpers
{
    public class ValidationHelper
    {
       //Model validation with reusable method
        public static void ModelValidation(Object obj)
        {
            
            //model validation
            ValidationContext validationContext = new ValidationContext(obj);

            List<ValidationResult> validationResults = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(obj, validationContext, validationResults, true);
            //true is for validate all properties not only [required]

            if (!isValid)
            {
                throw new ArgumentException(validationResults.FirstOrDefault().ErrorMessage);
            }


        }
    }
}
