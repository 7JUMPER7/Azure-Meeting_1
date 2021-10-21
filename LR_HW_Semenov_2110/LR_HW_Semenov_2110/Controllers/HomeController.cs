using LR_HW_Semenov_2110.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LR_HW_Semenov_2110.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly Uploader uploader;

        public HomeController(ILogger<HomeController> logger, Uploader uploader)
        {
            _logger = logger;
            this.uploader = uploader;

        }

        public async Task<IActionResult> Index()
        {
            return View(await uploader.GetImages());
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(IFormFile image)
        {
            if(!await uploader.AddImage(image.FileName, ImageToByteArray(image)))
            {
                return View();
            }
            return RedirectToAction("Index");
        }

        public byte[] ImageToByteArray(IFormFile image)
        {
            using (var ms = new MemoryStream())
            {
                image.CopyTo(ms);
                return ms.ToArray();
            }
        }
    }
}
