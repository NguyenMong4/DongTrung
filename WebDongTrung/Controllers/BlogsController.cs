using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebDongTrung.Datas;
using WebDongTrung.Models;
using WebDongTrung.Repositories;

namespace WebDongTrung.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BlogsController : ControllerBase
    {
        private readonly IBlog _blog;

        public BlogsController(IBlog blog)
        {
            _blog = blog;
        }

        // Type = 1 : blog
        // = 2 : about
        // = 3 : chuong trinh khuyen mai
        [HttpGet("Blogs")]
        public async Task<IActionResult> GetAllBlogs()
        {
            try
            {
                return Ok(await _blog.GetAllBlogAsync(1));
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("About")]
        public async Task<IActionResult> GetAbout()
        {
            try
            {
                return Ok(await _blog.GetAllBlogAsync(2));
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("Discount")]
        public async Task<IActionResult> GetAllDiscount()
        {
            try
            {
                return Ok(await _blog.GetAllBlogAsync(3));
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("UserManual")]
        public async Task<IActionResult> GetAllUserManual()
        {
            try
            {
                return Ok(await _blog.GetAllBlogAsync(4));
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBlog(int id)
        {
            var blog = await _blog.GetBlogAsync(id);
            return blog == null ? NotFound() : Ok(blog);
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult> AddBlog([FromForm]BlogModel blogModel)
        {
            try
            {
                var username = Request.Cookies["CookieUserName"];
                var newBlog = await _blog.AddBlogAsync(blogModel, username);
                var blg = await _blog.GetBlogAsync(newBlog!);
                return blg == null ? NotFound() : Ok(blg);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UpdateBlog(int id, [FromForm]BlogModel blogModel)
        {
            try
            {
                var username = Request.Cookies["CookieUserName"];
                await _blog.UpdateBlogAsync(id, blogModel, username);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBlog(int id)
        {
            try
            {
                await _blog.DeleteBlogAsync(id);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}