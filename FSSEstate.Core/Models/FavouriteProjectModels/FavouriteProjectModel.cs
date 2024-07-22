using FSSEstate.Core.Models.ProjectModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSSEstate.Core.Models.FavouriteProjectModels
{
    public class FavouriteProjectModel
    {
        public long Id { get; set; }
        public ProjectModel Project { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
