using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StoreNet.Application.Interfaces.Persistence;
using StoreNet.Domain.Entities;
using StoreNet.Domain.Filters.User;
using StoreNet.Infrastructure.Data;

namespace StoreNet.Infrastructure.Persistence;

public class UserRepository : IUserRepository
{
    private readonly UserManager<AppUser> _userManager;
    private readonly ApplicationDbContext _context;

    public UserRepository(UserManager<AppUser> userManager, ApplicationDbContext context)
    {
        _userManager = userManager;
        _context = context;
    }

    public async Task<AppUser?> GetUserByIdAsync(Guid id)
    {
        return await _userManager.FindByIdAsync(id.ToString());
    }

    public async Task<IReadOnlyList<AppUser>> GetAllUsersAsync(UserFilter filter)
    {
        IQueryable<AppUser> query = _context.Users
            .Include(u => u.Addresses)
            .Include(u => u.Orders)
            .AsQueryable();

        if (filter.Roles != null && filter.Roles.Count > 0)
        {
            var usersWithRoles = new List<AppUser>();

            foreach (var role in filter.Roles)
            {
                var usersInRole = await _userManager.GetUsersInRoleAsync(role);

                usersWithRoles.AddRange(usersInRole);
            }

            query = query.Where(u => usersWithRoles.Contains(u));
        }

        if (!string.IsNullOrEmpty(filter.SearchTerm))
        {
            query = query.Where(u => u.FirstName.Contains(filter.SearchTerm) ||
                                     u.LastName.Contains(filter.SearchTerm) ||
                                     u.Email!.Contains(filter.SearchTerm));
        }

        if (filter.IsActive.HasValue)
        {
            query = query.Where(u => u.IsAvailable == filter.IsActive.Value);
        }

        // Tri des résultats
        if (!string.IsNullOrEmpty(filter.SortBy))
        {
            query = filter.SortBy switch
            {
                "Name" => query.OrderBy(u => u.FirstName).ThenBy(u => u.LastName),
                "Email" => query.OrderBy(u => u.Email),
                "DateCreated" => query.OrderBy(u => u.CreatedAt),
                _ => query.OrderBy(u => u.LastName) 
            };
        }
        else
        {
            query = query.OrderBy(u => u.Id);  
        }

        int totalCount = await query.CountAsync();

        var users = await query
            .Skip((filter.PageIndex - 1) * filter.PageSize)
            .Take(filter.PageSize)
            .ToListAsync();

        return users.AsReadOnly();
    }
    public async Task<bool> UpdateUserAsync(AppUser user)
    {
        var result = await _userManager.UpdateAsync(user);
        return result.Succeeded;
    }

    public async Task<bool> DeleteUserAsync(Guid id)
    {
        var user = await GetUserByIdAsync(id);
        if (user is null) return false;

        var result = await _userManager.DeleteAsync(user);
        return result.Succeeded;
    }

    public async Task<bool> DeactivateUserAsync(Guid id)
    {
        var user = await GetUserByIdAsync(id);
        if (user is null) return false;

        user.SetAvailability(false);
        return await UpdateUserAsync(user);
    }

    public Task<AppUser?> GetUserByEmailAsync(string email)
    {
        return _userManager.Users
            .FirstOrDefaultAsync(u => u.Email == email);
    }
}