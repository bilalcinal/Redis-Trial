using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace InMemoryCache.Controllers;

    [ApiController]
    [Route("api/[controller]")]
    public class ValueController : ControllerBase
    {
    readonly IMemoryCache _memoryCache;

    public ValueController(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }

    [HttpGet("set/{name}" )]
    public void SetName(string name)
    {
        _memoryCache.Set("name", name);
    }
     
    [HttpGet("getName")]
    public string GetName()
    {
         if(_memoryCache.TryGetValue<string>("name", out string name))
         {
               return name.Substring(3);
         }
         return "";
          //  return  _memoryCache.Get<string>("name");

    }
     [HttpGet("setDate")]
    public void SetDate()
    {
             _memoryCache.Set<DateTime>("data", DateTime.Now, options: new()
             {
                     AbsoluteExpiration = DateTime.Now.AddSeconds(30),
                     SlidingExpiration = TimeSpan.FromSeconds(5)
             });
    }

     [HttpGet("getDate")]
    public DateTime GetDate()
    {
         return _memoryCache.Get<DateTime>("data");
    }
}