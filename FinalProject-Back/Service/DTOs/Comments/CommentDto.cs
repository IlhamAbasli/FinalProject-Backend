using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DTOs.Comments
{
    public class CommentDto
    {
        public int Id { get; set; } 
        public string CreatedDate { get; set; }
        public string Subject { get; set; }
        public string Username { get; set; }
        public string Comment {  get; set; }
        public string ProductName { get; set; }
    }
}
