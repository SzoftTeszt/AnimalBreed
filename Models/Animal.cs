using System.ComponentModel.DataAnnotations.Schema;

namespace AnimalsAPI.Models
{
    public class Animal
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int BreedId { get; set; }

        [ForeignKey("BreedId")]
        public Breed BreedName { get; set;}
    }
}
