using Sim.Application.ViewModels;
using Sim.Application.Exceptions;
using Sim.Domain.Abstractions;
using Sim.Domain.Constants;
using Sim.Domain.Entities;
using Sim.Application.Abstractions;

namespace Sim.Application.Services;

public class UserService: IUserService
{
    private readonly IUnitOfWork _unitOfWork;

    public UserService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task EnrollAsync(CreateUserViewModel viewModel)
    {
        //var User = new User(viewModel.FirstName, viewModel.Email);

        //var existingUser = await _unitOfWork.Users.GetByEmailAsync(viewModel.Email);
        //if (existingUser != null)
        //    throw new BusinessLogicException(ValidationMessages.DuplicateEmail);

        //await _unitOfWork.Users.AddAsync(User);
        //await _unitOfWork.CommitAsync();
    }

    public async Task<IEnumerable<UserViewModel>> GetAllAsync()
    {
        var Users = await _unitOfWork.Users.GetAllAsync();
        return Users.Select(s => new UserViewModel(s.Id));
    }

    public async Task DeactivateAsync(Guid id)
    {
        var User = await _unitOfWork.Users.GetByIdAsync(id);
        if (User == null)
            throw new BusinessLogicException(ValidationMessages.UserNotFound);

        User.Deactivate();
        await _unitOfWork.CommitAsync();
    }
}