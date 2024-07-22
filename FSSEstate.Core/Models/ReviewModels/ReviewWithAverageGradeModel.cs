using FSSEstate.Core.Utility.Listing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSSEstate.Core.Models.ReviewModels
{
    public class ReviewWithAverageGradeModel
    {
        public  PagedListData<ReviewModel> Reviews { get; set; }
        public  decimal AverageGrade { get; set; }  
    }
}
