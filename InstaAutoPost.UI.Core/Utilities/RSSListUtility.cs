using InstaAutoPost.UI.Core.Common.Constants;
using InstaAutoPost.UI.Core.Common.DTOS;
using InstaAutoPost.UI.Core.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace InstaAutoPost.UI.Core.Utilities
{
    public class RSSListUtility
    {
        public List<RSSCreatorDTO> CreateRssList()
        {
            List<RSSCreatorDTO> rssList = new CategoryService().GetCategoryNameAndLink();
            return rssList;
        }

    }
}
