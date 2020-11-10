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
    public partial class  LoaiVanBanBusiness :ILoaiVanBanBusiness
    {
        private ILoaiVanBanRepository _res;
       
    
        public LoaiVanBanBusiness(ILoaiVanBanRepository ItemGroupRes)
        {
            _res = ItemGroupRes;
        }
      
        public bool Delete(string id)
        {
            return _res.Delete(id);
        }
        public LoaiVanBanModel GetDatabyID(string id)
        {
            return _res.GetDatabyID(id);
        }
        public bool Create(LoaiVanBanModel model)
        {
            return _res.Create(model);
        }
        public bool Update(LoaiVanBanModel model)
        {
            return _res.Update(model);
        }
        public List<LoaiVanBanModel> Search(int pageIndex, int pageSize, out long total, string tenloaivanban)
        {
            return _res.Search(pageIndex, pageSize, out total, tenloaivanban);
        }
        public List<LoaiVanBanModel> GetDataAll()
        {
            return _res.GetDataAll();
        }
    }
}
