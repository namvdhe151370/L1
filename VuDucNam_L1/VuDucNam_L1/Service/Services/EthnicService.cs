using VuDucNam_L1.DataAccess;
using VuDucNam_L1.Models;
using VuDucNam_L1.Repository.IRepositories;
using VuDucNam_L1.Service.IServices;

namespace VuDucNam_L1.Service.Services
{
    public class EthnicService : IEthnicService
    {
        private readonly IEthnicRepository _ethnicRepository;

        public EthnicService(IEthnicRepository ethnicRepository)
        {
            _ethnicRepository = ethnicRepository;
        }

        public async Task<IEnumerable<EthnicModel>> GetAllEthnicAsync()
        {
            return await _ethnicRepository.GetAllEthnicAsync();
        }
    }

}
