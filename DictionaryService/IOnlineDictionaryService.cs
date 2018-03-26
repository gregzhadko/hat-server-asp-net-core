using System.Collections.Generic;
using System.Threading.Tasks;

namespace DictionaryService
{
    public interface IOnlineDictionaryService
    {
        Task<IEnumerable<string>> GetDescriptionsAsync(string word);
    }
}