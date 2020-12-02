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
    public partial class SoVanBanBusiness : ISoVanBanBusiness
    {
        private ISoVanBanRepository _res;
        private string Secret;
        public SoVanBanBusiness(ISoVanBanRepository res, IConfiguration configuration)
        {
            Secret = configuration["AppSettings:Secret"];
            _res = res;
        }
        public SoVanBanBusiness(ISoVanBanRepository ItemGroupRes)
        {
            _res = ItemGroupRes;
        }
        public bool Create(SoVanBanModel model)
        {
            return _res.Create(model);
        }
        public SoVanBanModel GetDatabyID(string id)
        {
            return _res.GetDatabyID(id);
        }
        public List<SoVanBanModel> GetDataAll()
        {
            return _res.GetDataAll();
        }
        public List<SoVanBanModel> Search(int pageIndex, int pageSize, out long total, string tenovanban)
        {
            return _res.Search(pageIndex, pageSize, out total, tenovanban);
        }
        public bool Delete(string id)
        {
            return _res.Delete(id);
        }
        public bool Update(SoVanBanModel model)
        {
            return _res.Update(model);
        }
    }
}
