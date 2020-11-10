using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public partial interface ILoaiVanBanBusiness
    {
        // UserModel Authenticate(string username, string password);
        List<LoaiVanBanModel> GetDataAll();
        LoaiVanBanModel GetDatabyID(string id);
        bool Create(LoaiVanBanModel model);
        bool Update(LoaiVanBanModel model);
        bool Delete(string id);
        List<LoaiVanBanModel> Search(int pageIndex, int pageSize, out long total, string tenloaivanban);
    }
}
