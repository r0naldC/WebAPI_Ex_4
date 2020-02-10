using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebAPI_Ex_4.Models.Entities;
using WebAPI_Ex_4.Services;

namespace WebAPI_Ex_4.Controllers
{
    public class MoviesController : ApiController
    {
        private Ex_4Entities db = new Ex_4Entities();
        private readonly MovieService _movieService = new MovieService();


        [HttpGet]
        [Route("api/movies")]
        public List<Movie> GetMovies()
        {
            return _movieService.GetMyMovies();
        }

        [HttpGet]
        [Route("api/movies/{id}")]
        [ResponseType(typeof(Movie))]
        public IHttpActionResult GetMovie([FromUri] int id)
        {
            var movie = _movieService.GetMyMovie(id);

            if (movie == null)
            {
                return NotFound();
            }

            return Ok(movie);
        }

        [HttpPut]
        [Route("api/updateMovie/{id}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutMovie([FromUri] int id, [FromBody] Movie movie)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != movie.Id)
            {
                return BadRequest();
            }

            _movieService.UpdateMovie(id, movie);

            return StatusCode(HttpStatusCode.NoContent);
        }

        
        [HttpPost]
        [Route("api/createMovie")]
        [ResponseType(typeof(Movie))]
        public IHttpActionResult PostMovie([FromBody] Movie movie)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _movieService.CreateMovie(movie);

            return StatusCode(HttpStatusCode.NoContent);
        }

        
        [HttpDelete]
        [Route("api/deleteMovie/{id}")]
        [ResponseType(typeof(Movie))]
        public IHttpActionResult DeleteMovie([FromUri] int id)
        {
            
            if (db.Movies.Find(id)  == null)
            {
                return NotFound();
            }

            _movieService.RemoveMovie(id);

            return StatusCode(HttpStatusCode.NoContent);
        }

    }
}