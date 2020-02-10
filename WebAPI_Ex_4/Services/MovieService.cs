using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using WebAPI_Ex_4.Models.Entities;

namespace WebAPI_Ex_4.Services
{
    public class MovieService
    {
        private readonly Ex_4Entities _dbContext = new Ex_4Entities();

        public List<Movie> GetMyMovies()
        {
            return _dbContext.Movies.ToList();
        }

        public Movie GetMyMovie(int id)
        {
            var movie = _dbContext.Movies.Find(id);

            return movie;
        }

        public void UpdateMovie(int id, Movie movie)
        {
            var movieToUpdate = _dbContext.Movies.Find(id);

            if (movieToUpdate != null)
            {
                movieToUpdate.Id = movie.Id;
                movieToUpdate.Name = movie.Name;
                movieToUpdate.Date = movie.Date;
                movieToUpdate.Owner = movie.Owner;
            }

            _dbContext.SaveChanges();

        }

        public void CreateMovie(Movie movie)
        {

            _dbContext.Movies.Add(movie);
            _dbContext.SaveChanges();

        }

        public void RemoveMovie(int id)
        {

            var movie = _dbContext.Movies.Find(id);

            _dbContext.Movies.Remove(movie ?? throw new InvalidOperationException());
            _dbContext.SaveChanges();
        }

    }
}