using InstaAutoPost.UI.Core.Abstract;
using InstaAutoPost.UI.Core.Common.DTOS;
using InstaAutoPost.UI.Core.Concrete;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InstaAutoPost.UI.WebUI.Controllers
{
    public class PostController : Controller
    {
        IPostService _postService;
        private readonly IHostEnvironment _environment;
        public PostController(IPostService service, IHostEnvironment environment)
        {
            _postService = service;
            _environment = environment;
        }
        public IActionResult Index()
        {
            return View();
        }
        public PartialViewResult GetAllPost(int quantity = 10)
        {
            var contents = _postService.GetPostList(quantity);
            var statistics = _postService.GetPostStatistics();
            var data = new Tuple<List<PostViewModelDTO>, PostStatistics>(contents, statistics);
            AutoJobService a = new AutoJobService();
            return PartialView("~/Views/Shared/Partials/_PostListPartial.cshtml", data);
        }
        public JsonResult ChangePostOrder(int postId,int order)
        {
            var result = _postService.ChangeOrder(postId, order);
            return Json(result);
        }
        
    }
}
