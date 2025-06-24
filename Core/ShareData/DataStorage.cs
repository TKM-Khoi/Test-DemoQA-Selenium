namespace Core.ShareData
{
    public class DataStorage
    {
        public static string HAS_CREATED_BOOK = "hasCreatedBook";
        public static string CREATED_BOOK_TOKEN(string isbn="", string userId="", string token="")
            => $"createdBookToken-{isbn}{userId}{token}";
        public static string CREATED_BOOK_UNAME_PASS(string isbn="", string userId="", string username="", string password="")
            => $"createdBookUnameAndPass-{isbn}{userId}{username}{password}";
        private static AsyncLocal<Dictionary<string, object>> _data = new AsyncLocal<Dictionary<string, object>>();
        // private static ThreadLocal<Dictionary<string, object>> _data2 = new ThreadLocal<Dictionary<string, object>>();
        public static void InitData()
        {
            _data.Value = new Dictionary<string, object>();
            // _data2.Value = new Dictionary<string, object>();
        }
        public static void SetData(string key, object value)
        {
            _data.Value.Add(key, value);
            // _data2.Value.Add(key, value);
        }
        public static object GetData(string key)
        {
            if (!_data.Value.ContainsKey(key))
            {
                return null;
            }
            return _data.Value.GetValueOrDefault(key);
        }
        public static Dictionary<string, object> GetAllData() => _data.Value;
        public static void ClearData()
        {
            if (_data.Value is not null)
            {
                _data.Value.Clear();
            }
            // if (_data2.Value is not null)
            // {
            //     _data2.Value.Clear();
            // }
        }

    }
}