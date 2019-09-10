using System;

namespace Falcon.DynamicSP
{
    /// <summary>
    /// 无法获取存储过程返回结果架构异常
    /// </summary>
    [Serializable]
    public class CanNotGetSchemaException:Exception
    {
        public CanNotGetSchemaException() : base("无法获取存储过程返回结果架构!") { }
    }
}
