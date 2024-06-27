using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using VuDucNam_L1.DataAccess;
using VuDucNam_L1.Migrations;
using VuDucNam_L1.Models;
using VuDucNam_L1.Repository.IRepositories;

namespace VuDucNam_L1.Repository.Repositories
{
    public class JobRepository : IJobRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public JobRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<JobModel>> GetAllJobAsync()
        {
            var jobs = await _context.Jobs.ToListAsync();
            return _mapper.Map<List<JobModel>>(jobs);
        }
        public async Task CheckJobIdAsync(int jobId)
        {
            var jobExists = await _context.Jobs.AsNoTracking().AnyAsync(e => e.JobId == jobId);
            if (!jobExists)
            {
                throw new Exception($"Job with ID {jobId} not found.");
            }
        }
        public async Task<int> GetJobIdByNameAsync(string jobName)
        {
            var job = await _context.Jobs.AsNoTracking().FirstOrDefaultAsync(j => j.JobName == jobName);
            return job == null ? throw new InvalidOperationException($"Job '{jobName}' not found.") : job.JobId;
        }
    }
}
