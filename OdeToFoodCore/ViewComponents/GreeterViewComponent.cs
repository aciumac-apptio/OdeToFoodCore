using Microsoft.AspNetCore.Mvc;
using OdeToFoodCore.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OdeToFoodCore.ViewComponents
{
    public class GreeterViewComponent : ViewComponent
    {
        private IGreeter __greeter;

        public GreeterViewComponent(IGreeter greeter)
        {
            __greeter = greeter;
        }

        public IViewComponentResult Invoke()
        {
            var model = __greeter.GetMessageOfTheDay();
            //If passing a string
            return View("Default", model);
        }
    }
}
