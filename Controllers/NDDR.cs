using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NDDR.Controllers
{
    [Route("api/[controller]")]
    public class NDDR : Controller
    {
        private readonly INDDRService _nDDRService;

        public NDDR(INDDRService nDDRService)
        {
            _nDDRService = nDDRService;
        }
        
        
        // GET: NDDR
        public string[] Index()
        {
            _nDDRService.Index();
            return new string[] {"123" , "123"};
        }

        // GET: NDDR/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        //// GET: NDDR/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: NDDR/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: NDDR/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: NDDR/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: NDDR/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: NDDR/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
