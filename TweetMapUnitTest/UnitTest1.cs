using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using TweetMap;
using TweetMap.Models;

namespace UnitTestTweetMap
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestDbInsertSuccess()
        {
            var result = DBManager.InsertObject(new TweetModel()
            {
                _id = new Random().Next(),
                coordinates = new CoordinatesModel(40.749743519532984, -73.996696472167983),
                Text = "This is a Test Tweet"
            });
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void TestDbInsertError()
        {
            var result = DBManager.InsertObject(null);
            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void TestDbReadSuccess()
        {
            var result = DBManager.SearchTweets(new CoordinatesModel(40.749743519532984, -73.996696472167983), 0.5);

            var JsonResult = JsonConvert.SerializeObject(result);
            Assert.AreNotEqual("[]", JsonResult);
        }

        [TestMethod]
        public void TestDbReadError()
        {
            var result = DBManager.SearchTweets(new CoordinatesModel(-40.749743519532984, -73.996696472167983), 0.5);

            var JsonResult = JsonConvert.SerializeObject(result);
            Assert.AreEqual("[]", JsonResult);
        }
    }
}