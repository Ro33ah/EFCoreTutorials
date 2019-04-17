using System;
using System.Collections.Generic;
using System.Text;

namespace EFCoreTutorials
{
    public class DataInfo
    {
        public int Id { get; set; }

        public string Date { get; set; }
        public string Tweet { get; set; }
        public int RTs { get; set; }
        public string Hashtags { get; set; }
        public string UserMentionID { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
    }
}
