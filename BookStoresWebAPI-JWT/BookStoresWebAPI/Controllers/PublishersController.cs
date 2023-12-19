using BookStoresWebAPI.Data;
using BookStoresWebAPI.Dtos;
using BookStoresWebAPI.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace BookStoresWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublishersController : ControllerBase
    {
        private readonly BookStoresDBContext _dBContext;

        public PublishersController(BookStoresDBContext dBContext)
        {
            _dBContext = dBContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Publisher>>>GetPublishers()
        {
            var publishers =await _dBContext.Publishers.ToListAsync();
            return Ok(publishers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Publisher>>>GetPublisher(int id)
        {
            var publisher = await _dBContext.Publishers.Where(x => x.PubId == id).SingleOrDefaultAsync();
            if (publisher is null) return NotFound($"{id} Publisher is not found!");

            return Ok(publisher);
        }

        [HttpGet("GetPublisherDetails/{id}")]
    
        public async Task<ActionResult<IEnumerable<Publisher>>> GetPublisherDetails(int id)
        {

            // Eager Loading (One to Many relational data)
            var publisher = await _dBContext.Publishers.Where(x => x.PubId == id)
                        .Include(x => x.Books)
                            .ThenInclude(z=>z.Sales)
                        .Include(y=>y.Users)
                        .ThenInclude(t=>t.Role)
                        .SingleOrDefaultAsync();
            if (publisher is null) return NotFound($"{id} Publisher is not found!");

            return Ok(publisher);
        }

        [HttpGet("GetPublisherBooksAndUsers/{id}")]
        public async Task<ActionResult<Publisher>>GetPublisherBooksAndUsers(int id)
        {
            var publisher = await _dBContext.Publishers.Where(x => x.PubId == id).SingleOrDefaultAsync();

            // Explicit Loading

            _dBContext.Entry(publisher)
                .Collection(pub => pub.Users)
                .Load();

            _dBContext.Entry(publisher)
                      .Collection(pub => pub.Books)
                      .Query()
                      .Where(book => book.Notes.Contains("a"))
                      .Include(book => book.Sales)
                      .Load();


            

            return publisher;

           
        }

        [HttpPut("{id}")]
        public async Task<IActionResult>UpdatePublisher(int id, Publisher publisher)
        {
            var existingPublisher = await _dBContext.Publishers.FindAsync(id);
            if (existingPublisher is null) return NotFound();

            existingPublisher.PublisherName = publisher.PublisherName == default ? existingPublisher.PublisherName : publisher.PublisherName;
            existingPublisher.Country = publisher.Country == default ? existingPublisher.Country : publisher.Country;
            existingPublisher.City = publisher.City == default ? existingPublisher.City : publisher.City;
            existingPublisher.State = publisher.State == default ? existingPublisher.State : publisher.State;

            await _dBContext.SaveChangesAsync();
            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> AddPublisher([FromBody] Publisher publisher)
        {
            await _dBContext.Publishers.AddAsync(publisher);
            await _dBContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetPublisher), new { id = publisher.PubId},publisher);
        }


        [HttpPost("AddPublisherDetails")]
        public async Task<IActionResult> AddPublisherDetails([FromBody] PublisherBookSaleDto publisherBookSale)
        {
            var publisher = new Publisher
            {
                PublisherName = publisherBookSale.PublisherName,
                City = publisherBookSale.City,
                State = publisherBookSale.State,
                Country = publisherBookSale.Country
            };

            await _dBContext.Publishers.AddAsync(publisher);
            await _dBContext.SaveChangesAsync();

            var publisherBook = new Book
            {
                Title = publisherBookSale.Title,
                Type = publisherBookSale.Type,
                PubId = publisher.PubId,
                Price = publisherBookSale.Price,
                Advance = publisherBookSale.Advance,
                Royalty = publisherBookSale.Royalty,
                YtdSales = publisherBookSale.YtdSales,
                Notes = publisherBookSale.Notes,
                PublishedDate = publisherBookSale.PublishedDate      
            };
            await _dBContext.Books.AddAsync(publisherBook);

            var bookSale = new Sale
            {
                StoreId = "6380",
                OrderNum = publisherBookSale.OrderNum,
                OrderDate = DateTime.Now,
                Quantity = publisherBookSale.Quantity,
                PayTerms = publisherBookSale.PayTerms,
                BookId = publisherBook.BookId
            };

            publisherBook.Sales.Add(bookSale);
            await _dBContext.SaveChangesAsync();
            return NoContent();



        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePublisher(int id)
        {
            var exitsingPublisher = await _dBContext.Publishers.SingleOrDefaultAsync(x=>x.PubId==id);

            if (exitsingPublisher is null) return NotFound();

            _dBContext.Publishers.Remove(exitsingPublisher);
            await _dBContext.SaveChangesAsync();
            return NoContent();

        }
    }
}
