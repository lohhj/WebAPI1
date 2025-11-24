using Domain.Entities;

namespace WebAPI1.Domain.Interfaces;

public interface IFreelancerRepository
{
    Task<IEnumerable<Freelancer>> GetAllAsync();
    Task<Freelancer?> GetByIdAsync(Guid id);
    Task<Guid> CreateAsync(Freelancer freelancer);
    Task<bool> UpdateAsync(Guid id, Freelancer freelancer);
    Task<bool> DeleteAsync(Guid id);
    Task<IEnumerable<Freelancer>> SearchAsync(string keyword);
    Task<bool> ArchiveAsync(Guid id, bool archive);
}
