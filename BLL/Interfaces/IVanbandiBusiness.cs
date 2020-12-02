using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL 
{
    public partial interface IVanbandiBusiness
    {
        List<LoaiVanBanModel> GetDataAll();
        List<VanbandiModel> GetDataAllByPhongBan(int pageIndex, int pageSize, out long total, string tenpb, string loaivb);

        VanbandiModel GetDatabyID(string id);
        bool Create(VanbandiModel model);
        bool Update(VanbandiModel model);
        bool Delete(string id);
        List<VanbandiModel> Search(int pageIndex, int pageSize, out long total, string tenphongban);
    }
}
