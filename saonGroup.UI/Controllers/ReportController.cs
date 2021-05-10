using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using saonGroup.UI.Models;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace saonGroup.UI.Controllers
{
    public class ReportController : Controller
    {
        public IActionResult Index()
        {
            BusinessModel bs = new BusinessModel();
            List<RegionModel> regions = bs.DataRegion();
            var regionDrop = new SelectList(regions.Select( r => new { r.name,r.iso }),"iso","name",string.Empty);
            ViewBag.regionDrop = regionDrop;
            return View(regions);
        }


        public IActionResult Province(string ISO) {
            BusinessModel bs = new BusinessModel();
            List<ProvinceModel> regions = bs.DataProvince(ISO);
            return  PartialView(regions);
        }
    }
}
