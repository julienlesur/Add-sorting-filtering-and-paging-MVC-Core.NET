using Recruiting.BL.Models;

namespace Recruiting.Web.Models.ViewModels
{
    public class JobDetails : SortSearchAndPagingViewModel
    {
        public Job Job { get; internal set; }
        public string Message { get; internal set; }
    }
}
