using BusinessObjects.Models;
using Common.Constants;
using DataAccessObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.HelperServices
{
    public class DbInitializer
    {
        private readonly AbsoluteCinemaContext _context;
        private readonly IHashPasswordService _hasher;

        public DbInitializer(AbsoluteCinemaContext context, IHashPasswordService hasher)
        {
            _context = context;
            _hasher = hasher;
        }

        public void Seed()
        {
            // Apply pending migrations
            _context.Database.Migrate();

            // Seed data in order of dependencies
            SeedSeatTypes();
            SeedTags();
            SeedActors();
            SeedDirectors();
            SeedUsers();
            SeedRooms();
            SeedSeats();
            SeedMovies();
            SeedMovieRelationships();
            SeedShowtimes();
            //SeedBookings();
            //SeedPayments();
            //SeedTickets();
            SeedReviews();

            Console.WriteLine("Database seeding completed successfully!");
        }

        private void SeedSeatTypes()
        {
            if (_context.SeatTypes.Any()) return;

            var seatTypes = new List<SeatType>
            {
                new SeatType { Id = Guid.NewGuid(), Name = "Standard", PriceMutiplier = 1.0m, CreatedAt = DateTime.Now },
                new SeatType { Id = Guid.NewGuid(), Name = "Premium", PriceMutiplier = 1.5m, CreatedAt = DateTime.Now },
                new SeatType { Id = Guid.NewGuid(), Name = "VIP", PriceMutiplier = 2.0m, CreatedAt = DateTime.Now }
            };

            _context.SeatTypes.AddRange(seatTypes);
            _context.SaveChanges();
            Console.WriteLine("Seat types seeded.");
        }

        private void SeedTags()
        {
            if (_context.Tags.Any()) return;

            var tags = new List<Tag>
            {
                new Tag { Id = Guid.NewGuid(), Name = "Action", CreatedAt = DateTime.Now },
                new Tag { Id = Guid.NewGuid(), Name = "Adventure", CreatedAt = DateTime.Now },
                new Tag { Id = Guid.NewGuid(), Name = "Comedy", CreatedAt = DateTime.Now },
                new Tag { Id = Guid.NewGuid(), Name = "Drama", CreatedAt = DateTime.Now },
                new Tag { Id = Guid.NewGuid(), Name = "Horror", CreatedAt = DateTime.Now },
                new Tag { Id = Guid.NewGuid(), Name = "Romance", CreatedAt = DateTime.Now },
                new Tag { Id = Guid.NewGuid(), Name = "Sci-Fi", CreatedAt = DateTime.Now },
                new Tag { Id = Guid.NewGuid(), Name = "Thriller", CreatedAt = DateTime.Now },
                new Tag { Id = Guid.NewGuid(), Name = "Animation", CreatedAt = DateTime.Now },
                new Tag { Id = Guid.NewGuid(), Name = "Documentary", CreatedAt = DateTime.Now }
            };

            _context.Tags.AddRange(tags);
            _context.SaveChanges();
            Console.WriteLine("Tags seeded.");
        }

        private void SeedActors()
        {
            if (_context.Actors.Any()) return;

            var actors = new List<Actor>
            {
                new Actor
                {
                    Id = Guid.NewGuid(),
                    Name = "Tom Hanks",
                    AvatarURL = "https://upload.wikimedia.org/wikipedia/commons/thumb/a/a9/Tom_Hanks_TIFF_2019.jpg/800px-Tom_Hanks_TIFF_2019.jpg",
                    Dob = new DateTime(1956, 7, 9),
                    Gender = "Male",
                    Description = "Academy Award-winning actor known for his versatile performances.",
                    CreatedAt = DateTime.Now
                },
                new Actor
                {
                    Id = Guid.NewGuid(),
                    Name = "Scarlett Johansson",
                    AvatarURL = "https://upload.wikimedia.org/wikipedia/commons/thumb/6/60/Scarlett_Johansson_by_Gage_Skidmore_2_%28cropped%29.jpg/800px-Scarlett_Johansson_by_Gage_Skidmore_2_%28cropped%29.jpg",
                    Dob = new DateTime(1984, 11, 22),
                    Gender = "Female",
                    Description = "Versatile actress known for her roles in action and drama films.",
                    CreatedAt = DateTime.Now
                },
                new Actor
                {
                    Id = Guid.NewGuid(),
                    Name = "Leonardo DiCaprio",
                    AvatarURL = "https://upload.wikimedia.org/wikipedia/commons/thumb/4/46/Leonardo_Dicaprio_Cannes_2019.jpg/800px-Leonardo_Dicaprio_Cannes_2019.jpg",
                    Dob = new DateTime(1974, 11, 11),
                    Gender = "Male",
                    Description = "Academy Award-winning actor and environmental activist.",
                    CreatedAt = DateTime.Now
                },
                new Actor
                {
                    Id = Guid.NewGuid(),
                    Name = "Emma Stone",
                    AvatarURL = "https://upload.wikimedia.org/wikipedia/commons/thumb/0/0b/Emma_Stone_2_%28cropped%29.jpg/800px-Emma_Stone_2_%28cropped%29.jpg",
                    Dob = new DateTime(1988, 11, 6),
                    Gender = "Female",
                    Description = "Academy Award-winning actress known for her comedic and dramatic roles.",
                    CreatedAt = DateTime.Now
                },
                new Actor
                {
                    Id = Guid.NewGuid(),
                    Name = "Ryan Reynolds",
                    AvatarURL = "https://upload.wikimedia.org/wikipedia/commons/thumb/1/14/Deadpool_2_Japan_Premiere_Red_Carpet_Ryan_Reynolds_%28cropped%29.jpg/800px-Deadpool_2_Japan_Premiere_Red_Carpet_Ryan_Reynolds_%28cropped%29.jpg",
                    Dob = new DateTime(1976, 10, 23),
                    Gender = "Male",
                    Description = "Canadian actor known for his comedic timing and action roles.",
                    CreatedAt = DateTime.Now
                }
            };

            _context.Actors.AddRange(actors);
            _context.SaveChanges();
            Console.WriteLine("Actors seeded.");
        }

        private void SeedDirectors()
        {
            if (_context.Directors.Any()) return;

            var directors = new List<Director>
            {
                new Director
                {
                    Id = Guid.NewGuid(),
                    Name = "Christopher Nolan",
                    AvatarURL = "https://upload.wikimedia.org/wikipedia/commons/thumb/9/95/Christopher_Nolan_Cannes_2018.jpg/800px-Christopher_Nolan_Cannes_2018.jpg",
                    Dob = new DateTime(1970, 7, 30),
                    Gender = "Male",
                    Description = "Acclaimed director known for complex narratives and practical effects.",
                    CreatedAt = DateTime.Now
                },
                new Director
                {
                    Id = Guid.NewGuid(),
                    Name = "Greta Gerwig",
                    AvatarURL = "https://upload.wikimedia.org/wikipedia/commons/thumb/8/8d/Greta_Gerwig_2017.jpg/800px-Greta_Gerwig_2017.jpg",
                    Dob = new DateTime(1983, 8, 4),
                    Gender = "Female",
                    Description = "Actress-turned-director known for her unique storytelling style.",
                    CreatedAt = DateTime.Now
                },
                new Director
                {
                    Id = Guid.NewGuid(),
                    Name = "Denis Villeneuve",
                    AvatarURL = "https://upload.wikimedia.org/wikipedia/commons/thumb/7/7a/Denis_Villeneuve_2017.jpg/800px-Denis_Villeneuve_2017.jpg",
                    Dob = new DateTime(1967, 10, 3),
                    Gender = "Male",
                    Description = "Canadian filmmaker known for his visually stunning and thought-provoking films.",
                    CreatedAt = DateTime.Now
                },
                new Director
                {
                    Id = Guid.NewGuid(),
                    Name = "Chloé Zhao",
                    AvatarURL = "https://upload.wikimedia.org/wikipedia/commons/thumb/4/4a/Chloe_Zhao_2021.jpg/800px-Chloe_Zhao_2021.jpg",
                    Dob = new DateTime(1982, 3, 31),
                    Gender = "Female",
                    Description = "Academy Award-winning director known for her intimate storytelling.",
                    CreatedAt = DateTime.Now
                }
            };

            _context.Directors.AddRange(directors);
            _context.SaveChanges();
            Console.WriteLine("Directors seeded.");
        }

        private void SeedUsers()
        {
            var users = new List<User>();
            if (!_context.Users.Any())
            {
                users = new List<User>
                {
                    new User
                    {
                        Id = Guid.NewGuid(),
                        Username = "admin",
                        Email = "admin@gmail.com",
                        Password = _hasher.HashPassword("123123"),
                        Role = RoleConstants.Admin,
                        CreatedAt = DateTime.Now,
                        IsVerify = true,
                        IsActive = true,
                    },
                    new User
                    {
                        Id = Guid.NewGuid(),
                        Username = "user1",
                        Email = "user@gmail.com",
                        Password = _hasher.HashPassword("123123"),
                        Role = RoleConstants.User,
                        CreatedAt = DateTime.Now,
                        IsVerify = true,
                        IsActive = true,
                    },
                    new User
                    {
                        Id = Guid.NewGuid(),
                        Username = "user2",
                        Email = "user2@gmail.com",
                        Password = _hasher.HashPassword("123123"),
                        Role = RoleConstants.User,
                        CreatedAt = DateTime.Now,
                        IsVerify = false,
                        IsActive = true,
                    },
                    new User
                    {
                        Id = Guid.NewGuid(),
                        Username = "user3",
                        Email = "user3@gmail.com",
                        Password = _hasher.HashPassword("123123"),
                        Role = RoleConstants.User,
                        CreatedAt = DateTime.Now,
                        IsVerify = true,
                        IsActive = false,
                    }
                };
                _context.Users.AddRange(users);
                _context.SaveChanges();
                Console.WriteLine("Users seeded.");
            }

            if (!_context.UserDetails.Any())
            {
                // Seed user details
                var userDetails = new List<UserDetail>
                {
                    new UserDetail
                    {
                        Id = Guid.NewGuid(),
                        FullName = "Admin",
                        Phone = "1234567890",
                        Dob = new DateOnly(1990, 1, 1),
                        Gender = GenderConstants.MALE,
                        CreatedAt = DateTime.Now,
                        UserId = users[0].Id
                    },
                    new UserDetail
                    {
                        Id = Guid.NewGuid(),
                        FullName = "User 1",
                        Phone = "1234567890",
                        Dob = new DateOnly(1990, 1, 1),
                        Gender = GenderConstants.MALE,
                        CreatedAt = DateTime.Now,
                        UserId = users[1].Id
                    },
                    new UserDetail
                    {
                        Id = Guid.NewGuid(),
                        FullName = "User 2",
                        Phone = "1234567890",
                        Dob = new DateOnly(1990, 1, 1),
                        Gender = GenderConstants.MALE,
                        CreatedAt = DateTime.Now,
                        UserId = users[2].Id
                    }
                };
                _context.UserDetails.AddRange(userDetails);
                _context.SaveChanges();
                Console.WriteLine("User details seeded.");
            }
        }

        private void SeedRooms()
        {
            if (_context.Rooms.Any()) return;

            var rooms = new List<Room>
            {
                new Room
                {
                    Id = Guid.NewGuid(),
                    Name = "Room A",
                    Capacity = 50,
                    ScreenType = "2D",
                    Description = "Standard 2D screening room with comfortable seating.",
                    CreatedAt = DateTime.Now
                },
                new Room
                {
                    Id = Guid.NewGuid(),
                    Name = "Room B",
                    Capacity = 80,
                    ScreenType = "3D",
                    Description = "Premium 3D screening room with enhanced audio.",
                    CreatedAt = DateTime.Now
                },
                new Room
                {
                    Id = Guid.NewGuid(),
                    Name = "Room C",
                    Capacity = 120,
                    ScreenType = "IMAX",
                    Description = "Large IMAX screening room with premium experience.",
                    CreatedAt = DateTime.Now
                }
            };

            _context.Rooms.AddRange(rooms);
            _context.SaveChanges();
            Console.WriteLine("Rooms seeded.");
        }

        private void SeedSeats()
        {
            if (_context.Seats.Any()) return;

            var seatTypes = _context.SeatTypes.ToList();
            var rooms = _context.Rooms.ToList();
            var seats = new List<Seat>();

            foreach (var room in rooms)
            {
                // TODO Adjust the model
                int rows = room.Capacity / 10; // Assume 10 seats per row
                for (int row = 1; row <= rows; row++)
                {
                    for (int col = 1; col <= 10; col++)
                    {
                        var seatType = seatTypes[new Random().Next(seatTypes.Count)]; // Random seat type
                        seats.Add(new Seat
                        {
                            Id = Guid.NewGuid(),
                            RoomId = room.Id,
                            SeatTypeId = seatType.Id,
                            SeatRow = "A",
                            SeatNumber = col,
                            CreatedAt = DateTime.Now
                        });
                    }
                }
            }

            _context.Seats.AddRange(seats);
            _context.SaveChanges();
            Console.WriteLine("Seats seeded.");
        }

        private void SeedMovies()
        {
            if (_context.Movies.Any()) return;

            var movies = new List<Movie>
            {
                new Movie
                {
                    Id = Guid.NewGuid(),
                    Title = "Inception",
                    OriginalTitle = "Inception",
                    Description = "A thief who steals corporate secrets through the use of dream-sharing technology is given the inverse task of planting an idea into the mind of a C.E.O.",
                    Duration = 148,
                    ReleaseDate = new DateTime(2010, 7, 16),
                    Country = "USA",
                    PosterURL = "https://upload.wikimedia.org/wikipedia/en/2/2e/Inception_%282010%29_theatrical_poster.jpg",
                    BackgroundURL = "https://upload.wikimedia.org/wikipedia/en/2/2e/Inception_%282010%29_theatrical_poster.jpg",
                    TrailerURL = "https://www.youtube.com/watch?v=YoHD9XEInc0",
                    ImdbRating = 8.8m,
                    AgeRestriction = 13,
                    Status = "Released",
                    CreatedAt = DateTime.Now
                },
                new Movie
                {
                    Id = Guid.NewGuid(),
                    Title = "Barbie",
                    OriginalTitle = "Barbie",
                    Description = "Barbie suffers a crisis that leads her to question her world and her existence.",
                    Duration = 114,
                    ReleaseDate = new DateTime(2023, 7, 21),
                    Country = "USA",
                    PosterURL = "https://upload.wikimedia.org/wikipedia/en/0/0b/Barbie_2023_poster.jpg",
                    BackgroundURL = "https://upload.wikimedia.org/wikipedia/en/0/0b/Barbie_2023_poster.jpg",
                    TrailerURL = "https://www.youtube.com/watch?v=pBk4NYhWNMM",
                    ImdbRating = 6.9m,
                    AgeRestriction = 12,
                    Status = "Released",
                    CreatedAt = DateTime.Now
                },
                new Movie
                {
                    Id = Guid.NewGuid(),
                    Title = "Dune",
                    OriginalTitle = "Dune",
                    Description = "Feature adaptation of Frank Herbert's science fiction novel about the son of a noble family entrusted with the protection of the most valuable asset and most vital element in the galaxy.",
                    Duration = 155,
                    ReleaseDate = new DateTime(2021, 10, 22),
                    Country = "USA",
                    PosterURL = "https://upload.wikimedia.org/wikipedia/en/8/86/Dune_%282021_film%29.jpg",
                    BackgroundURL = "https://upload.wikimedia.org/wikipedia/en/8/86/Dune_%282021_film%29.jpg",
                    TrailerURL = "https://www.youtube.com/watch?v=n9xhJrPXop4",
                    ImdbRating = 8.0m,
                    AgeRestriction = 13,
                    Status = "Released",
                    CreatedAt = DateTime.Now
                },
                new Movie
                {
                    Id = Guid.NewGuid(),
                    Title = "Nomadland",
                    OriginalTitle = "Nomadland",
                    Description = "After losing everything in the Great Recession, a woman in her sixties embarks on a journey through the American West, living as a van-dwelling modern-day nomad.",
                    Duration = 107,
                    ReleaseDate = new DateTime(2020, 9, 11),
                    Country = "USA",
                    PosterURL = "https://upload.wikimedia.org/wikipedia/en/4/4e/Nomadland_poster.jpg",
                    BackgroundURL = "https://upload.wikimedia.org/wikipedia/en/4/4e/Nomadland_poster.jpg",
                    TrailerURL = "https://www.youtube.com/watch?v=6sxCFZ8_d84",
                    ImdbRating = 7.4m,
                    AgeRestriction = 16,
                    Status = "Released",
                    CreatedAt = DateTime.Now
                },
                new Movie
                {
                    Id = Guid.NewGuid(),
                    Title = "Deadpool",
                    OriginalTitle = "Deadpool",
                    Description = "A wisecracking mercenary gets experimented on and becomes immortal but ugly, and sets out to track down the man who ruined his looks.",
                    Duration = 108,
                    ReleaseDate = new DateTime(2016, 2, 12),
                    Country = "USA",
                    PosterURL = "https://upload.wikimedia.org/wikipedia/en/4/46/Deadpool_poster.jpg",
                    BackgroundURL = "https://upload.wikimedia.org/wikipedia/en/4/46/Deadpool_poster.jpg",
                    TrailerURL = "https://www.youtube.com/watch?v=ONHBaC-pfsk",
                    ImdbRating = 8.0m,
                    AgeRestriction = 18,
                    Status = "Released",
                    CreatedAt = DateTime.Now
                }
            };

            _context.Movies.AddRange(movies);
            _context.SaveChanges();
            Console.WriteLine("Movies seeded.");
        }

        private void SeedMovieRelationships()
        {
            if (_context.MovieActors.Any() || _context.MovieDirectors.Any() || _context.MovieTags.Any()) return;

            var movies = _context.Movies.ToList();
            var actors = _context.Actors.ToList();
            var directors = _context.Directors.ToList();
            var tags = _context.Tags.ToList();

            // Movie-Actor relationships
            var movieActors = new List<MovieActor>
            {
                new MovieActor { Id = Guid.NewGuid(), MovieId = movies[0].Id, ActorId = actors[0].Id, CreatedAt = DateTime.Now }, // Inception - Tom Hanks
                new MovieActor { Id = Guid.NewGuid(), MovieId = movies[1].Id, ActorId = actors[3].Id, CreatedAt = DateTime.Now }, // Barbie - Emma Stone
                new MovieActor { Id = Guid.NewGuid(), MovieId = movies[2].Id, ActorId = actors[2].Id, CreatedAt = DateTime.Now }, // Dune - Leonardo DiCaprio
                new MovieActor { Id = Guid.NewGuid(), MovieId = movies[3].Id, ActorId = actors[1].Id, CreatedAt = DateTime.Now }, // Nomadland - Scarlett Johansson
                new MovieActor { Id = Guid.NewGuid(), MovieId = movies[4].Id, ActorId = actors[4].Id, CreatedAt = DateTime.Now }, // Deadpool - Ryan Reynolds
            };

            // Movie-Director relationships
            var movieDirectors = new List<MovieDirector>
            {
                new MovieDirector { Id = Guid.NewGuid(), MovieId = movies[0].Id, DirectorId = directors[0].Id, CreatedAt = DateTime.Now }, // Inception - Christopher Nolan
                new MovieDirector { Id = Guid.NewGuid(), MovieId = movies[1].Id, DirectorId = directors[1].Id, CreatedAt = DateTime.Now }, // Barbie - Greta Gerwig
                new MovieDirector { Id = Guid.NewGuid(), MovieId = movies[2].Id, DirectorId = directors[2].Id, CreatedAt = DateTime.Now }, // Dune - Denis Villeneuve
                new MovieDirector { Id = Guid.NewGuid(), MovieId = movies[3].Id, DirectorId = directors[3].Id, CreatedAt = DateTime.Now }, // Nomadland - Chloé Zhao
            };

            // Movie-Tag relationships
            var movieTags = new List<MovieTag>
            {
                new MovieTag { Id = Guid.NewGuid(), MovieId = movies[0].Id, TagId = tags[6].Id, CreatedAt = DateTime.Now }, // Inception - Sci-Fi
                new MovieTag { Id = Guid.NewGuid(), MovieId = movies[0].Id, TagId = tags[7].Id, CreatedAt = DateTime.Now }, // Inception - Thriller
                new MovieTag { Id = Guid.NewGuid(), MovieId = movies[1].Id, TagId = tags[2].Id, CreatedAt = DateTime.Now }, // Barbie - Comedy
                new MovieTag { Id = Guid.NewGuid(), MovieId = movies[1].Id, TagId = tags[3].Id, CreatedAt = DateTime.Now }, // Barbie - Drama
                new MovieTag { Id = Guid.NewGuid(), MovieId = movies[2].Id, TagId = tags[6].Id, CreatedAt = DateTime.Now }, // Dune - Sci-Fi
                new MovieTag { Id = Guid.NewGuid(), MovieId = movies[2].Id, TagId = tags[1].Id, CreatedAt = DateTime.Now }, // Dune - Adventure
                new MovieTag { Id = Guid.NewGuid(), MovieId = movies[3].Id, TagId = tags[3].Id, CreatedAt = DateTime.Now }, // Nomadland - Drama
                new MovieTag { Id = Guid.NewGuid(), MovieId = movies[4].Id, TagId = tags[0].Id, CreatedAt = DateTime.Now }, // Deadpool - Action
                new MovieTag { Id = Guid.NewGuid(), MovieId = movies[4].Id, TagId = tags[2].Id, CreatedAt = DateTime.Now }, // Deadpool - Comedy
            };

            _context.MovieActors.AddRange(movieActors);
            _context.MovieDirectors.AddRange(movieDirectors);
            _context.MovieTags.AddRange(movieTags);
            _context.SaveChanges();
            Console.WriteLine("Movie relationships seeded.");
        }

        private void SeedShowtimes()
        {
            if (_context.Showtimes.Any()) return;

            var movies = _context.Movies.ToList();
            var rooms = _context.Rooms.ToList();
            var showtimes = new List<Showtime>();

            var basePrice = 12.00m;
            var startDate = DateTime.Now.AddDays(1);

            for (int i = 0; i < movies.Count; i++)
            {
                var movie = movies[i];
                var room = rooms[i % rooms.Count];

                // Create multiple showtimes for each movie
                for (int j = 0; j < 3; j++)
                {
                    var showtime = startDate.AddDays(j).AddHours(10 + j * 4); // 10 AM, 2 PM, 6 PM
                    showtimes.Add(new Showtime
                    {
                        Id = Guid.NewGuid(),
                        MovieId = movie.Id,
                        RoomId = room.Id,
                        StartTime = showtime,
                        EndTime = showtime.AddMinutes(movie.Duration),
                        BasePrice = basePrice + j * 2, // Different prices for different times
                        Status = true,
                        CreatedAt = DateTime.Now
                    });
                }
            }

            _context.Showtimes.AddRange(showtimes);
            _context.SaveChanges();
            Console.WriteLine("Showtimes seeded.");
        }

        private void SeedShowtimeSeats()
        {
            if (_context.ShowtimeSeats.Any()) return;

            var showtimes = _context.Showtimes.ToList();
            var seats = _context.Seats.ToList();
            var showtimeSeats = new List<ShowtimeSeat>();

            foreach (var showtime in showtimes)
            {
                var roomSeats = seats.Where(s => s.RoomId == showtime.RoomId).ToList();

                foreach (var seat in roomSeats)
                {
                    showtimeSeats.Add(new ShowtimeSeat
                    {
                        Id = Guid.NewGuid(),
                        ShowtimeId = showtime.Id,
                        SeatId = seat.Id,
                        CreatedAt = DateTime.Now
                    });
                }
            }

            _context.ShowtimeSeats.AddRange(showtimeSeats);
            _context.SaveChanges();
            Console.WriteLine("Showtime seats seeded.");
        }

        private void SeedBookings()
        {
            if (_context.Bookings.Any()) return;

            var users = _context.Users.Where(u => u.Role == RoleConstants.User).ToList();
            var showtimes = _context.Showtimes.Take(3).ToList(); // Use first 3 showtimes
            var bookings = new List<Booking>();

            foreach (var user in users)
            {
                var showtime = showtimes[users.IndexOf(user) % showtimes.Count];
                bookings.Add(new Booking
                {
                    Id = Guid.NewGuid(),
                    UserId = user.Id,
                    BookingDate = DateTime.Now.AddDays(-1),
                    NumberOfTickets = 2, // Fixed number of tickets for simplicity
                    CreatedAt = DateTime.Now
                });
            }

            _context.Bookings.AddRange(bookings);
            _context.SaveChanges();
            Console.WriteLine("Bookings seeded.");
        }

        //private void SeedPayments()
        //{
        //    if (_context.Payments.Any()) return;

        //    var bookings = _context.Bookings.ToList();
        //    var payments = new List<Payment>();

        //    foreach (var booking in bookings)
        //    {
        //        payments.Add(new Payment
        //        {
        //            Id = Guid.NewGuid(),
        //            BookingId = booking.Id,
        //            Amount = booking.Tickets.Count(),
        //            PaymentMethod = "Credit Card",
        //            PaymentDate = booking.BookingDate.AddMinutes(5),
        //            Status = "Completed",
        //            CreatedAt = DateTime.Now
        //        });
        //    }

        //    _context.Payments.AddRange(payments);
        //    _context.SaveChanges();
        //    Console.WriteLine("Payments seeded.");
        //}

        private void SeedTickets()
        {
            //if (_context.Tickets.Any()) return;

            //var bookings = _context.Bookings.ToList();
            //var showtimeSeats = _context.ShowtimeSeats.Take(6).ToList(); // Use first 6 seats
            //var tickets = new List<Ticket>();

            //foreach (var booking in bookings)
            //{
            //    // Create 2 tickets per booking
            //    for (int i = 0; i < 2; i++)
            //    {
            //        var seatIndex = (bookings.IndexOf(booking) * 2 + i) % showtimeSeats.Count;
            //        var showtimeSeat = showtimeSeats[seatIndex];

            //        tickets.Add(new Ticket
            //        {
            //            Id = Guid.NewGuid(),
            //            BookingId = booking.Id,
            //            Price = showtimeSeat.Showtime.BasePrice,
            //            CreatedAt = DateTime.Now
            //        });
            //    }
            //}

            //_context.Tickets.AddRange(tickets);
            //_context.SaveChanges();
            Console.WriteLine("Tickets seeded.");
        }

        private void SeedReviews()
        {
            if (_context.Reviews.Any()) return;

            var users = _context.Users.Where(u => u.Role == RoleConstants.User).ToList();
            var movies = _context.Movies.Take(3).ToList(); // Use first 3 movies
            var reviews = new List<Review>();

            var reviewTexts = new[]
            {
                "Amazing movie! Highly recommended.",
                "Great storyline and excellent acting.",
                "A must-watch film with stunning visuals.",
                "Good entertainment value.",
                "Interesting plot with good character development."
            };

            foreach (var user in users)
            {
                var movie = movies[users.IndexOf(user) % movies.Count];
                reviews.Add(new Review
                {
                    Id = Guid.NewGuid(),
                    UserId = user.Id,
                    MovieId = movie.Id,
                    Rating = new Random().Next(3, 6), // Random rating 3-5
                    Content = reviewTexts[new Random().Next(reviewTexts.Length)],
                    ReviewDate = DateTime.Now.AddDays(-new Random().Next(1, 30)),
                    CreatedAt = DateTime.Now
                });
            }

            _context.Reviews.AddRange(reviews);
            _context.SaveChanges();
            Console.WriteLine("Reviews seeded.");
        }
    }
}
