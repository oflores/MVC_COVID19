using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using saonGroup.UI.Models;

namespace saonGroup.UI.Controllers
{
    public class ReportController : Controller
    {
        public IActionResult Index()
        {
            return View(this.initialData());
        }

        private List<RegionModel> initialData() {
            List<RegionModel> result = new List<RegionModel>();
            result.Add( new RegionModel { iso="USA", confirmed=100, deaths=50, name="United States", recovered=1});
            result.Add(new RegionModel { iso = "CHN", confirmed = 100, deaths = 50, name = "CHINA", recovered = 1 });
            result.Add(new RegionModel { iso = "GTM", confirmed = 100, deaths = 50, name = "GUATEMALA", recovered = 1 });
            result.Add(new RegionModel { iso = "ESL", confirmed = 100, deaths = 50, name = "El SALVADOR", recovered = 1 });
            return result;
        } 
    }
}
