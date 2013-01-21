using System.Web.Mvc;
using SixDegrees.Data;

namespace SixDegrees.Web.Configuration.Binders
{
    public class DegreeTypeModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            string searchType = GetValue(bindingContext, "type") ?? DegreeType.Movie.ResourceName;
            DegreeType type = DegreeType.Parse(searchType);

            return type;
        }

        private string GetValue(ModelBindingContext bindingContext, string key)
        {
            ValueProviderResult result = bindingContext.ValueProvider.GetValue(key);
            return (result == null) ? null : result.AttemptedValue;
        }
    }
}