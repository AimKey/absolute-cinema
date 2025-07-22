using BusinessObjects.Models;
using Common.DTOs.MovieDTOs;
using Common.Filters.Movies;
using Common.Pagination;
using Common.ViewModels;

namespace Services.Interfaces;

public interface IMovieService
{
    // common CRUD operations
    IEnumerable<Movie> GetAll();
    Movie GetById(Guid id);
    void Add(Movie movie);
    void Update(Movie movie);
    void Delete(Guid id);
    void Delete(Movie movie);

    // enhanced operations
    PagedResult<MovieVM> FilterMovies(MovieFilterCriteria criteriam, bool isAdmin);
    MovieVM GetMovieVMById(Guid id);
    List<MovieVM> GetRalatedMovies(MovieVM movie);
    void AddNewMovie(MovieDTO movie);
    void UpdateMovie(UpdateMovieDTO updateMovieDTO);
    List<Showtime> GetMovieFutureShowtime(Guid movieId);
    MovieDTO MapMovieToDTO(Movie movieInfo);
}