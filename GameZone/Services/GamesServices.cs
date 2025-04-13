

using System.Threading.Tasks;

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
                Include(g => g.Category)
                .Include(g => g.Devices)
                .ThenInclude(d => d.Device)
                .AsNoTracking().ToList();
        }
        public Game? GetGameById(int id)
        {
            return _context.Games
                .Include(g => g.Category)
                .Include(g => g.Devices)
                .ThenInclude(d => d.Device)
                .AsNoTracking()
                .SingleOrDefault(g => g.Id == id);
        }
        public async Task addGame(CreateGameFormViewModel model)
        {
            var coverName=await UploadCover(model.Cover);
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

        public async Task<Game?> Edit(EditGameFormViewModel model)
        {
            var game = _context.Games.Include(g => g.Devices).SingleOrDefault(g => g.Id == model.Id);
            if (game is null)
            {
                return null;
            }
            var hasCover = model.Cover is not null;
            var OldCover = game.Cover;
            if (hasCover)
            {
                game.Cover = await UploadCover(model.Cover);
            }
            game.Name = model.Name;
            game.Description = model.Description;
            game.CategoryId = model.CategoryId;
            game.Devices=model.SelectedDevices.Select(d => new GameDevice
            {
                DeviceId = d
            }).ToList();
            var affectedRows = await _context.SaveChangesAsync();
            if (affectedRows > 0)
            {
                if (hasCover)
                {
                    var oldCoverPath = Path.Combine(_imagePath, OldCover);
                    if (System.IO.File.Exists(oldCoverPath))
                    {
                        System.IO.File.Delete(oldCoverPath);
                    }
                }
                return game;
            }
            else
            {
                if (hasCover)
                {
                    removeCover(game.Cover);
                }
                return null;
            }
        }
        public bool Delete(int id)
        {
            var isDeleted = false;
            var game = _context.Games.Include(g => g.Devices).SingleOrDefault(g => g.Id == id);
            if (game is null)
            {
                return isDeleted;
            }

            _context.Games.Remove(game);
            var affectedRows = _context.SaveChanges();
            if (affectedRows > 0)
            {
                removeCover(game.Cover);
                isDeleted = true;
            }
            return isDeleted;
        }
        private async Task<string> UploadCover(IFormFile cover)
        {
            var coverName = $"{Guid.NewGuid()}{Path.GetExtension(cover.FileName)}";
            var coverPath = Path.Combine(_imagePath, coverName);
            using var stream = File.Create(coverPath);
            await cover.CopyToAsync(stream);
            stream.Dispose();
            return coverName;
        }
        private void removeCover(string cover)
        {
            var coverPath = Path.Combine(_imagePath, cover);
            if (System.IO.File.Exists(coverPath))
            {
                System.IO.File.Delete(coverPath);
            }
        }

        
    }
}
