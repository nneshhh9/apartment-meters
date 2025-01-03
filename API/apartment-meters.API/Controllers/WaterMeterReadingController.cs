using System.Net;
using Application.Interfaces.Commands;
using Application.Interfaces.Queries;
using Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

/// <summary>
/// Контроллер для работы с показаниями водомеров
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class WaterMeterReadingController : ControllerBase
{
    private readonly IWaterMeterReadingCommandService _commandService;
    private readonly IWaterMeterReadingQueryService _queryService;

    public WaterMeterReadingController(IWaterMeterReadingCommandService commandService, IWaterMeterReadingQueryService queryService)
    {
        _commandService = commandService;
        _queryService = queryService;
    }

    /// <summary>
    /// Получить все показания водомеров
    /// </summary>
    [HttpGet]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> GetAllWaterMeterReadings()
    {
        var readings = await _queryService.GetAllMeterReadingAsync();
        return Ok(readings);
    }
    
    /// <summary>
    /// Получить все показания водомеров по пользователю
    /// </summary>
    /// <param name="userId">Идентификатор пользователя</param>
    [HttpGet("{userId:guid}")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> GetMeterReadingsByUserId(Guid userId)
    {
        var readings = await _queryService.GetMeterReadingByUserIdAsync(userId);
        return Ok(readings);
    }

    /// <summary>
    /// Добавить новое показание
    /// </summary>
    /// <param name="request">Модель добавления показания</param>
    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> AddWaterMeterReading([FromBody] AddWaterMeterReadingDto request)
    {
        await _commandService.AddMeterReadingAsync(request);
        return Ok("Показание добавлено");
    }
    
    /// <summary>
    /// Обновить данные показания водомеров
    /// </summary>
    /// <param name="id">Идентификатор пользователя</param>
    /// <param name="updateWaterMeterReadingDto">Обновленные данные показания водомера</param>
    [HttpPut("{id:guid}")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> UpdateWaterMeterReading(Guid id, [FromBody] UpdateWaterMeterReadingDto updateWaterMeterReadingDto)
    {
        await _commandService.UpdateMeterReadingAsync(id, updateWaterMeterReadingDto);
        return NoContent();
    }

    /// <summary>
    /// Удалить показание
    /// </summary>
    /// <param name="id">Идентификатор показания</param>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> DeleteWaterMeterReading(Guid id)
    {
        await _commandService.DeleteMeterReadingAsync(id);
        return Ok("Показание удалено");
    }
}