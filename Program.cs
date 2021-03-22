using System;
using NLog.Web;
using System.IO;
using System.Linq;

namespace MediaLibrary
{
    class Program
    {
        // create static instance of Logger
        private static NLog.Logger logger = NLogBuilder.ConfigureNLog(Directory.GetCurrentDirectory() + "\\nlog.config").GetCurrentClassLogger();
        static void Main(string[] args)
        {

            logger.Info("Program started");

            string movies = "movies.scrubbed.csv";

            MovieFile movieFile = new MovieFile(movies);

            string choice = "";
            do
            {
                Console.WriteLine("1) Add Movie");
                Console.WriteLine("2) Display All Movies");
                Console.WriteLine("3) Find a Movie");
                Console.WriteLine("Press Enter to quit.");
                choice = Console.ReadLine();

                if(choice == "1"){
                    Movie movie = new Movie();
                    Console.WriteLine("Enter movie title");
                    movie.title = Console.ReadLine();
                    if (movieFile.isUniqueTitle(movie.title))
                    {
                        string input;
                        do
                        {
                            Console.WriteLine("Enter genre (or done to quit)");
                            input = Console.ReadLine();
                            if (input != "done" && input.Length > 0)
                            {
                                movie.genres.Add(input);
                            }
                        } while (input != "done");
                        if (movie.genres.Count == 0)
                        {
                            movie.genres.Add("(no genres listed)");
                        }
                        Console.WriteLine("Enter movie director");
                        input = Console.ReadLine();
                        movie.director = input.Length == 0 ? "unassigned" : input;
                        Console.WriteLine("Enter running time (h:m:s)");
                        input = Console.ReadLine();
                        movie.runningTime = input.Length == 0 ? new TimeSpan(0) : TimeSpan.Parse(input);
                        movieFile.AddMovie(movie);
                    }
                    else
                    {
                        Console.WriteLine("Movie title already exists\n");
                    }


                } else if (choice == "2")
                {
                    // display movies
                    foreach (Movie m in movieFile.Movies)
                    {
                        Console.WriteLine(m.Display());
                    }
                } else if (choice == "3"){
                    //find movie
                    Console.Write("Enter the name of Movie: ");
                    string input = Console.ReadLine();

                    //display amount
                    var movieCount = movieFile.Movies.Where(m => m.title.Contains(input)).Count();
                    Console.WriteLine("Matching Results: " + movieCount);
                    
                    //display movies
                    var titles = movieFile.Movies.Where(m => m.title.Contains(input)).Select(m => m.title);
                    foreach(string t in titles)
                    {
                        Console.WriteLine($"  {t}");
                    }


                }



            } while (choice == "1" || choice == "2" || choice == "3");

            
            // Movie movie = new Movie
            // {
            //     mediaId = 123,
            //     title = "Greatest Movie Ever, The (2020)",
            //     director = "Jeff Grissom",
            //     // timespan (hours, minutes, seconds)
            //     runningTime = new TimeSpan(2, 21, 23),
            //     genres = { "Comedy", "Romance" }
            // };

            // Console.WriteLine(movie.Display());

            // Album album = new Album
            // {
            //     mediaId = 321,
            //     title = "Greatest Album Ever, The (2020)",
            //     artist = "Jeff's Awesome Band",
            //     recordLabel = "Universal Music Group",
            //     genres = { "Rock" }
            // };
            // Console.WriteLine(album.Display());

            // Book book = new Book
            // {
            //     mediaId = 111,
            //     title = "Super Cool Book",
            //     author = "Jeff Grissom",
            //     pageCount = 101,
            //     publisher = "",
            //     genres = { "Suspense", "Mystery" }
            // };
            // Console.WriteLine(book.Display());

            logger.Info("Program ended");
        }
    }
}
