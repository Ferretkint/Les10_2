using Microsoft.AspNetCore.Mvc;
using VivesHelpdesk.Model;
using VivesHelpdesk.Data;

namespace VivesHelpdesk.Ui.WebApp.Controllers
{
    public class PeopleController : Controller
    {
        private readonly VivesHelpdeskDbContext _dbContext;

        public PeopleController(VivesHelpdeskDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var people = _dbContext.People
                .ToList();
            return View(people);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create([FromForm]Person person)
        {
            //Validate
            if (!ModelState.IsValid)
            {
                return View(person);
            }
            
            _dbContext.People.Add(person);

            _dbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit([FromRoute]int id)
        {
            var person = _dbContext.People
                .SingleOrDefault(t => t.Id == id);

            if(person is null)
            {
                return RedirectToAction("Index");
            }

            return View(person);
        }

        [HttpPost]
        public IActionResult Edit([FromRoute]int id, [FromForm]Person person)
        {
            //Validate
            if (!ModelState.IsValid)
            {
                return View(person);
            }

            var dbPerson = _dbContext.People
                .SingleOrDefault(t => t.Id == id);

            if(dbPerson is null)
            {
                return RedirectToAction("Index");
            }

            dbPerson.FirstName = person.FirstName;
            dbPerson.LastName = person.LastName;

            _dbContext.SaveChanges();

            return RedirectToAction("Index");

        }

        [HttpPost]
        [Route("[controller]/Delete/{id:int?}")]
        public IActionResult DeleteConfirmed(int id)
        {
            var person = _dbContext.People
                .SingleOrDefault(t => t.Id == id);

            if(person is null)
            {
                return RedirectToAction("Index");
            }

            _dbContext.People.Remove(person);

            _dbContext.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
