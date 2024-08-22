using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DTOs.Comments
{
    public class AddCommentDto
    {
        public string UserId { get; set; }
        public string ProductId { get; set; }
        public string Subject { get; set; }
        public string UserComment {  get; set; }
    }
}
