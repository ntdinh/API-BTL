﻿using System;
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
    public class VanBanDiController : ControllerBase
    {
        private IVanbandiBusiness _vanbanBusiness;
        private string _path;
        public VanBanDiController(IVanbandiBusiness userBusiness, IConfiguration configuration)
        {
            _vanbanBusiness = userBusiness;
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
            return _vanbanBusiness.GetDataAll();
        }
        [Route("get-all-phongban")]
        [HttpPost]
        public ResponseModel GetDataAllByPhongBan([FromBody] Dictionary<string, object> formData)
        {
            var response = new ResponseModel();
            try
            {
                var page = int.Parse(formData["page"].ToString());
                var pageSize = int.Parse(formData["pageSize"].ToString());
                string loaivb = "";
                if (formData.Keys.Contains("loaivb") && !string.IsNullOrEmpty(Convert.ToString(formData["loaivb"]))) { loaivb = Convert.ToString(formData["loaivb"]); }
                string tenpb = "";
                if (formData.Keys.Contains("tenpb") && !string.IsNullOrEmpty(Convert.ToString(formData["tenpb"]))) { tenpb = Convert.ToString(formData["tenpb"]); }

                long total = 0;
                var data = _vanbanBusiness.GetDataAllByPhongBan(page, pageSize, out total, tenpb,loaivb);
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
        [Route("delete-vanbandi")]
        [HttpPost]
        public IActionResult DeleteVanbandi([FromBody] Dictionary<string, object> formData)
        {
            string vanbanid = "";
            if (formData.Keys.Contains("vanbanid") && !string.IsNullOrEmpty(Convert.ToString(formData["vanbanid"]))) { vanbanid = Convert.ToString(formData["vanbanid"]); }
            _vanbanBusiness.Delete(vanbanid);
            return Ok();
        }
        [Route("create-vanbandi")]
        [HttpPost]
        public VanbandiModel CreateWork([FromBody] VanbandiModel model)
        {

            model.vanbanid = Guid.NewGuid().ToString();
            _vanbanBusiness.Create(model);
            return model;
        }
        [Route("update-vanbandi")]
        [HttpPost]
        public VanbandiModel UpdateVanbandi([FromBody] VanbandiModel model)
        {
            _vanbanBusiness.Update(model);
            return model;
        }

        [Route("get-by-id/{id}")]
        [HttpGet]
        public VanbandiModel GetDatabyID(string id)
        {
            return _vanbanBusiness.GetDatabyID(id);
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
                var data = _vanbanBusiness.Search(page, pageSize, out total, tenphongban);
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
