using SGHServer.Application.Models;

namespace SGHServer.Application.Interfaces;

public interface IRabbitMQService
{
    Task SendMessage(Guid device, string module, State state);
    Task Register(Guid guidDevice);
}