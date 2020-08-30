using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
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
    public class StudentsController : ControllerBase
    {
        private readonly ILogger<StudentsController> _logger;
        private readonly SchoolContext _context;
        public StudentsController(SchoolContext ctx, ILogger<StudentsController> logger)
        {
            _context = ctx;
            _logger = logger;
        }
        [HttpGet("{Id}")]
        public IActionResult Get(int Id)
        {
         //   var poco = _context.Students.Include(e => e.Enrollments).ThenInclude(e => e.Course).FirstOrDefault(e => e.Id == Id);
            var poco = _context.Students.FirstOrDefault(e => e.Id == Id);

            if (poco == null) return NotFound();
            else return Ok(poco);
        }
        [HttpGet]
        public IActionResult AllList()
        {
            var list = _context.Students.ToList();

            return Ok(list);
        }

        [HttpPost]
        public IActionResult Create([FromBody] StudentPoco poco)
        {
            _context.Students.Add(poco);
            _context.SaveChanges();
            return Ok();
        }
        [HttpPut]
        public IActionResult Update([FromBody] StudentPoco poco)
        {
            _context.Students.Update(poco);
            _context.SaveChanges();
            return Ok();
        }
        [HttpDelete]
        public IActionResult Delete([FromBody] StudentPoco poco)
        {
            _context.Students.Remove(poco);
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
