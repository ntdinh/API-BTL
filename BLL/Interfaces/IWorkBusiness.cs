using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public partial interface IWorkBusiness
    {
        WorkModel GetDatabyID(string id);
        bool Create(WorkModel model);
        bool Update(WorkModel model);
        bool Delete(string id);
        List<WorkModel> Search(int pageIndex, int pageSize, out long total, string workname );
    }
}
