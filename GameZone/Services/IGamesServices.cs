namespace GameZone.Services
{
    public interface IGamesServices
    {
        IEnumerable<Game> GetAll();
         Task addGame(CreateGameFormViewModel model);
    }
}
