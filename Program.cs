
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
        app.MapPost("/movies", (Movie? movie, IMovieservice movieservice) => 
        {
            if (movie == null){
                return Results.BadRequest();
            } 
            var createdMovie = movieservice.CreateMovie(movie);

            return Results.Created($"/movies/{createdMovie.Id}", createdMovie); 
            
        });

        //delete a movie with id
        app.MapDelete("/movies/{Id}", (int Id, IMovieservice movieservice) => 
        {
        
          movieservice.DeleteMovieWithId(Id); 

            return Results.Ok(); 

        }); 

        //update a movie
        app.MapPut("/movies/{Id}", (int Id, Movie updatedMovie, IMovieservice movieservice)=>
        {
            if (updatedMovie == null)
            {
                return Results.BadRequest();
            }
            var movie = movieservice.UpdateMoiveWithId(Id, updatedMovie); 

            if (movie == null)
            {
                return Results.NotFound();   
            }

            return Results.Ok(movie);

        });
    }
}