using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LayersDLL.Interfaces;
using LayersDLL.Services;
using Ninject.Modules;

namespace Layers.Util
{
    public class TMSModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ITMSService>().To<TMSService>();
        }
    }
}