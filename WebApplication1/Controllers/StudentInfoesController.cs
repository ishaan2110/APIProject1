using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Mod;

namespace WebApplication1.Controllers
{
    [Route("api/students")]
    [ApiController]
    public class StudentInfoesController : ControllerBase
    {
        private readonly StudentsContext _context;

        public StudentInfoesController(StudentsContext context)
        {
            _context = context;
        }

        // GET: api/StudentInfoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentInfo>>> GetStudentInfo()
        {
            return await _context.StudentInfo.ToListAsync();
        }

        // GET: api/StudentInfoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StudentInfo>> GetStudentInfo(int id)
        {
            var studentInfo = await _context.StudentInfo.FindAsync(id);

            if (studentInfo == null)
            {
                return NotFound();
            }

            return studentInfo;
        }

        // PUT: api/StudentInfoes/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudentInfo(int id, StudentInfo studentInfo)
        {
            if (id != studentInfo.StudentId)
            {
                return BadRequest();
            }

            _context.Entry(studentInfo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentInfoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/StudentInfoes
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<StudentInfo>> PostStudentInfo(StudentInfo studentInfo)
        {
            _context.StudentInfo.Add(studentInfo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStudentInfo", new { id = studentInfo.StudentId }, studentInfo);
        }

        // DELETE: api/StudentInfoes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<StudentInfo>> DeleteStudentInfo(int id)
        {
            var studentInfo = await _context.StudentInfo.FindAsync(id);
            if (studentInfo == null)
            {
                return NotFound();
            }

            _context.StudentInfo.Remove(studentInfo);
            await _context.SaveChangesAsync();

            return studentInfo;
        }

        private bool StudentInfoExists(int id)
        {
            return _context.StudentInfo.Any(e => e.StudentId == id);
        }
    }
}
