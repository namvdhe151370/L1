using VuDucNam_L1.Models;

namespace VuDucNam_L1.Repository.IRepositories
{
    public interface IJobRepository
    {
        Task<IEnumerable<JobModel>> GetAllJobAsync();
        Task CheckJobIdAsync( int id);
        Task<int> GetJobIdByNameAsync(string jobName);
    }

}
