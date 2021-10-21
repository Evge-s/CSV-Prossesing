using CSVProssesing.Data;
using CSVProssesing.Models.File;
using CSVProssesing.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;

namespace CSVProssesing.Controllers
{
    public class FileController : Controller
    {
        private DataContext dataContext;
        private IWebHostEnvironment appEnvironment;

        public FileController(DataContext dataContext, IWebHostEnvironment appEnvironment)
        {
            this.dataContext = dataContext;
            this.appEnvironment = appEnvironment;
        }

        [HttpPost]
        public async Task<IActionResult> UploadCSV(IFormFile uploadedFile)
        {
            if (uploadedFile != null)
            {
                string path = "/Resources/" + uploadedFile.FileName;
                string fullPath = appEnvironment.WebRootPath + path;

                using (var fileSrtream = new FileStream(appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await uploadedFile.CopyToAsync(fileSrtream);
                }

                FileCSV csv = new FileCSV { Name = uploadedFile.FileName, Path = path, UploadDate = DateTime.Now };

                var people = ParserCSV.CSVParseToModel(fullPath);
                if (people != null)
                {
                    dataContext.Files.Add(csv);
                    dataContext.People.AddRange(people);
                    dataContext.SaveChanges();
                }
                else
                {
                    // Pass, file deletion logic (wrong format, corrupted file)
                }
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
