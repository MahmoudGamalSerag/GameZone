

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
    }
}
