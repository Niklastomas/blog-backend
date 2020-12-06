using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Data.DTO
{
    public class PostDTO
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime Published { get; set; }
        public string Image { get; set; }

        public UserDTO User { get; set; }
    }
}