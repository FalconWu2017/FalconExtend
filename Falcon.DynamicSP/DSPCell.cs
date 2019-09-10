using System;

namespace Falcon.DynamicSP
{
    /// <summary>
    /// 存储过程返回的一个数据
    /// </summary>
    public class DSPCell
    {
        /// <summary>
        /// 列名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 存储过程列数据类型
        /// </summary>
        public Type ColumnType { get; set; }
        /// <summary>
        /// 存储过程返回值
        /// </summary>
        public object Value { get; set; }
    }

}
