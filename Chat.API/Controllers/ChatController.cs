using Chat.Application.Common.Interfaces;
using Chat.Persistence.Dependencies.IServices;
using Microsoft.AspNetCore.Mvc;

namespace Chat.API.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class ChatController : ControllerBase
{
    private readonly IServerService _server;

    public ChatController(
        IServerService server)
    {
        _server = server;
    }
    
    [HttpGet]
    [Route("/[action]/{id}")]
    public async Task<IActionResult> GetServer(string id)
    {
        var result = await _server.GetAsync(id)!;
        return Ok(result);
    }
    
    [HttpPost]
    [Route("/[action]")]
    public async Task<IActionResult> CreateServer()
    {
        
        
        return Ok();
    }



}