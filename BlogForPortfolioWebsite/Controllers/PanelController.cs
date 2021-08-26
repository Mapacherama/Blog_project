using System.Threading.Tasks;
using BlogForPortfolioWebsite.Data.FileManager;
using BlogForPortfolioWebsite.Data.Repository;
using BlogForPortfolioWebsite.Models;
using BlogForPortfolioWebsite.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogForPortfolioWebsite.Controllers
{
    [Authorize(Roles = "Admin")]
    public class PanelController : Controller
    {
        private readonly IRepository _repo;
        private readonly IFileManager _fileManager;

        public PanelController(IRepository repo,
            IFileManager fileManager
        )
        {
            _repo = repo;
            _fileManager = fileManager;
        }

        public IActionResult Index()
        {
            var posts = _repo.GetAllPosts();
            return View(posts);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return View(new PostViewModel());
            }
            else
            {
                var post = _repo.GetPost((int) id);
                return View(new PostViewModel
                {
                    Id = post.Id,
                    Title = post.Title,
                    Body = post.Body
                });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(PostViewModel vm)
        {
            var post = new Post
            {
                Id = vm.Id,
                Title = vm.Title,
                Body = vm.Body,
                Image = await _fileManager.SaveImage(vm.Image) // Handle image
            };
            if (post.Id > 0)
                _repo.UpdatePost(post);
            else
                _repo.AddPost(post);

            if (await _repo.SaveChangesAsync())
                return RedirectToAction("Index");
            else
                return View(vm);
        }

        [HttpGet]
        public async Task<RedirectToActionResult> Remove(int id)
        {
            _repo.RemovePost(id);
            await _repo.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}