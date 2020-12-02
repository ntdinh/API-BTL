
using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public   interface ISoVanBanRepository
    {
        //bool Create(SoVanBanModel model);
        SoVanBanModel GetDatabyID(string id);
        List<SoVanBanModel> GetDataAll();
       // SoVanBanModel GetDatabyID(string id);
        bool Create(SoVanBanModel model);
        bool Update(SoVanBanModel model);
        bool Delete(string id);
        // List<ItemModel> Search(int pageIndex, int pageSize, out long total, string item_group_id);
        List<SoVanBanModel> Search(int pageIndex, int pageSize, out long total, string tensovanban);
    }
}
