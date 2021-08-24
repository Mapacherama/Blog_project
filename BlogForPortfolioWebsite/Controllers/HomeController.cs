using System.Threading.Tasks;
using BlogForPortfolioWebsite.Data.Repository;
using BlogForPortfolioWebsite.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlogForPortfolioWebsite.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepository _repo;

        public HomeController(IRepository repo)
        {
            _repo = repo;
        }

        public IActionResult Index()
        {
            var posts = _repo.GetAllPosts();
            return View(posts);
        }
        
        [HttpGet]
        public IActionResult Post(int id)
        {
            var post = _repo.GetPost(id);
            return View(post);
        }
        
    }
}