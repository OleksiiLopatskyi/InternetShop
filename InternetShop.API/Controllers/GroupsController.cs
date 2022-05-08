using Microsoft.AspNetCore.Mvc;
using InternetShop.BAL.DTOs.Group;
using InternetShop.DAL.Entities;
using InternetShop.BAL.Contracts;
using InternetShop.API.Validation;

namespace InternetShop.API.Controllers
{
    [Route("api/groups")]
    [ApiController]
    public class GroupsController : CustomControllerBase
    {
        private IGroupService _groupService;

        public GroupsController(IGroupService groupService)
        {
            _groupService = groupService;
        }

        [RoleAuthorize(Role = Role.User)]
        [HttpGet]
        public async Task<IActionResult> GetAllGroups()
        {
            var result = await _groupService.GetGroupsAsync();
            return CustomResult(result);
        }

        [RoleAuthorize(Role = Role.Admin)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetGroup(int id)
        {
            var result = await _groupService.GetByIdAsync(id);
            return CustomResult(result);
        }

        [RoleAuthorize(Role = Role.Admin)]
        [HttpPost]
        public async Task<IActionResult> CreateGroup([FromBody] GroupDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _groupService.CreateAsync(model);
            return CustomResult(result);
        }

        [RoleAuthorize(Role = Role.Admin)]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGroup(int id, [FromBody] GroupDTO model)
        {
            var result = await _groupService.UpdateAsync(id, model);
            return CustomResult(result);
        }

        [RoleAuthorize(Role = Role.Admin)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGroup(int id)
        {
            var result = await _groupService.DeleteAsync(id);
            return CustomResult(result);
        }
    }
}
