using Microsoft.AspNetCore.Mvc;
using StudentExample.Entities;
using StudentExample.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StudentExample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {

        private readonly IStudentService studentService;

        public StudentController(IStudentService studentService)
        {
            this.studentService = studentService;
        }

        [HttpGet]
        public ActionResult Get()
        {
            try
            {
                return Ok(studentService.GetStudents());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        // get student by id
        [HttpGet("{id}")]
        public ActionResult<Student> GetStudent(int id)
        {
            try
            {
                var result = studentService.GetStudent(id);

                if (result == null) return NotFound();

                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        // POST a student
        [HttpPost]
        public ActionResult<Student> CreateStudent(Student student)
        {
            try
            {
                if (student == null)
                    return BadRequest();

                var createdStuden = studentService.AddStudent(student);

                //return CreatedAtAction(nameof(Student), new { id = student.Id });
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error creating new student");
            }
        }

        // PUT api/<StudentController>/5
        [HttpPut("{id}")]
        public ActionResult<Student> UpdateStudent(int id, Student student)
        {
            try
            {
                if (id != student.Id)
                    return BadRequest("Student ID mismatch");

                var studentToUpdate = studentService.UpdateStudent(id, student);

                return studentToUpdate != null ? Ok(studentToUpdate) : NotFound($"Student with Id = {id} not found");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating data");
            }
        }

        // DELETE api/<StudentController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var studentToRemove = studentService.RemoveStudent(id);

                return studentToRemove == true ? Ok() : NotFound($"Student with Id = {id} not found");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting data");
            }
        }
    }
}
