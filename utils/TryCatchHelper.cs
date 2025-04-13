using LibraryManagement.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.utils;

public static class TryCatchHelper
{
    public static async Task TryCatchAsync(Func<Task> action, ILogger logger, Controller controller, string methodName)
    {
        try
        {
            await action();
        }
        catch (LibraryException ex)
        {
            controller.TempData["Error"] = ex.Message;
            logger.Log(LogLevel.Error, $"{nameof(Controller)}::{methodName}\r\n{ex.Message}");
        }
        catch (Exception ex)
        {
            controller.TempData["Error"] = "An unexpected error occurred!";
            logger.Log(LogLevel.Error, $"{nameof(Controller)}::{methodName}\r\n{ex.Message}");
        }
    }
    
    public static async Task<T?> TryCatchAsync<T>(Func<Task<T>> func, ILogger logger, Controller controller, string methodName)
    {
        try
        {
            return await func();
        }
        catch (LibraryException ex)
        {
            controller.TempData["Error"] = ex.Message;
            logger.Log(LogLevel.Error, $"{controller.GetType().Name}::{methodName}\r\n{ex.Message}");
        }
        catch (Exception ex)
        {
            controller.TempData["Error"] = "An unexpected error occurred!";
            logger.Log(LogLevel.Error, $"{controller.GetType().Name}::{methodName}\r\n{ex.Message}");
        }

        return default;
    }
}
