using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SixDegrees.Web.Configuration
{
    public interface IWebConfigurationModule
    {
        void Configure();
    }
}