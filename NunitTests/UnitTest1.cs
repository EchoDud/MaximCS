using MaximCS.API;
using MaximCS.Models;
using MaximCS.Services;

namespace NunitTests
{
    public class Tests
    {
        private ISorter quickSorter;
        private ISorter treeSorter;
        private IApiClient apiClient;

        [SetUp]
        public void Setup()
        {
            quickSorter = new QuickSorter();
            treeSorter = new TreeSorter();
            apiClient = new FakeApiClient();//ƒл€ тестов наверно лучше не зависить от чегото, но € бы лучше использовал моки(но это отдельна€ библиотека)
        }

        [Test]
        public async Task SeparationStringEven()
        {
            var result = await MaximCS.Services.StringSeparator.Do("abcd", quickSorter, apiClient, new List<string>());
            Assert.That(result.ProcessedString, Is.EqualTo("badc"));
        }

        [Test]
        public async Task SeparationStringOdd()
        {
            var result = await MaximCS.Services.StringSeparator.Do("abcde", quickSorter, apiClient, new List<string>());
            Assert.That(result.ProcessedString, Is.EqualTo("edcbaabcde"));
        }

        [Test]
        public async Task CharacterFilter()
        {
            var ex = Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await StringSeparator.Do("abc1!Ww¬в*", quickSorter, apiClient, new List<string>());
            });

            Assert.That(ex.Message, Is.EqualTo("Invalid characters in input: 1!W¬в*"));
        }

        [Test]
        public async Task QuantityOfChar()
        {
            var result = await MaximCS.Services.StringSeparator.Do("abcde", quickSorter, apiClient, new List<string>());

            Assert.That(result.CharCount['a'], Is.EqualTo(2));
            Assert.That(result.CharCount['b'], Is.EqualTo(2));
            Assert.That(result.CharCount['c'], Is.EqualTo(2));
            Assert.That(result.CharCount['d'], Is.EqualTo(2));
            Assert.That(result.CharCount['e'], Is.EqualTo(2));
        }

        [Test]
        public async Task SearchForVowelSubstring()
        {
            var result = await MaximCS.Services.StringSeparator.Do("abcde", quickSorter, apiClient, new List<string>());

            Assert.That(result.LongestVowelSubstring, Is.EqualTo("edcbaabcde"));
        }

        [Test]
        public async Task QuickSortedString()
        {
            var result = await MaximCS.Services.StringSeparator.Do("abcde", quickSorter, apiClient, new List<string>());

            Assert.That(result.SortedString, Is.EqualTo("aabbccddee"));
        }

        [Test]
        public async Task TreeSortedString()
        {
            var result = await MaximCS.Services.StringSeparator.Do("abcde", treeSorter, apiClient, new List<string>());

            Assert.That(result.SortedString, Is.EqualTo("aabbccddee"));
        }

        public class FakeApiClient : IApiClient
        {
            public Task<int> GetRandomNumberAsync(int maxValue)
            {
                return Task.FromResult(0);
            }
        }
    }
}