using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace SocialMediaReader.Models.SocialMedia.Facebook
{
    public class FacebookClient : SocialMediaClient
    {
        public string FeedUrl { get; set; } =
            "me?fields=id,name,birthday,feed.limit(1000){message,story,created_time,attachments{description,type,url,title,media,target},comments{id,from}}";

        public FacebookClient()
        {
            ProviderKey = "facebook";
            baseUrl = "https://graph.facebook.com/";
        }

        public async Task<posts> Posts()
        {
            //Access Token
            await GetAccessToken();


            string url = String.Format("{0}{1}&access_token={2}", baseUrl, FeedUrl, "EAADtSvZAF4lgBALCZBrvAymqZC8Evwj1GrvxsPdl6v2mBV3rQLDcOxqxiZBU2ZBVO7ERIKygjYc9i7Xim7BUuD4cZCOpFXXB68aN4mWCVZCIiNUSvpqn0iI5cxeynADyQP9ZCMpMaSeRQVA9ZC9WrzuRjA0fumTCLT1QPqcGjV02JA8d931z9nXRfyWuLZCtYFhsPo0nyIHyUoKQZDZD");
            Console.WriteLine(url);

            //Get data
            dynamic jsonObj = await Get(url);

            //Convert JSON
            Models.SocialMedia.Facebook.posts posts =
                new Models.SocialMedia.Facebook.posts(jsonObj);


            using (SqlConnection connection = new SqlConnection("Data Source=DESKTOP-LNSE3O5;AttachDbFilename=|DataDirectory|SocialMediaReader2020.mdf;Initial Catalog=SocialMediaReader2020;Integrated Security=True"))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    int id = jsonObj.id;
                    DateTime created_time = jsonObj.created_time;
                    string src = jsonObj.src;
                    int width = jsonObj.width;
                    int height = jsonObj.height;

                    command.Connection = connection;            // <== lacking
                    command.CommandType = CommandType.Text;
                    command.CommandText = "INSERT into feed (src,width,height) VALUES (@src,@width,@height)";
                    command.Parameters.AddWithValue("@id", id);
                    command.Parameters.AddWithValue("@created_time", created_time);
                    command.Parameters.AddWithValue("@src", src);
                    command.Parameters.AddWithValue("@height", height);
                    command.Parameters.AddWithValue("@width", width);

                    try
                    {
                        connection.Open();
                        int recordsAffected = command.ExecuteNonQuery();
                    }
                    catch (SqlException)
                    {
                        // error here
                    }
                    finally
                    {
                        connection.Close();
                    }
                }

                return (posts);
            }

        }
    }
}