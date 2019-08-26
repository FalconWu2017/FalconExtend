using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;


namespace Falcon.ModelSP
{
    /// <summary>
    /// 数据库上下文方法扩展
    /// </summary>
    public static class DataExtend
    {
        /// <summary>
        /// 执行无返回值的存储过程
        /// </summary>
        /// <typeparam name="TPrarmType">参数类型</typeparam>
        /// <param name="db">数据上下文</param>
        /// <param name="data">参数数据</param>
        public static int RunProcuder<TPrarmType>(this DbContext db,TPrarmType data) {
            var parms = getParams(data);
            return db.Database.ExecuteSqlCommand(getProcuderName<TPrarmType>(),parms.ToArray());
        }
        /// <summary>
        /// 执行有返回值的存储过程
        /// </summary>
        /// <typeparam name="TPrarmType">参数类型</typeparam>
        /// <typeparam name="TResultType">返回结果项类型</typeparam>
        /// <param name="db">数据上下文</param>
        /// <param name="data">参数数据</param>
        public static IEnumerable<TResultType> RunProcuder<TPrarmType, TResultType>(this DbContext db,TPrarmType data)
            where TResultType : class, new() {
            var parms = getParams(data);
            return db.Database.SqlQuery<TResultType>(getProcuderName<TPrarmType>(),parms.ToArray()).ToList();
        }

        /// <summary>
        /// 获取存储过程参数枚举
        /// </summary>
        /// <typeparam name="T">参数模型类型</typeparam>
        /// <param name="data">参数实例</param>
        public static IEnumerable<SqlParameter> getParams<T>(T data) {
            if(data == null)
                yield break;
            foreach(var p in typeof(T).GetProperties()) {
                if(!p.CanRead || ignoreProp(p))
                    continue;
                yield return new SqlParameter($"@{getName(p)}",p.GetValue(data));
            }
        }

        /// <summary>
        /// 是否忽略属性
        /// </summary>
        /// <param name="p">要检查的属性</param>
        private static bool ignoreProp(PropertyInfo p) {
            return p.GetCustomAttribute<FalconSPIgnoreAttribute>(true) != null;
        }

        /// <summary>
        /// 获取存储过程参数名称
        /// </summary>
        /// <param name="p">对应的属性</param>
        private static string getName(PropertyInfo p) {
            var np = p.GetCustomAttribute<FalconSPPrarmNameAttribute>(true);
            if(np != null && np is FalconSPPrarmNameAttribute na) {
                return na.Name;
            }
            return p.Name;
        }
        /// <summary>
        /// 获取存储过程名
        /// </summary>
        /// <typeparam name="T">参数模型</typeparam>
        public static string getProcuderName<T>() {
            var attr = typeof(T).GetCustomAttribute<FalconSPProcuderNameAttribute>(true);
            if(attr != null && attr is FalconSPProcuderNameAttribute pna && !string.IsNullOrEmpty(pna.ProcuderName)) {
                return pna.ProcuderName;
            }
            return typeof(T).Name;
        }

        /// <summary>
        /// 根据传入的sql语句和参数枚举执行存储过程，并且返回类型枚举
        /// </summary>
        /// <typeparam name="TR">返回值类型</typeparam>
        /// <param name="db">数据库上下文</param>
        /// <param name="sql">要执行的sql语句</param>
        /// <param name="para">参数枚举</param>
        /// <returns></returns>
        public static IEnumerable<TR> SqlQuery<TR>(this DatabaseFacade db,string sql,params SqlParameter[] paras)
            where TR : class, new() {
            var connection = db.GetDbConnection();
            using(var cmd = connection.CreateCommand()) {
                cmd.CommandText = sql;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddRange(paras);
                connection.Open();
                var dr = cmd.ExecuteReader();
                var result = new List<TR>();
                if(!dr.CanGetColumnSchema())
                    return result;
                while(dr.Read()) {
                    var item = new TR();
                    var columnSchema = dr.GetColumnSchema();
                    for(var i = 0;i < columnSchema.Count;i++) {
                        var name = dr.GetName(i);
                        var value = dr.IsDBNull(i) ? null : dr.GetValue(i);
                        var pi = typeof(TR).GetProperty(name);
                        if(pi == null || !pi.CanWrite)
                            continue;
                        pi.SetValue(item,value);
                    }
                    result.Add(item);
                }
                connection.Close();
                return result;
            }
        }

    }
}
