namespace Falcon.DI.Test
{
    public interface IMyInterface
    {
        string Getname();
    }

    public interface IMyInterface2
    {
        string Getname();
    }

    [FalconDIRegister(typeof(IMyInterface))]
    public class MyClassInterface:IMyInterface
    {
        public string Getname() {
            return this.GetType().Name;
        }
    }

    [FalconDIRegister]
    public class MyClassInterfaces:IMyInterface, IMyInterface2
    {
        public string Getname() {
            return this.GetType().Name;
        }
    }

    [FalconDIRegister]
    public class MyClassDefault:IMyInterface
    {
        public string Getname() {
            return this.GetType().Name;
        }
    }

    [FalconDIRegister(typeof(MyClassSelf))]
    public class MyClassSelf:IMyInterface
    {
        public string Getname() {
            return this.GetType().Name;
        }
    }
}
