using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.Linq;

namespace TesteBackendEnContact.Extensions
{
    public static class ModelStateExtension
    {
        public static List<string> GetErrors(this ModelStateDictionary modelState)
        {

            var errors = new List<string>();

            foreach (var error in modelState.Values)
            {
                errors.AddRange(error.Errors.Select(x => x.ErrorMessage));
            }

            return errors;

        }
    }
}
