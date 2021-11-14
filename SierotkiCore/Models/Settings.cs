using System;
using System.Collections.Generic;

namespace SierotkiCore.Models
{
    public sealed class Settings
    {
        public int Length { get; set; } = 1;
        public IEnumerable<string> Words { get; set; } = Array.Empty<string>();
        public string FilePath { get; set; } = string.Empty;
        public string FolderPath { get; set; } = string.Empty;
    }
}
