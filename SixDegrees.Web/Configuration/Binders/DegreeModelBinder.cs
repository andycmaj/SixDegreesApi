using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SixDegrees.Web.Configuration.Binders
{
    public class DegreeModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            string id = 
        }

        private string GetValue(ModelBindingContext bindingContext, string key)
        {
            ValueProviderResult result = bindingContext.ValueProvider.GetValue(key);
            return (result == null) ? null : result.AttemptedValue;
        }
    }
}