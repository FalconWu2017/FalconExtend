using System;

namespace Falcon.ModelSP
{
    /// <summary>
    /// 定义存储过程名称
    /// </summary>
    public class FalconSPProcuderNameAttribute:Attribute
    {
        public string ProcuderName { get; set; }

        public FalconSPProcuderNameAttribute(string m) => this.ProcuderName = m;
    }
}
