namespace war.Models
{
    public class Card
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Person Person { get; set; }
    }
}
