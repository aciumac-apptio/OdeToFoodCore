using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OdeToFoodCore.Controllers
{
    [Route("company/[controller]/[action]")]
    public class AboutController
    {        
        public string Phone()
        {
            return "425-208-9999";
        }
                
        public string Address()
        {
            return "fake address";
        }
    }
}
