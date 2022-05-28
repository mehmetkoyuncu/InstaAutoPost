using InstaAutoPost.UI.Core.Abstract;
using InstaAutoPost.UI.Core.AutoMapper;
using InstaAutoPost.UI.Core.Common.DTOS;
using InstaAutoPost.UI.Core.ScheduleJobs.Main;
using InstaAutoPost.UI.Data.Context;
using InstaAutoPost.UI.Data.Entities.Concrete;
using InstaAutoPost.UI.Data.UnitOfWork.Abstract;
using InstaAutoPost.UI.Data.UnitOfWork.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InstaAutoPost.UI.Core.Concrete
{
    public class AutoJobService:IAutoJobService
    {
        IUnitOfWork _uow;
        public AutoJobService()
        {
            _uow = new EFUnitOfWork(new RSSContextEF());
        }
        public bool CreateAutoJobSetting(List<AutoJobDTO> autoJobsDTO)
        {
            bool control = false;
            var autoJobs = Mapping.Mapper.Map<List<AutoJob>>(autoJobsDTO);
            _uow.GetRepository<AutoJob>().AddList(autoJobs);
            int result = _uow.SaveChanges();
            if (result > 0)
                control = true;
            return control;
        }
        public List<AutoJobDTO> GetAutoJobs()
        {
            var autoJobs=_uow.GetRepository<AutoJob>().Get(x => x.IsDeleted == false).ToList();
            var autoJobDTO = Mapping.Mapper.Map<List<AutoJobDTO>>(autoJobs);
            return autoJobDTO;
        }
        public int UpdateAutoJob(AutoJobDTO autoJobDTO,string environment)
        {
            var autoJob = GetAutoJobById(autoJobDTO.Id);
            autoJob.CronDescription = autoJobDTO.CronDescription;
            autoJob.IsWork = autoJobDTO.IsWork;
            autoJob.Cron = autoJobDTO.Cron;
            autoJob.UpdatedAt = DateTime.Now;
            _uow.GetRepository<AutoJob>().Update(autoJob);
            int result = _uow.SaveChanges();
            new ScheduleJobRunner().RemoveAll();
            new ScheduleJobRunner().RunJobs(environment);
            return result;
        }
        public AutoJob GetAutoJobById(int id)
        {
           var autoJob= _uow.GetRepository<AutoJob>().Get(x => x.Id == id && x.IsDeleted == false).FirstOrDefault();
            return autoJob;
        }
    }
}
