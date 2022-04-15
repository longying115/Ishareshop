using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Ishareshop.Api.Controllers
{
    public class JsonController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            using (var fs = new FileStream("test.json", FileMode.Open))
            {
                using(var cmt=JsonDocument.Parse(fs))
                {
                    var t = cmt.RootElement[0].GetProperty("name");
                    var k = cmt.RootElement[0].GetProperty("students").GetProperty("name")[1];
                }
            }
            return Ok();
        }
        [HttpGet]
        public async Task<IActionResult> Write()
        {
            return Ok();
        }
    }
    public class ClassGrade
    {
        public string Name { get; set; }
        public string Number { get; set; }
        public List<StudentModel> Students { get; set; }
    }
    public class StudentModel
    {
        public string Name { get; set; }
        public string Number { get; set; }
        public int Age { get; set; }

    }
}
