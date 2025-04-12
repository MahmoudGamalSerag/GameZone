namespace GameZone.Attribute
{
    public class MaxFileSizeAttribute : ValidationAttribute
    {
        private readonly int _maxSize;
        public MaxFileSizeAttribute(int maxSize)
        {
            _maxSize = maxSize;
        }
        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            var file = value as IFormFile;
            if (file is not null)
            {
                if (file.Length > _maxSize)
                {
                    return new ValidationResult($"File size exceeds the maximum limit of {_maxSize} bytes.");
                }
            }
            return ValidationResult.Success!;
        }
    }
}
