using MaximCS.API;
using MaximCS.Models;
using MaximCS.Services;
using Microsoft.AspNetCore.Mvc;

namespace MaximCS.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StringSeparatorController : ControllerBase
    {
        private readonly ISorter quickSorter = new QuickSorter();
        private readonly ISorter treeSorter = new TreeSorter();
        private readonly IConfiguration _configuration;
        private readonly IApiClient apiClient;
        private readonly SemaphoreSlim _semaphore;

        public StringSeparatorController(IConfiguration configuration)
        {
            _configuration = configuration;
            string apiUrl = _configuration["RandomApi"];
            apiClient = new RandomNumberApiClient(apiUrl); //тут можно придумать чтото интересное

            int parallelLimit = _configuration.GetValue<int>("Settings:ParallelLimit");
            _semaphore = new SemaphoreSlim(parallelLimit, parallelLimit);
        }

        [HttpGet]
        public async Task<IActionResult> Get(string input, string sortMethod)
        {
            if (!await _semaphore.WaitAsync(0))
            {
                return StatusCode(503, "Service is currently unavailable due to high load. Please try again later.");
            }

            try
            {
                ISorter sorter = sortMethod switch
                {
                    "QuickSort" => quickSorter,
                    "TreeSort" => treeSorter,
                    _ => throw new ArgumentException("Invalid sorting method")
                };

                var blacklist = _configuration.GetSection("Settings:BlackList").Get<List<string>>() ?? new List<string>();

                var result = await StringSeparator.Do(input, sorter, apiClient, blacklist);

                return Ok(new
                {
                    ProcessedString = result.ProcessedString,
                    CharCount = result.CharCount,
                    LongestVowelSubstring = result.LongestVowelSubstring,
                    SortedString = result.SortedString,
                    TrimmedString = result.TrimmedString
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            finally
            {
                _semaphore.Release();
            }
        }
    }


}
