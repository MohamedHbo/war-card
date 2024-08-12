using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.IO;
using System.Linq;
using war.Models;
using war.Models.Repositories;
using war.Models.ViewModels;

namespace war.Controllers
{
    public class CardController : Controller
    {
        private readonly ICardRepositories<Card> cardRepositories;
        private readonly ICardRepositories<Person> personRepositories;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public CardController(ICardRepositories<Card> cardRepositories, ICardRepositories<Person> personRepositories, IWebHostEnvironment hostingEnvironment)
        {
            this.cardRepositories = cardRepositories;
            this.personRepositories = personRepositories;
            this._hostingEnvironment = hostingEnvironment;
        }

        // GET: CardController/Index
        public ActionResult Index(int? personId)
        {
            
            var cards = cardRepositories.List();

            
            var persons = personRepositories.List().ToList();
            ViewBag.Persons = new SelectList(persons, "Id", "Name", personId);

            
            if (personId.HasValue)
            {
                cards = cards.Where(c => c.Person.Id == personId.Value).ToList();
            }

           
            var cardViewModels = cards.Select(card => new CardPersonViewModel
            {
                CardId = card.Id,
                CardName = card.Name,
                CardDescription = card.Description,
                CardImage = card.Image,
                PersonName = card.Person.Name
            }).ToList();

            return View(cardViewModels);
        }


       
        public ActionResult Details(int id)
        {
            var card = cardRepositories.Find(id);
            var model = new CardPersonViewModel
            {
                CardId = card.Id,
                CardName = card.Name,
                CardDescription = card.Description,
                CardImage = card.Image,
                PersonName = card.Person.Name
            };
            return View(model);
        }

        // GET: CardController/Create
        public ActionResult Create()
        {
            var model = new CardPersonViewModel
            {
                Persons = personRepositories.List().ToList()
            };
            return View(model);
        }

        // POST: CardController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CardPersonViewModel model)
        {
            try
            {
                if (model.File != null && model.File.Length > 0)
                {
                    string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + model.File.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        model.File.CopyTo(fileStream);
                    }
                    model.CardImage = uniqueFileName;
                }

                var person = personRepositories.Find(model.PersonId);
                var newCard = new Card
                {
                    Name = model.CardName,
                    Description = model.CardDescription,
                    Image = model.CardImage,
                    Person = person
                };

                cardRepositories.Add(newCard);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(model);
            }
        }

        // GET: CardController/Edit/5
        public ActionResult Edit(int id)
        {
            var card = cardRepositories.Find(id);
            var persons = personRepositories.List().ToList();

            if (persons == null || !persons.Any())
            {
                ViewData["ErrorMessage"] = "No persons available. Please add a person first.";
                return View(new CardPersonViewModel { Persons = new List<Person>() });
            }

            var model = new CardPersonViewModel
            {
                CardId = card.Id,
                CardName = card.Name,
                CardDescription = card.Description,
                CardImage = card.Image,
                PersonId = card.Person.Id,
                Persons = persons
            };

            // Log or debug the model to ensure it is correct
            Console.WriteLine($"Model: {System.Text.Json.JsonSerializer.Serialize(model)}");

            return View(model);
        }





        // POST: CardController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, CardPersonViewModel model)
        {
            try
            {
                var cardToUpdate = cardRepositories.Find(id);

                if (model.File != null && model.File.Length > 0)
                {
                    string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + model.File.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        model.File.CopyTo(fileStream);
                    }
                    model.CardImage = uniqueFileName;
                }
                else
                {
                    model.CardImage = cardToUpdate.Image;
                }

                cardToUpdate.Name = model.CardName;
                cardToUpdate.Description = model.CardDescription;
                cardToUpdate.Image = model.CardImage;
                cardToUpdate.Person = personRepositories.Find(model.PersonId);

                cardRepositories.Update(cardToUpdate);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(model);
            }
        }

        // GET: CardController/Delete/5
        public ActionResult Delete(int id)
        {
            var card = cardRepositories.Find(id);
            if (card == null)
            {
                return NotFound();
            }

            // Debugging: Ensure the card object is populated correctly
            Console.WriteLine($"Deleting Card: {card.Name}, {card.Description}, {card.Person?.Name}, {card.Image}");

            return View(card);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                var card = cardRepositories.Find(id);
                if (card == null)
                {
                    return NotFound();
                }

                cardRepositories.Delete(card);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }



    }
}
