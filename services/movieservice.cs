class Movie {


    private static int _id = 0;
    public int Id {get; set;}
   public string Title {get; set;}
   
        
   public Movie(string title){

    Title = title;
    Id = _id++;
   }


}




interface IMovieservice {
    public IEnumerable<Movie> GetAllMovies(); 
    public Movie CreateMovie(Movie movie);
    public Movie? UpdateMoiveWithId(int id, Movie UpdateMovieInfo);
    public void DeleteMovieWithId(int id);
}

class MovieService : IMovieservice
 {
    private List<Movie> movies;

    public MovieService()
    {
        movies = new List<Movie>(); 

    }

    public IEnumerable<Movie> GetAllMovies()
    {
        return movies;

    }

    public Movie CreateMovie (Movie movie)
    {
        movies.Add(movie);
        
        return movie;
    }

    public Movie? UpdateMoiveWithId (int id, Movie updateMovieInfo)
    {
        // find the correct movie to update.
        var movie = movies.Find((movie)=> movie.Id == id);

        if (movie == null)
        {
            return null;
        }

        movie.Title = updateMovieInfo.Title; 

        return movie;
    }

    public void DeleteMovieWithId(int id){
        var movie = movies.Find((movie) => movie.Id == id);

        if (movie == null)
        {
            return;
        }

        movies.Remove(movie);
    }

}