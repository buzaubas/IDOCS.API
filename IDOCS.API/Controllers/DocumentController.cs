using IDOCS.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IDOCS.API.Controllers
{
    [Authorize]
    public class DocumentController : Controller
    {
        private readonly ApplicationDbContext context;

        public DocumentController(ApplicationDbContext _context)
        {
            context = _context;
        }

        [HttpGet]
        public IEnumerable<Document> Get()
        {
            return context.Documents;
        }

        [HttpGet("{number}")]
        public ActionResult<Document> Get(int number)
        {
            if (number == 0)
                return BadRequest("Value must be passed to the request");
            return Ok(context.Documents.FirstOrDefault(x => x.Number == number));
        }

        [HttpPost]
        public ActionResult<Document> Post([FromBody] Document document)
        {
            try
            {
                context.Documents.Add(document);
                context.SaveChanges();
                return Ok(document);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        public ActionResult<Document> Put([FromForm] Document document)
        {
            var temp = context.Documents.Find(document.Number);
            if (temp == null)
            {
                return BadRequest();
            }
            else
            {
                try
                {
                    temp.Number = document.Number;
                    temp.Type = document.Type;
                    temp.CreatedDate = document.CreatedDate;    
                    temp.CreatedPersonId = document.CreatedPersonId;
                    temp.Name = document.Name;
                    temp.Data = document.Data;
                    temp.ReceiverPersonId = document.ReceiverPersonId; 
                    context.SaveChanges();
                    return Ok();
                }
                catch (Exception e)
                {
                    return BadRequest(e.Message);
                }
            }
        }

        [HttpDelete("{number}")]
        public ActionResult<Person> Delete(int number)
        {
            try
            {
                var temp = context.Documents.Find(number);
                _ = context.Documents.Remove(temp); //?
                context.SaveChanges();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("AddDocument")]
        public async Task<Document> AddDocument(IFormFile file, int number)
        {
            Document document = new Document();

            if (file.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    file.CopyTo(ms);
                    document.Data = ms.ToArray();
                }
            }
            document.Number = number;

            context.Documents.Add(document);
            await context.SaveChangesAsync();

            return document;
        }

        //[HttpGet("GetContentDocumentById")]
        //public async Task<IActionResult> GetContentDocumentById(int Id)
        //{
        //    var file = context.Documents.Find(Id);
        //    return File(file.Data, "application/octet-stream", "file.txt");
        //}
    }
}
