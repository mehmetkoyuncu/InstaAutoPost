using System;
using System.Collections.Generic;
using System.Text;

namespace InstaAutoPost.UI.Core.Abstract
{
    public interface IImagesService
    {
        int AddImages(string url,int sourceContentId);
    }
}
