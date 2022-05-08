using InternetShop.BAL.Models;
using InternetShop.BAL.DTOs.Group;
using InternetShop.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetShop.BAL.Contracts
{
    public interface IGroupService
    {
        Task<Result<IEnumerable<Group>>> GetGroupsAsync();
        Task<Result<Group>> GetByIdAsync(int groupId);
        Task<Result> CreateAsync(GroupDTO groupDto);
        Task<Result> UpdateAsync(int groupId, GroupDTO groupDto);
        Task<Result> DeleteAsync(int groupId);
    }
}
