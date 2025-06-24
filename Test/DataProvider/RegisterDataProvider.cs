using Test.Const;
using Test.DataModels;

using static Core.Utils.JsonFileUtils;

namespace Test.DataProvider
{
    public class RegisterDataProvider
    {
        private static Dictionary<string, RegisterData> _registerData;

        public static Dictionary<string, RegisterData> LoadAccountDataFile()
        {
            if (_registerData == null || _registerData.Count == 0)
            {
                _registerData = JsonFileUltils.ReadJsonAndParse<Dictionary<string, RegisterData>>(
                    // FilePathConst.REGISTER_DATA.GetAbsolutePath());
                    DataFilePathConst.REGISTER_DATA);
            }
            return _registerData;
        }
        public static IEnumerable<RegisterData> ValidAllFieldsRegister()
        {
            yield return LoadAccountDataFile()["valid_all_fields_register_01"];
        }
        public static IEnumerable<RegisterData> ValidRequiredFieldsRegister()
        {
            yield return LoadAccountDataFile()["valid_required_fields_register_01"];
        }
    } 
}