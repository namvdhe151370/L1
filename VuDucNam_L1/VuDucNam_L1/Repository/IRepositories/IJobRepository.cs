using VuDucNam_L1.Models;

namespace VuDucNam_L1.Repository.IRepositories
{
    public interface IJobRepository
    {
        Task<IEnumerable<JobModel>> GetAllJobAsync();
    }

}
