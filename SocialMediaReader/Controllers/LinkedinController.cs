using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SocialMediaReader.Controllers
{
    public class LinkedinController : Controller
    {
        // GET: Linkedin
        public ActionResult Index()
        {
            return View();
        }
        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager;
        public async Task<ActionResult>getData()
        {

           var currentClaims = await UserManager.GetClaimsAsync(HttpContext.User.Identity.GetUserId());
            var accesstoken = currentClaims.FirstOrDefault(x => x.Type == "urn:tokens:facebook");
            if (accesstoken == null)
            {
                return (new HttpStatusCodeResult(HttpStatusCode.NotFound, "Token not found"));
            }
            string url = "https://www.linkedin.com/oauth/v2/accessToken?grant_type=authorization_code&redirect_uri=https://www.google.com&client_id=7769jcad1ykzl2&client_secret=qNNT9kaXfxzfRZqv&code=AQTAuhxKeTO-3SupiheCeVmchPlUliCW1hdq3NqVqAw8jtHLrlW-oR8eNChOiGExANFb2YPa87lydRJhbow9tPjbKMBnesZcmH3ENY_kBaiay6YS6lN5BZo_igSdqf2qV96EPocRP_l_BQDhU9DmlM8oFqR1pAdsHFBRLLBgq9D3IeilZVOkDTcgKibuKA";
            //generate a web request using url
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            request.Method = "GET";
            //ask data from server
            using (HttpWebResponse response = await request.GetResponseAsync() as HttpWebResponse)
            {
                StreamReader reader = new StreamReader(response.GetResponseStream());
                //place the result into string
                string result = await reader.ReadToEndAsync();
                //convert result into json object



                ViewBag.JSON = result;
                return View();
            // }

        }
    }
}
}