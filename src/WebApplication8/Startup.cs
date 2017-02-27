using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;

namespace WebApplication8
{
    public class Startup
    {
        ConnectionMultiplexer redis;
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            redis = ConnectionMultiplexer.Connect("192.168.10.25:6379");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            IDatabase db = redis.GetDatabase(2);
            db.StringSet("mykey", 0);

            app.Run(async (context) =>
            {

                var key = "mykey";
                IDatabase db1 = redis.GetDatabase(2);

                var val = db1.StringGet(key);

                await db1.StringIncrementAsync(key);



                //string value = db.StringGet(key);
                //await context.Response.WriteAsync("Hello World!");

            });
        }
    }
}
