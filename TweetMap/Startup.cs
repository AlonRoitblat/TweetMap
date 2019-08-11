using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tweetinvi;
using TweetMap.Models;


namespace TweetMap
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}");
            });

            #region For Test
            // Add Test Data for debug
            //DBManager.InsertObject(new TweetModel()
            //{
            //    _id = new Random().Next(),
            //    Coordinates = new CoordinatesModel(40.749743519532984, -73.996696472167983),
            //    Text = "This is a Test Tweet",
            //    UserName = "Alon"
            //});

            #endregion

            // here we are dealing with the twitter api stream
            Task.Run(BeginTwitterStream);
        }


        /// <summary>
        /// Authenticate with keys and start listening to twitter sample stream 
        /// </summary>
        public static void BeginTwitterStream()
        {
            // Get Authentication
            Auth.SetUserCredentials("QLX3za3r0cdo4b11D3uoD9uqZ",
                "WE8fLGr2oRkTJjpwJmwoN9xVZmXGPXYAS23NSdA1qP7jFVDE1m",
                "1159076078369091584-T2hT14Q4NIZZUbMH2UVWyNPikwdNmS",
                "T7gb0JAKj3yErHbSW9CCO8eLLdm0l6Ki4IwiF94DoFYRm");

            // Create Stream
            var stream = Stream.CreateSampleStream();

            // sign into event 
            stream.TweetReceived += (sender, args) =>
            {
                if (!(args.Tweet.Coordinates is null))
                {
                    // Add to DB
                    Console.WriteLine("Tweet received with location");
                    var tweetToInsertToDB = new TweetModel()
                    {
                        _id = args.Tweet.Id,
                        Text = args.Tweet.FullText,
                        UserName = args.Tweet.CreatedBy.Name,
                        Coordinates = new CoordinatesModel(args.Tweet.Coordinates.Latitude,
                                                           args.Tweet.Coordinates.Longitude)
                    };
                    DBManager.InsertObject(tweetToInsertToDB);
                }
            };

            // Start steam
            stream.StartStream();
        }

    }
}
