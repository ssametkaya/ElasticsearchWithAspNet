using ElasticsearchWithAspNet.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ElasticsearchWithAspNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public sealed class ValuesController : ControllerBase
    {
        AppDbContext context = new();
        [HttpGet("[action]")]
        public async Task<IActionResult> CreateData(CancellationToken cancellationToken)
        {
            IList<Travel> travels = new List<Travel>();
            var random = new Random();

            for (int i = 0; i < 10000; i++)
            {
                var title = new string(Enumerable.Repeat("abcdefgğhıjklmnoöprsştuüvyz", 5).Select(s =>s[random.Next(s.Length)]).ToArray());

                var words=new List<string>();
                for (int j = 0; j < 500; j++)
                {
                    words.Add(new string(Enumerable.Repeat("abcdefgğhıjklmnoöprsştuüvyz", 5).Select(s =>s[random.Next(s.Length)]).ToArray()));
                }

                var description = string.Join(" ", words);
                var traver = new Travel()
                {
                    Title=title,
                    Description=description
                };

                travels.Add(traver);
            }

            await context.Set<Travel>().AddRangeAsync(travels, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);

            return Ok();
        }

        [HttpGet("[action]/{description}")]
        public async Task<IActionResult> GetDataList(string description)
        {
            IList<Travel> travels = await context.Set<Travel>().Where(p => p.Description.Contains(description)).AsNoTracking().ToListAsync();
            return Ok(travels.Take(10));
        }
    }
}
