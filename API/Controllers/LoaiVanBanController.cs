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
    public class LoaiVanBanController : ControllerBase
    {
        private ILoaiVanBanBusiness _userBusiness;
        private string _path;
        public LoaiVanBanController( ILoaiVanBanBusiness userBusiness, IConfiguration configuration)
        {
            _userBusiness = userBusiness;
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
        public IEnumerable<LoaiVanBanModel> GetDatabAll()
        {
            return _userBusiness.GetDataAll();
        }
        [Route("delete-lvb")]
        [HttpPost]
        public IActionResult DeleteLvb([FromBody] Dictionary<string, object> formData)
        {
            string loaivanbanid = "";
            if (formData.Keys.Contains("loaivanbanid") && !string.IsNullOrEmpty(Convert.ToString(formData["loaivanbanid"]))) { loaivanbanid = Convert.ToString(formData["loaivanbanid"]); }
            _userBusiness.Delete(loaivanbanid);
            return Ok();
        }

        [Route("create-lvb")]
        [HttpPost]
        public LoaiVanBanModel CreateLvb([FromBody] LoaiVanBanModel model)
        {
            
            model.loaivanbanid = Guid.NewGuid().ToString();
            _userBusiness.Create(model);
            return model;
        }

        [Route("update-lvb")]
        [HttpPost]
        public LoaiVanBanModel UpdateLvb([FromBody] LoaiVanBanModel model)
        {
         
            _userBusiness.Update(model);
            return model;
        }

        [Route("get-by-id/{id}")]
        [HttpGet]
        public LoaiVanBanModel GetDatabyID(string id)
        {
            return _userBusiness.GetDatabyID(id);
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
                string tenloaivanban = "";
                if (formData.Keys.Contains("tenloaivanban") && !string.IsNullOrEmpty(Convert.ToString(formData["tenloaivanban"]))) { tenloaivanban = Convert.ToString(formData["tenloaivanban"]); }
                long total = 0;
                var data = _userBusiness.Search(page, pageSize, out total, tenloaivanban);
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
