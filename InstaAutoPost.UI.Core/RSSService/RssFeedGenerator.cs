using InstaAutoPost.UI.Core.Abstract;
using InstaAutoPost.UI.Core.Common.CharacterConverter;
using InstaAutoPost.UI.Core.Common.DTOS;
using InstaAutoPost.UI.Core.Concrete;
using InstaAutoPost.UI.Core.Utilities;
using InstaAutoPost.UI.Data.Entities.Concrete;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.ServiceModel.Syndication;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace InstaAutoPost.UI.Core.RSSService
{
    public class RssFeedGenerator
    {

        private readonly string _rssLink;
        private readonly string _rssName;
        private readonly string _environment;
        public RssFeedGenerator(string categoryLink, string categoryName, string environment)
        {
            _rssLink = categoryLink;
            _rssName = categoryName;
            _environment = environment;
        }
        public RssResultDTO RSSCreator()
        {
            var result = 0;
            Category _category = null;
            CategoryService categoryService = new CategoryService();
            SourceService sourceService = new SourceService();
            SourceContentService sourceContentService = new SourceContentService();
            ImagesService imageService = new ImagesService();
            SyndicationFeed feed = null;

            try
            {
                using (var reader = XmlReader.Create(_rssLink))
                {
                    feed = SyndicationFeed.Load(reader);
                }
            }
            catch (Exception ex) { throw new Exception("Doğru bir RSS kodu giriniz.."); } // TODO: Deal with unavailable resource.

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
                    if (categoryResult > 0)
                        _category = CategoryGet(categoryService);
                    else
                        throw new Exception("Kategori eklenirken hata oluştu.");
                    int sourceContentResult = SourceContentAdd(sourceContentService, feed, _category, imageService);
                    if (sourceContentResult > 0)
                    {
                        result = sourceContentResult;
                    }
                    else
                        throw new Exception("İçerikler yüklenemedi");
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


                        if (categoryResult > 0)
                            _category = CategoryGet(categoryService);
                        else
                            throw new Exception("Kategori eklenirken hata oluştu.");

                        int sourceContentResult = SourceContentAdd(sourceContentService, feed, _category, imageService);
                        if (sourceContentResult > 0)
                        {
                            result = sourceContentResult;
                        }
                        else
                            throw new Exception("İçerikler yüklenemedi");
                    }
                    else
                    {
                        _category = CategoryGet(categoryService);
                        int sourceContentResult = SourceContentAdd(sourceContentService, feed, _category, imageService);
                        result = sourceContentResult;
                    }
                }




            }
            return new RssResultDTO() { RssAddedCount = result, CategoryId = _category.Id };
        }
        private int SourceContentAdd(SourceContentService sourceContentService, SyndicationFeed feed, Category _category, ImagesService imagesService)
        {
            List<SourceContent> sourceContentList = new List<SourceContent>();
            ImageUtility imageUtility = new ImageUtility();
            SourceContent sourceContent = null;
            string content = null;
            List<string> contents = sourceContentService.GetAll();
            foreach (var element in feed.Items)
            {

                string image = null;


                if (element.Summary != null || element.Content != null)
                {
                    content = ContentSlice(element);
                }

                if (element.Links.Any(x => x.RelationshipType != null ? x.RelationshipType.Contains("enclosure") : false))
                {
                    image = (element.Links.Where(x => x.RelationshipType != null ? x.RelationshipType.Contains("enclosure") : false).Select(x => x.Uri.OriginalString.Trim().ToLower()).FirstOrDefault());
                }
                else if (element.ElementExtensions.Any(x => x.OuterName != null ? x.OuterName.Contains("image") : false))
                {
                    foreach (SyndicationElementExtension extension in element.ElementExtensions)
                    {
                        if (extension.OuterName == "image" || extension.OuterName == "ipimage")
                        {
                            XElement ele = extension.GetObject<XElement>();
                            image = ele.Value.Trim();
                        }
                    }
                }
                else if (element.Content != null)
                {
                    image = SliceImage(image, ((TextSyndicationContent)element.Content).Text.Trim().ToString());
                }
                else
                    image = SliceImage(image, content);
                var controlTitle = sourceContentList.Where(x => x.Title.Trim() == element.Title.Text.Trim()).FirstOrDefault();
                var contentNameList = sourceContentService.GetNotDeletedContentsName();
                var databaseControl = contentNameList.Where(x => x.Trim() == element.Title.Text.Trim()).FirstOrDefault();

                if (controlTitle == null && databaseControl == null)
                {
                    var imageData = imageUtility.Download(image, (element.Title.Text), ImageFormat.Jpeg,_environment);
                    sourceContent = new SourceContent()
                    {
                        ContentInsertAt = element.PublishDate != null ? Convert.ToDateTime(new DateTime(element.PublishDate.Year, element.PublishDate.Month, element.PublishDate.Day, element.PublishDate.Hour, element.PublishDate.Minute, element.PublishDate.Second, element.PublishDate.Millisecond)) : DateTime.Now,
                        InsertedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now,
                        CategoryId = _category.Id,
                        Description = content,
                        SourceContentId = element.Id,
                        IsDeleted = false,
                        SendOutForPost = false,
                        imageURL = imageData,
                        Title = element.Title.Text.Trim()
                    };
                    sourceContentList.Add(sourceContent);
                }


            }



            int sourceContentResult = sourceContentService.AddSourceContent(sourceContentList);
            return sourceContentResult;
        }

        private static string ContentSlice(SyndicationItem element)
        {
            string content = element.Summary != null ? element.Summary.Text : ((TextSyndicationContent)element.Content).Text.Trim().ToString();
            if (content.Contains('<') && content.Contains('>'))
            {
                foreach (var item in content)
                {
                    if (item == '<')
                    {
                        int beginIndex = content.IndexOf(item);
                        int lastIndex = content.IndexOf('>');
                        content = content.Remove(beginIndex, (lastIndex - beginIndex) + 1);
                    }
                }
            }

            return content;
        }
        private static string SliceImage(string imageL, string content)
        {
            int imgurLBeginIndex = content.IndexOf("<img");

            if (imgurLBeginIndex != -1)
            {
                int imgUrlEndIndex = content.IndexOf(">", imgurLBeginIndex);
                string URL = content.Substring(imgurLBeginIndex, (imgUrlEndIndex - imgurLBeginIndex) + 1);
                string beginString = "src=";
                int imageBeginIndex = URL.IndexOf(beginString);
                imageBeginIndex += imageBeginIndex;

                int imageLastIndex = URL.IndexOf("\"", imageBeginIndex);
                imageL = URL.Substring(imageBeginIndex, (imageLastIndex - imageBeginIndex));
            }
            return imageL;
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
                _source = sourceService.GetByURL(feed.Links[0].Uri.OriginalString, feed.Title.Text);
            }
            else if (!string.IsNullOrEmpty(feed.Id))
            {
                _source = sourceService.GetByURL(feed.Id, feed.Title.Text);
            }
            if (_source == null)
                throw new Exception("Kaynak eklendi fakat getirilirken hata oluştu");
            return _source;
        }
        private int SourceAdd(SourceService sourceService, SyndicationFeed feed)
        {
            ImageUtility imageUtility = new ImageUtility();
            int sourceResult = 0;
            if (feed.ImageUrl != null)
            {

                string imageSrc = null;

                imageSrc = imageUtility.Download(feed.ImageUrl.OriginalString, feed.Title.Text, ImageFormat.Png,_environment);

                sourceResult = sourceService.Add(feed.Title.Text.Trim(), imageSrc, feed.Links[0].Uri.OriginalString);
            }
            else if (feed.Links.Count != 0)
            {
                sourceResult = sourceService.Add(feed.Title.Text.Trim(), null, feed.Links[0].Uri.OriginalString);
            }
            else
            {
                sourceResult = sourceService.Add(feed.Title.Text.Trim(), null, feed.Id);
            }

            return sourceResult;
        }

        private Category CategoryControl(CategoryService categoryService)
        {
            return categoryService.GetByRSSURL(_rssLink);
        }
        private Source SourceControl(SourceService sourceService, SyndicationFeed feed)
        {
            Source controlSource = null;
            if (feed.Links.Count != 0)
            {
                controlSource = sourceService.GetByURL(feed.Links[0].Uri.OriginalString, feed.Title.Text);
            }
            else if (!string.IsNullOrEmpty(feed.Id))
            {
                controlSource = sourceService.GetByURL(feed.Id, feed.Title.Text);
            }

            return controlSource;
        }
    }
}

