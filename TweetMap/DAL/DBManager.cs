using System;
using System.Collections.Generic;
using LiteDB;
using TweetMap.Models;

namespace TweetMap
{
    public static class DBManager
    {

        // Create or use existing DB
        public static bool InsertObject(TweetModel tweetToInsert)
        {
            // Open database (or create if not exits)
            using (var db = new LiteDatabase(@"TweetsDB.db"))
            {
                // Get tweet collection
                var tweets = db.GetCollection<TweetModel>("tweets");

                try
                {
                    return tweets.Insert(tweetToInsert) != null;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.InnerException);
                    return false;
                }


            }
        }

        public static IEnumerable<TweetModel> SearchTweets(CoordinatesModel coordinates, double radius)
        {
            using (var db = new LiteDatabase(@"TweetsDB.db"))
            {
                // Get tweet collection
                var tweets = db.GetCollection<TweetModel>("tweets");

                var Result = tweets.Find(tweetToCheck => isLocInRadOfTweet(coordinates, radius, tweetToCheck));

                return Result;
            }
        }


        /// <summary>
        /// Function to determine if a location of tweet matches the location and radius
        /// </summary>
        private static bool isLocInRadOfTweet(CoordinatesModel coords, double rad, TweetModel TweetToCheck)
        {
            if (!(TweetToCheck.Coordinates is null))
            {
                // Get Distance
                double Distance = GetDistance(coords.Latitude,
                    coords.Longitude,
                    TweetToCheck.Coordinates.Latitude,
                    TweetToCheck.Coordinates.Longitude);

                // If the distance is less than the rad then its within
                return Distance < rad;
            }
            else
            {
                return false;
            }

        }

        /// <summary>
        /// Function to get distance between two points
        /// </summary>
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
