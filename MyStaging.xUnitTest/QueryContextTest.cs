using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MyStaging.Common;
using MyStaging.Helpers;
using MyStaging.xUnitTest.DAL;
using MyStaging.xUnitTest.Model;
using MyStaging.xUnitTest.Model.Schemas;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace MyStaging.xUnitTest
{
    public class QueryContextTest
    {
        private readonly ITestOutputHelper output;
        public QueryContextTest(ITestOutputHelper output)
        {
            this.output = output;
            LoggerFactory factory = new LoggerFactory();
            var log = factory.CreateLogger<PgSqlHelper>();
            var options = new StagingOptions()
            {
                ConnectionMaster = ConstantUtil.CONNECTIONSTRING,
                ConnectionSlaves = new string[] { ConstantUtil.CONNECTIONSTRING, ConstantUtil.CONNECTIONSTRING },
                Logger = log
            };
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            Console.OutputEncoding = Encoding.UTF8;
            _startup.Init(options);
        }

        private string Sha256Hash(string text)
        {
            return Convert.ToBase64String(SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(text)));
        }

        [Fact]
        public void MultiTest()
        {
            //PgSqlHelper.Transaction(() =>
            //{
            //    var user = User.Context.OrderBy("random()").ToOne();
            //    user.UpdateBuilder.SetCreatetime(DateTime.Now).SaveChange();
            //    //int i = 0;
            //    //int j = 50 / i;
            //});

            //List<TopicModel> topics = new List<TopicModel>();
            //for (int i = 0; i < 10; i++)
            //{
            //    topics.Add(new TopicModel()
            //    {
            //        Title = $"第 {i} 个帖子"
            //    });
            //}

            //Topic.InsertRange(topics);

            //List<PostModel> posts = new List<PostModel>();
            //for (int i = 0; i < 10; i++)
            //{
            //    posts.Add(new PostModel()
            //    {
            //        Title = $"第 {i} 个回复",
            //        Content = JToken.FromObject(new { type = 1, message = "success", create_time = DateTime.Now }),
            //        Role = new Et_role[] { Et_role.普通成员, Et_role.管理员 },
            //        State = Et_data_state.正常,
            //        Text = JToken.FromObject(new { type = 1, message = "success", create_time = DateTime.Now }),
            //    });
            //}

            //Post.InsertRange(posts);
            var content = JToken.FromObject(new { type = 2, message = "fault", create_time = DateTime.Now });
            var id = Guid.Parse("db8df745-0f31-4ddf-8399-80ba47299853");
            var post = Post.Context.Where(f => f.Id == id).ToOne();
            //post.UpdateBuilder.SetRoleAppend(Et_role.群主).SaveChange();
            post.UpdateBuilder.SetState(Et_data_state.删除).SetContent(content).SaveChange();
            return;

            List<Task> tasks = new List<Task>();
            for (int i = 0; i < 1000; i++)
            {
                var task = Task.Run(() =>
                {
                    try
                    {
                        var user = User.Context.OrderBy("random()").ToOne();
                        user.UpdateBuilder.SetCreatetime(DateTime.Now).SaveChange();
                    }
                    catch (Exception ex)
                    {
                        output.WriteLine("{0}/{1}", ex.Message, ex.StackTrace);
                    }
                });
                tasks.Add(task);
            }
            Task.WaitAll(tasks.ToArray());
        }

        static int num = 0;
        [Fact]
        public void InsertTest()
        {
            for (int i = 0; i < 10000; i++)
            {
                Thread thr = new Thread(new ThreadStart(() =>
                {
                    for (int j = 0; j < 10; j++)
                    {
                        UserModel user = new UserModel()
                        {
                            Age = 18,
                            Createtime = DateTime.Now,
                            Id = ObjectId.NewId().ToString(),
                            Loginname = Guid.NewGuid().ToString("N").Substring(0, 8),
                            Money = 0,
                            Nickname = "北极熊",
                            Password = Sha256Hash("123456"),
                            Sex = true
                        };
                        var result = User.Insert(user);
                        Assert.Equal(user.Id, result.Id);
                    }
                    num++;
                }))
                {
                    IsBackground = true
                };
                thr.Start();
            }
            while (num < 10)
            {
                Thread.Sleep(1000);
            }
        }

        private void TestException()
        {
            User user = null;
            bool iscacheing = user.Cacheing;
        }

        [Fact]
        public void Insert()
        {
            UserModel user = new UserModel()
            {
                Age = 18,
                Createtime = DateTime.Now,
                Id = ObjectId.NewId().ToString(),
                Loginname = Guid.NewGuid().ToString("N").Substring(0, 8),
                Money = 0,
                Nickname = "北极熊",
                Password = Sha256Hash("123456"),
                Sex = true
            };
            var result = User.Insert(user);
            Assert.Equal(user.Id, result.Id);
        }

        [Fact]
        public void ToList()
        {
            var list2 = User.Context.InnerJoin<ArticleModel>("b", (a, b) => a.Id == b.Userid).OrderByDescing(f => f.Createtime).Page(1, 10).ToList<UserViewModel>("a.id,a.nickname,a.password");

            List<Task> tasks = new List<Task>();
            for (int i = 0; i < 50; i++)
            {
                var t = Task.Run(() =>
                  {
                      Stopwatch sw = new Stopwatch();
                      sw.Start();
                      var result = User.Context.OrderByDescing(f => f.Createtime).Page(i, 5).ToList();
                      sw.Stop();

                      this.output.WriteLine("Index:{0},Milli:{1},Count:{2}", i, sw.ElapsedMilliseconds, result.Count);
                  });
                tasks.Add(t);
            }

            Task.WaitAll(tasks.ToArray());
            var pool = PgSqlHelper.InstanceSlave.Pool;

            foreach (var item in pool.ConnectionList)
            {
                output.WriteLine("{0}:{1}", item.DbConnection.DataSource, item.Used);
            }

            Assert.Equal(1, 1);
        }

        public class UserViewModel
        {
            public string Id { get; set; }
            public string NickName { get; set; }
            public string Password { get; set; }
        }

        [Fact]
        public void ToListValueType()
        {
            var list = User.Context.OrderByDescing(f => f.Createtime).Page(1, 10).ToList<string>("id");

            Assert.Equal(10, list.Count);
        }

        [Fact]
        public void ToListValueTulpe()
        {
            var list = User.Context.OrderByDescing(f => f.Createtime).Page(1, 10).ToList<(string id, string loginname, string nickname)>("id", "loginname", "nickname");

            Assert.Equal(10, list.Count);
        }

        [Fact]
        public void ToOne()
        {
            string hash = Sha256Hash("123456");
            var user = User.Context.OrderBy(f => f.Createtime).ToOne();

            Assert.Equal(hash, user.Password);
        }

        [Fact]
        public void ToScalar()
        {
            string hash = Sha256Hash("123456");
            var password = User.Context.Where(f => f.Password == hash).OrderBy(f => f.Createtime).ToScalar<string>("password");

            Assert.Equal(hash, password);
        }

        [Fact]
        public void Sum()
        {
            int total = 360;
            // 先把数据库任意两条记录修改为 180 
            var age = User.Context.Where(f => f.Age == 180).Sum<long>(f => f.Age);

            Assert.Equal(total, age);
        }

        [Fact]
        public void Avg()
        {
            decimal avg = 180;
            // 先把数据库任意两条记录的 age 字段修改为 180 
            var age = User.Context.Where(f => f.Age == 180).Avg<decimal>(f => f.Age);

            Assert.Equal(avg, age);
        }

        [Fact]
        public void Count()
        {
            int count = 2;
            // 先把数据库任意两条记录的 age 字段修改为 180 
            var age = User.Context.Where(f => f.Age == 180).Count();

            Assert.Equal(count, age);
        }

        [Fact]
        public void Max()
        {
            int max = 180;
            var age = User.Context.Max<int>(f => f.Age);

            /// 上面插入数据库的 age 字段是 180
            Assert.Equal(max, age);
        }

        [Fact]
        public void Min()
        {
            int min = 18;
            var age = User.Context.Min<int>(f => f.Age);

            /// 上面插入数据库的 age 字段是 18
            Assert.Equal(min, age);
        }
    }
}


