﻿using System.Windows.Media.Imaging;

namespace CinemaMagic.Models.DTOs
{
    public class ErrorDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Staff_Id { get; set; }
        public string DateAdded { get; set; }
        public string Status { get; set; }
        public string EndDate { get; set; }
        public string Cost { get; set; }
        public BitmapImage Image { get; set; }

        public System.Windows.Media.Brush StatusColor { get; set; }
    }
}
