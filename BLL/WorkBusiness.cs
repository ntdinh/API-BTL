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
    public partial class WorkBusiness :IWorkBusiness
    {
        private IWorkRepository _res;
        private string Secret;
        public WorkBusiness(IWorkRepository res, IConfiguration configuration)
        {
            Secret = configuration["AppSettings:Secret"];
            _res = res;
        }
        public bool Delete(string id)
        {
            return _res.Delete(id);
        }
        public WorkModel GetDatabyID(string id)
        {
            return _res.GetDatabyID(id);
        }
        public bool Create(WorkModel model)
        {
            return _res.Create(model);
        }
        public bool Update(WorkModel model)
        {
            return _res.Update(model);
        }
        public List<WorkModel> Search(int pageIndex, int pageSize, out long total, string workname )
        {
            return _res.Search(pageIndex, pageSize, out total, workname);
        }
    }
}
