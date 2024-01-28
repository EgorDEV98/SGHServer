using System.Diagnostics;
using System.Net;
using System.Text;
using SGHServer.Application.Interfaces;
using SGHServer.Application.Models;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace SGHServer.Application.BrokerMQ;

public class RabbitMQService : IRabbitMQService
{
    private bool messageIsRecived = false;

    public async Task<bool> SendMessageConfirming(Guid device, string module, State state)
    {
        var routingKey = device + "." + module;
        var client = new MqttClient(IPAddress.Parse("92.255.107.96"));
        client.Subscribe(new[] { routingKey + "/CONFIRM" }, new[] { (byte)1 });
        client.Connect("server", "server", "server");
        
        client.MqttMsgPublishReceived += ClientOnMqttMsgPublishReceived;

        client.Publish(routingKey, Encoding.UTF8.GetBytes(state.ToString()), 1, false);
        
        for (int i = 0; i < 50; i++)
        {
            if (messageIsRecived)
            {
                break;
            }
            await Task.Delay(200);
        }

        client.Unsubscribe(new[] { routingKey });
        if (messageIsRecived)
        {
            return true;
        }
        
        return false;
    }
    private void ClientOnMqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
    {
        messageIsRecived = true;
        Debug.WriteLine("[1] ClientOnMqttMsgPublishReceived:  "+ Encoding.UTF8.GetString(e.Message));
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