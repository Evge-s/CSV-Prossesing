using System;
using System.ComponentModel.DataAnnotations;

namespace CSVProssesing.Models.File
{
    public class FileCSV
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Path { get; set; }
        public DateTime UploadDate { get; set; }
    }
}
