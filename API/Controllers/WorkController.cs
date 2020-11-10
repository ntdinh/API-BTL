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
    public class WorkController : ControllerBase
    {
        private IWorkBusiness _workBusiness;
        private string _path;
        public WorkController(IWorkBusiness userBusiness, IConfiguration configuration)
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
        [Route("delete-work")]
        [HttpPost]
        public IActionResult DeleteWork([FromBody] Dictionary<string, object> formData)
        {
            string workid = "";
            if (formData.Keys.Contains("workid") && !string.IsNullOrEmpty(Convert.ToString(formData["workid"]))) { workid = Convert.ToString(formData["workid"]); }
            _workBusiness.Delete(workid);
            return Ok();
        }
        [Route("create-work")]
        [HttpPost]
        public WorkModel CreateWork([FromBody] WorkModel model)
        {
           
            model.workid = Guid.NewGuid().ToString();
            _workBusiness.Create(model);
            return model;
        }
        [Route("update-work")]
        [HttpPost]
        public WorkModel UpdateWork([FromBody] WorkModel model)
        {
            _workBusiness.Update(model);
            return model;
        }

        [Route("get-by-id/{id}")]
        [HttpGet]
        public WorkModel GetDatabyID(string id)
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
                string workname = "";
                if (formData.Keys.Contains("workname") && !string.IsNullOrEmpty(Convert.ToString(formData["workname"]))) { workname = Convert.ToString(formData["workname"]); }
             
                long total = 0;
                var data = _workBusiness.Search(page, pageSize, out total, workname );
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
