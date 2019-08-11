using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tweetinvi.Models;

namespace TweetMap.Models
{
    public class TweetModel
    {
        public TweetModel()
        {
        }

        public long _id { get; set; }

        public string UserName { get; set; }

        public string Text { get; set; }

        public ICoordinates coordinates { get; set; }
    }
}
