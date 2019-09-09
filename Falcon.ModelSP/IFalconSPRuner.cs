using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Falcon.ModelSP
{
    /// <summary>
    /// 执行存储过程接口
    /// </summary>
    public interface IFalconSPRuner
    {
        /// <summary>
        /// 通过数据库上下文执行无返回值的存储过程
        /// </summary>
        /// <typeparam name="TPrarmType">参数类型</typeparam>
        /// <param name="db">数据上下文</param>
        /// <param name="data">参数数据</param>
        int RunSP<TPrarmType>(DbContext db,TPrarmType data);

        /// <summary>
        /// 通过数据库上下文执行存储过程，并返回查询结果
        /// </summary>
        /// <typeparam name="TPrarmType">参数类型</typeparam>
        /// <typeparam name="TResultType">返回结果项类型</typeparam>
        /// <param name="db">数据上下文</param>
        /// <param name="data">参数数据</param>
        IEnumerable<TResultType> RunSP<TPrarmType, TResultType>(DbContext db,TPrarmType data) where TResultType : class, new();
    }
}
