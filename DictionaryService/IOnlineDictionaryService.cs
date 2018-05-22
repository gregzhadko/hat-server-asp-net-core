using System.Collections.Generic;
using System.Threading.Tasks;

namespace DictionaryService
{
    internal interface IOnlineDictionaryService
    {
        Task<IEnumerable<string>> GetDescriptionsAsync(string word);
    }
}