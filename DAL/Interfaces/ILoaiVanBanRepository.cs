
using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public partial interface ILoaiVanBanRepository
    {
        //bool Create(SoVanBanModel model);
        // ItemModel GetDatabyID(string id);
        List<LoaiVanBanModel> GetDataAll();
        LoaiVanBanModel GetDatabyID(string id);
        bool Create(LoaiVanBanModel model);
          bool Update(LoaiVanBanModel model);
        bool Delete(string id);
        // List<ItemModel> Search(int pageIndex, int pageSize, out long total, string item_group_id);
        List<LoaiVanBanModel> Search(int pageIndex, int pageSize, out long total, string tenloaivanban);
    }
}
