# LancheProject
asp.net web 开发框架  
基于 .net framework 4.5.1  
包含  asp.net mvc 5    , asp.net web api 2 , ef 6 , mongodb , redis, rabbitmq
asp.net mvc 5 用来返回 view  , Dynamic web api 省去了 apicontroller 的创建  ,减少工作量 , 与前端 通过 json 交互 , unitofwork 使得 一个请求 中的dbcontext得到重用，并提供分布式事务。
基本组件通过 castle windsor 注入,可通过 属性或构造函数中获取。
