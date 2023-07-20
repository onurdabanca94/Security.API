using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Encodings.Web;
using XSS.WebUI.Models;

namespace XSS.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private HtmlEncoder _htmlEncoder;
        private JavaScriptEncoder _javaScriptEncoder;
        private UrlEncoder _urlEncoder;

        public HomeController(ILogger<HomeController> logger,HtmlEncoder htmlEncoder, JavaScriptEncoder javaScriptEncoder, UrlEncoder urlEncoder)
        {
            _logger = logger;
            _htmlEncoder = htmlEncoder;
            _javaScriptEncoder = javaScriptEncoder;
            _urlEncoder = urlEncoder;
        }

        public IActionResult CommentAdd()
        {
            HttpContext.Response.Cookies.Append("email", "onurdabanca@gmail.com");
            HttpContext.Response.Cookies.Append("password", "1234");

            if (System.IO.File.Exists("comment.txt"))
            {
                ViewBag.comments = System.IO.File.ReadAllLines("comment.txt");
            }

            return View();
        }

        [IgnoreAntiforgeryToken]
        [HttpPost]
        public IActionResult CommentAdd(string name, string comment)
        {
            string encodeName = _urlEncoder.Encode(name);

            ViewBag.name = name;
            ViewBag.comment = comment;

            System.IO.File.AppendAllText("comment.txt", $"{name}-{comment}\n");
            return RedirectToAction("CommentAdd");
        }

        public IActionResult Login(string returnUrl="/")
        {
            TempData["returnUrl"] = returnUrl;
            return View();
        }
        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            string returnUrl = TempData["returnUrl"].ToString();
            //email password checking

            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return Redirect("/");
            }

            return Redirect(returnUrl);
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}