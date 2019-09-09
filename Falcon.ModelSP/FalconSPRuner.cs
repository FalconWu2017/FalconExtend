using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Falcon.ModelSP
{
    /// <summary>
    /// 存储过程执行器实现
    /// </summary>
    public class FalconSPRuner:IFalconSPRuner
    {
        public int RunSP<TPrarmType>(DbContext db,TPrarmType data) {
            return db.RunProcuder(data);
        }

        public IEnumerable<TResultType> RunSP<TPrarmType, TResultType>(DbContext db,TPrarmType data)
            where TResultType : class, new() {
            return db.RunProcuder<TPrarmType,TResultType>(data);
        }
    }
}
