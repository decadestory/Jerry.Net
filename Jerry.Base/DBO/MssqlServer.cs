using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace Jerry.Base.DBO
{
    public class MssqlServer
    {

        public static readonly Database Db;
        public static Database Dbr;
        private static readonly int CmdTimeOut;
        private static DateTime? CheckIntval;

        static MssqlServer()
        {
            CmdTimeOut = 180;
            Db = new DatabaseProviderFactory().Create("connStr");
            Dbr = new DatabaseProviderFactory().Create("connStr");

        }

        private static void CheckReadDB()
        {
            try
            {
                if (CheckIntval.HasValue && (DateTime.Now - CheckIntval.Value).TotalMinutes < 30)
                    Dbr = new DatabaseProviderFactory().Create("connStr");
                else
                {
                    Dbr = new DatabaseProviderFactory().Create("connStr_R");
                    Dbr.ExecuteNonQuery(CommandType.Text, "select count(1) from sysobjects");
                    CheckIntval = null;
                }
            }
            catch (Exception)
            {
                CheckIntval = DateTime.Now;
                Dbr = new DatabaseProviderFactory().Create("connStr");
            }
        }

        public static DataTable ExeDt(string sql)
        {
            CheckReadDB();
            var cmd = Dbr.GetSqlStringCommand(sql);
            cmd.CommandTimeout = CmdTimeOut;
            var dt = Dbr.ExecuteDataSet(cmd);
            return dt == null ? null : dt.Tables[0];
        }

        public static DataTable ExeDt(string sql, params Param[] parms)
        {
            CheckReadDB();
            var cmd = Dbr.GetSqlStringCommand(sql);

            foreach (Param p in parms)
            {
                Dbr.AddInParameter(cmd, p.Name, p.DbType, p.Value);
            }

            cmd.CommandTimeout = CmdTimeOut;
            var dt = Dbr.ExecuteDataSet(cmd);
            return dt == null ? null : dt.Tables[0];
        }

        public static int ExeSql(string sql, params Param[] parms)
        {
            var cmd = Db.GetSqlStringCommand(sql);

            foreach (Param p in parms)
            {
                Db.AddInParameter(cmd, p.Name, p.DbType, p.Value);
            }

            cmd.CommandTimeout = CmdTimeOut;
            return Db.ExecuteNonQuery(cmd);
        }

        //public static DataTable ExeDt(string sql)
        //{
        //    var dt = Db.ExecuteDataSet(CommandType.Text, sql);
        //    return dt == null ? null : dt.Tables[0];
        //}

        public static int ExeSql(string sql)
        {
            return Db.ExecuteNonQuery(CommandType.Text, sql);
        }

        public static T ExeFir<T>(string sql)
        {
            CheckReadDB();
            return (T)Dbr.ExecuteScalar(CommandType.Text, sql);
        }

        public static object ExeFir(string sql)
        {
            CheckReadDB();
            return Dbr.ExecuteScalar(CommandType.Text, sql);
        }

        public static bool ExeExist(string sql)
        {
            CheckReadDB();
            var result = (int)Dbr.ExecuteScalar(CommandType.Text, sql);
            return result > 0;
        }

        public static int ExeCount(string sql)
        {
            CheckReadDB();
            return (int)Dbr.ExecuteScalar(CommandType.Text, sql);
        }
        public static int ExeCount(string sql, params Param[] parms)
        {
            CheckReadDB();
            var cmd = Dbr.GetSqlStringCommand(sql);

            foreach (Param p in parms)
            {
                Dbr.AddInParameter(cmd, p.Name, p.DbType, p.Value);
            }

            cmd.CommandTimeout = CmdTimeOut;
            return (int)Dbr.ExecuteScalar(cmd);
        }

        //下面执行存储过程
        public static DataTable ExeDt(string procName, object[] parms)
        {
            var dt = Db.ExecuteDataSet(procName, parms);
            return dt == null ? null : dt.Tables[0];
        }

        public static DataTable ExeDt(string procName, Param[] parms, string retstr, out int cnt)
        {
            var cmd = Db.GetStoredProcCommand(procName);
            cmd.CommandTimeout = CmdTimeOut;
            foreach (var p in parms)
                Db.AddInParameter(cmd, p.Name, p.DbType, p.Value);
            Db.AddOutParameter(cmd, retstr, DbType.Int32, 0);
            var dt = Db.ExecuteDataSet(cmd);
            cnt = (int)Db.GetParameterValue(cmd, retstr);
            return dt == null ? null : dt.Tables[0];
        }

        public static int ExeSql(string procName, params string[] parms)
        {
            return Db.ExecuteNonQuery(procName, parms);
        }

        public static bool ExeExist(string procName, object[] parms)
        {
            var result = (int)Db.ExecuteScalar(procName, parms);
            return result != 0;
        }



        public static int ExeCount(string procName, object[] parms)
        {
            return (int)Db.ExecuteScalar(procName, parms);
        }

        //下面执行带事务的SQL
        public static int ExeTran(string[] sqls)
        {
            var cnt = 0;
            using (var conn = Db.CreateConnection())
            {
                conn.Open();
                var tran = conn.BeginTransaction();
                try
                {
                    foreach (var sql in sqls)
                    {
                        var dbc = Db.GetSqlStringCommand(sql);
                        cnt += Db.ExecuteNonQuery(dbc, tran);
                    }
                    tran.Commit();
                    return cnt;

                }
                catch (Exception)
                {
                    tran.Rollback();
                    return 0;
                }
            }
        }

        public static bool ExeTran(List<SqlItem> lst)
        {
            using (var conn = Db.CreateConnection())
            {
                conn.Open();
                var tran = conn.BeginTransaction();
                try
                {
                    foreach (SqlItem item in lst)
                    {
                        if (item.cmdType == CommandType.Text)
                        {
                            var cmd = Db.GetSqlStringCommand(item.Sql);

                            if (null != item.pArray && item.pArray.Count() > 0)
                            {
                                foreach (Param p in item.pArray)
                                {
                                    Db.AddInParameter(cmd, p.Name, p.DbType, p.Value);
                                }
                            }

                            Db.ExecuteNonQuery(cmd, tran);
                        }
                        else if (item.cmdType == CommandType.StoredProcedure)
                        {
                            var cmd = Db.GetStoredProcCommand(item.Sql);

                            if (null != item.pArray && item.pArray.Count() > 0)
                            {
                                foreach (Param p in item.pArray)
                                {
                                    Db.AddInParameter(cmd, p.Name, p.DbType, p.Value);
                                }
                            }
                            //Db.AddOutParameter(cmd, "@ReturnParam", System.Data.DbType.String, 50);    //输出

                            //关键在这里，添加一个参数@RETURN_VALUE 类型为ReturnValue  
                            Db.AddParameter(cmd, "@RETURN_VALUE", DbType.String, ParameterDirection.ReturnValue, "", DataRowVersion.Current, null);

                            //执行存储过程
                            Db.ExecuteNonQuery(cmd, tran);
                            string result = Db.GetParameterValue(cmd, "@RETURN_VALUE").ToString();    //得到输出参数的值

                            //金额操作存储过程 1成功 -1不成功
                            if (result == "-1")
                            {
                                tran.Rollback();
                                return false;
                            }

                        }
                    }
                    tran.Commit();
                    return true;
                }
                catch (Exception)
                {
                    tran.Rollback();
                    return false;
                }
            }
        }


        #region  内部类
        public class Param
        {
            public string Name { get; set; }
            public DbType DbType { get; set; }
            public object Value { get; set; }

        }

        public class SqlItem
        {
            public string Sql { get; set; }
            public CommandType cmdType { get; set; }
            public MssqlServer.Param[] pArray { get; set; }
        }

        #endregion
    }

     public class Ms
    {
        public string ConnStr;
        SqlConnection _conn;

        private static MssqlServer _mInstance = null;
        private Ms()
        {
            ConnStr = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString.ToString();
        }
        public static MssqlServer GetInstance()
        {
            return _mInstance ?? (_mInstance = new MssqlServer());
        }

        /// <summary>
        /// 执行无返回值的SQL语句
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <returns></returns>
        public void ExeSql(string sql)
        {
            /*Using:当创建 SqlCommand 的实例时，读/写属性将被设置为它们的初始值。
             您可以重置 CommandText 属性并重复使用 SqlCommand 对象。但是，
            * 在执行新的命令或先前命令之前，必须关闭 SqlDataReader。如果执行 
            * SqlCommand 的方法生成 SqlException，那么当严重级别小于等于 19 时，
            * SqlConnection 将仍保持打开状态。当严重级别大于等于 20 时，
            * 服务器通常会关闭 SqlConnection。但是，用户可以重新打开连接并继续*/
            using (_conn = new SqlConnection(ConnStr))
            {
                _conn.Open();
                using (var cmd = new SqlCommand(sql, _conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// 执行查询语句，返回SqlDataReader ( 注意：调用该方法后，一定要对SqlDataReader进行Close )
        /// </summary>
        /// <param name="sql">查询语句</param>
        /// <returns>SqlDataReader</returns>
        public SqlDataReader ExeReader(string sql)
        {
            using (_conn = new SqlConnection(ConnStr))
            {
                _conn.Open();
                using (var cmd = new SqlCommand(sql, _conn))
                {
                    var myReader = cmd.ExecuteReader();
                    return myReader;
                }
            }
        }

        /// <summary>
        /// 执行sql查询语句，返回记录条数
        /// </summary>
        /// <param name="sql">带有count(*)的sql语句</param>
        /// <returns>记录条数</returns>
        public int ExeScr(string sql)
        {
            using (_conn = new SqlConnection(ConnStr))
            {
                _conn.Open();
                using (var cmd = new SqlCommand(sql, _conn))
                {
                    var scr = (int)cmd.ExecuteScalar();
                    return scr;
                }
            }
        }

        /// <summary>
        /// 执行查询语句，返回DataSet
        /// </summary>
        /// <param name="sql">查询语句</param>
        /// <returns>DataSet</returns>
        public DataSet ExeDS(string sql)
        {
            using (_conn = new SqlConnection(ConnStr))
            {
                _conn.Open();
                var ds = new DataSet();
                try
                {
                    var command = new SqlDataAdapter(sql, _conn);
                    command.Fill(ds, "ds");
                }
                catch (SqlException ex)
                {
                    throw new Exception(ex.Message);
                }
                return ds;
            }
        }

        /// <summary>
        /// 执行查询语句，返回DataTable
        /// </summary>
        /// <param name="sql">查询语句</param>
        /// <returns>DataTable</returns>
        public DataTable ExeDt(string sql)
        {
            using (_conn = new SqlConnection(ConnStr))
            {
                _conn.Open();
                var dt = new DataTable();
                try
                {
                    var command = new SqlDataAdapter(sql, _conn);
                    command.Fill(dt);
                }
                catch (SqlException ex)
                {
                    throw new Exception(ex.Message);
                }
                return dt;
            }
        }

        /// <summary>
        /// 存储过程查询
        /// </summary>
        /// <param name="procName">存储过程名</param>
        /// <returns>DataTable</returns>
        public DataTable ExeProcWithNonParam(string procName)
        {
            using (_conn = new SqlConnection(ConnStr))
            {
                _conn.Open();
                var cmd = new SqlCommand(procName, _conn) { CommandType = CommandType.StoredProcedure };
                var ds = new DataSet();
                var da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                return ds.Tables[0];
            }
        }

        /// <summary>
        /// 带参数的存储过程执行
        /// </summary>
        /// <param name="procName">存储过程名</param>
        /// <param name="param">参数列表</param>
        /// <returns>影响行数</returns>
        public int ExeProcWithParams(string procName, Dictionary<string, string> param)
        {
            using (_conn = new SqlConnection(ConnStr))
            {
                _conn.Open();
                var cmd = new SqlCommand(procName, _conn) { CommandType = CommandType.StoredProcedure };
                foreach (var item in param)
                {
                    cmd.Parameters.Add(new SqlParameter("@" + item.Key, item.Value));
                }

                return cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// 带参数的存储过程执行,并返回数据
        /// </summary>
        /// <param name="procName">存储过程名</param>
        /// <param name="param">参数列表</param>
        /// <returns>查询数据</returns>
        public DataTable ExeProcWithParamsAndReturns(string procName, Dictionary<string, string> param)
        {
            using (_conn = new SqlConnection(ConnStr))
            {
                _conn.Open();
                var cmd = new SqlCommand(procName, _conn) { CommandType = CommandType.StoredProcedure };
                foreach (var item in param)
                {
                    cmd.Parameters.Add(new SqlParameter("@" + item.Key, item.Value));
                }

                var ds = new DataSet();
                var da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                return ds.Tables[0];
            }
        }

    }
}
