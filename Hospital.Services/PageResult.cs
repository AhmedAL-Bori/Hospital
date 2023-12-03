using Hospital.ViewModels;

namespace Hospital.Services
{
    public class PageResult<T>
    {
        public List<HospitalInfoViewModel> Data { get; internal set; }
        public int TotalItems { get; internal set; }
        public int PageNumber { get; internal set; }
        public int PageSize { get; internal set; }
    }
}