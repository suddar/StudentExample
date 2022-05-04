using Microsoft.AspNetCore.Mvc;
using StudentExample.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StudentExample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        List<Student> studentlist = new List<Student>();

        public StudentController()
        {
            Student student = new Student();
            student.Name = "Mai Thi Hoa";
            student.Address = "Ha noi";
            studentlist.Add(student);

            student = new Student();
            student.Name = "Nguyen Van Nam";
            student.Address = "Ho chi minh";
        }


        [HttpGet]
        public ActionResult Get()
        {
            try
            {
                return Ok(studentlist);
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
                var result = studentlist[id];

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

                studentlist.Add(student);

                return CreatedAtAction(nameof(Student), new { id = student.Id });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error creating new student");
            }
        }

        // PUT api/<StudentController>/5
        [HttpPut("{id}")]
        [HttpPut("{id:int}")]
        public ActionResult<Student> UpdateStudent(int id, Student student)
        {
            try
            {
                if (id != student.Id)
                    return BadRequest("Student ID mismatch");

                var studentToUpdate = studentlist[id];

                if (studentToUpdate == null)
                    return NotFound($"Student with Id = {id} not found");

                return Ok(studentToUpdate);
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
                var studentToRemove = studentlist[id];

                if (studentToRemove == null)
                {
                    return NotFound($"Student with Id = {id} not found");
                }

                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting data");
            }
        }
    }
}
