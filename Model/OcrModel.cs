using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace Model
{
    public class OcrModel
    {
        public String DestinationLanguage { get; set; }
        public IFormFile Image { get; set; }
    }
}
