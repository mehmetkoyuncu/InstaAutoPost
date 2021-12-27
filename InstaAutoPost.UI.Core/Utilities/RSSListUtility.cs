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
            //var listRss = new List<RSSCreatorDTO>()
            //{

            //    //new RSSCreatorDTO(){
            //    //    CategoryName=RSSContants.RSSNameContants.HaberturkEkonomiCategoryName,
            //    //    CategoryURL=RSSContants.RSSURLConstants.HaberturkEkonomiCategoryURL
            //    //},
            //    //new RSSCreatorDTO(){
            //    //    CategoryName=RSSContants.RSSNameContants.HaberturkMansetCategoryName,
            //    //    CategoryURL=RSSContants.RSSURLConstants.HaberturkMansetCategoryURL
            //    //},
            //    // new RSSCreatorDTO(){
            //    //    CategoryName=RSSContants.RSSNameContants.HaberturkSporCategoryName,
            //    //    CategoryURL=RSSContants.RSSURLConstants.HaberturkSporCategoryURL
            //    //},
            //    //   new RSSCreatorDTO(){
            //    //    CategoryName=RSSContants.RSSNameContants.HaberturkTumHaberlerCategoryName,
            //    //    CategoryURL=RSSContants.RSSURLConstants.HaberturkTumHaberlerCategoryURL
            //    //},
            //    //    new RSSCreatorDTO(){
            //    //    CategoryName=RSSContants.RSSNameContants.MilliyetEkonomiCategoryName,
            //    //    CategoryURL=RSSContants.RSSURLConstants.MilliyetEkonomiCategoryURL
            //    //},
            //    //    new RSSCreatorDTO(){
            //    //    CategoryName=RSSContants.RSSNameContants.MilliyetMagazinCategoryName,
            //    //    CategoryURL=RSSContants.RSSURLConstants.MilliyetMagazinCategoryURL
            //    //},
            //    //      new RSSCreatorDTO(){
            //    //    CategoryName=RSSContants.RSSNameContants.NTVSporCategoryName,
            //    //    CategoryURL=RSSContants.RSSURLConstants.NTVSporCategoryURL
            //    //},
            //    // new RSSCreatorDTO(){
            //    //    CategoryName=RSSContants.RSSNameContants.CNNTurkMagazinCategoryName,
            //    //    CategoryURL=RSSContants.RSSURLConstants.CNNTurkMagazinCategoryURL
            //    //},
            //    //    new RSSCreatorDTO(){
            //    //    CategoryName=RSSContants.RSSNameContants.HurriyetAnasayfaCategoryName,
            //    //    CategoryURL=RSSContants.RSSURLConstants.HurriyetAnasayfaCategoryURL
            //    //},
            //};
            //return listRss;
        }

    }
}
