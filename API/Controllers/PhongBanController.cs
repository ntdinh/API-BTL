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
    public class PhongBanController : ControllerBase
    {
        private IPhongBanBusiness _workBusiness;
        private string _path;
        public PhongBanController(IPhongBanBusiness userBusiness, IConfiguration configuration)
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
        [Route("get-all")]
        [HttpGet]
        public IEnumerable<PhongBanModel> GetDatabAll()
        {
            return _workBusiness.GetDataAll();
        }
        [Route("delete-phongban")]
        [HttpPost]
        public IActionResult DeletePhongban([FromBody] Dictionary<string, object> formData)
        {
            string phongbanid = "";
            if (formData.Keys.Contains("phongbanid") && !string.IsNullOrEmpty(Convert.ToString(formData["phongbanid"]))) { phongbanid = Convert.ToString(formData["phongbanid"]); }
            _workBusiness.Delete(phongbanid);
            return Ok();
        }
        [Route("create-phongban")]
        [HttpPost]
        public PhongBanModel CreatePhongban([FromBody] PhongBanModel model)
        {
           
            model.phongbanid = Guid.NewGuid().ToString();
            _workBusiness.Create(model);
            return model;
        }
        [Route("update-phongban")]
        [HttpPost]
        public PhongBanModel UpdatePhongban([FromBody] PhongBanModel model)
        {
            _workBusiness.Update(model);
            return model;
        }

        [Route("get-by-id/{id}")]
        [HttpGet]
        public PhongBanModel GetDatabyID(string id)
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
                string tenphongban = "";
                if (formData.Keys.Contains("tenphongban") && !string.IsNullOrEmpty(Convert.ToString(formData["tenphongban"]))) { tenphongban = Convert.ToString(formData["tenphongban"]); }
             
                long total = 0;
                var data = _workBusiness.Search(page, pageSize, out total, tenphongban);
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
