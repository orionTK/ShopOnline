using Microsoft.EntityFrameworkCore;
using ShopOnline.Data.EF;
using ShopOnline.ViewModel.Utilities.Slides;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopOnline.Application.Utilities
{
    public class SlideService : ISlideService
    {
        private readonly ShopOnlineDbContext _context;

        public SlideService(ShopOnlineDbContext context)
        {
            _context = context;
        }

        public async Task<List<SlideViewModel>> GetAll()
        {
            var slides = await _context.Slides.OrderBy(x => x.SortOrder)
                .Select(x => new SlideViewModel()
                {
                    Id = x.SlideId,
                    Name = x.SlideName,
                    Description = x.Description,
                    Url = x.Url,
                    Image = x.Image
                }).ToListAsync();

            return slides;
        }
    }
}
