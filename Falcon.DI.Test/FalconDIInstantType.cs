using System;

namespace Falcon.DI.Test
{
    public interface IFDIIT
    {
        int Val { get; set; }
    }

    [FalconDIRegister]
    public class FalconDIInstantType:IFDIIT
    {
        public int Val { get; set; } = 1;
    }


    public interface IFalconDIInstantTypeFactory:IFalconDIInstantFactory<IFDIIT> { }

    [FalconDIRegister]
    public class FalconDIInstantTypeFactory:IFalconDIInstantTypeFactory, IFalconDIInstantFactory<IFDIIT>
    {
        public IServiceProvider Provider { get; set; }

        public FalconDIInstantTypeFactory(IServiceProvider sp = null) {
            this.Provider = sp;
        }

        public IFDIIT Instance() {
            return this.Provider == null ? new FalconDIInstantType { Val = 3 } :
                new FalconDIInstantType { Val = 2 };
        }
    }
}
