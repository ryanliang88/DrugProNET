using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace DrugProNET.Utility
{
    public static class DataUtilities
    {
        /// <summary>
        /// Author: Ryan Liang
        /// </summary>
        public static List<string> FilterDropdownList(List<string> valuesList, string prefixText = null, bool startsWith = false)
        {
            valuesList = valuesList.Where(s => !string.IsNullOrEmpty(s?.Trim())).ToList();

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

            return valuesList.Distinct(StringComparer.CurrentCultureIgnoreCase).OrderBy(alphabet => alphabet).ToList();
        }
    }
}