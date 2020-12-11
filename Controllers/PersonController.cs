using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ASPNETCore5Demo.Models;
using Microsoft.EntityFrameworkCore;

namespace ASPNETCore5Demo.Controllers
{
    public class PersonController : UscControllerBase<ContosoUniversityContext, Person>
    {
        public PersonController(ContosoUniversityContext db)
            : base(db, nameof(db.People))
        { }

        [HttpGet("")]
        public ActionResult<IEnumerable<Person>> GetPersons() => InnerGetList();

        [HttpGet("{id}")]
        public ActionResult<Person> GetPersonById(int id) => InnerGetByKeys(id);

        [HttpPost("")]
        public ActionResult<Person> PostPerson(PersonData data) => InnerPost(data, nameof(GetPersonById), new { id = data.Id });

        [HttpPut("")]
        public IActionResult PutPerson(PersonData data) => InnerPutByKeys(data, data.Id);

        [HttpDelete("{id}")]
        public ActionResult<Person> DeletePersonById(int id)
            //=> InnerDeleteByKeys(id);
            => InnerMarkDeletedByKeys(id);
    }
}
