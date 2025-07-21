
namespace Absolute_cinema.Controllers.Movies;

using Microsoft.AspNetCore.Mvc;
using BusinessObjects.Models;
using System.Collections.Generic;
using Common.ViewModels;
using Common.Filters.Movies;
using Common.Mappers;
using Common.ViewModels.MovieVMs;
using Services.Interfaces;

public class MoviesController : Controller
{
    // Dependency injection for services
    private readonly IMovieService _movieService;
    private readonly ITagService _tagService;

    // Constructor to inject services
    public MoviesController(IMovieService movieService, ITagService tagService)
    {
        _movieService = movieService;
        _tagService = tagService;
    }


    // Index action to display the list of movies with filters and sorting
    public IActionResult Index(
            string search = "",
            string[]? tags = null,
            string country = "",
            string quality = "",
            string year = "",
            string rating = "",
            string price = "",
            string featured = "",
            string newRelease = "",
            string sort = "newest",
            int page = 1)
    {

        // filter criteria object
        var criteria = new MovieFilterCriteria
        {
            Search = search,
            Tags = tags ?? Array.Empty<string>(),
            Country = country,
            Quality = quality,
            Year = year,
            Rating = rating,
            Price = price,
            Featured = featured == "true" ? true : null,
            NewRelease = newRelease == "true" ? true : null,
            Sort = sort,
            Page = page
        };

        // filter and sort movies based on the criteria
        var result = _movieService.FilterMovies(criteria, isAdmin : false);

        // set ViewBag properties for the view
        ViewBag.CurrentPage = result.CurrentPage;
        ViewBag.TotalPages = result.TotalPages;
        ViewBag.AllTags = GetAllTags();
        ViewBag.Criteria = criteria;
        ViewBag.PageSize = result.PageSize;

        // return view
        return View(result.Items);
    }


    // Details action to display the details of a specific movie
    public IActionResult Details(Guid id)
    {
        // TODO: CHECK ROLE -> IF ADMIN : can navigate

        MovieVM movie;
        try 
        {
            // get movie by id
            movie = _movieService.GetMovieVMById(id);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        catch (Exception)
        {
            return StatusCode(500);
        }

        // get related movies
        var relatedMovies = _movieService.GetRalatedMovies(movie);

        // set ViewBag properties for the view
        ViewBag.RelatedMovies = relatedMovies;

        // return view
        return View(movie);
    }

    // Action to get all tags for filtering
    private List<TagVM> GetAllTags()
    {
        List<Tag> tags = _tagService.GetAll().ToList();
        return tags.Select(t => TagMapper.MaptoTagVM(t)).ToList();
    }

    // Get movie information JSON
    public IActionResult GetMovieInfo(Guid movieId)
    {
        try
        {
            var movieInfo = _movieService.GetById(movieId);
            var movieDto = _movieService.MapMovieToDTO(movieInfo);
            return Json(new
            {
                status = "ok",
                msg = "ok",
                movie = movieDto
            });
        }
        catch (Exception e)
        {
            return Json(new
            {
                status = "error",
                msg = e.Message
            });
        }
    }
}


