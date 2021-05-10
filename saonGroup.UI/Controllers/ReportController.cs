using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using saonGroup.UI.Models;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.Json;

namespace saonGroup.UI.Controllers
{
    public class ReportController : Controller
    {
        public IActionResult Index()
        {
            BusinessModel bs = new BusinessModel();
            List<RegionModel> regions = bs.DataRegion(); 
            ViewBag.regionDrop = new SelectList(regions.Select(r => new { r.name, r.iso }), "iso", "name", string.Empty); 
            return View(regions);
        }


        public IActionResult Province(string ISO) {
            BusinessModel bs = new BusinessModel();
            List<ProvinceModel> province = bs.DataProvince(ISO); 
            return  PartialView(province);
        }
    }
}
