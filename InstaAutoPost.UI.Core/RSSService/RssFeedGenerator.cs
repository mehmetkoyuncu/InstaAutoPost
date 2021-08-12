using InstaAutoPost.UI.Core.Abstract;
using InstaAutoPost.UI.Core.Concrete;
using InstaAutoPost.UI.Data.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.ServiceModel.Syndication;
using System.Text;
using System.Xml;

namespace InstaAutoPost.UI.Core.RSSService
{
    public class RssFeedGenerator
    {

        private readonly string _rssLink;
        private readonly string _rssName;
        public RssFeedGenerator(string categoryLink, string categoryName)
        {
            _rssLink = categoryLink;
            _rssName = categoryName;
        }

        public void RSSCreator()
        {
            CategoryService categoryService = new CategoryService();
            SourceService sourceService = new SourceService();
            SourceContentService sourceContentService = new SourceContentService();
            SyndicationFeed feed = null;

            try
            {
                using (var reader = XmlReader.Create(_rssLink))
                {
                    feed = SyndicationFeed.Load(reader);
                }
            }
            catch { throw new Exception("Doğru bir RSS kodu giriniz.."); } // TODO: Deal with unavailable resource.

            Category controlCategory = CategoryControl(categoryService);
            Source controlSource = SourceControl(sourceService, feed);

            if (feed != null)
            {
                if (controlSource == null)
                {
                    int sourceResult = SourceAdd(sourceService, feed);
                    Source _source = null;
                    if (sourceResult > 0)
                        _source = SourceGet(sourceService, feed, _source);
                    else
                        throw new Exception("Kaynak eklenirken hata oluştu.");
                    int categoryResult = CategoryAdd(categoryService, _source);
                    Category _category;
                    if (categoryResult > 0)
                        _category = CategoryGet(categoryService);
                    else
                        throw new Exception("Kategori eklenirken hata oluştu.");
                    int sourceContentResult=SourceContentAdd(sourceContentService, feed, _category);
                    if (sourceContentResult > 0)
                    {
                        //Buraya devamı yazılacak..
                    }
                    else
                    {
                        throw new Exception("İçerikler yüklenemedi");
                    }
                }
                else
                {
                    if (controlCategory == null)
                    {
                        int categoryResult = 0;
                        if (controlSource != null)
                            categoryResult = CategoryAdd(categoryService, controlSource);
                        else
                            throw new Exception("Kategori yanlış kaydedilmiştir.");

                        Category _category;
                        if (categoryResult > 0)
                        {
                            _category = CategoryGet(categoryService);
                        }
                        else
                            throw new Exception("Kategori eklenirken hata oluştu.");

                        int sourceContentResult = SourceContentAdd(sourceContentService, feed, _category);
                        if (sourceContentResult > 0)
                        {
                            //Buraya devamı yazılacak..
                        }
                        else
                        {
                            throw new Exception("İçerikler yüklenemedi");
                        }
                    }
                    else
                    {
                        throw new Exception("Kategori kaydı mevcuttur");
                    }
                }




            }

        }
        private int SourceContentAdd(SourceContentService sourceContentService, SyndicationFeed feed, Category _category)
        {
            List<SourceContent> sourceContentList = new List<SourceContent>();

            SourceContent sourceContent = null;
            foreach (var element in feed.Items)
            {
                sourceContent = new SourceContent()
                {
                    ContentInsertAt = element.PublishDate != null ? Convert.ToDateTime(new DateTime(element.PublishDate.Year, element.PublishDate.Month, element.PublishDate.Day, element.PublishDate.Hour, element.PublishDate.Minute, element.PublishDate.Second, element.PublishDate.Millisecond)) : DateTime.Now,
                    InsertedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    CategoryId = _category.Id,
                    Description = element.Summary != null ? element.Summary.Text : ((TextSyndicationContent)element.Content).Text.ToString(),
                    SourceContentId = element.Id,
                    IsDeleted = false,
                    SendOutForPost = false,
                    Link = null,
                    Title = element.Title.Text
                };
                sourceContentList.Add(sourceContent);
            }

            if (sourceContentList.Count == 0 || sourceContent == null)
                throw new Exception("Bu linkte içerik bulunmamaktadır.");
            int sourceContentResult = sourceContentService.AddSourceContent(sourceContentList);
            return sourceContentResult;
        }
        private Category CategoryGet(CategoryService categoryService)
        {
            Category _category = categoryService.GetByRSSURL(_rssLink);
            if (_category == null)
                throw new Exception("Kategori eklendi fakat getirilirken hata oluştu");
            return _category;
        }
        private int CategoryAdd(CategoryService categoryService, Source _source)
        {
            int categoryResult = 0;
            categoryResult = categoryService.AddCategory(_rssName, _rssLink, _source.Id);
            return categoryResult;
        }
        private Source SourceGet(SourceService sourceService, SyndicationFeed feed, Source _source)
        {
            if (feed.Links.Count != 0)
            {
                _source = sourceService.GetByURL(feed.Links[0].Uri.OriginalString,feed.Title.Text);
            }
            else if (!string.IsNullOrEmpty(feed.Id))
            {
                _source = sourceService.GetByURL(feed.Id,feed.Title.Text);
            }
            if (_source == null)
                throw new Exception("Kaynak eklendi fakat getirilirken hata oluştu");
            return _source;
        }
        private int SourceAdd(SourceService sourceService, SyndicationFeed feed)
        {
            int sourceResult = 0;
            if (feed.ImageUrl != null)
            {
                sourceResult = sourceService.Add(feed.Title.Text, feed.ImageUrl.OriginalString, feed.Links[0].Uri.OriginalString);
            }
            else if (feed.Links.Count != 0)
            {
                sourceResult = sourceService.Add(feed.Title.Text, null, feed.Links[0].Uri.OriginalString);
            }
            else
            {
                sourceResult = sourceService.Add(feed.Title.Text, null, feed.Id);
            }

            return sourceResult;
        }
        private Category CategoryControl(CategoryService categoryService)
        {
            return categoryService.GetByRSSURL(_rssLink);
        }
        private  Source SourceControl(SourceService sourceService, SyndicationFeed feed)
        {
            Source controlSource = null;
            if (feed.Links.Count != 0)
            {
                controlSource = sourceService.GetByURL(feed.Links[0].Uri.OriginalString,feed.Title.Text);
            }
            else if (!string.IsNullOrEmpty(feed.Id))
            {
                controlSource = sourceService.GetByURL(feed.Id,feed.Title.Text);
            }

            return controlSource;
        }
    }
}

