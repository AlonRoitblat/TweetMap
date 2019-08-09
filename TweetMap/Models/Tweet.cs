using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace TweetMap.Models
{
    public class Tweet
    {
        public int tweetID { get; set; }
        public string tweetText { get; set; }
        public Point Location { get; set; }
    }
}
