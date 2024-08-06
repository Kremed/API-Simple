using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Simple.Backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CurrenciesController : ControllerBase
{

    //01 - Types of HTTP Requests:
    //[HttpGet]      HttpGet   : Retrieve data from the server.
    //[HttpPost]     HttpPost  : Submit data to the server to create a new resource.
    //[HttpPut]      HttpPut   : Update an existing resource on the server.
    //[HttpDelete]   HttpDelete: Delete a resource from the server.
    //[HttpPatch]    HttpPatch : Apply partial updates to a resource.

    //02 - Types of HTTP Responses:
    //تشير الردود من واجهة برمجة التطبيقات إلى نتيجة الطلب.وهي تشتمل على رمز الحالة، واختياريًا بيانات أو نص يحتوي على معلومات خطأ.

    //A. Sucsses Responses 200 family :
    //200 OK         => Ok();
    //201 Created    => Created();
    //204 No Content => NoContent()

    //B. Error Responses 400 family :
    //400 Bad Request => BadRequest();
    //404 Not Found   => NotFound();
    //401 Unauthorized=> Unauthorized();

    //C. Server Error Responces 500 family :=> 
    //500 Internal Server Error





    List<Student> students = new List<Student>
    {
        new Student{ID =2,Name="Moatasem Kremed",Phone="0924474464", Birthdate = new DateTime(1990,06,12)},
        new Student{ID =2,Name="Elis Ben Shaban",Phone="0924474464", Birthdate = new DateTime(1997,09,22)},
        new Student{ID =3,Name="Mahmoud Jafer",Phone="0924474464", Birthdate = new DateTime(1995,06,18)},
        new Student{ID =4,Name="Salem Mohamed",Phone="0924474464", Birthdate = new DateTime(1997,01,13)},
        new Student{ID =5,Name="Rami Tegaz",Phone="0929487788", Birthdate = new DateTime(1993,12,06)},
    };

    public class Student
    {
        public int ID { get; set; }
        public string Name { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public int Age { get; set; }
        public DateTime Birthdate { get; set; }
    }






    [HttpGet("GetAllStudents")]
    public IActionResult GetAllStudents()
    {
        return Ok(students);
    }

    [HttpGet("GetStudentById/{id}")] //Route Patrameter
    public IActionResult GetStudentById([FromRoute] int id)
    {
        foreach (var student in students)
        {
            if (student.ID == id)
            {
                return Ok(student);
            }
        }
        return NotFound($"لم يتم ايجاد الطلاب المختار صاحب ID : {id}");
    }

    [HttpGet("GetStudentByIdFromQuery")] //Query Patrameter
    public IActionResult GetStudentByIdFromQuery([FromQuery] int id, string name)
    {
        foreach (var student in students)
        {
            if (student.ID == id)
            {
                return Ok(student);
            }
        }
        return NotFound($"لم يتم ايجاد الطلاب المختار صاحب ID : {id}");
    }


    [HttpPost("CreateStudent")] //Body Patrameter
    public IActionResult CreateStudent([FromBody] Student student)
    {
        //TO DO Validate Student Object Props !!!
        if (student.Name == "")
        {
            return BadRequest("اسم الطالب حقل مطلوب");
        }
        else if (student.Birthdate > DateTime.Now)
        {
            return BadRequest("لايمكن اضافة تاريخ ميلاد بتاريخ مستقبلي");
        }

        students.Add(student);

        return Ok(students);

    }

    [HttpDelete("DeleteStudent")] //Body Patrameter
    public IActionResult DeleteStudent(string name)
    {
        //LINQ

        var selectedStudent = students.FirstOrDefault(student => student.Name == name);

        return Ok(selectedStudent);

    }

}

