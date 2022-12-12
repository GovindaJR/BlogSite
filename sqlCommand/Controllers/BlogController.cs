using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Web;
using BlogCity.Models;
using BlogCity.Models;

namespace BlogCity.Controllers
{
    public class BlogController : Controller
    {
        private Blog blog = new Blog();


        // GET: BlogController
        public ActionResult Index()
        {
            List <Post> posts = blog.getPosts();
            return View(posts);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(IFormCollection collection)
        {
            try
            {
                EmailRecipient emailRecipient = new EmailRecipient(collection["Email"]);
                blog.registerRecipient(emailRecipient);                                
                TempData["AlertMessage"] = "Email added to notification list!";
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }


        // GET: BlogController/Post/5
        public ActionResult Post(int id)
        {
           
            Post currentPost = blog.getPostByID(id);
            ViewBag.post = currentPost;
            return View();
        }


        // GET: BlogController/Create
        public ActionResult Create()
        {
            return View();
        }


        // POST: BlogController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {         
                Post post = new Post(collection["Title"], collection["Body"], collection["Author"]);
                blog.addPost(post);
                blog.notifyRecipients(post);          
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
