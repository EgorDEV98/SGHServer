﻿using SGHServer.Application.Models;

namespace SGHServer.Application.Interfaces;

public interface IRabbitMQService
{
    Task<bool> SendMessageConfirming(Guid device, string module, State state);
    Task SendMessage(Guid device, string module, State state);
}