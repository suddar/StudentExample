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
            studentlist.Add(new());
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
        public ActionResult<Student> GetEmployee(int id)
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
        public IActionResult Post([FromBody] Student value)
        {
            studentlist.Add(value);

            return Ok(value);
        }

        // PUT api/<StudentController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Student value)
        {
            studentlist[id] = value;
            return Ok(studentlist[id]);
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
