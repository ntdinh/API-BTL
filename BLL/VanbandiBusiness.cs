﻿using DAL;
using Microsoft.IdentityModel.Tokens;
using Model;
using System;
using Helper;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace BLL
{
    public partial class VanbandiBusiness :IVanbandiBusiness
    {
        private IVanbandiRepository _res;
        private string Secret;
        public VanbandiBusiness(IVanbandiRepository res, IConfiguration configuration)
        {
            Secret = configuration["AppSettings:Secret"];
            _res = res;
        }
        public List<LoaiVanBanModel> GetDataAll()
        {
            return _res.GetDataAll();
        }
        public List<VanbandiModel> GetDataAllByPhongBan(int pageIndex, int pageSize, out long total, string tenpb, string loaivb)
        {
            return _res.GetDataAllByPhongBan(pageIndex, pageSize, out total, tenpb,loaivb);
        }
        public bool Delete(string id)
        {
            return _res.Delete(id);
        }
        public VanbandiModel GetDatabyID(string id)
        {
            return _res.GetDatabyID(id);
        }
        public bool Create(VanbandiModel model)
        {
            return _res.Create(model);
        }
        public bool Update(VanbandiModel model)
        {
            return _res.Update(model);
        }
        public List<VanbandiModel> Search(int pageIndex, int pageSize, out long total, string tenphongban)
        {
            return _res.Search(pageIndex, pageSize, out total, tenphongban);
        }
    }
}
