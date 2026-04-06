namespace FilmDiary.API.Models
{
    public class Actor
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public List<FilmActor> FilmActors { get; set; } = new();
    }
}