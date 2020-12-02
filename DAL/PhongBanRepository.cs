using DAL.Helper;
using Model;
using Helper;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System;

namespace DAL
{
    public partial class PhongBanRepository : IPhongBanRepository
    {
        private IDatabaseHelper _dbHelper;
        public PhongBanRepository(IDatabaseHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }
        public bool Create(PhongBanModel model)
        {
            string msgError = "";
            try
            {
                var result = _dbHelper.ExecuteScalarSProcedureWithTransaction(out msgError, "phongban_create",
                "@phongbanid", model.phongbanid,
                "@tenphongban", model.tenphongban,
                "@ghichu", model.ghichu);
                if ((result != null && !string.IsNullOrEmpty(result.ToString())) || !string.IsNullOrEmpty(msgError))
                {
                    throw new Exception(Convert.ToString(result) + msgError);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Delete(string id)
        {
            string msgError = "";
            try
            {
                var result = _dbHelper.ExecuteScalarSProcedureWithTransaction(out msgError, "phongban_delete",
                "@phongbanid", id);
                if ((result != null && !string.IsNullOrEmpty(result.ToString())) || !string.IsNullOrEmpty(msgError))
                {
                    throw new Exception(Convert.ToString(result) + msgError);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool Update(PhongBanModel model)
        {
            string msgError = "";
            try
            {
                var result = _dbHelper.ExecuteScalarSProcedureWithTransaction(out msgError, "phongban_update",
                  "@phongbanid", model.phongbanid,
                "@tenphongban", model.tenphongban,
                "@ghichu", model.ghichu);
                if ((result != null && !string.IsNullOrEmpty(result.ToString())) || !string.IsNullOrEmpty(msgError))
                {
                    throw new Exception(Convert.ToString(result) + msgError);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PhongBanModel> GetDataAll()
        {
            string msgError = "";
            try
            {
                var dt = _dbHelper.ExecuteSProcedureReturnDataTable(out msgError, "phongban_all");
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                return dt.ConvertTo<PhongBanModel>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public PhongBanModel GetDatabyID(string id)
        {
            string msgError = "";
            try
            {
                var dt = _dbHelper.ExecuteSProcedureReturnDataTable(out msgError, "phongban_get_by_id",
                     "@phongbanid", id);
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                return dt.ConvertTo<PhongBanModel>().FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<PhongBanModel> Search(int pageIndex, int pageSize, out long total, string tenphongban)
        {
            string msgError = "";
            total = 0;
            try
            {
                var dt = _dbHelper.ExecuteSProcedureReturnDataTable(out msgError, "phongban_search",
                    "@page_index", pageIndex,
                    "@page_size", pageSize,
                    "@tenphongban", tenphongban);
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                if (dt.Rows.Count > 0) total = (long)dt.Rows[0]["RecordCount"];
                return dt.ConvertTo<PhongBanModel>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
