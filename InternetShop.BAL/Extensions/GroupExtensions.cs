using InternetShop.BAL.DTOs.Group;
using InternetShop.DAL.Entities;

namespace InternetShop.BAL.Extensions
{
    public static class GroupExtensions
    {
        public static Group MapFromDto(this Group group, GroupDTO dto)
        {
            group.Name = dto.Name;
            group.Description = dto.Description;
            return group;
        }
    }
}
