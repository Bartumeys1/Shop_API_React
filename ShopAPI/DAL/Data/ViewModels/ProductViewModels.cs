﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Data.ViewModels
{
    public class ProductUploadImageViewModels
    {
        public IFormFile Image { get; set; }
    }
}