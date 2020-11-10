using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public partial interface ISoVanBanBusiness
    {
        bool Update(SoVanBanModel model);
        SoVanBanModel GetDatabyID(string id);
        List<SoVanBanModel> GetDataAll();
        bool Create(SoVanBanModel model);
        List<SoVanBanModel> Search(int pageIndex, int pageSize, out long total, string tenovanban);
        bool Delete(string id);
    }
}
