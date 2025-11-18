using Domain.Entities;

namespace WebAPI1.Domain.Interfaces;

public interface IFreelancerRepository
{
    Task<IEnumerable<Freelancer>> GetAllAsync();
    Task<Freelancer?> GetByIdAsync(int id);
    Task<int> CreateAsync(Freelancer freelancer);
    Task<bool> UpdateAsync(int id, Freelancer freelancer);
    Task<bool> DeleteAsync(int id);
    Task<IEnumerable<Freelancer>> SearchAsync(string keyword);
    Task<bool> ArchiveAsync(int id, bool archive);
}
