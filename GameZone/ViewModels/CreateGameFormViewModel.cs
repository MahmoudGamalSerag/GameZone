﻿using GameZone.Attribute;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Runtime.CompilerServices;

namespace GameZone.ViewModels
{
    public class CreateGameFormViewModel
    {
        [MaxLength(250)]
        public string Name { get; set; } = string.Empty;
        [Display(Name = "Category")]
        public int CategoryId { get; set; }
        public IEnumerable<SelectListItem> Categories { get; set; } = new List<SelectListItem>();
        [Display(Name = "SupportedDevices")]
        public List<int> SelectedDevices { get; set; } = new List<int>();
        public IEnumerable<SelectListItem> Devices { get; set; } = new List<SelectListItem>();

        [MaxLength(2500)]
        public string Description { get; set; } = string.Empty;
        [AllowdExtensions(SettingFile.Extensions),
         MaxFileSize(SettingFile.MaxFileSizeInBytes)   ]
        public IFormFile Cover { get; set; } = default!;
    }
}
