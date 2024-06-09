using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

public class RoleStore : IRoleStore<IdentityRole>
{
    private readonly IDataStorage<IdentityRole> _roleStorage;
    private List<IdentityRole> _roles;

    public RoleStore(IDataStorage<IdentityRole> roleStorage)
    {
        _roleStorage = roleStorage;
        _roles = _roleStorage.GetAllAsync().Result.ToList();
    }

    public IQueryable<IdentityRole> Roles => _roles.AsQueryable();

    public async Task<IdentityResult> CreateAsync(IdentityRole role, CancellationToken cancellationToken)
    {
        _roles.Add(role);
        await _roleStorage.AddAsync(role);
        return IdentityResult.Success;
    }

    public async Task<IdentityResult> DeleteAsync(IdentityRole role, CancellationToken cancellationToken)
    {
        _roles.Remove(role);
        await _roleStorage.DeleteAsync(role.Id);
        return IdentityResult.Success;
    }

    public void Dispose()
    {

    }

    public async Task<IdentityRole> FindByIdAsync(string roleId, CancellationToken cancellationToken)
    {
        return await _roleStorage.GetAsync(roleId);
    }

    public async Task<IdentityRole> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
    {
        return _roles.FirstOrDefault(r => r.NormalizedName == normalizedRoleName);
    }

    public Task<string> GetNormalizedRoleNameAsync(IdentityRole role, CancellationToken cancellationToken)
    {
        return Task.FromResult(role.NormalizedName);
    }

    public Task<string> GetRoleIdAsync(IdentityRole role, CancellationToken cancellationToken)
    {
        return Task.FromResult(role.Id);
    }

    public Task<string> GetRoleNameAsync(IdentityRole role, CancellationToken cancellationToken)
    {
        return Task.FromResult(role.Name);
    }

    public Task SetNormalizedRoleNameAsync(IdentityRole role, string normalizedName, CancellationToken cancellationToken)
    {
        role.NormalizedName = normalizedName;
        return Task.CompletedTask;
    }

    public Task SetRoleNameAsync(IdentityRole role, string roleName, CancellationToken cancellationToken)
    {
        role.Name = roleName;
        return Task.CompletedTask;
    }

    public async Task<IdentityResult> UpdateAsync(IdentityRole role, CancellationToken cancellationToken)
    {
        var existingRole = _roles.FirstOrDefault(r => r.Id == role.Id);
        if (existingRole != null)
        {
            _roles.Remove(existingRole);
            _roles.Add(role);
            await _roleStorage.UpdateAsync(role);
        }
        return IdentityResult.Success;
    }
}
