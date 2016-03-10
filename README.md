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
            /// 框架应用入口    引用 using Lanche.Web 命名空间
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



## UnitOfWork  数据库连接及分布式事务管理
本MarkDown中的代码注入方式基本以构造函数注入为主，但是Castle windsor 也提供属性注入，二者选一即可</br>

默认事务是关闭的，以提高数据库访问效率
```c#
        public class TestApplicationBiz : ApplicationBizBase
    {
        private readonly IEfRepository<Students> _studentRepository;
        private readonly IUnitOfWorkManager _uowManger;
        private readonly ILogger _logger;
        
        public TestApplicationBiz(IEfRepository<Students> studentRepository, IUnitOfWorkManager uowManger,ILogger logger)
        {
            _studentRepository = studentRepository;
            _uowManger = uowManger;
            _logger = logger;
        }
        /// 开启事务
         [UnitOfWork(isTransactional: true)]
        public virtual void TransactionMethod(Students s)
        {

            _studentRepository.InsertAsync(s);
            _uowManger.Current.SaveChanges();

            throw new Exception("dc");
        }
         [UnitOfWork(IsDisabled=false)]  //关闭 UnitOfWork
        public virtual Task<Students> GetOneAsync(string name)
        {
            return _studentRepository.SingleAsync(m => m.Name == name);
        }

    }
```
     ```c#
         /// 开启事务   此时整个方法在同一个事务中
         [UnitOfWork(isTransactional: true)]
     ```
     ```c#
          //关闭 UnitOfWork  此时这个方法不再受到 unitofwork的管理
          [UnitOfWork(IsDisabled=false)] 
     ```
###以EntityFramework作为传统sql数据库访问层
首先创建DbContext
```c#
    ///引用 using Lanche.Entityframework.UnitOfWork 命名空间，注意这里DbContext继承自DbContextBase，这里并不约束使用codefirst还是dbfirst,且不约束映射规则的实现方式
     public  class TestDbContext : DbContextBase   
    {
        public TestDbContext()
            : base("name=TestDbContext")
        {
        }

        public virtual DbSet<Students> Students { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

        }
    }
    //实体类
     public partial class Students
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }
    }
```
配置连接字符串
```xml

       <add name="TestDbContext" connectionString="server=.;database=AbpTest;uid=sa;pwd=123456" providerName="System.Data.SqlClient" />
```

