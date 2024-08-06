
using Domain.Entities;

namespace Service.DTOs.Genre
{
    public class GenreDto
    {
        public int Id { get; set; } 
        public string GenreName { get; set; }
        public ICollection<Domain.Entities.Product> Products { get; set; }
    }
}
