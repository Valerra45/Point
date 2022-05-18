using AutoMapper;
using Point.Admin.Core.Domain.Entity;
using Point.Admin.WebHost.Models;
using Point.SharedKernel.DtoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Point.Admin.WebHost.MapProfiles
{
    public class AdminMapProfiles : Profile
    {
        public AdminMapProfiles()
        {
            CreateMap<CreateIssuePoint, IssuePoint>();
 
            CreateMap<IssuePoint, IssuePointDto>();
        }
    }
}
