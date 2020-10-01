using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Aspcore.Utils
{
    public static class ValidEntity
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static IList<ValidationResult> CheckPropertyValidation(this object model)
        {
            var result = new List<ValidationResult>();
            var validationContext = new ValidationContext(model);
            Validator.TryValidateObject(model, validationContext, result);
            if (model is IValidatableObject) (model as IValidatableObject).Validate(validationContext);
            return result;
        }


        public static string Errors(this object model)
        {
            var errors = model.CheckPropertyValidation();
            return string.Join(";", errors.ToList().Select(e => e.ErrorMessage));
        }

        public static List<string> GetMembers(this object model)
        {
            var errors = model.CheckPropertyValidation();
            return errors.ToList().Select(e => e.MemberNames?.FirstOrDefault()).ToList();
        }


        public static bool IsValid(this object model)
        {
            var errors = model.CheckPropertyValidation();
            return errors.Count() == 0;
        }
    }
}

