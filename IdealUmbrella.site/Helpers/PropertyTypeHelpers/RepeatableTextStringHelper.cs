using System.Text;

namespace IdealUmbrella.site.Helpers.PropertyTypeHelpers
{
    internal static class RepeatableTextStringHelper
    {
        /// <summary>
        /// Takes a list of strings, and formats them for the RepeatableTextString content property
        /// </summary>
        /// <param name="items">A <see cref="List{string}"/> of items to insert into the RepeatableTextString 
        /// content property
        /// </param>
        /// <seealso cref="RepeatableTextStringHelper.FormatRepeatableTextStringContentFromList(IEnumerable{string} items)"/>
        /// <seealso cref="https://docs.umbraco.com/umbraco-cms/fundamentals/backoffice/property-editors/built-in-umbraco-property-editors/multiple-textbox"/>
        /// <returns>A well formatted string, including NewLines</returns>
        public static string FormatRepeatableTextStringProperty(List<string> items)
        {
            return RepeatableTextStringContent(items);
        }

        /// <summary>
        /// Takes an IEnumerable of strings, and formats them for the RepeatableTextString content property
        /// </summary>
        /// <param name="items">A <see cref="IEnumerable{string}"/> of items to insert into the RepeatableTextString 
        /// content property
        /// </param>
        /// <seealso cref="https://docs.umbraco.com/umbraco-cms/fundamentals/backoffice/property-editors/built-in-umbraco-property-editors/multiple-textbox"/>
        /// <seealso cref="RepeatableTextStringHelper.FormatRepeatableTextStringContentFromList(List{string} items)"/>
        /// <returns>A well formatted string, including NewLines</returns>
        public static string FormatRepeatableTextStringProperty(IEnumerable<string> items)
        {
            return RepeatableTextStringContent(items);
        }


        /// <summary>
        /// provides a single method for formatting textstrings
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        private static string RepeatableTextStringContent(IEnumerable<string> items)
        {
            if (items is null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            var sb = new StringBuilder();

            sb.Append(items.ElementAt(0));

            for (int i = 1; i < items.Count(); i++)
            {
                if (!string.IsNullOrEmpty(items.ElementAt(i)))
                {
                    sb.Append($"{Environment.NewLine}{items.ElementAt(i).Trim()}");
                }
            }
            return sb.ToString();
        }

        

    }
}
