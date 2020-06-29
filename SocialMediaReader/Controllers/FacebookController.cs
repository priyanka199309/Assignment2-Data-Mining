using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
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
  
   
    [Authorize]
    public class FacebookController : Controller
    {
        
        // GET: Facebook
        public ActionResult Index()
        {
            return View();
        }
        public async Task<ActionResult>Posts()
        {
            Models.SocialMedia.Facebook.FacebookClient facebookClient = new Models.SocialMedia.Facebook.FacebookClient();
            facebookClient.HttpContext = HttpContext;
            Models.SocialMedia.Facebook.posts posts =await facebookClient.Posts();
            return (View(posts));
          
            
        }
    }
}