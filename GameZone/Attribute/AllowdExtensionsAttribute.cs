namespace GameZone.Attribute
{
    public class AllowdExtensionsAttribute : ValidationAttribute
    {
        private readonly string _extensions;
        public AllowdExtensionsAttribute(string extensions)
        {
            _extensions = extensions;
        }
        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
           
            var file = value as IFormFile;
            if (file is not null)
            {
            var extension = Path.GetExtension(file.FileName);
            var IsAllowd=_extensions.Split(",").Contains(extension, StringComparer.OrdinalIgnoreCase);
            if (!IsAllowd)
            {
                return new ValidationResult($"This file extension is not allowed. Allowed extensions are: {_extensions}");
            }
            }
            return ValidationResult.Success!;
        }
    }
}
