using Dapper;
using Domain.Entities;
using Npgsql;
using WebAPI1.Domain.Entities;
using WebAPI1.Domain.Interfaces;

namespace WebAPI1.Infrastructure.Data
{
    public class DapperFreelancerRepository(string connectionString) : IFreelancerRepository
    {
        private NpgsqlConnection CreateConnection() => new(connectionString);

        public async Task<IEnumerable<Freelancer>> GetAllAsync()
        {
            const string sql = @"
                SELECT id, username, email, phone_number AS PhoneNumber, archived FROM freelancers;
                SELECT id, freelancer_id AS FreelancerId, skill_name AS Skill FROM skillsets;
                SELECT id, freelancer_id AS FreelancerId, hobby_name AS Hobby FROM hobbies;";

            using var connection = CreateConnection();
            using var multi = await connection.QueryMultipleAsync(sql);

            var freelancers = (await multi.ReadAsync<Freelancer>()).ToDictionary(f => f.Id);
            var skills = (await multi.ReadAsync<Skillset>()).GroupBy(s => s.FreelancerId);
            var hobbies = (await multi.ReadAsync<Hobbies>()).GroupBy(h => h.FreelancerId);

            foreach (var freelancer in freelancers.Values)
            {
                if (skills.Any(g => g.Key == freelancer.Id))
                {
                    freelancer.Skillsets = skills.First(g => g.Key == freelancer.Id).ToList();
                }
                if (hobbies.Any(g => g.Key == freelancer.Id))
                {
                    freelancer.Hobbies = hobbies.First(g => g.Key == freelancer.Id).ToList();
                }
            }

            return freelancers.Values;
        }

        public async Task<Freelancer?> GetByIdAsync(int id)
        {
            using var connection = CreateConnection();
            var freelancer = await connection.QueryFirstOrDefaultAsync<Freelancer>(
                "SELECT id, username, email, phone_number AS PhoneNumber, archived FROM freelancers WHERE id=@Id",
                new { Id = id });

            if (freelancer != null)
            {
                freelancer.Skillsets = (await connection.QueryAsync<Skillset>(
                    "SELECT id, freelancer_id AS FreelancerId, skill_name AS Skill FROM skillsets WHERE freelancer_id=@FreelancerId",
                    new { FreelancerId = id })).ToList();

                freelancer.Hobbies = (await connection.QueryAsync<Hobbies>(
                    "SELECT id, freelancer_id AS FreelancerId, hobby_name AS Hobby FROM hobbies WHERE freelancer_id=@FreelancerId",
                    new { FreelancerId = id })).ToList();
            }

            return freelancer;
        }

        public async Task<int> CreateAsync(Freelancer freelancer)
        {
            using var connection = CreateConnection();
            await connection.OpenAsync();

            await using var transaction = await connection.BeginTransactionAsync();

            try
            {
                var id = await connection.ExecuteScalarAsync<int>(@"
                    INSERT INTO freelancers (username,email,phone_number,archived)
                    VALUES (@Username,@Email,@PhoneNumber,@Archived) RETURNING id",
                    freelancer,
                    transaction: transaction);

                foreach (var skill in freelancer.Skillsets)
                {
                    skill.FreelancerId = id;
                    await connection.ExecuteAsync(
                        "INSERT INTO skillsets (freelancer_id, skill_name) VALUES (@FreelancerId,@Skill)",
                        skill,
                        transaction: transaction);
                }

                foreach (var hobby in freelancer.Hobbies)
                {
                    hobby.FreelancerId = id;
                    await connection.ExecuteAsync(
                        "INSERT INTO hobbies (freelancer_id, hobby_name) VALUES (@FreelancerId,@Hobby)",
                        hobby,
                        transaction: transaction);
                }

                await transaction.CommitAsync();
                return id;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<bool> UpdateAsync(int id, Freelancer freelancer)
        {
            using var connection = CreateConnection();
            freelancer.Id = id;

            var rows = await connection.ExecuteAsync(
                @"UPDATE freelancers
                  SET username=@Username, email=@Email, phone_number=@PhoneNumber, archived=@Archived
                  WHERE id=@Id", freelancer);

            await connection.ExecuteAsync("DELETE FROM skillsets WHERE freelancer_id=@Id", new { Id = id });
            foreach (var skill in freelancer.Skillsets)
            {
                skill.FreelancerId = id;
                await connection.ExecuteAsync("INSERT INTO skillsets (freelancer_id, skill_name) VALUES (@FreelancerId,@Skill)", skill);
            }

            await connection.ExecuteAsync("DELETE FROM hobbies WHERE freelancer_id=@Id", new { Id = id });
            foreach (var hobby in freelancer.Hobbies)
            {
                hobby.FreelancerId = id;
                await connection.ExecuteAsync("INSERT INTO hobbies (freelancer_id, hobby_name) VALUES (@FreelancerId,@Hobby)", hobby);
            }

            return rows > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using var connection = CreateConnection();
            await connection.ExecuteAsync("DELETE FROM skillsets WHERE freelancer_id=@Id", new { Id = id });
            await connection.ExecuteAsync("DELETE FROM hobbies WHERE freelancer_id=@Id", new { Id = id });
            var rows = await connection.ExecuteAsync("DELETE FROM freelancers WHERE id=@Id", new { Id = id });
            return rows > 0;
        }

        public async Task<IEnumerable<Freelancer>> SearchAsync(string keyword)
        {
            using var connection = CreateConnection();
            var freelancers = (await connection.QueryAsync<Freelancer>(
                @"SELECT id, username, email, phone_number AS PhoneNumber, archived FROM freelancers
                  WHERE username ILIKE @Keyword OR email ILIKE @Keyword",
                new { Keyword = $"%{keyword}%" })).ToList();

            foreach (var f in freelancers)
            {
                f.Skillsets = (await connection.QueryAsync<Skillset>(
                    "SELECT id, freelancer_id AS FreelancerId, skill_name AS Skill FROM skillsets WHERE freelancer_id=@FreelancerId",
                    new { FreelancerId = f.Id })).ToList();

                f.Hobbies = (await connection.QueryAsync<Hobbies>(
                    "SELECT id, freelancer_id AS FreelancerId, hobby_name AS Hobby FROM hobbies WHERE freelancer_id=@FreelancerId",
                    new { FreelancerId = f.Id })).ToList();
            }

            return freelancers;
        }

        public async Task<bool> ArchiveAsync(int id, bool archive)
        {
            using var connection = CreateConnection();
            var rows = await connection.ExecuteAsync(
                "UPDATE freelancers SET archived=@Archived WHERE id=@Id",
                new { Id = id, Archived = archive });
            return rows > 0;
        }
    }
}
