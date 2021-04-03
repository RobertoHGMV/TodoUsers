using Microsoft.AspNetCore.Mvc;
using System;
using TodoUsers.Api.Rest.ViewModels;
using TodoUsers.Domain.Commands.Inputs.Users;
using TodoUsers.Domain.Services;

namespace TodoUsers.Api.Rest.Controllers
{
    [Route("api")]
    [ApiController]
    public class UserController : Controller
    {
        IUserService _service;

        public UserController(IUserService userService)
        {
            _service = userService;
        }

        [HttpGet, Route("v1/[controller]")]
        public IActionResult Get()
        {
            try
            {
                return Ok(new ResultViewModel { Success = true, Docs = _service.GetAll() });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResultViewModel { Success = false, Message = ex.Message, Docs = ex });
            }
        }

        [HttpGet, Route("v1/[controller]/{userName}")]
        public IActionResult Get(string userName)
        {
            try
            {
                var user = _service.GetByUserName(userName);

                if (user is null)
                    return NotFound(new ResultViewModel { Success = false, Message = $"Usuário [{userName}] não encontrado", Docs = { } });

                return Ok(new ResultViewModel { Success = true, Docs = user });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResultViewModel { Success = false, Message = ex.Message, Docs = ex });
            }
        }

        [HttpPost, Route("v1/[controller]")]
        public IActionResult Add(AddUserInput user)
        {
            try
            {
                var userInput = _service.Add(user);

                if (userInput.Invalid)
                    return BadRequest(new ResultViewModel { Success = false, Docs = userInput.Notifications });

                return Created($"v1/user/{userInput.Id}", new ResultViewModel { Success = true, Docs = userInput });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResultViewModel { Success = false, Message = ex.Message, Docs = ex });
            }
        }

        [HttpPut, Route("v1/[controller]")]
        public IActionResult Update(UpdateUserInput user)
        {
            try
            {
                var userUpdate = _service.Update(user);

                if (userUpdate.Invalid)
                    return BadRequest(new ResultViewModel { Success = false, Docs = userUpdate.Notifications });

                return Ok(new ResultViewModel { Success = true, Docs = userUpdate });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResultViewModel { Success = false, Message = ex.Message, Docs = ex });
            }
        }

        [HttpDelete, Route("v1/[controller]/{userName}")]
        public IActionResult Delete(string userName)
        {
            try
            {
                _service.Remove(userName);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new ResultViewModel { Success = false, Message = ex.Message, Docs = ex });
            }
        }
    }
}
