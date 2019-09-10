using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Falcon.DynamicSP
{
    public static class DbContextExtend
    {
        /// <summary>
        /// 对数据上下文调用存储过程，并获取返回数据
        /// </summary>
        /// <param name="db">数据库上下文</param>
        /// <param name="name">存储过程名称</param>
        /// <param name="paras">传入存储过程的参数</param>
        /// <returns>存储返回的数据结果</returns>
        public static DSPTable RunDynamicSP(this DbContext db,string name,params SqlParameter[] paras) {
            return db.Database.RunDynamicSP(name,paras);
        }
    }
}