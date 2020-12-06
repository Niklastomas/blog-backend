using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.API.ViewModels
{
    public class PostViewModel
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string Image { get; set; }

        public string UserId { get; set; }
    }
}