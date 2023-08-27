using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using MongoDbDotNet.Core.Abstracts;
using MongoDbDotNet.Core.Dtos;
using MongoDbDotNet.Core.Entities;

namespace MongoDbDotNet.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    [Produces("application/json")]
    public class ChatController : ControllerBase
    {
        private readonly ILogger<ChatController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;

        public ChatController(ILogger<ChatController> logger, IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _configuration = configuration;
        }


        [ProducesResponseType(typeof(List<Chat>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status500InternalServerError)]
        [HttpGet]
        public async Task<IActionResult> GetAllChats()
        {
            try
            {
                var version = _configuration.GetValue<string>("Chat:Version")!;
                var result = await _unitOfWork.Chats.GetAllChatsAsync(version);

                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponseDto(e.Message));
            }
        }

        [ProducesResponseType(typeof(List<Chat>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status500InternalServerError)]
        [HttpGet]
        [Route("email")]
        public async Task<IActionResult> GetChatsByEmail(string userEmail)
        {
            try
            {
                var version = _configuration.GetValue<string>("Chat:Version")!;
                var result = await _unitOfWork.Chats.GetAllByEmailAsync(userEmail, version);

                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponseDto(e.Message));
            }
        }

        [ProducesResponseType(typeof(Chat), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status500InternalServerError)]
        [HttpGet]
        public async Task<IActionResult> CreateChat(CreateChatRequestModel model)
        {
            try
            {
                var version = _configuration.GetValue<string>("Chat:Version")!;
                var chat = new Chat()
                {
                    Version = version,
                    Message = new Message()
                    {
                        CreatedDate = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                        CreatedTimestamp = DateTimeOffset.Now.ToUnixTimeSeconds(),
                        User = model.UserEmail,
                        Topic = model.Topic,
                        Content = model.Content
                    }
                };

                await _unitOfWork.Chats.InsertAsync(chat);
                //after insert id will created and will be assigned to Id field.

                return Created(new Uri($"Chat/GetChatsByEmail/{model.UserEmail}"), chat);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponseDto(e.Message));
            }
        }

        //and you can add more Api endpoints due to your need ...
    }
}