using VuDucNam_L1.DataAccess;
using VuDucNam_L1.Models;

namespace VuDucNam_L1.Service.IServices
{
    public interface IEthnicService
    {
        Task<IEnumerable<EthnicModel>> GetAllEthnicAsync();
    }

}
