using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SchoolApi.Models;
using SchoolApi.Repository;

namespace SchoolApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly ILogger<CoursesController> _logger;
        private readonly SchoolContext _context;
        public CoursesController(SchoolContext ctx, ILogger<CoursesController> logger)
        {
            _context = ctx;
            _logger = logger;
        }
        [HttpGet("{Id}")]
        public IActionResult Get(int Id)
        {
            var poco = _context.Courses.Include(e => e.Enrollments).ThenInclude(e => e.Student).FirstOrDefault(e => e.Id == Id);
        //    _logger.LogInformation("&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&");
            if (poco == null) return NotFound();
            else return Ok(poco);
        }
        [HttpGet]
        public IActionResult AllList()
        {
            var list = _context.Courses.ToList();

            return Ok(list);
        }

        [HttpPost]
        public IActionResult Create([FromBody] CoursePoco poco)
        {
            _context.Courses.Add(poco);
            _context.SaveChanges();
            return Ok();
        }
        [HttpPut]
        public IActionResult Update([FromBody] CoursePoco poco)
        {
            _context.Courses.Update(poco);
            _context.SaveChanges();
            return Ok();
        }
        [HttpDelete]
        public IActionResult Delete([FromBody] CoursePoco poco)
        {
            _context.Courses.Remove(poco);
            _context.SaveChanges();
            return Ok();
        }

        [HttpPost("{Id}/enroll")]
        public IActionResult Enroll(int Id, [FromBody] ICollection<EnrollmentPoco> enrolls)
        {
            _context.Enrollments.AddRange(enrolls);
            _context.SaveChanges();
            return Ok();
        }

        [HttpDelete("{Id}/enroll")]
        public IActionResult EnrollRemove(int Id, [FromBody] ICollection<EnrollmentPoco> enrolls)
        {
            _context.Enrollments.RemoveRange(enrolls);
            _context.SaveChanges();
            return Ok();
        }
    }
}
