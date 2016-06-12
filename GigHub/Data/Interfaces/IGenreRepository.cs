using GigHub.Models;
using System.Collections.Generic;

namespace GigHub.Data.Interfaces
{
    public interface IGenreRepository
    {
        IEnumerable<Genre> GetGenres();
    }
}
