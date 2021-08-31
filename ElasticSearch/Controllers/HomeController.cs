using Elasticsearch.Net;
using ElasticSearch.Models;
using Microsoft.AspNetCore.Mvc;
using Nest;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ElasticSearch.Controllers
{
    [ApiController]
    [Route("Books")]
    public class HomeController : Controller
    {
        private readonly IElasticClient _elasticClient;

        public HomeController(IElasticClient elasticClient)
        {
            _elasticClient = elasticClient;
        }

        [HttpPost("")]
        public async Task<IActionResult> Create([FromForm] BookModel model)
        {
            var result = await _elasticClient.LowLevel.IndexAsync<StringResponse>("books", model.Id, PostData.Serializable(model));
            return Created("", result.Body);
        }

        [HttpPut("")]
        public async Task<IActionResult> Edit([FromForm] BookModel model)
        {
            var result = await _elasticClient.LowLevel.IndexAsync<StringResponse>("books", model.Id, PostData.Serializable(model));
            if (!result.Success)
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            var result = await _elasticClient.LowLevel.DeleteAsync<StringResponse>("books", id);
            if (!result.Success)
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpGet("")]
        public IEnumerable<BookModel> GetAll()
        {
            var result = _elasticClient.LowLevel.Search<StringResponse>("books", PostData.Serializable(new { from = 0, size = 100 }));

            var response = JsonConvert.DeserializeObject<ResponseBookModel>(result.Body);
            var books = response.Hits.Info;
            ICollection<BookModel> responseResult = new List<BookModel>();

            foreach (var book in books)
            {
                responseResult.Add(book.Book);
            }
            return responseResult;
        }
        [HttpPost("Search")]
        public IEnumerable<BookModel> Search([FromForm] SearchModel model)
        {
            var result = _elasticClient.Search<BookModel>(settings =>
                settings.Size(100)
                .Query(q => q
                            .Match(m => m
                                .Field(f => f.Category)
                                .Query(model.Category)
                                .Operator(Operator.Or)
                            ) && q
                            .Match(m => m
                                .Field(f => f.Title)
                                .Query(model.Title)
                            ) && q
                            .Match(m => m
                                .Field(f => f.Author)
                                .Query(model.Author)
                            ) && q
                            .Range(r => r
                                .Field(f => f.CountPages)
                                .GreaterThanOrEquals(model.FirstCountPages)
                                .LessThanOrEquals(model.LastCountPages)
                            ) && q
                            .DateRange(dr => dr
                                .Field(f => f.Date)
                                .GreaterThanOrEquals(model.FirstDate)
                                .LessThanOrEquals(model.LastDate)
                            ) 
                )
            );
            return result.Documents;
        }

        [HttpPost("Search/{text}")]
        public IEnumerable<BookModel> SearchByText([FromRoute]string text)
        {
            var result = _elasticClient.Search<BookModel>(settings =>
                settings.Size(100)
                .Query(q => q
                    .MultiMatch(m => m.Query(text)
            )));
            return result.Documents;
        }
    }
}
