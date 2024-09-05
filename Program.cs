class Movie {


    private static int _id = 0;
    public int Id {get; set;}

   public string Title {get; set;}
   
        
   public Movie(string title){

    Title = title;
    Id = _id;
   }


}





internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

       //register the list with the dependency injection container
        builder.Services.AddSingleton<List<Movie>>(); 

        var app = builder.Build();

        //get all movies
        app.MapGet("/movies", (List<Movie> movies) => movies);

        //adds a new movie
        app.MapPost("/movies", (Movie? movie,List<Movie> movies) => {
            if (movie == null){
                return Results.BadRequest();
            }
            movies.Add(movie);
            return Results.Created();
        });

        //delete a movie with id
        app.MapDelete("/movies/{Id}", (int Id) => $"Delete movie with id: {Id}");

        //update a movie
        app.MapPut("/movies/{Id}", (int Id) => $"update movie with id: {Id}");

        // system status
        app.MapGet("/health", () => "System healthy");

        app.Run();
    }
}