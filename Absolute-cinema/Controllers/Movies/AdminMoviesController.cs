using BusinessObjects.Models;
using Common;
using Common.DTOs.MovieDTOs;
using Common.Filters.Movies;
using Common.Mappers;
using Common.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace Absolute_cinema.Controllers.Movies;

[Route("Admin/Movies")]
public class AdminMoviesController : Controller
{
    private readonly IMovieService _movieService;
    private readonly ITagService _tagService;
    private readonly IDirectorService _directorService;
    private readonly IActorService _actorService;

    public AdminMoviesController(
        IMovieService movieService, 
        ITagService tagService, 
        IDirectorService directorService, 
        IActorService actorService)
    {
        _movieService = movieService;
        _tagService = tagService;
        _directorService = directorService;
        _actorService = actorService;
    }

    // GET: /Admin/Movies
    [Route("")]
    [Route("Index")]
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
        var result = _movieService.FilterMovies(criteria);

        // set ViewBag properties for the view
        ViewBag.CurrentPage = result.CurrentPage;
        ViewBag.TotalPages = result.TotalPages;
        ViewBag.AllTags = GetAllTags();
        ViewBag.Criteria = criteria;
        ViewBag.TotalCount = result.TotalCount;
        ViewBag.PageSize = result.PageSize;

        // stats viewbags
        var allmovies = _movieService.GetAll();
        ViewBag.OverallTotalTags = allmovies.Count();
        ViewBag.OverallActiveTags = allmovies.Count(t => t.RemovedAt == null);
        ViewBag.OverallDeletedTags = allmovies.Count(t => t.RemovedAt != null);
        ViewBag.OverallMonthAddedTags = allmovies.Count(t => t.CreatedAt.HasValue && t.CreatedAt.Value.Month == DateTime.Now.Month && t.CreatedAt.Value.Year == DateTime.Now.Year);


        // return view
        return View("~/Views/Admin/Movies/Index.cshtml", result.Items);
    }

    // GET: /Admin/Movies/Create
    [Route("Create")]
    public IActionResult Create()
    {
        // Tạo DTO rỗng cho form tạo mới
        var movieDto = new MovieDTO
        {
            ReleaseDate = DateTime.Now,
            ImdbRating = 0,
            Price = 0,
            Status = "Active"
        };

        // Lấy danh sách tags để hiển thị trong form
        SetUpViewBagForMovieForm();

        return View("~/Views/Admin/Movies/Create.cshtml", movieDto);
    }

    // POST: /Admin/Movies/Create
    [HttpPost]
    [Route("Create")]
    [ValidateAntiForgeryToken]
    public IActionResult Create(MovieDTO movieDto)
    {
        if (ModelState.IsValid)
        {
            try
            {
                // TODO: Implement movie creation logic
                _movieService.AddNewMovie(movieDto);

                // For now, just redirect with success message
                SetTempMessage($"Phim '{movieDto.Title}' đã được thêm thành công!", StatusConstants.Success);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Log error
                SetTempMessage("Có lỗi xảy ra khi thêm phim. Vui lòng thử lại.", StatusConstants.Error);
                ModelState.AddModelError("", ex.Message);
            }
        }

        // Nếu có lỗi, load lại form với dữ liệu
        SetUpViewBagForMovieForm();

        return View("~/Views/Admin/Movies/Create.cshtml", movieDto);
    }

    // GET: /Admin/Movies/Edit/{id}
    [Route("Edit/{id:guid}")]
    public IActionResult Edit(Guid id)
    {
        var movie = _movieService.GetById(id);
        if (movie == null)
        {
            SetTempMessage("Không tìm thấy phim.", StatusConstants.Error);
            return RedirectToAction("Index");
        }

        // Chuyển đổi Movie sang DTO để edit
        var movieDto = MovieMapper.MapToUpdateMovieDTO(movie);
        movieDto.SelectedTags = movie.MovieTags.Select(mt => mt.Tag.Name).ToList();
        movieDto.SelectedActorIds = movie.MovieActors.Select(ma => ma.Actor.Id).ToList();
        movieDto.SelectedDirectorIds = movie.MovieDirectors.Select(md => md.Director.Id).ToList();

        // Set ViewBag
        SetUpViewBagForMovieForm();

        return View("~/Views/Admin/Movies/Edit.cshtml",movieDto);
    }

    [HttpPost]
    [Route("Edit/{id:guid}")]
    public IActionResult Edit(UpdateMovieDTO updateMovieDTO)
    {
        if (ModelState.IsValid)
        {
            try
            {
                // TODO: Implement movie creation logic
                _movieService.UpdateMovie(updateMovieDTO);

                // For now, just redirect with success message
                SetTempMessage($"Phim '{updateMovieDTO.Title}' đã được cập nhật thành công!", StatusConstants.Success);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Log error
                SetTempMessage("Có lỗi xảy ra khi cập nhật phim. Vui lòng thử lại.", StatusConstants.Error);
                ModelState.AddModelError("", ex.Message);
            }
        }

        // Nếu có lỗi, load lại form với dữ liệu
        SetUpViewBagForMovieForm();
        return View("~/Views/Admin/Movies/Edit.cshtml", updateMovieDTO);
    }


    [HttpPost]
    [Route("Delete/{id:guid}")]
    public IActionResult Delete(Guid id)
    {
        try
        {
            var movie = _movieService.GetById(id);
            if (movie == null)
            {
                SetTempMessage("Không tìm thấy phim.", StatusConstants.Error);
                return RedirectToAction("Index");
            }

            // check movie have showtime or not
            if (movie.Showtimes.Any())
            {
                SetTempMessage($"Phim '{movie.Title}' không thể xóa vì đã có lịch chiếu.", StatusConstants.Warning);
                return RedirectToAction("Index");
            }

            // delete movie
            _movieService.Delete(id);
            SetTempMessage($"Phim '{movie.Title}' đã được xóa thành công!", StatusConstants.Success);
        }
        catch (Exception)
        {
            SetTempMessage($"Có lỗi xảy ra khi xóa phim", StatusConstants.Error);
        }

        return RedirectToAction("Index");
    }

    // Action to get all tags for filtering
    private List<TagVM> GetAllTags()
    {
        List<Tag> tags = _tagService.GetAll().Where(t => t.RemovedAt == null).ToList();
        return tags.Select(t => TagMapper.MaptoTagVM(t)).ToList();
    }

    private List<ActorVM> GetAllActors()
    {
        List<Actor> actors = _actorService.GetAll().Where(a => a.RemovedAt == null).ToList();
        return actors.Select(a => ActorMapper.MaptoActorVM(a)).ToList();
    }

    private List<DirectorVM> GetAllDirectors()
    {
        List<Director> directors = _directorService.GetAll().Where(d => d.RemovedAt == null).ToList();
        return directors.Select(d => DirectorMapper.MaptoDirectorVM(d)).ToList();
    }

    private void SetUpViewBagForMovieForm()
    {
        // Set ViewBag
        ViewBag.AllTags = GetAllTags();
        ViewBag.AllActors = GetAllActors();
        ViewBag.AllDirectors = GetAllDirectors();
    }

    private void SetTempMessage(string message, string messageType)
    {
        TempData["msg"] = message;
        TempData["msgType"] = messageType;
    }
}
