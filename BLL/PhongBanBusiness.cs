using DAL;
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
    public partial class PhongBanBusiness : IPhongBanBusiness
    {
        private IPhongBanRepository _res;
        private string Secret;
        public PhongBanBusiness(IPhongBanRepository res, IConfiguration configuration)
        {
            Secret = configuration["AppSettings:Secret"];
            _res = res;
        }
        public List<PhongBanModel> GetDataAll()
        {
            return _res.GetDataAll();
        }
        public bool Delete(string id)
        {
            return _res.Delete(id);
        }
        public PhongBanModel GetDatabyID(string id)
        {
            return _res.GetDatabyID(id);
        }
        public bool Create(PhongBanModel model)
        {
            return _res.Create(model);
        }
        public bool Update(PhongBanModel model)
        {
            return _res.Update(model);
        }
        public List<PhongBanModel> Search(int pageIndex, int pageSize, out long total, string tenphongban)
        {
            return _res.Search(pageIndex, pageSize, out total, tenphongban);
        }
    }
}
