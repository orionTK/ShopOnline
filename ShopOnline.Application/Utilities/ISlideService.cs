using ShopOnline.Data.Entities;
using ShopOnline.ViewModel.Utilities.Slides;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShopOnline.Application.Utilities
{
    public interface ISlideService
    {
        Task<List<SlideViewModel>> GetAll();
    }
}
