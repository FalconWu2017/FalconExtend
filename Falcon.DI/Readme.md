**使用说明**

1. 安装：可以通过下载源码安装，也可以通过nuget包安装。   
1. 引入名字空间    
~~~   
using Microsoft.Extensions.DependencyInjection;
using Falcon.DI
~~~   
1. 初始化容器：使用UseFalconDI方法注册所有服务。
~~~   
IServiceCollection ser = new ServiceCollection();
ser.UseFalconDI();
~~~
1. 添加注册特性，服务可以注册到基础接口或自身。
~~~
public interface IMyInterface
{
	string Getname();
}

[FalconDIRegister]   
public class MyClassInterfaces:IMyInterface   
{   
	public string Getname() {    
		return this.GetType().Name;  
	}   
}    
~~~   
1. 注入可以使用ServiceCollection获取注册的服务。  
	~~~   
	using(var pd = ser.BuildServiceProvider()) {   
        var service = pd.GetServices<IMyInterface>();   
		// Do something   
	}

	~~~   
	或者使用构造注入   
	~~~   
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
	~~~  
以上例子中首先注入了NotFull类型实现INfi1服务，然后又注册了NotFullObj类型，并通过构造注入方式消费了INfi1服务。INfi2因为没有注册所以使用默认值跳过注入。

