using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Blog.Model.Entities
{
    public class Post : IEntityBase
    {
        public string Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        [MaxLength(100)]
        public DateTime Published { get; set; }

        [Required]
        [MaxLength(255)]
        public string Image { get; set; }

        public User User { get; set; }
        public string UserId { get; set; }
    }
}