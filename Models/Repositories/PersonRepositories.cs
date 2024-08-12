using System.Collections.Generic;
using System.Linq;

namespace war.Models.Repositories
{
    public class PersonRepositories : ICardRepositories<Person>
    {
        private readonly List<Person> _persons;

        public PersonRepositories()
        {
            _persons = new List<Person>
            {
                new Person { Id = 1, Name = "Person Fire" },
                new Person { Id = 2, Name = "Person Water" },
                new Person { Id = 3, Name = "Person Windey" },
                new Person { Id = 4, Name = "Person Cold" }
            };
        }

        public void Add(Person entity)
        {
            _persons.Add(entity);
        }

        public void Delete(int id)
        {
            var person = Find(id);
            if (person != null)
            {
                _persons.Remove(person);
            }
        }

        public void Delete(Card card)
        {
            throw new NotImplementedException();
        }

        public Person Find(int id)
        {
            return _persons.SingleOrDefault(p => p.Id == id);
        }

        public IList<Person> List()
        {
            return _persons;
        }

        public void Update(int id, Person newPerson)
        {
            var person = Find(id);
            if (person != null)
            {
                person.Name = newPerson.Name;
            }
        }

        public void Update(Card cardToUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
