using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Tweetinvi;
using Tweetinvi.Models;
using Tweetinvi.Parameters;
using TweetMap.Models;
using Tweet = Tweetinvi.Tweet;

namespace TweetMap.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {

            return View();
        }


        public string GetTweetsInLocationRadius(double lat,double lng, int rad)
        {
            Auth.SetUserCredentials("QLX3za3r0cdo4b11D3uoD9uqZ",
                "WE8fLGr2oRkTJjpwJmwoN9xVZmXGPXYAS23NSdA1qP7jFVDE1m",
                "1159076078369091584-T2hT14Q4NIZZUbMH2UVWyNPikwdNmS",
                "T7gb0JAKj3yErHbSW9CCO8eLLdm0l6Ki4IwiF94DoFYRm");

            var searchParameter = new SearchTweetsParameters("tweetinvi")
            {
                GeoCode = new GeoCode(lat, lng, 25, DistanceMeasure.Miles),

            };

            var matchingTweets = Search.SearchTweets("This is a test tweet");
            var tweets = Search.SearchTweets(searchParameter);

            foreach (Tweetinvi.Logic.Tweet currentTweet in matchingTweets)
            {
                if (currentTweet.Place != null)
                {
                    Console.WriteLine("");
                }
            }


            return tweets.ToJson();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
