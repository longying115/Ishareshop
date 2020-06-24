using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Winner.Models;
using Winner.IRepository;

namespace Ishareshop.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productservice;
        public ProductController(IProductService productservice)
        {
            _productservice = productservice;
        }
    }
}
