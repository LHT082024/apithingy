
internal class Program
{
    //en metode som e void betyr at det er en metode som ikke skal retunere en verdi.
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

       //register the list with the dependency injection container
        builder.Services.AddSingleton<IMovieservice, MovieService>(); 

        var app = builder.Build();

        //get all movies
        app.MapGet("/movies", (IMovieservice movieservice) =>{
            return movieservice.GetAllMovies();
        } );

        //adds a new movie
        app.MapPost("/movies", (Movie movie,List<Movie> movies) => 
        {
            if (movie == null){
                return Results.BadRequest();
            }
            movies.Add(movie);
            return Results.Created();
        });

        //delete a movie with id
        app.MapDelete("/movies/{Id}", (int Id, List<Movie> movies) => 
        {
        
            var movie = movies.Find((movie) => movie.Id == Id);

            if (movie == null)
            {
                return Results.NotFound();
            }

            movies.Remove(movie);

            return Results.Ok(); 

        });

        //update a movie
        app.MapPut("/movies/{Id}", (int Id) => $"update movie with id: {Id}");

        // system status
        app.MapGet("/health", () => "System healthy");

        app.Run();
    }
}