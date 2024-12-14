using Microsoft.AspNetCore.Identity;
using XYZ_Pharmaceuticals.Entities;
using Microsoft.EntityFrameworkCore;
using XYZ_Pharmaceuticals.Models;

namespace XYZ_Pharmaceuticals.Services
{
    public class CustomUserStore : IUserStore<Candidate>, IUserPasswordStore<Candidate>
    {
        private readonly AppDbContext _context;

        public CustomUserStore(AppDbContext context)
        {
            _context = context;
        }

        public Task<IdentityResult> CreateAsync(Candidate user, CancellationToken cancellationToken)
        {
            _context.Candidates.Add(user);
            return _context.SaveChangesAsync(cancellationToken).ContinueWith(task =>
                task.Result > 0 ? IdentityResult.Success : IdentityResult.Failed(),
                cancellationToken);
        }

        public Task<IdentityResult> UpdateAsync(Candidate user, CancellationToken cancellationToken)
        {
            _context.Candidates.Update(user);
            return _context.SaveChangesAsync(cancellationToken).ContinueWith(task =>
                task.Result > 0 ? IdentityResult.Success : IdentityResult.Failed(),
                cancellationToken);
        }

        public Task<IdentityResult> DeleteAsync(Candidate user, CancellationToken cancellationToken)
        {
            _context.Candidates.Remove(user);
            return _context.SaveChangesAsync(cancellationToken).ContinueWith(task =>
                task.Result > 0 ? IdentityResult.Success : IdentityResult.Failed(),
                cancellationToken);
        }

        public Task<Candidate> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            int id = int.Parse(userId);
            return _context.Candidates.FirstOrDefaultAsync(c => c.ID == id, cancellationToken);
        }

        public Task<Candidate> FindByEmailAsync(string email, CancellationToken cancellationToken)
        {
            return _context.Candidates.FirstOrDefaultAsync(c => c.Email == email, cancellationToken);
        }

        public Task<string> GetPasswordHashAsync(Candidate user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Password);
        }

        public Task<bool> HasPasswordAsync(Candidate user, CancellationToken cancellationToken)
        {
            return Task.FromResult(!string.IsNullOrEmpty(user.Password));
        }

        public Task SetPasswordHashAsync(Candidate user, string passwordHash, CancellationToken cancellationToken)
        {
            user.Password = passwordHash;
            return Task.CompletedTask;
        }

        public Task<string> GetUserIdAsync(Candidate user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.ID.ToString());
        }

        public Task<string> GetUserNameAsync(Candidate user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Email);
        }

        public Task SetUserNameAsync(Candidate user, string userName, CancellationToken cancellationToken)
        {
            user.Email = userName;
            return Task.CompletedTask;
        }

        public void Dispose()
        {
        }

        public Task<string?> GetNormalizedUserNameAsync(Candidate user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetNormalizedUserNameAsync(Candidate user, string? normalizedName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<Candidate?> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
