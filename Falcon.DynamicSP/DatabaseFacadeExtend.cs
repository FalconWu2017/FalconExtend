using System.Data.Common;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Falcon.DynamicSP
{
    public static class DatabaseFacadeExtend
    {
        /// <summary>
        /// 执行动态存储过程。通过传入存储过程名称和参数获取返回结果
        /// </summary>
        /// <param name="dbf">数据库引用</param>
        /// <param name="name">存储过程名称</param>
        /// <param name="paras">传入存储过程的参数</param>
        /// <returns>存储返回的数据结果</returns>
        public static DSPTable RunDynamicSP(this DatabaseFacade dbf,string name,params SqlParameter[] paras) {
            var table = new DSPTable();
            var connection = dbf.GetDbConnection();
            using(var cmd = connection.CreateCommand()) {
                cmd.CommandText = name;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddRange(paras);
                connection.Open();
                var dr = cmd.ExecuteReader();
                if(!dr.CanGetColumnSchema())
                    throw new CanNotGetSchemaException();
                while(dr.Read()) {
                    var row = new DSPRow();
                    var columnSchema = dr.GetColumnSchema();
                    for(var i = 0;i < columnSchema.Count;i++) {
                        var cell = new DSPCell();
                        cell.Name = dr.GetName(i);
                        cell.Value = dr.IsDBNull(i) ? null : dr.GetValue(i);
                        cell.ColumnType = dr.GetFieldType(i);
                        row.Add(cell);
                    }
                    table.Add(row);
                }
                connection.Close();
                return table;
            }

        }
    }

}
