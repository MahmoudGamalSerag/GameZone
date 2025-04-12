

namespace GameZone.Services
{
    public class GamesServices : IGamesServices
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly string _imagePath;
        public GamesServices(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _imagePath = $"{_webHostEnvironment.WebRootPath}/assets/images/games";
        }
        public IEnumerable<Game> GetAll()
        {
            return _context.Games.
                Include(g=>g.Category)
                .Include(g=>g.Devices)
                .ThenInclude(d=>d.Device)
                .AsNoTracking().ToList();
        }
        public async Task addGame(CreateGameFormViewModel model)
        {
            var coverName = $"{Guid.NewGuid()}{Path.GetExtension(model.Cover.FileName)}";
            var coverPath = Path.Combine(_imagePath, coverName);
            using var stream = File.Create(coverPath);
            await model.Cover.CopyToAsync(stream);
            stream.Dispose();
            Game game = new Game
            {
                Name = model.Name,
                Description = model.Description,
                Cover = coverName,
                CategoryId = model.CategoryId,
                Devices = model.SelectedDevices.Select(d => new GameDevice
                {
                    DeviceId = d
                }).ToList()
            };
            _context.Games.Add(game);
            await _context.SaveChangesAsync();
        }

        
    }
}
