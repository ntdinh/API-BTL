using System;
using System.Collections.Generic;
using System.Text;
using Model;

namespace DAL
{
    public interface IPhongBanRepository
    {
        // WorkModel GetUser(string username, string password);
        List<PhongBanModel> GetDataAll();
        PhongBanModel GetDatabyID(string id);
        bool Create(PhongBanModel model);
        bool Update(PhongBanModel model);
        bool Delete(string id);
        List<PhongBanModel> Search(int pageIndex, int pageSize, out long total, string workname);
    }
}
