using OneriPlatform.Entities;
using System.Web;

namespace OneriPlatForm.Models
{
    public class CurrentSession
    {
        public static SuggestionUsers User
        {
            get
            {
                return Get<SuggestionUsers>("login");
            }
        }
        public static void Set<T>(string key, T obj)
        {
            HttpContext.Current.Session[key] = obj;
        }
        private static T Get<T>(string key)
        {
            if (HttpContext.Current.Session[key] == null)
            {
                return (T)HttpContext.Current.Session[key];
            }
            return default(T);
        }
        public static void Remove(string key)
        {
            if (HttpContext.Current.Session[key] != null)
            {
                HttpContext.Current.Session.Remove(key);
            }
        }
        public static void Clear()
        {
            HttpContext.Current.Session.Clear();
        }
    }
}