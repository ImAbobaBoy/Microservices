using Logic.Services.SemaphoreService;
using Logic.Services.SemaphoreService.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

/// <summary>
/// Тестовый контроллер для проверки работы семафора
/// </summary>
public class TestSemaphoreController : ControllerBase
{
    private ISemaphoreService _semaphoreService;

    /// <summary>
    /// Конструктор
    /// </summary>
    public TestSemaphoreController(ISemaphoreService semaphoreService)
    {
        _semaphoreService = semaphoreService;
    }
    
    /// <summary>
    /// Тестируем семафор на нескольких потоках
    /// Должен выдать только 2 пермита
    /// </summary>
    /// <returns></returns>
    [HttpGet("test-semaphore")]
    public async Task<IActionResult> TestSemaphore()
    {
        var semaphore = _semaphoreService.GetSemaphore();

        var tasks = Enumerable.Range(1, 5).Select(async i =>
        {
            await using var handle = await semaphore.TryAcquireAsync();
            if (handle != null)
            {
                Console.WriteLine($"Таска {i} получила пермит");
                await Task.Delay(20000); 
            }
            else
            {
                Console.WriteLine($"Таска {i} не смогла получить пермит");
            }
        });

        await Task.WhenAll(tasks);

        return Ok();
    }
}