namespace Falcon.DI.Test
{
    public interface INfi1 { }
    public interface INfi2 { }

    [FalconDIRegister]
    public class NotFull:INfi1
    {
    }

    [FalconDIRegister(typeof(NotFullObj))]
    public class NotFullObj
    {
        public INfi1 F1 { get; set; }
        public INfi2 F2 { get; set; }

        public NotFullObj(INfi1 f1,INfi2 f2=null) {
            this.F1 = f1;
            this.F2 = f2;
        }
    }
}
