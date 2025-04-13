

using GameZone.Services;

namespace GameZone.Controllers
{
    public class GamesController : Controller
    {
        private readonly ICategoriesServices _CategoryServices;
        private readonly IDevicesServices _DevicesServices;
        private readonly IGamesServices _GamesServices;

        public GamesController(ICategoriesServices CategoryServices, IDevicesServices devicesServices, IGamesServices gamesServices)
        {
            _CategoryServices = CategoryServices;
            _DevicesServices = devicesServices;
            _GamesServices = gamesServices;
        }
        
        public IActionResult Index()
        {
            var games = _GamesServices.GetAll();
            return View(games);
        }
        [HttpGet]
        public IActionResult Create()
        {
            CreateGameFormViewModel ViewModel = new CreateGameFormViewModel
            {
                Categories =_CategoryServices.GetCategories(),
                Devices = _DevicesServices.GetDevices()
            };
            return View(ViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateGameFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = _CategoryServices.GetCategories();
                model.Devices = _DevicesServices.GetDevices();
                return View(model);
            }
            await _GamesServices.addGame(model);
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Details(int id)
        {
            var game = _GamesServices.GetGameById(id);
            if (game is null)
            {
                return NotFound();
            }
            return View(game);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var game = _GamesServices.GetGameById(id);
            if (game is null)
            {
                return NotFound();
            }
            EditGameFormViewModel ViewModel = new EditGameFormViewModel
            {
                Id = game.Id,
                Name = game.Name,
                Description = game.Description,
                CategoryId = game.CategoryId,
                SelectedDevices = game.Devices.Select(d => d.DeviceId).ToList(),
                Categories = _CategoryServices.GetCategories(),
                Devices = _DevicesServices.GetDevices(),
                OldCover = game.Cover,
            };
            return View(ViewModel);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditGameFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = _CategoryServices.GetCategories();
                model.Devices = _DevicesServices.GetDevices();
                return View(model);
            }
            var game = await _GamesServices.Edit(model);
            if (game is null)
            {
                return BadRequest();
            }

            return RedirectToAction(nameof(Index));
        }
        
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var isDeleted = _GamesServices.Delete(id);
            if (isDeleted)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
