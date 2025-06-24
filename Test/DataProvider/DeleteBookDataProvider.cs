using Test.Const;
using Test.DataModels;

using static Core.Utils.JsonFileUtils;

namespace Test.DataProvider
{
    public class DeleteBookDataProvider
    {
         private static Dictionary<string, DeleteBookData> _registerData;

        public static Dictionary<string, DeleteBookData> LoadDeleteBookDataFile()
        {
            if (_registerData == null || _registerData.Count == 0)
            {
                _registerData = JsonFileUltils.ReadJsonAndParse<Dictionary<string, DeleteBookData>>(
                    // FilePathConst.REGISTER_DATA.GetAbsolutePath());
                    DataFilePathConst.DELETE_BOOK_DATA);
            }
            return _registerData;
        }
        public static IEnumerable<DeleteBookData> ValidData()
        {
            yield return LoadDeleteBookDataFile()["DeleteBook_01"];
        }
    }
}