using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TweetMap.Models;

namespace TweetMap.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {

            return View();
        }

        /// <summary>
        /// Get Tweets in location and radius from DB
        /// </summary>
        /// <returns></returns>
        public string GetTweetsInLocationRadius(double lat, double lng, double rad)
        {
            #region Old code for data from api
            //// Set Credentials
            //Auth.SetUserCredentials("QLX3za3r0cdo4b11D3uoD9uqZ",
            //    "WE8fLGr2oRkTJjpwJmwoN9xVZmXGPXYAS23NSdA1qP7jFVDE1m",
            //    "1159076078369091584-T2hT14Q4NIZZUbMH2UVWyNPikwdNmS",
            //    "T7gb0JAKj3yErHbSW9CCO8eLLdm0l6Ki4IwiF94DoFYRm");

            //// Set Paramter
            //double distancerequested = rad / 1000;
            //var searchParameter = new SearchTweetsParameters(new GeoCode(lat, lng, distancerequested, DistanceMeasure.Kilometers));

            //var tweets = Search.SearchTweets(searchParameter);
            #endregion

            // Search the tweets according to input data
            var tweets = DBManager.SearchTweets(new CoordinatesModel(lat, lng), rad / 1000);

            try
            {
                return JsonConvert.SerializeObject(tweets);
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.InnerException);
                return null;
            }

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
