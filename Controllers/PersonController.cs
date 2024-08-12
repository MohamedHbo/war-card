using Microsoft.AspNetCore.Mvc;
using war.Models;
using war.Models.Repositories;
using war.Models.ViewModels;

namespace war.Controllers
{
    public class PersonController : Controller
    {
        private readonly ICardRepositories<Person> _personRepositories;

        public PersonController(ICardRepositories<Person> personRepositories)
        {
            _personRepositories = personRepositories;
        }

        // GET: PersonController
        public ActionResult Index()
        {
            var persons = _personRepositories.List();
            return View(persons);
        }

        // GET: PersonController/Details/5
        public ActionResult Details(int id)
        {
            var person = _personRepositories.Find(id);
            if (person == null)
            {
                return NotFound();
            }
            return View(person);
        }

        // GET: PersonController/Create
        public ActionResult Create()
        {
            var viewModel = new CardPersonViewModel
            {
                Persons = (List<Person>)_personRepositories.List()
            };
            return View(viewModel);
        }

        // POST: PersonController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CardPersonViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var person = _personRepositories.Find(viewModel.PersonId);
                if (person != null)
                {
                    viewModel.PersonName = person.Name;
                    // Save the card information along with the person name
                }
                return RedirectToAction(nameof(Index));
            }
            viewModel.Persons = (List<Person>)_personRepositories.List();
            return View(viewModel);
        }

        // GET: PersonController/Edit/5
        public ActionResult Edit(int id)
        {
            var person = _personRepositories.Find(id);
            if (person == null)
            {
                return NotFound();
            }
            return View(person);
        }

        // POST: PersonController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Person person)
        {
            if (ModelState.IsValid)
            {
                _personRepositories.Update(id, person);
                return RedirectToAction(nameof(Index));
            }
            return View(person);
        }

        // GET: PersonController/Delete/5
        public ActionResult Delete(int id)
        {
            var person = _personRepositories.Find(id);
            if (person == null)
            {
                return NotFound();
            }
            return View(person);
        }

        // POST: PersonController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                _personRepositories.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
