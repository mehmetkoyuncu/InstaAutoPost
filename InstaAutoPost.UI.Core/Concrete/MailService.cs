using InstaAutoPost.UI.Core.Abstract;
using InstaAutoPost.UI.Core.AutoMapper;
using InstaAutoPost.UI.Core.Common.Constants;
using InstaAutoPost.UI.Core.Common.DTOS;
using InstaAutoPost.UI.Data.Context;
using InstaAutoPost.UI.Data.Entities.Concrete;
using InstaAutoPost.UI.Data.UnitOfWork.Abstract;
using InstaAutoPost.UI.Data.UnitOfWork.Concrete;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InstaAutoPost.UI.Core.Concrete
{
    public class MailService : IMailService
    {
        IUnitOfWork _uow;
        public MailService()
        {
            _uow = new EFUnitOfWork(new RSSContextEF());
        }
        public int CreateMailAuthenticate(MailAuthenticate authenticateInfo)
        {
            try
            {
                var authenticate = _uow.GetRepository<EmailAccountOptions>().Get(x => x.IsDeleted == false).FirstOrDefault();
                EmailAccountOptions email = Mapping.Mapper.Map<MailAuthenticate, EmailAccountOptions>(authenticateInfo);
                if (authenticate == null)
                {
                    email.InsertedAt = DateTime.Now;
                    email.UpdatedAt = DateTime.Now;
                    email.IsDeleted = false;
                    _uow.GetRepository<EmailAccountOptions>().Add(email);
                }

                else
                {
                    authenticate.AccountMailAddress = authenticateInfo.AccountMailAddress;
                    authenticate.AccountMailPassword = authenticateInfo.AccountMailPassword;
                    authenticate.UpdatedAt = DateTime.Now;
                    _uow.GetRepository<EmailAccountOptions>().Update(authenticate);
                }
                var result = _uow.SaveChanges();
                Log.Logger.Information($"Mail hesabı oluşturuldu -{authenticateInfo.AccountMailAddress} ");
                return result;
            }
            catch (Exception exMessage)
            {
                Log.Logger.Error($"Mail hesabı oluşturulamadı -{authenticateInfo.AccountMailAddress} - {exMessage}");
                throw;
            }
        }
        public int CreateMailOptions(MailOptionsDTO options)
        {
            try
            {
                var option = _uow.GetRepository<EmailOptions>().Get(x => x.IsDeleted == false).FirstOrDefault();
                EmailOptions email = Mapping.Mapper.Map<MailOptionsDTO, EmailOptions>(options);
                if (option == null)
                {
                    email.InsertedAt = DateTime.Now;
                    email.UpdatedAt = DateTime.Now;
                    email.IsDeleted = false;
                    _uow.GetRepository<EmailOptions>().Add(email);
                }

                else
                {
                    option.MailDefaultHTMLContent = options.MailDefaultHTMLContent;
                    option.MailDefaultSubject = options.MailDefaultSubject;
                    option.MailDefaultTo = options.MailDefaultTo;
                    option.UpdatedAt = DateTime.Now;
                    _uow.GetRepository<EmailOptions>().Update(option);
                }
                var result = _uow.SaveChanges();
                Log.Logger.Information($"Mail opsiyonu oluşturuldu -{options.MailDefaultSubject} ");
                return result;
            }
            catch (Exception exMessage)
            {
                Log.Logger.Error($"Mail opsiyonu oluşturulamadı -{options.MailDefaultSubject} - {exMessage}");
                throw;
            }

        }
        public MailOptionsDTO GetByMailOptionDTO()
        {
            var option = _uow.GetRepository<EmailOptions>().Get(x => x.IsDeleted == false).FirstOrDefault();
            MailOptionsDTO
            opt = Mapping.Mapper.Map<EmailOptions, MailOptionsDTO>(option);
            return opt;
        }
        public MailAuthenticate GetByMailAuthenticateByMailAddress()
        {
            var account = _uow.GetRepository<EmailAccountOptions>().Get(x => x.IsDeleted == false).FirstOrDefault();
            MailAuthenticate
             authenticate = Mapping.Mapper.Map<EmailAccountOptions, MailAuthenticate>(account);
            return authenticate;
        }
        public int SendMailDefault(MailOptionsDTO mailOptionsDTO)
        {
            int result = 0;
            MailDTO mailDTO = null;
            MailAuthenticate sender = null;
            try
            {
                sender = GetByMailAuthenticateByMailAddress();
                MimeMessage mimeMessage = new MimeMessage();
                if (sender != null)
                {
                    mimeMessage.From.Add(MailboxAddress.Parse(sender.AccountMailAddress));
                    mimeMessage.Subject = mailOptionsDTO.MailDefaultSubject;
                    mimeMessage.To.Add(MailboxAddress.Parse(mailOptionsDTO.MailDefaultTo));
                    BodyBuilder bodyBuilder = new BodyBuilder();
                    if (!string.IsNullOrEmpty(mailOptionsDTO.MailDefaultHTMLContent))
                        bodyBuilder.HtmlBody = mailOptionsDTO.MailDefaultHTMLContent;
                    mimeMessage.Body = bodyBuilder.ToMessageBody();
                    string configure = ConfigureSMTP(sender.AccountMailAddress);
                    if (configure != null)
                    {
                        using var smtp = new SmtpClient();
                        smtp.Connect(configure, 587, SecureSocketOptions.StartTls);
                        smtp.Authenticate(sender.AccountMailAddress, sender.AccountMailPassword);
                        smtp.Send(mimeMessage);
                        smtp.Disconnect(true);
                        result = 1;
                        mailDTO = new MailDTO()
                        {
                            To = mailOptionsDTO.MailDefaultTo,
                            HtmlBody = mailOptionsDTO.MailDefaultHTMLContent,
                            From = sender.AccountMailAddress,
                            Subject = mailOptionsDTO.MailDefaultSubject,
                            IsSuccess = true
                        };
                        Log.Logger.Information($"Mail Gönderildi. Gönderen : {sender.AccountMailAddress} - Alıcı : {mailOptionsDTO.MailDefaultTo} ");
                    }
                    else
                    {
                        mailDTO = new MailDTO()
                        {
                            To = mailOptionsDTO.MailDefaultTo,
                            HtmlBody = mailOptionsDTO.MailDefaultHTMLContent,
                            From = sender.AccountMailAddress,
                            Subject = mailOptionsDTO.MailDefaultSubject,
                            IsSuccess = false,
                            ErrorText = "Mail konfigürasyon ayarlarında bir hata oluştu"
                        };
                        Log.Logger.Error("Mail konfigürasyon ayarlarında hata oluştu");
                    }
                    AddMail(mailDTO);
                }
                else
                {
                    throw new Exception("Mail göndermeden önce kullanıcı ayarlarından bir mail adresi giriniz.");
                }

            }
            catch (Exception ex)
            {
                mailDTO = new MailDTO()
                {
                    IsSuccess = false,
                    ErrorText = ex.Message,
                    HtmlBody = mailOptionsDTO.MailDefaultHTMLContent,
                    Subject = mailOptionsDTO.MailDefaultSubject,
                    To = mailOptionsDTO.MailDefaultTo
                };
                if (sender != null)
                    mailDTO.From = sender.AccountMailAddress;
                if (ex.GetType() == typeof(AuthenticationException))
                {
                    mailDTO.ErrorText = "Mail giriş bilgileri hatalıdır. Kullanıcı ayarlarından kontrol ediniz.";
                }
                AddMail(mailDTO);
            }
            return result;
        }
        public int SendMailAutoForSourceContent(SourceContentDTO sourceContentDTO, string contentRooth)
        {
            int result = 0;
            MailDTO mailDTO = null;
            MailAuthenticate sender = null;
            MailOptionsDTO options = GetByMailOptionDTO();
            try
            {
                string htmlContent = "";
                sender = GetByMailAuthenticateByMailAddress();
                MimeMessage mimeMessage = new MimeMessage();
                if (options != null && sender != null)
                {
                    if (options.MailDefaultHTMLContent != null)
                        htmlContent = ReplaceConfigure(options.MailDefaultHTMLContent, sourceContentDTO: sourceContentDTO);
                    mimeMessage.From.Add(MailboxAddress.Parse(sender.AccountMailAddress));
                    mimeMessage.Subject = ReplaceConfigure(options.MailDefaultSubject, sourceContentDTO: sourceContentDTO);
                    mimeMessage.To.Add(MailboxAddress.Parse(options.MailDefaultTo));
                    BodyBuilder bodyBuilder = new BodyBuilder();
                    if (!string.IsNullOrEmpty(htmlContent))
                        bodyBuilder.HtmlBody = htmlContent;
                    if (!String.IsNullOrEmpty(sourceContentDTO.imageURL))
                        bodyBuilder.Attachments.Add($"{contentRooth}/wwwroot/images/{sourceContentDTO.imageURL}");
                    mimeMessage.Body = bodyBuilder.ToMessageBody();
                    string configure = ConfigureSMTP(sender.AccountMailAddress);
                    if (configure != null)
                    {
                        using var smtp = new SmtpClient();
                        smtp.Connect(configure, 587, SecureSocketOptions.StartTls);
                        smtp.Authenticate(sender.AccountMailAddress, sender.AccountMailPassword);
                        smtp.Send(mimeMessage);
                        smtp.Disconnect(true);
                        result = 1;
                        mailDTO = new MailDTO()
                        {
                            To = options.MailDefaultTo,
                            HtmlBody = htmlContent,
                            From = sender.AccountMailAddress,
                            Subject = mimeMessage.Subject,
                            IsSuccess = true
                        };
                        Log.Logger.Information($"Mail Gönderildi. Gönderen : {sender.AccountMailAddress} - Alıcı : {options.MailDefaultTo} ");
                    }
                    else
                    {
                        mailDTO = new MailDTO()
                        {
                            To = options.MailDefaultTo,
                            HtmlBody = htmlContent,
                            From = sender.AccountMailAddress,
                            Subject = mimeMessage.Subject,
                            IsSuccess = false,
                            ErrorText = "Mail konfigürasyon ayarlarında bir hata oluştu"
                        };
                        Log.Logger.Error("Mail konfigürasyon ayarlarında hata oluştu");
                    }
                    AddMail(mailDTO);
                }
            }
            catch (Exception ex)
            {
                mailDTO = new MailDTO()
                {
                    IsSuccess = false,
                    ErrorText = ex.Message,
                    HtmlBody = options.MailDefaultHTMLContent,
                    Subject = options.MailDefaultSubject,
                    To = options.MailDefaultTo
                };
                if (sender != null)
                    mailDTO.From = sender.AccountMailAddress;
                if (ex.GetType() == typeof(AuthenticationException))
                {
                    mailDTO.ErrorText = "Mail giriş bilgileri hatalıdır. Kullanıcı ayarlarından kontrol ediniz.";
                }
                AddMail(mailDTO);
            }
            return result;
        }
        public string ConfigureSMTP(string fromMail)
        {
            string configureText = default;
            var lastIndex = fromMail.IndexOf("@");
            fromMail = fromMail.Substring(lastIndex + 1);
            switch (fromMail)
            {
                case "hotmail.com":
                    configureText = SMTPUri.Hotmail;
                    break;
                case "gmail.com":
                    configureText = SMTPUri.Gmail;
                    break;
                case "outlook.com":
                    configureText = SMTPUri.Office365;
                    break;
                default:
                    configureText = null;
                    break;
            }
            return configureText;
        }
        public void AddMail(MailDTO mail)
        {
            int result = default;
            mail.InsertedAt = DateTime.Now;
            mail.UpdatedAt = DateTime.Now;
            Email email = Mapping.Mapper.Map<MailDTO, Email>(mail);
            _uow.GetRepository<Email>().Add(email);
            result = _uow.SaveChanges();
            if (result <= 0)
                Log.Logger.Error($"Hata ! Mail kaydedilemedi - {mail.Subject}");
            else
                Log.Logger.Information($"Mail başarıyla kaydedildi - {mail.Subject}");
        }
        public void SendMessage(MailDTO mailDTO, string rootPath = null, string fileName = null)
        {
            throw new NotImplementedException();
        }
        public string ReplaceConfigure(string text,string changeText=null ,SourceContentDTO sourceContentDTO = null,Category category=null,Source source=null,RssResultDTO resultDTO=null,string removedContent=null)
        {
            if (sourceContentDTO != null)
            {
                text = text.Replace(MailConfigureConstants.CategoryName, sourceContentDTO.CategoryName);
                text = text.Replace(MailConfigureConstants.SourceContentDescription, sourceContentDTO.Description);
                text = text.Replace(MailConfigureConstants.SourceContentInsertAt.ToString(), sourceContentDTO.ContentInsertAt.ToShortDateString());
                text = text.Replace(MailConfigureConstants.SourceContentTitle, sourceContentDTO.Title);
                text = text.Replace(MailConfigureConstants.SourceName, sourceContentDTO.SourceName);
            }
            if (category != null)
                text = text.Replace(MailConfigureConstants.CategoryName, category.Name);
            if(source!=null)
                text = text.Replace(MailConfigureConstants.SourceName, source.Name);
            if(resultDTO!=null)
                text = text.Replace(MailConfigureConstants.RSSAddedCount, resultDTO.RssAddedCount.ToString());
            text = text.Replace(MailConfigureConstants.AutoJobName,changeText);
            text = text.Replace(MailConfigureConstants.RemovedContent, removedContent);
            return text;
        }
        public List<SentMailDTO> GetSentEmailList()
        {
            var mailList = _uow.GetRepository<Email>().Get(x => x.IsDeleted == false).OrderByDescending(x=>x.UpdatedAt).ToList();
            return Mapping.Mapper.Map<List<SentMailDTO>>(mailList);
        }
        public int RemoveAllMail()
        {
            _uow.GetRepository<Email>().RemoveRange(GetAllEmail());
            var result = _uow.SaveChanges();
            return result;
        }
        public List<Email> GetAllEmail()
        {
            List<Email> emailList=_uow.GetRepository<Email>().GetAll();
            return emailList;
        }
        public void SendMailAutoJob(string content,string subject)
        {
            MailDTO mailDTO = null;
            MailAuthenticate sender = null;
            MailOptionsDTO options = GetByMailOptionDTO();
            try
            {
                sender = GetByMailAuthenticateByMailAddress();
                MimeMessage mimeMessage = new MimeMessage();
                if (options != null && sender != null)
                {
                    mimeMessage.From.Add(MailboxAddress.Parse(sender.AccountMailAddress));
                    mimeMessage.Subject = subject;
                    mimeMessage.To.Add(MailboxAddress.Parse(options.MailDefaultTo));
                    BodyBuilder bodyBuilder = new BodyBuilder();
                    if (!string.IsNullOrEmpty(content))
                        bodyBuilder.HtmlBody = content;
                    mimeMessage.Body = bodyBuilder.ToMessageBody();
                    string configure = ConfigureSMTP(sender.AccountMailAddress);
                    if (configure != null)
                    {
                        using var smtp = new SmtpClient();
                        smtp.Connect(configure, 587, SecureSocketOptions.StartTls);
                        smtp.Authenticate(sender.AccountMailAddress, sender.AccountMailPassword);
                        smtp.Send(mimeMessage);
                        smtp.Disconnect(true);
                        mailDTO = new MailDTO()
                        {
                            To = options.MailDefaultTo,
                            HtmlBody = content,
                            From = sender.AccountMailAddress,
                            Subject = subject,
                            IsSuccess = true
                        };
                        Log.Logger.Information($"Mail Gönderildi. Gönderen : {sender.AccountMailAddress} - Alıcı : {options.MailDefaultTo} ");
                    }
                    else
                    {
                        mailDTO = new MailDTO()
                        {
                            To = options.MailDefaultTo,
                            HtmlBody = content,
                            From = sender.AccountMailAddress,
                            Subject = subject,
                            IsSuccess = false,
                            ErrorText = "Mail konfigürasyon ayarlarında bir hata oluştu"
                        };
                        Log.Logger.Error("Mail konfigürasyon ayarlarında hata oluştu");
                    }
                    AddMail(mailDTO);
                }
            }
            catch (Exception ex)
            {
                mailDTO = new MailDTO()
                {
                    IsSuccess = false,
                    ErrorText = ex.Message,
                    HtmlBody = options.MailDefaultHTMLContent,
                    Subject = options.MailDefaultSubject,
                    To = options.MailDefaultTo
                };
                if (sender != null)
                    mailDTO.From = sender.AccountMailAddress;
                if (ex.GetType() == typeof(AuthenticationException))
                {
                    mailDTO.ErrorText = "Mail giriş bilgileri hatalıdır. Kullanıcı ayarlarından kontrol ediniz.";
                }
                AddMail(mailDTO);
            }
        }
    }
}
