using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace DrugProNET.Utility
{
    public static class DataUtilities
    {
        public static void AddIfExists(List<string> list, params string[] values)
        {
            foreach (string value in values)
            {
                if (!string.IsNullOrEmpty(value))
                {
                    list.Add(value);
                }
            }
        }

        public static List<ListItem> GenerateListItemsFromValues(params string[] values)
        {
            List<ListItem> listItemList = new List<ListItem>();

            foreach (string value in values)
            {
                if (!string.IsNullOrEmpty(value))
                {
                    listItemList.Add(new ListItem(value, value, true));
                }
            }

            return listItemList;
        }

        public static List<string> FilterDropdownList(List<string> valuesList, string prefixText = null, bool startsWith = false)
        {
            if (prefixText != null)
            {
                if (startsWith)
                {
                    valuesList = valuesList.Where(v => v.ToLower().StartsWith(prefixText.ToLower())).ToList();
                }
                else
                {
                    valuesList = valuesList.Where(v => v.ToLower().Contains(prefixText.ToLower())).ToList();
                }
            }

            return valuesList.Where(s => !string.IsNullOrEmpty(s.Trim())).Distinct(StringComparer.CurrentCultureIgnoreCase).OrderBy(alphabet => alphabet).ToList();
        }
    }
}