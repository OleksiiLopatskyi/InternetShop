using InternetShop.BAL.Extensions;
using InternetShop.DAL.Contracts;
using InternetShop.BAL.Contracts;
using InternetShop.BAL.DTOs.Group;
using InternetShop.DAL.Entities;
using InternetShop.BAL.Models;

namespace InternetShop.BAL.Services.GroupService
{
    public class GroupService : IGroupService
    {
        private IRepositoryWrapper _repositoryWrapper;

        public GroupService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task<Result<IEnumerable<Group>>> GetGroupsAsync()
        {
            try
            {
                var groups = await _repositoryWrapper.GroupRepository.FindAllAsync();
                return new Result<IEnumerable<Group>> { Data = groups };
            }
            catch (Exception ex)
            {
                return new Result<IEnumerable<Group>>
                {
                    Message = ex.Message,
                    StatusCode = StatusCodes.InternalServerError
                };
            }
        }

        public async Task<Result> CreateAsync(GroupDTO groupDto)
        {
            var mappedGroup = new Group().MapFromDto(groupDto);
            try
            {
                var group = await _repositoryWrapper.GroupRepository
                    .FindEntityAsync(i => i.Name == mappedGroup.Name);
                if (group != null)
                {
                    return new Result
                    {
                        Message = "That group already exists",
                        StatusCode = StatusCodes.BadRequest
                    };
                }
                await _repositoryWrapper.GroupRepository.CreateAsync(mappedGroup);
                await _repositoryWrapper.SaveAsync();
                return new Result<Group> { Data = mappedGroup };
            }
            catch (Exception ex)
            {
                return new Result
                {
                    Message = ex.Message,
                    StatusCode = StatusCodes.InternalServerError
                };
            }
        }

        public async Task<Result<Group>> GetByIdAsync(int groupId)
        {
            try
            {
                var group = await _repositoryWrapper.GroupRepository.FindEntityAsync(i => i.Id == groupId);
                if (group == null)
                {
                    return new Result<Group>
                    {
                        Message = "Group doesn't exists",
                        StatusCode = StatusCodes.NotFound
                    };
                }
                return new Result<Group> { Data = group };
            }
            catch (Exception ex)
            {
                return new Result<Group>
                {
                    Message = ex.Message,
                    StatusCode = StatusCodes.InternalServerError
                };
            }
        }

        public async Task<Result> DeleteAsync(int groupId)
        {
            try
            {
                var group = await _repositoryWrapper.GroupRepository
                    .FindEntityAsync(i => i.Id == groupId);
                if (group == null)
                {
                    return new Result
                    {
                        Message = "Group doesn't exists",
                        StatusCode = StatusCodes.NotFound
                    };
                }
                _repositoryWrapper.GroupRepository.Delete(group);
                await _repositoryWrapper.SaveAsync();
                return new Result<Group> { Data = group };
            }
            catch (Exception ex)
            {
                return new Result { Message = ex.Message, StatusCode = StatusCodes.InternalServerError };
            }
        }

        public async Task<Result> UpdateAsync(int groupId, GroupDTO groupDto)
        {
            try
            {
                var group = await _repositoryWrapper.GroupRepository.FindEntityAsync(i => i.Id == groupId);
                if (group == null)
                {
                    return new Result
                    {
                        Message = "Group doesn't exists",
                        StatusCode = StatusCodes.NotFound
                    };
                }
                group.MapFromDto(groupDto);
                _repositoryWrapper.GroupRepository.Update(group);
                await _repositoryWrapper.SaveAsync();
                return new Result<Group> { Data = group };
            }
            catch (Exception ex)
            {
                return new Result
                {
                    Message = ex.Message,
                    StatusCode = StatusCodes.InternalServerError
                };
            }
        }
    }
}