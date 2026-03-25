using Sim.Application.ViewModels;

namespace Sim.Application.Abstractions;

public interface IUserService
{
    Task EnrollAsync(CreateUserViewModel viewModel);
    Task<IEnumerable<UserViewModel>> GetAllAsync();
    Task DeactivateAsync(Guid id);
}