﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DTOs.Wishlist
{
    public class AddWishlistDto
    {
        public int ProductId { get; set; }
        public string UserId {  get; set; }
    }
}
