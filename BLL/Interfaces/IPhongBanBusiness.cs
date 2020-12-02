using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public partial interface IPhongBanBusiness
    {
        List<PhongBanModel> GetDataAll();
        PhongBanModel GetDatabyID(string id);
        bool Create(PhongBanModel model);
        bool Update(PhongBanModel model);
        bool Delete(string id);
        List<PhongBanModel> Search(int pageIndex, int pageSize, out long total, string tenphongban);
    }
}
