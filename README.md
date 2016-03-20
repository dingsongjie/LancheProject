# LancheProject
asp.net web 开发框架  
基于 .net framework 4.5.1 ，owin , asp.net mvc 5 ,asp.net web api 2 ,castle windsor 3 , ef 6 , mongodb , redis, rabbitmq, log4net 帮助开发人员快速开发web后端，减少工作量，以专注业务层的建设。目前后台和前端的交互只支持json ,而且LancheProject 没有 提供前端框架 ,但是 不久就会新开一个项目 提供基架生成curd前端代码，并在不久提供基于LancheProject的权限框架 ，vs下直接Nuget搜索Lanche即可安装。</br>
联系方式 : 腾讯QQ 377973147



## 框架使用
下载框架代码，并引入依赖项，然后在startup中配置

  ```c#
   public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            
            app.UseLancheProject()
                /// log   log4net 作为日志
               .UseLog4Net("log4net.config")
                /// cache 
               .UseMemoryCache()
                ///    这里 use 两个缓存  后者覆盖前者
               .UseRedisCache()
               /// 消息队列
               .UseRabbitMq()
               ///创建一个消息队列的连接  test1 连接字符串在web.config中配置
               .UseMqConnection("test1");
                              
        }
    }
```



在 iis 下运行 必须添加 Microsoft.Owin.Host.SystemWeb   不然 startup会被跳过
##简单示例

一个简单的框架使用[示例](https://github.com/dingsongjie/SimpleWithLanche) 



## 模块化
[文档](https://github.com/dingsongjie/LancheProject.Docs/blob/master/module.md)

## 普通业务层
[文档](https://github.com/dingsongjie/LancheProject.Docs/blob/master/ApplicationBiz.md)

##公开成api的业务层
[文档](https://github.com/dingsongjie/LancheProject.Docs/blob/master/DynamicApi.md)

##统一工作单元
[文档](https://github.com/dingsongjie/LancheProject.Docs/blob/master/UnitOfWork.md)

##Mvc
[文档](https://github.com/dingsongjie/LancheProject.Docs/blob/master/Mvc.md)

## Entityframework
[文档](https://github.com/dingsongjie/LancheProject.Docs/blob/master/Entityframework.md)

## mongoDb
[文档](https://github.com/dingsongjie/LancheProject.Docs/blob/master/mongodb.md)

## 异常消息
[文档](https://github.com/dingsongjie/LancheProject.Docs/blob/master/Exception.md)

## 缓存
[文档](https://github.com/dingsongjie/LancheProject.Docs/blob/master/cache.md)

##消息队列

[文档](https://github.com/dingsongjie/LancheProject.Docs/blob/master/MessageQueue.md)




