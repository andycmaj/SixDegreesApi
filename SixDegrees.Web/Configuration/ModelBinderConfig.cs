using System.Web.Mvc;
using SixDegrees.Data;
using SixDegrees.Web.Configuration.Binders;

namespace SixDegrees.Web.Configuration
{
    public static class ModelBinderConfig
    {
        public static void RegisterModelBinders(ModelBinderDictionary binders)
        {
            binders.Add(typeof (DegreeType), new DegreeTypeModelBinder());
        }
    }
}