using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using BLL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Model;
namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SoVanBanController : ControllerBase
    {
        private ISoVanBanBusiness _workBusiness;
        private string _path;
        public SoVanBanController(ISoVanBanBusiness userBusiness, IConfiguration configuration)
        {
            _workBusiness = userBusiness;
            _path = configuration["AppSettings:PATH"];
        }
        public string SaveFileFromBase64String(string RelativePathFileName, string dataFromBase64String)
        {
            if (dataFromBase64String.Contains("base64,"))
            {
                dataFromBase64String = dataFromBase64String.Substring(dataFromBase64String.IndexOf("base64,", 0) + 7);
            }
            return WriteFileToAuthAccessFolder(RelativePathFileName, dataFromBase64String);
        }
        public string WriteFileToAuthAccessFolder(string RelativePathFileName, string base64StringData)
        {
            try
            {
                string result = "";
                string serverRootPathFolder = _path;
                string fullPathFile = $@"{serverRootPathFolder}\{RelativePathFileName}";
                string fullPathFolder = System.IO.Path.GetDirectoryName(fullPathFile);
                if (!Directory.Exists(fullPathFolder))
                    Directory.CreateDirectory(fullPathFolder);
                System.IO.File.WriteAllBytes(fullPathFile, Convert.FromBase64String(base64StringData));
                return result;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        [Route("delete-svb")]
        [HttpPost]
        public IActionResult DeleteSVB([FromBody] Dictionary<string, object> formData)
        {
            string sovanbanid = "";
            if (formData.Keys.Contains("sovanbanid") && !string.IsNullOrEmpty(Convert.ToString(formData["sovanbanid"]))) { sovanbanid = Convert.ToString(formData["sovanbanid"]); }
            _workBusiness.Delete(sovanbanid);
            return Ok();
        }
        [Route("create-svb")]
        [HttpPost]
        public SoVanBanModel CreateSVB([FromBody] SoVanBanModel model)
        {
           
            model.sovanbanid = Guid.NewGuid().ToString();
            _workBusiness.Create(model);
            return model;
        }
        [Route("update-svb")]
        [HttpPost]
        public SoVanBanModel UpdateSVB([FromBody] SoVanBanModel model)
        {
            _workBusiness.Update(model);
            return model;
        }
        [Route("get-all")]
        [HttpGet]
        public IEnumerable<SoVanBanModel> GetDatabAll()
        {
            return _workBusiness.GetDataAll();
        }

        [Route("get-by-id/{id}")]
        [HttpGet]
        public SoVanBanModel GetDatabyID(string id)
        {
            return _workBusiness.GetDatabyID(id);
        }

        [Route("search")]
        [HttpPost]
        public ResponseModel Search([FromBody] Dictionary<string, object> formData)
        {
            var response = new ResponseModel();
            try
            {
                var page = int.Parse(formData["page"].ToString());
                var pageSize = int.Parse(formData["pageSize"].ToString());
                string tensovanban = "";
                if (formData.Keys.Contains("tensovanban") && !string.IsNullOrEmpty(Convert.ToString(formData["tensovanban"]))) { tensovanban = Convert.ToString(formData["tensovanban"]); }
             
                long total = 0;
                var data = _workBusiness.Search(page, pageSize, out total, tensovanban);
                response.TotalItems = total;
                response.Data = data;
                response.Page = page;
                response.PageSize = pageSize;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return response;
        }

    }
}
