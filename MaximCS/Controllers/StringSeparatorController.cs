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
        private readonly RandomNumberApiClient apiClient = new RandomNumberApiClient("https://www.randomnumberapi.com/api/v1.0/random");

        [HttpGet]
        public async Task<IActionResult> Get(string input, string sortMethod)
        {
            try
            {
                ISorter sorter = sortMethod switch
                {
                    "QuickSort" => quickSorter,
                    "TreeSort" => treeSorter,
                    _ => throw new ArgumentException("Invalid sorting method")
                };

                var result = await StringSeparator.Do(input, sorter, apiClient);

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
        }
    }
}
