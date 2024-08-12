using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace war.Models.ViewModels
{
    public class CardPersonViewModel
    {
        public int CardId { get; set; }

        [Required]
        public string CardName { get; set; }

        [Required]
        public string CardDescription { get; set; }

        public string CardImage { get; set; }

        public int PersonId { get; set; }

        public string PersonName { get; set; }

        public IFormFile File { get; set; }
        public List<Person> Persons { get; set; }
    }
}
