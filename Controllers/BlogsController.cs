using BloggerBE.Services;
using Microsoft.AspNetCore.Mvc;

namespace BloggerBE.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class BlogsController : ControllerBase
    {
    private readonly BlogsService _blogsService;
    public BlogsController(BlogsService blogsService)
    {
      _blogsService = blogsService;
    }

    // [HttpGet]

  }
}