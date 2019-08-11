using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using LiteDB;
using Newtonsoft.Json.Bson;
using Tweetinvi.Models;
using TweetMap.Models;

namespace TweetMap
{
    public static class DBManager
    {

        public static bool InsertObject(Tweet tweetToInsert)
        {
            // Open database (or create if not exits)
            using (var db = new LiteDatabase(@"TweetsDB.db"))
            {
                // Get customer collection
                var tweets = db.GetCollection<Tweet>("tweets");

                // Insert new customer document (Id will be auto-incremented)
                return tweets.Insert(tweetToInsert) != null;
            }
        }

        // TODO ADD SEARCH FUNCTION

        public static IEnumerable<Tweet> SearchTweets(Coordinates coordinates,double radius)
        {
            using (var db = new LiteDatabase(@"TweetsDB.db"))
            {
                // Get customer collection
                var tweets = db.GetCollection<Tweet>("tweets");

                //return tweets.Find(x => isLocInRad(coordinates, radius, x));
                var Result = tweets.Find(x => x.Text.Contains("i"));
                return Result;
            }
        }


        public static bool isLocInRad(Coordinates coords, double rad, ITweet TweetToCheck)
        {
            double Distance = GetDistance(coords.Latitude,
                                coords.Longitude,
                                TweetToCheck.Coordinates.Latitude,
                                TweetToCheck.Coordinates.Longitude);
            return Distance < rad;
        }

        private static double GetDistance(double lat1, double lon1, double lat2, double lon2)
        {
            var R = 6371; // Radius of the earth in km
            var dLat = ToRadians(lat2 - lat1);  // deg2rad below
            var dLon = ToRadians(lon2 - lon1);
            var a =
                Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                Math.Cos(ToRadians(lat1)) * Math.Cos(ToRadians(lat2)) *
                Math.Sin(dLon / 2) * Math.Sin(dLon / 2);

            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            var d = R * c; // Distance in km
            return d;
        }

        private static double ToRadians(double deg)
        {
            return deg * (Math.PI / 180);
        }
    }
}
