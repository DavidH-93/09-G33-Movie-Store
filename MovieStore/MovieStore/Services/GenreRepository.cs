using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieStore.Models;
namespace MovieStore.Services
{
    public class GenreRepository : BaseRepository<Genre>, IGenreRepository
    {
    }
}
