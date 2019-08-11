using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace TweetMap.Models
{
    public class Tweet
    {
        public long tweetID { get; set; }
        public string tweetUser { get; set; }
        public string tweetText { get; set; }
    }
}
