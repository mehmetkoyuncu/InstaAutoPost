using InstaAutoPost.UI.Core.Common.DTOS;
using System;
using System.Collections.Generic;
using System.Text;

namespace InstaAutoPost.UI.Core.Abstract
{
    public interface IAutoJobService
    {
        List<AutoJobDTO> GetAutoJobs();
        bool CreateAutoJobSetting(List<AutoJobDTO> autoJobsDTO);
        int UpdateAutoJob(AutoJobDTO autoJobDTO, string environment);
    }
}
