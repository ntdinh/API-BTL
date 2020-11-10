using System;
using System.Collections.Generic;
using System.Text;
using Model;

namespace DAL
{
    public interface   IWorkRepository
    {
       // WorkModel GetUser(string username, string password);
        WorkModel GetDatabyID(string id);
        bool Create(WorkModel model);
        bool Update(WorkModel model);
        bool Delete(string id);
        List<WorkModel> Search(int pageIndex, int pageSize, out long total, string workname);
    }
}
