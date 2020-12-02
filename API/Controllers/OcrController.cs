using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;
using System.IO;
 
using Tesseract;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OcrController : ControllerBase
    {
        public const string folderName = "images/";
        public const string trainedDataFolderName = "tessdata";

        [HttpPost]
        public String DoOCR([FromForm] OcrModel request)
        {

            string name = request.Image.FileName;
            var image = request.Image;

            if (image.Length > 0)
            {
                using (var fileStream = new FileStream(folderName + image.FileName, FileMode.Create))
                {
                    image.CopyTo(fileStream);
                }
            }

            string tessPath = Path.Combine(trainedDataFolderName, "");
            string result = "";

            using (var engine = new TesseractEngine(tessPath, request.DestinationLanguage, EngineMode.Default))
            {
                using (var img = Pix.LoadFromFile(folderName + name))
                {
                    var page = engine.Process(img);
                    result = page.GetText();
                    Console.WriteLine(result);
                }
            }
            return String.IsNullOrWhiteSpace(result) ? "Ocr is finished. Return empty" : result;


        }
    }
}
