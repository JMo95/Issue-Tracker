using AutoMapper;
using Bug_Tracker_V1._0.BuisinessLogic.Models;
using Bug_Tracker_V1._0.ViewModels.TicketViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bug_Tracker_V1._0.WebApi.Profiles
{

    public class TicketProfile : Profile
    {

        public TicketProfile()
        {
            CreateMap<TicketCreateViewModel, Ticket>();  
        }
       
      
    }
}
