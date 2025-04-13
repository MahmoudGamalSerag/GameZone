
namespace GameZone.ViewModels
{
    public class CreateGameFormViewModel:GameFormViewModel
    {
        
        [AllowdExtensions(SettingFile.Extensions),
         MaxFileSize(SettingFile.MaxFileSizeInBytes)   ]
        public IFormFile Cover { get; set; } = default!;
    }
}
