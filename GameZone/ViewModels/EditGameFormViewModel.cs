namespace GameZone.ViewModels
{
    public class EditGameFormViewModel: GameFormViewModel
    {
        public int Id { get; set; }
        public string? OldCover { get; set; } = string.Empty;

        [AllowdExtensions(SettingFile.Extensions),
         MaxFileSize(SettingFile.MaxFileSizeInBytes)]
        public IFormFile? Cover { get; set; } = default!;
    }
    
}
