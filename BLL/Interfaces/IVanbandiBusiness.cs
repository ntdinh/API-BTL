using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL 
{
    public partial interface IVanbandiBusiness
    {
        VanbandiModel GetDatabyID(string id);
        bool Create(VanbandiModel model);
        bool Update(VanbandiModel model);
        bool Delete(string id);
        List<VanbandiModel> Search(int pageIndex, int pageSize, out long total, string noinhan);
    }
}
