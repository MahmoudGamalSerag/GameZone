namespace GameZone.Services
{
    public interface ICategoriesServices
    {
        public IEnumerable<SelectListItem> GetCategories();
    }
}
