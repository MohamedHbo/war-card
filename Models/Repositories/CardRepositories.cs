using System;
using System.Collections.Generic;
using System.Linq;

namespace war.Models.Repositories
{
    public class CardRepositories : ICardRepositories<Card>
    {
        private readonly List<Card> _cards;

        public CardRepositories()
        {
            _cards = new List<Card>()
        {
            new Card() { Id = 1, Name = "Card 1", Description = "Description 1", Image = "1.jpg", Person = new Person { Id = 1, Name = "Person Fire" } },
            new Card() { Id = 2, Name = "Card 2", Description = "Description 2", Image = "2.jpg", Person = new Person { Id = 2, Name = "Person Fire" } },
            new Card() { Id = 3, Name = "Card 3", Description = "Description 3", Image = "3.jpg", Person = new Person { Id = 3, Name = "Person Fire" } },
            new Card() { Id = 4, Name = "Card 4", Description = "Description 4", Image = "4.jpg", Person = new Person { Id = 4, Name = "Person Cold" } },
        };
        }


        public void Add(Card entity)
        {
            _cards.Add(entity);
        }

        public void Delete(int id)
        {
            var card = Find(id);
            if (card != null)
            {
                _cards.Remove(card);
            }
        }

        public void Delete(Card card)
        {
            if (card == null)
            {
                throw new ArgumentNullException(nameof(card));
            }

            var existingCard = _cards.FirstOrDefault(c => c.Id == card.Id);
            if (existingCard != null)
            {
                _cards.Remove(existingCard);
            }
        }

        public Card Find(int id)
        {
            return _cards.SingleOrDefault(c => c.Id == id);
        }

        public IList<Card> List()
        {
            return _cards;
        }

        public void Update(int id, Card newCard)
        {
            var card = Find(id);
            if (card != null)
            {
                card.Name = newCard.Name;
                card.Description = newCard.Description;
                card.Image = newCard.Image;
            }
        }

        public void Update(Card cardToUpdate)
        {
            var existingCard = Find(cardToUpdate.Id);
            if (existingCard != null)
            {
                existingCard.Name = cardToUpdate.Name;
                existingCard.Description = cardToUpdate.Description;
                existingCard.Image = cardToUpdate.Image;
                existingCard.Person = cardToUpdate.Person;
            }
        }
    }
}
