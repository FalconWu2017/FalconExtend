using System;

namespace Falcon.ModelSP
{
    /// <summary>
    /// 存储过程没有找到的异常
    /// </summary>
    public class ProcuderNotFoundException:Exception
    {
        public ProcuderNotFoundException() : base("存储过程没有找到") { }
    }
}
