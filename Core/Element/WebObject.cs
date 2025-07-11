using OpenQA.Selenium;

namespace Core.Element
{
    public class WebObject
    {
        public By By { get; set; }
        public string Name { get; set; }
        public WebObject(By by, string name = "")
        {
            By = by;
            Name = name;
        }
    }
}