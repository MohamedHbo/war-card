using Microsoft.EntityFrameworkCore;

namespace war.Models
{
    public class CardDbContext:DbContext
    {
        public CardDbContext(DbContextOptions<CardDbContext>option):base(option) {
        
        
        
        }
        

        public DbSet<Person> Persons { get; set; }
        public DbSet<Card> Cards { get; set; }
        
        
    }
}
