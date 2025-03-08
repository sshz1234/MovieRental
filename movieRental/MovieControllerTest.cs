using COMP2084Assign2Real.Controllers;
using COMP2084Assign2Real.Data;
using COMP2084Assign2Real.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Assign2Test
{
    [TestClass]
    public class MovieControllerTest
    {
        private ApplicationDbContext _context;
        MoviesController controller;
        [TestInitialize]
        public void TestInitialize()
        {
            //create new in-memory db to pass as dependency to our controller
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            _context = new ApplicationDbContext(options);

            //add some data to the mock db
            _context.Movie.Add(new Movie { MovieId = 56, rating = "2.5", duration = 12, name = "scary movie" });
            _context.Movie.Add(new Movie { MovieId = 32, rating = "5", duration = 3, name = "batman" });
            _context.Movie.Add(new Movie { MovieId = 91, rating = "4.5", duration = 4, name = "fortnite" });
            _context.SaveChanges();

            //instantiate instance of CategoriesController and pass mock db as dependancy to constructor
            controller = new MoviesController(_context);
        }

        [TestMethod]
        #region "Index"
        public void IndexReturnsView()
        {
            //no arrange - done by TestInitialize() automatically

            //act have a to add. .result property as index() is async
            var result = (ViewResult)controller.Index().Result;

            //assert
            Assert.AreEqual("Index", result.ViewName);
        }
        [TestMethod]
        public void IndexReturnsMovies()
        {
            // no arrange - done by TestInitialize() automatically 

            //act have to add .result property as index() is async
            var result = (ViewResult)controller.Index().Result;
            var dataModel = (List<Movie>)result.Model;

            //assert
            CollectionAssert.AreEqual(_context.Movie.ToList(), dataModel);

        }
        #endregion

        #region "Details"
        [TestMethod]
        public void DetailsNoIdReturns404()
        {
            //act 
            var result = (ViewResult)controller.Details(null).Result;

            //asert
            Assert.AreEqual("404", result.ViewName);
        }

        [TestMethod]
        public void DetailsInvalidReturns404()
        {
            //act
            var results = (ViewResult)controller.Details(-1).Result;

            //assert
            Assert.AreEqual("404", results.ViewName);
        }
        [TestMethod]
        public void DetailsValidIdReturnsView()
        {
            // act, passing one of the ids used in the mock db above
            var result = (ViewResult)controller.Details(91).Result;

            // assert
            Assert.AreEqual("Details", result.ViewName);
        }
        [TestMethod]
        public void DetailsValidIdReturnsMovie()
        {
            // arrange - set valid id from mock db
            int id = 91;

            // act, passing one of the ids used in the mock db above
            var result = (ViewResult)controller.Details(id).Result;
            var movie = (Movie)result.Model;

            // assert
            Assert.AreEqual(_context.Movie.Find(id), movie);
        }
        #endregion

        #region "Create"
        // There is no instance where my movie is invalid as
        // any input is accpted
        //[TestMethod]
        //public void CreateInvalidMovieReturns404()
        //{
        //    Movie movie = new Movie { MovieId = 45 };
        //    //act
        //    var results = (ViewResult)controller.Create(movie).Result;

        //    //assert
        //    Assert.AreEqual("Create", results.ViewName);
        //}
        [TestMethod]
        public void CreateValidMovieReturnsView()
        {
            // act, passing one of the ids used in the mock db above
            var moviecreate = new Movie { MovieId = 33, rating = "3.5", duration = 22, name = "orange man" };
            var result = (RedirectToActionResult)controller.Create(moviecreate).Result;

            // assert
            Assert.AreEqual("Index", result.ActionName);
        }
        //[TestMethod]
        //public void CreateInvalidMovieReturnsMovie()
        //{
        //    // arrange - set valid id from mock db
        //    var moviecreate = new Movie { MovieId = 72, rating="", duration=4, name="hhhh" };

        //    // act, passing one of the ids used in the mock db above
        //    var result = (ViewResult)controller.Create(moviecreate).Result;
        //    var movie = (Movie)result.Model;

        //    // assert
        //    Assert.AreEqual(moviecreate, movie);
        //}

        #endregion

    }
}
