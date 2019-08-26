using System;

namespace Falcon.ModelSP
{
    /// <summary>
    /// 定义名称
    /// </summary>
    public class FalconSPPrarmNameAttribute:Attribute
    {
        public FalconSPPrarmNameAttribute(string name) { this.Name = name; }
        public string Name { get; set; }
    }
}
