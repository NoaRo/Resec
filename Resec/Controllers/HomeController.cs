using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Resec.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Resec.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public string docPath;

        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment hostingEnvironment)
        {
            _logger = logger;
            _hostingEnvironment = hostingEnvironment;
            docPath = Path.Combine(_hostingEnvironment.WebRootPath, "textFiles");
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        [HttpPost]
        [Route("SaveFile")]
        public async Task SaveFile(string text,string name)
        {
            
            // DirectoryInfo di = new DirectoryInfo(docPath);

            // Set a variable to the Documents path.
            //string docPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            // Write the specified text asynchronously to a new file named "WriteTextAsync.txt".
            using (StreamWriter outputFile = new StreamWriter(Path.Combine(docPath, name)))
            {
                await outputFile.WriteAsync(text);
            }
        }

        [HttpDelete]
        [Route("RemoveFile/{fileName}")]
        public void RemoveFile(string fileName)
        {
            var file = Path.Combine(docPath, fileName);
            System.IO.File.Delete(file);

        }

        [HttpGet]
        [Route("GetFileContent/{fileName}")]
        public IActionResult GetFileContent(string fileName)
        {
            string content;
            var file = Path.Combine(docPath, fileName);

            string SendData = System.IO.File.ReadAllText(file);
            //using (StreamReader outputFile = new StreamReader(Path.Combine(docPath, fileName)))
            //{
            //    content = await outputFile.Read();
            //}
            return Ok(SendData);
        }

        [HttpGet]
        [Route("GetAllfilesInTheServer")]
        public IActionResult GetAllfilesInTheServer()
        {
            string[] filePaths = Directory.GetFiles(docPath);
            var files = new FilesList(filePaths);
            ViewData["filesList"] = files;
            return Ok(files);
        }
        /*        [HttpPost]
                [Route("DeleteFile")]
                public async Task DeleteFile(string text)
                {
                    string fileName = text;
                    string path = docPath + fileName;
                    FileInfo fileInfo = new FileInfo(path);
                    if (fileInfo.Exists)
                    {
                        fileInfo.Delete();
                    }
                }*/
        /*        public async Task saveAsync(string textForSave)
                {
                    List<string> Instructions_Files = new List<string>();

                    // The location where instructions files should be saved.
                    string rootFolder = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");
                    DirectoryInfo di = new DirectoryInfo(rootFolder);


                    // Save the new instructions files in the directory.

                    string filePath = Path.Combine(rootFolder, "newTextFile");

                    using (var stream = System.IO.File.Create(filePath))
                    {
                           await formFile.CopyToAsync(stream);
                    }
                }*/
    }
}
