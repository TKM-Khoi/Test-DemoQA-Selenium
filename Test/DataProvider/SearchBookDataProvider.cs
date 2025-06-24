using Test.Const;

using static Core.Utils.JsonFileUtils;

namespace Test.DataProvider
{
    public class SearchBookDataProvider
    {
        private static Dictionary<string, string> _registerData;

        public static IEnumerable<string> LoadAccountDataFile(string key)
        {
            if (_registerData == null || _registerData.Count == 0)
            {
                _registerData = JsonFileUltils.ReadJsonAndParse<Dictionary<string, string>>(
                    DataFilePathConst.SEARCH_BOOK_DATA);
            }
            foreach (KeyValuePair<string, string> data in _registerData)
            {
                if (data.Key.ToLower().Contains(key.ToLower()))
                {
                    string value = data.Value;
                    yield return value; 
                }
            }
            // return _registerData;
        }
        public static IEnumerable<string> ValidSearch()
        {
            return LoadAccountDataFile("valid_search");
        }
        public static IEnumerable<string> EmptySearch()
        {
            return LoadAccountDataFile("empty_search");
        }
    }
}