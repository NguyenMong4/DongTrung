using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebDongTrung.Datas;
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
        [HttpGet]
        public async Task<IActionResult> GetAllEmployee()
        {
            try
            {
                return Ok(await _blog.GetAllBlogAsync());
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
        public async Task<ActionResult> AddEmployee(Blog blog)
        {
            try
            {
                var newBlog = await _blog.AddBlogAsync(blog);
                var blg = await _blog.GetBlogAsync(newBlog!);
                return blg == null ? NotFound() : Ok(blg);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, Blog blog)
        {
            try
            {
                await _blog.UpdateBlogAsync(id, blog);
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