###创建业务层
业务层  必须继承自 ApplicationBizBase  以获得作为公开服务的能力</br>
这里可以照常使用 webapi 2中的 IFliters,作为过滤器,所有继承自 ApplicationBizBase 的类 路由，参数绑定，都由 webApi提供，但路由规则和mvc controller 类似，约定<br>
{xxxxxx}ApplicationBiz  或者  {xxxxxx}Biz   最终的url则为   /api/services/{custom}/xxxxxx/action，具体信息请看下文DynamicWebApi介绍<br>
Biz 即 业务的意思
```c#
    ///  using Lanche.DynamicWebApi.Application;
         public class TestApplicationBiz : ApplicationBizBase
    {
       /// 属性注入
        public IEfRepository<Students> StudentRepository{get;set;}
        private readonly IEfRepository<Students> _studentRepository;
        private readonly IUnitOfWorkManager _uowManger;
        ///构造函数注入
        public TestApplicationBiz(IEfRepository<Students> studentRepository, IUnitOfWorkManager uowManger)
        {
            _studentRepository = studentRepository;
            _uowManger = uowManger;
        }
        
        public virtual PagingEntity<Students> GetInPaging(int pageIndex, int PageSize, bool sort, string orderProperty)
        {

            return _studentRepository.GetInPaging(m => m.IsDeleted == false, pageIndex, PageSize, orderProperty, sort);

        }
    }
```
###Repository 提供的方法
查询 
```c#
        /// <summary>
        /// 得到 IQueryable ,以提供linq 查询能力
        /// </summary>
        /// <returns></returns>
        IQueryable<TEntity> GetAll();

        /// <summary>
        /// 返回所有实体List
        /// </summary>
        /// <returns>实体List</returns>
        List<TEntity> GetAllList();

        /// <summary>
        /// 返回所有实体List
        /// </summary>
        /// <returns>实体List</returns>
        Task<List<TEntity>> GetAllListAsync();

 

        /// <summary>
        /// 根据 lambda 返回 实体List
        /// </summary>
        /// <param name="predicate">where 条件</param>
        /// <returns>实体 list</returns>
        List<TEntity> GetAllList(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 根据 lambda 返回 实体List
        /// </summary>
        /// <param name="predicate">where 条件</param>
        /// <returns>实体 list</returns>
        Task<List<TEntity>> GetAllListAsync(Expression<Func<TEntity, bool>> predicate);
        /// <summary>
        /// 返回单个，找到多个 直接报错
        /// </summary>
        /// <param name="predicate">where 条件</param>
        /// <returns></returns>
        TEntity Single(Expression<Func<TEntity, bool>> predicate);
        /// <summary>
        /// 返回单个，找到多个 直接报错
        /// </summary>
        /// <param name="predicate">where 条件</param>
        /// <returns></returns>
        Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate);


        /// <summary>
        /// 得到第一个
        /// </summary>
        /// <param name="predicate">where 条件</param>
        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate);
        /// <summary>
        /// 得到第一个
        /// </summary>
        /// <param name="predicate">where 条件</param>
        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="query">where</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">页size</param>
        /// <param name="orderPropertyName">order Property</param>
        /// <param name="sort"> 正或逆 </param>
        /// <returns>包含所有分页信息的数据传递对象</returns>
        PagingEntity<TEntity> GetInPaging(Expression<Func<TEntity, bool>> query, int pageIndex, int pageSize, string orderPropertyName, bool sort = true);

        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="query">where</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">页size</param>
        /// <param name="orderPropertyName">order Property</param>
        /// <param name="sort"> 正或逆 </param>
        /// <returns>包含所有分页信息的数据传递对象</returns>
        Task<PagingEntity<TEntity>> GetInPagingAsync(Expression<Func<TEntity, bool>> query, int pageIndex, int pageSize, string orderPropertyName, bool sort = true);

        /// <summary>
        /// 提供 sql 语句查询能力
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        DbRawSqlQuery<TResult> SqlQuery<TResult>(string sql, params object[] parameters);

        /// <summary>
        /// 提供SQL语句 sqlCommand
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        int ExecuteSqlCommand(string sql, params object[] parameters);

        /// <summary>
        /// 提供SQL语句 sqlCommand 异步
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        Task<int> ExecuteSqlCommandAsync(string sql, params object[] parameters);
          int Count();

        Task<int> CountAsync();

        int Count(Expression<Func<TEntity, bool>> predicate);

        Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 当个数超过 2^31-1
        /// </summary>
        /// <returns>Count of entities</returns>
        long LongCount();

        /// <summary>
        /// 当个数超过 2^31-1
        /// </summary>
        /// <returns>Count of entities</returns>
        Task<long> LongCountAsync();

        /// <summary>
        /// 当个数超过 2^31-1
        /// </summary>
        /// <returns>Count of entities</returns>
        long LongCount(Expression<Func<TEntity, bool>> predicate);

        Task<long> LongCountAsync(Expression<Func<TEntity, bool>> predicate);

```
新增
```c#
        ///同步 添加单个
        TEntity Insert(TEntity entity);

        ///异步 添加单个
        Task<TEntity> InsertAsync(TEntity entity);

        /// <summary>
        /// 批量添加
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="options"></param>
        /// <param name="bulkSize"></param>
        void BulkInsert(IEnumerable<TEntity> entities, SqlBulkCopyOptions options, int? bulkSize=null);

        /// <summary>
        /// 批量添加
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="bulkSize"></param>

        void BulkInsert(IEnumerable<TEntity> entities, int? bulkSize=null);
```
更新
```c#
        /// <summary>
        /// 更新 单个 同步
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        TEntity Update(TEntity entity);

        /// <summary>
        /// 更新 单个实体 异步
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<TEntity> UpdateAsync(TEntity entity);

         /// <summary>
        /// 批量更新
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="update"></param>
        /// <returns></returns>
        int Update(Expression<Func<TEntity, bool>> filter,Expression<Func<TEntity,TEntity>> update);

        /// <summary>
        /// 批量更新异步
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="update"></param>
        /// <returns></returns>
        Task<int> UpdateAsync(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, TEntity>> update);
```
删除
```c#
        /// <summary>
        /// 删除单个实体
        /// </summary>
        /// <param name="entity"></param>
        void Delete(TEntity entity);
        /// <summary>
        /// 删除单个实体 异步
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>

        Task DeleteAsync(TEntity entity);
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        int Delete(Expression<Func<TEntity, bool>> filter);

        /// <summary>
        /// 批量删除 异步
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        Task<int> DeleteAsync(Expression<Func<TEntity, bool>> filter);
```
###mongodb 
系统中部分数据可能用mongodb储存，LancheProject实现了mongodb存储的简单实现
首先添加MongoDbContext
```c#
    /// using Lanche.MongoDB.DbContext;
         public class FeturnMongoDbContext:MongoDbContext
    {
        public MongoDbSet<Car> Car { get; set; }

        public FeturnMongoDbContext()
            : base("Feture", "Test")
        {

        }
        
    }
    public class Car
    {
        public Guid Id{get;set;}
        public string Name{get;set;}
        
    }
```
配置连接字符串
```xml
        <add name="Feture" connectionString="mongodb://localhost:27017" />  ///未设置账号密码
```
直接在 业务层的使用
```c#
        public class MongoDbTestBiz : ApplicationBizBase
    {
        private readonly IMongoDbRepository<Car> _carRepository;
       ///构造函数注入
        public MongoDbTestBiz(IMongoDbRepository<Car> carRepository)
        {
            this._carRepository = carRepository;
        }
                      
        }
        public Car Get()
        {
           return _carRepository.Single(m => m.Name =="Alto");
        }
    }
```
###mongodb IMongoDbRepository 提供的方法
查询
```c#
        /// <summary>
        /// 得到 IQueryable ,以提供linq 查询能力
        /// </summary>
        /// <returns></returns>
        IQueryable<TEntity> GetAll();

        /// <summary>
        /// 返回所有实体List
        /// </summary>
        /// <returns>实体List</returns>
        List<TEntity> GetAllList();

        /// <summary>
        /// 返回所有实体List
        /// </summary>
        /// <returns>实体List</returns>
        Task<List<TEntity>> GetAllListAsync();

 

        /// <summary>
        /// 根据 lambda 返回 实体List
        /// </summary>
        /// <param name="predicate">where 条件</param>
        /// <returns>实体 list</returns>
        List<TEntity> GetAllList(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 根据 lambda 返回 实体List
        /// </summary>
        /// <param name="predicate">where 条件</param>
        /// <returns>实体 list</returns>
        Task<List<TEntity>> GetAllListAsync(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="queryMethod"></param>
        /// <returns></returns>
        T Query<T>(Func<IQueryable<TEntity>, T> queryMethod);


        /// <summary>
        /// 返回单个，找到多个 直接报错
        /// </summary>
        /// <param name="predicate">where 条件</param>
        /// <returns></returns>
        TEntity Single(Expression<Func<TEntity, bool>> predicate);
        /// <summary>
        /// 返回单个，找到多个 直接报错
        /// </summary>
        /// <param name="predicate">where 条件</param>
        /// <returns></returns>
        Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate);


        /// <summary>
        /// 得到第一个
        /// </summary>
        /// <param name="predicate">where 条件</param>
        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate);
        /// <summary>
        /// 得到第一个
        /// </summary>
        /// <param name="predicate">where 条件</param>
        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="query">where</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">页size</param>
        /// <param name="orderPropertyName">order Property</param>
        /// <param name="sort"> 正或逆 </param>
        /// <returns>包含所有分页信息的数据传递对象</returns>
        PagingEntity<TEntity> GetInPaging(Expression<Func<TEntity, bool>> query, int pageIndex, int pageSize, string orderPropertyName, bool sort = true);

        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="query">where</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">页size</param>
        /// <param name="orderPropertyName">order Property</param>
        /// <param name="sort"> 正或逆 </param>
        /// <returns>包含所有分页信息的数据传递对象</returns>
        Task<PagingEntity<TEntity>> GetInPagingAsync(Expression<Func<TEntity, bool>> query, int pageIndex, int pageSize, string orderPropertyName, bool sort = true);
          int Count();

        Task<int> CountAsync();

        int Count(Expression<Func<TEntity, bool>> predicate);

        Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 当个数超过 2^31-1
        /// </summary>
        /// <returns>Count of entities</returns>
        long LongCount();

        /// <summary>
        /// 当个数超过 2^31-1
        /// </summary>
        /// <returns>Count of entities</returns>
        Task<long> LongCountAsync();

        /// <summary>
        /// 当个数超过 2^31-1
        /// </summary>
        /// <returns>Count of entities</returns>
        long LongCount(Expression<Func<TEntity, bool>> predicate);

        Task<long> LongCountAsync(Expression<Func<TEntity, bool>> predicate);
```
新增
```c#
        
        TEntity Insert(TEntity entity);


        Task<TEntity> InsertAsync(TEntity entity);

        void InsertMany(IEnumerable<TEntity> entities);

        Task InsertManyAsync(IEnumerable<TEntity> entities);
```
更新
```c#
        /// <summary>
        /// 更新 单个 同步
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        TEntity Update(TEntity entity);

        /// <summary>
        /// 更新 单个实体 异步
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<TEntity> UpdateAsync(TEntity entity);

        int Update(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, TEntity>> update);

        Task<int> UpdateAsync(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, TEntity>> update);
```
删除
```c#
        /// <summary>
        /// 删除单个实体
        /// </summary>
        /// <param name="entity"></param>
        void Delete(TEntity entity);
        /// <summary>
        /// 删除单个实体 异步
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>

        Task DeleteAsync(TEntity entity);

        int Delete(Expression<Func<TEntity, bool>> filter);

        Task<int> DeleteAsync(Expression<Func<TEntity, bool>> filter);
```
###Log
log4net 配置 实例
```xml
<?xml version="1.0" encoding="utf-8" ?>
<log4net>
  <appender name="RollingFileAppender" type="log4net.Appender.RollingFileAppender" >
    <file value="Logs/Logs.txt" />
    <appendToFile value="true" />
    <rollingStyle value="Size" />
    <maxSizeRollBackups value="10" />
    <maximumFileSize value="1000KB" />
    <staticLogFileName value="true" />
    <layout type="log4net.Layout.PatternLayout">
      <conversionPattern value="%-5level %date [%-5.5thread] %-40.40logger - %message%newline" />
    </layout>
  </appender>
  <root>
    <appender-ref ref="RollingFileAppender" />
    <level value="DEBUG" />
  </root>
 
</log4net>
```
使用代码
```c#
        public class TestApplicationBiz : ApplicationBizBase
    {
        private readonly IEfRepository<Students> _studentRepository;
        private readonly IUnitOfWorkManager _uowManger;

        private readonly ILogger _logger;
        ///构造函数注入
        public TestApplicationBiz(IEfRepository<Students> studentRepository, IUnitOfWorkManager uowManger,ILogger logger)
        {
            _studentRepository = studentRepository;
            _uowManger = uowManger;
            _logger = logger;
        }
        
        public virtual PagingEntity<Students> GetInPaging(int pageIndex, int PageSize, bool sort, string orderProperty)
        {
            /// debug 级别的 日志
            _logger.Debug("ss");
            return _studentRepository.GetInPaging(m => m.IsDeleted == false, pageIndex, PageSize, orderProperty, sort);

        }
    }
```
### Cache
Lanche.Cache 的存储全部在string的基础上。对象会被序列化成json存储，当获取时又会自动反序列化，强烈建议  string 字符串作为Key，不然可能发生意想不到的结果
```c#
        public class CacheTestBiz : ApplicationBizBase
    {
        private readonly ICacheManager cacheManager;

        public CacheTestBiz(ICacheManager cacheManager)
        {
            this.cacheManager = cacheManager;
            
        }
        public void SetOne()
        {

            var cache = cacheManager.GetOrCreateCache("test1");
            cache.Set("Id", 123456, new TimeSpan(1, 0, 0));
        }
        public int GetOne()
        {
            var cache = cacheManager.GetOrCreateCache("test1");
            return cache.GetOrDefault<string, int>("Id");
        }
        public void SetOneInRedis()
        {
            Car car = new Car() { Id = Guid.NewGuid(), Name = "111" };
            var cache = cacheManager.GetOrCreateCache("test1");

            cache.SetAsync("Id", car);

            //var cache2 = _redisCacheManager.GetOrCreateCache("test2");
            //cache.SetAsync("Id", 123456);
            //var cache3= _redisCacheManager.GetOrCreateCache("test3");
            //cache.SetAsync("Id", 123456);
        }
        public async Task<Car> GetOneInRedis()
        {

            var cache = cacheManager.GetOrCreateCache("test1");
            var value = await cache.GetOrCreateAsync<Car>("Id", new Car() { Id = Guid.NewGuid(), Name = "222" });
            return value;
        }
    }
```
### 业务层动态生成WebApi层
注册
```c#
         // 默认url 为  /api/services开头
         DynamicApiControllerBuilder.ForAll<ApplicationBizBase>(Assembly.GetExecutingAssembly(), "test").Build();
         // 现在url 为  /api/services/test
         //最终  url 为  /api/services/test/{BizName}/{ActionName}
```
示例
```c#
        //因为 此 Biz 名称 为  
        Test  ApplicationBiz之前的字符串将作为Biz地址开放,
        // 所以 此 Biz 地址 localhost://api/services/test/test/     
        public class TestApplicationBiz : ApplicationBizBase
        {
        private readonly IEfRepository<Students> _studentRepository;
        private readonly IUnitOfWorkManager _uowManger;
        private readonly ILogger _logger;

        public TestApplicationBiz(IEfRepository<Students> studentRepository, IUnitOfWorkManager uowManger,ILogger logger)
        {
            _studentRepository = studentRepository;
            _uowManger = uowManger;
            _logger = logger;
        }
        /// url   localhost://api/services/test/test/GetInPaging?........
        public virtual PagingEntity<Students> GetInPaging(int pageIndex, int PageSize, bool sort, string orderProperty)
        {
            _logger.Debug("ss");
            return _studentRepository.GetInPaging(m => m.IsDeleted == false, pageIndex, PageSize, orderProperty, sort);

        }
        // localhost://api/services/test/test/GetInPagingS
        
        public virtual List<Students> GetInPagingS()
        {

            var v = _studentRepository.GetAll().OrderBy(m => m.Age).Where(m => m.IsDeleted == false).Skip(1).Take(1).ToList();
            return v;
        }
    }
```
###MessageQueue
```c#
        public class RabbitMqTestBiz : ApplicationBizBase
    {
        private readonly IMessageQueryManager _manager;
 
        public RabbitMqTestBiz(IMessageQueryManager manager)
        {
            this._manager = manager;
            
        }
        public void Send()
        {
            var channel = _manager.GetChannal("test1");
            channel.Send("test", "hellow");
           //ioc 自动 disopse
          
        }
    }
```




