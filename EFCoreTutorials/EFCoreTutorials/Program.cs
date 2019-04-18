using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

using FastMember;

namespace EFCoreTutorials
{
    public class Program
    {
        public static IConfigurationRoot Configuration ;
        public static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
               .AddEnvironmentVariables();

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
            string connectionstring = Configuration["ConnectionString"];

            List<string> records = new List<string>();
            using (StreamReader sr = new StreamReader(File.OpenRead("file\\location")))
            {

                string file = sr.ReadToEnd();
                records = new List<string>(file.Split('\n'));
            }


            List<DataInfo> tweetlist = new List<DataInfo>();

            foreach (string record in records)
            {
                DataInfo tweet = new DataInfo();
                string[] textpart = record.Split(',');
                tweet.Id  =  Convert.ToInt16(textpart[0]);
                tweet.Date = textpart[1];
                tweet.Tweet = textpart[2];
                tweet.RTs = Convert.ToInt16(textpart[3]);
                tweet.Hashtags = textpart[4];
                tweet.UserMentionID = textpart[5];
                tweet.City = textpart[6];
                tweet.Country = textpart[7];
                tweet.Longitude = Convert.ToDouble(textpart[8]);
                tweet.Latitude = Convert.ToDouble(textpart[9]);
                tweetlist.Add(tweet);

            }


            var copyParameters = new[]
             {
                        nameof(DataInfo.Id),
                        nameof(DataInfo.Date),
                        nameof(DataInfo.Tweet),
                        nameof(DataInfo.RTs),
                        nameof(DataInfo.Hashtags),
                        nameof(DataInfo.UserMentionID),
                        nameof(DataInfo.City),
                        nameof(DataInfo.Country),
                        nameof(DataInfo.Longitude),
                        nameof(DataInfo.Latitude)
            };


            using (var sqlCopy = new SqlBulkCopy(connectionstring))
            {
                sqlCopy.DestinationTableName = "[DataInfo]";
                sqlCopy.BatchSize = 500;
                using (var reader = ObjectReader.Create(tweetlist, copyParameters))
                {
                    sqlCopy.WriteToServer(reader);
                }
            }


        }
    }
}
