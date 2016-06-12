using GigHub.Data.Infrastructure;
using GigHub.Data.Interfaces;
using GigHub.Models;
using System.Collections.Generic;
using System.Linq;

namespace GigHub.Data.Repositories
{
    public class GenreRepository : IGenreRepository
    {
        private readonly ApplicationDbContext _context;
        public GenreRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Genre> GetGenres()
        {
            return _context.Genres.ToList();
        }
    }
}