using System.Collections.Generic;
using System.Web.Mvc.Html;

namespace System.Web.Mvc
{
    public static class ListExtensions
    {
        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> collection, Action<T> action)
        {
            foreach (var item in collection) action(item);
            return collection;
        }

        public static MvcHtmlString DropDownListForString(this HtmlHelper helper, string name, IEnumerable<string> collection)
        {
            return DropDownListForString(helper, name, collection, null);
        }

        public static MvcHtmlString DropDownListForString(this HtmlHelper helper, string name, IEnumerable<string> collection, string optionLabel)
        {
            return helper.DropDownList(name, ToSelectList(collection), optionLabel);
        }

        public static MvcHtmlString DropDownListForString(this HtmlHelper helper, string name, IEnumerable<string> collection, string optionLabel, string selectedValue)
        {
            return helper.DropDownList(name, ToSelectList(collection, selectedValue), optionLabel);
        }

        public static SelectList ToSelectList(this IEnumerable<string> collection)
        {
            var rawItems = new List<object>();
            collection.ForEach(m => rawItems.Add(new { Key = m, Value = m } ));
            return new SelectList(rawItems, "Key", "Value");
        }

        public static SelectList ToSelectList(this IEnumerable<string> collection, string selectedValue)
        {
            var rawItems = new List<object>();
            collection.ForEach(m => rawItems.Add(new { Key = m, Value = m }));
            return new SelectList(rawItems, "Key", "Value", selectedValue);
        }

        public static SelectList ToSelectList<T>(this IEnumerable<T> collection)
        {
            return new SelectList(collection, "Key", "Value");
        }

        public static SelectList ToSelectList<T>(this IEnumerable<T> collection,
                             string dataValueField, string dataTextField)
        {
            return new SelectList(collection, dataValueField, dataTextField);
        }

        public static SelectList ToSelectList<T>(this IEnumerable<T> collection,
                             string dataValueField, string dataTextField, string selectedValue)
        {
            return new SelectList(collection, dataValueField, dataTextField, selectedValue);
        }
    }
}