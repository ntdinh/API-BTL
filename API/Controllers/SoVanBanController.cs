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
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class SoVanBanController : ControllerBase
    {
        private ISoVanBanBusiness _sovbBusiness;
        private string _path;
        public SoVanBanController(ISoVanBanBusiness itemBusiness)
        {
            _sovbBusiness = itemBusiness;
        }
        public SoVanBanController(ISoVanBanBusiness userBusiness, IConfiguration configuration)
        {
            _sovbBusiness = userBusiness;
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

        [Route("create-svb")]
        [HttpPost]
        public SoVanBanModel CreateItem([FromBody] SoVanBanModel model)
        {
            _sovbBusiness.Create(model);
            return model;
        }

        [Route("get-by-id/{id}")]
        [HttpGet]
        public SoVanBanModel GetDatabyID(string id)
        {
            return _sovbBusiness.GetDatabyID(id);
        }

        [Route("get-all")]
        [HttpGet]
        public IEnumerable<SoVanBanModel> GetDatabAll()
        {
            return _sovbBusiness.GetDataAll();
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
                string sovanbanid = "";
                if (formData.Keys.Contains("sovanbanid") && !string.IsNullOrEmpty(Convert.ToString(formData["sovanbanid"]))) { sovanbanid = Convert.ToString(formData["sovanbanid"]); }
                long total = 0;
                var data = _sovbBusiness.Search(page, pageSize, out total, sovanbanid);
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
        [Route("delete-svb")]
        [HttpPost]
        public IActionResult DeleteUser([FromBody] Dictionary<string, object> formData)
        {
            string sovanbanid = "";
            if (formData.Keys.Contains("sovanbanid") && !string.IsNullOrEmpty(Convert.ToString(formData["sovanbanid"]))) { sovanbanid = Convert.ToString(formData["sovanbanid"]); }
            _sovbBusiness.Delete(sovanbanid);
            return Ok();
        }

       
        [Route("update-svb")]
        [HttpPost]
        public SoVanBanModel UpdateUser([FromBody] SoVanBanModel model)
        {

            _sovbBusiness.Update(model);
            return model;
        }
    }
}
