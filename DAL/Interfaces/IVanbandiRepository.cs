using System;
using System.Collections.Generic;
using System.Text;
using Model;

namespace DAL
{
    public interface IVanbandiRepository
    {
        VanbandiModel GetDatabyID(string id);
        bool Create(VanbandiModel model);
        bool Update(VanbandiModel model);
        bool Delete(string id);
        List<VanbandiModel> Search(int pageIndex, int pageSize, out long total, string noinhan);
    }
}
