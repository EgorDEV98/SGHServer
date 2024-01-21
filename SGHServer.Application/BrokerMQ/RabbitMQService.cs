using System.Diagnostics;
using System.Net;
using System.Text;
using SGHServer.Application.Interfaces;
using SGHServer.Application.Models;
using uPLibrary.Networking.M2Mqtt;

namespace SGHServer.Application.BrokerMQ;

public class RabbitMQService : IRabbitMQService
{
    public async Task Register(Guid guid)
    {
        var client = new MqttClient(IPAddress.Parse("92.255.107.96"));
        client.Connect("server", "server", "server");

        var routingKey = guid + ".Init";
        client.Publish(routingKey, Encoding.UTF8.GetBytes("OK"), 1, false);
    }
    
    /// <summary>
    /// Функция передаваемая на устройство
    /// </summary>
    /// <param name="device">GUID устройства</param>
    /// <param name="module">Модуль устройства</param>
    /// <param name="state">Состояние функции</param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task SendMessage(Guid device, string module, State state)
    {
        var client = new MqttClient(IPAddress.Parse("92.255.107.96"));
        client.Connect("server", "server", "server");

        var routingKey = device + "." + module;
        client.Publish(routingKey, Encoding.UTF8.GetBytes(state.ToString()), 1, false);
        
        await Task.CompletedTask;
    }
}