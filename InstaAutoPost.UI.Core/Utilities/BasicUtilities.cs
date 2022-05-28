using System;
using System.Collections.Generic;
using System.Text;

namespace InstaAutoPost.UI.Core.Utilities
{
    public static class BasicUtilities
    {
        public static string IsNullText(string text)
        {
            if (String.IsNullOrEmpty(text))
                text = "";
            return text;
        }
    }
}
