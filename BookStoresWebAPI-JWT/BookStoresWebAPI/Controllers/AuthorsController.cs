
using BookStoresWebAPI.Data;
using BookStoresWebAPI.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStoresWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly BookStoresDBContext _dBContext;
        public AuthorsController(BookStoresDBContext dBContext)
        {
            _dBContext = dBContext;
        }

        [HttpGet]
        public IEnumerable<Author> GetAuthors()
        {
            var authors = _dBContext.Authors.OrderBy(x => x.FirstName).ToList();
            return authors;
        }

      
    }
}
