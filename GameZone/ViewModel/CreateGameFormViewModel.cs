using Microsoft.AspNetCore.Mvc.Rendering;

namespace GameZone.ViewModel
{
    public class CreateGameFormViewModel
    {
        [MaxLength(250)]
        public string Name { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public IEnumerable<SelectListItem> Categories { get; set; } = new List<SelectListItem>();
        public List<int> SelectedDevices { get; set; } = new List<int>();
        public IEnumerable<SelectListItem> Devices { get; set; } = new List<SelectListItem>();

        [MaxLength(2500)]
        public string Description { get; set; } = string.Empty;
        public IFormFile Cover { get; set; } = default!;
    }
}
