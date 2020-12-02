using DAL.Helper;
using Model;
using Helper;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System;

namespace DAL
{
    public partial class VanbandiRepository :IVanbandiRepository
    {
        private IDatabaseHelper _dbHelper;
        public VanbandiRepository(IDatabaseHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }
        public List<LoaiVanBanModel> GetDataAll()
        {
            string msgError = "";
            try
            {
                var dt = _dbHelper.ExecuteSProcedureReturnDataTable(out msgError, "lvb_all");
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                return dt.ConvertTo<LoaiVanBanModel>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<VanbandiModel> GetDataAllByPhongBan(int pageIndex, int pageSize, out long total, string tenpb, string loaivb)
        {
            string msgError = "";
            total = 0;
            try
            {
                var dt = _dbHelper.ExecuteSProcedureReturnDataTable(out msgError, "vanbandi_by_phongban_search",
                    "@page_index", pageIndex,
                    "@page_size", pageSize,
                    "@ten_pb", tenpb,
                    "@tenloaivanban",loaivb);
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                if (dt.Rows.Count > 0) total = (long)dt.Rows[0]["RecordCount"];
                return dt.ConvertTo<VanbandiModel>().ToList();          
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool Create(VanbandiModel model)
        {
            string msgError = "";
            try
            {
                var result = _dbHelper.ExecuteScalarSProcedureWithTransaction(out msgError, "vanbandi_create",
                "@vanbanid", model.vanbanid,
                "@ngaybanhanh", model.ngaybanhanh,
                "@tenloaivanban", model.tenloaivanban,
                "@tenphongban", model.tenphongban,
                "@noidung", model.noidung.Substring(12),
                "@user_id", model.user_id);
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
                var result = _dbHelper.ExecuteScalarSProcedureWithTransaction(out msgError, "vanbandi_delete",
                "@vanbanid", id);
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
        public bool Update(VanbandiModel model)
        {

            string msgError = "";
            try
            {;
          
                var result = _dbHelper.ExecuteScalarSProcedureWithTransaction(out msgError, "vanbandi_update",
                "@vanbanid", model.@vanbanid,
                "@ngaybanhanh", model.ngaybanhanh,
                "@tenloaivanban ", model.tenloaivanban,
                "@tenphongban", model.tenphongban,
                "@noidung", model.noidung,
                "@user_id", model.user_id);
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


        public VanbandiModel GetDatabyID(string id)
        {
            string msgError = "";
            try
            {
                var dt = _dbHelper.ExecuteSProcedureReturnDataTable(out msgError, "vanban_get_by_id",
                     "@vanbanid", id);
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                return dt.ConvertTo<VanbandiModel>().FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<VanbandiModel> Search(int pageIndex, int pageSize, out long total, string tenphongban)
        {
            string msgError = "";
            total = 0;
            try
            {
                var dt = _dbHelper.ExecuteSProcedureReturnDataTable(out msgError, "vanbandi_search",
                    "@page_index", pageIndex,
                    "@page_size", pageSize,
                    "@tenphongban", tenphongban);
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                if (dt.Rows.Count > 0) total = (long)dt.Rows[0]["RecordCount"];
                return dt.ConvertTo<VanbandiModel>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
