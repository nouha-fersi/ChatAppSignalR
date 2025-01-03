using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatModels.DTOs
{
    public class RequestChatDTO
    {
        public string? SenderId { get; set; }
        public string? ReceiverId { get; set; }
    }
}
