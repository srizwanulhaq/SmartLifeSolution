
using Microsoft.Extensions.Configuration;
using MQTTnet.Protocol;
using MQTTnet.Server;
using SmartLifeSolution.BLL.Helpers;
using System;
using System.Net.NetworkInformation;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

//var config = new ConfigurationBuilder()
//           .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
//           .Build();


//string connectionString = config.GetConnectionString("DefaultConnection");

var mqttServerFactory = new MqttServerFactory();

var mqttServerOptions = new MqttServerOptionsBuilder()
    .WithDefaultEndpoint()
    .WithDefaultEndpointPort(1883)
    .WithKeepAlive()
    .Build();

var util = new EmailUtility();


using (var mqttServer = mqttServerFactory.CreateMqttServer(mqttServerOptions))
{

    mqttServer.ValidatingConnectionAsync += e =>
    {
        if (e.ClientId != "3071224A00039")
        {
            e.ReasonCode = MqttConnectReasonCode.ClientIdentifierNotValid;
            Console.WriteLine("Valid client");
        }
        else
            Console.WriteLine("invalid client");


        if (e.UserName != "admin" && e.Password != "123456")
        {
            e.ReasonCode = MqttConnectReasonCode.BadUserNameOrPassword;
        }

        return Task.CompletedTask;
    };

    mqttServer.InterceptingSubscriptionAsync += e =>
    {
        var data = JsonSerializer.Serialize(e.Response);
        Console.WriteLine("subscribe response:" + data);
        return Task.CompletedTask;
    };

    mqttServer.InterceptingPublishAsync += e =>
    {
        string data = Encoding.UTF8.GetString(e.ApplicationMessage.Payload);
        Console.WriteLine(data);
        return Task.CompletedTask;
    };


    mqttServer.ClientConnectedAsync += e =>
    {
        var data = JsonSerializer.Serialize(e.ClientId);
        Console.WriteLine("client connected: " + data);

        //   util.SendEmailAsync(data);
        return Task.CompletedTask;
    };


    mqttServer.ClientDisconnectedAsync += e =>
    {
        Console.WriteLine("client disconnecetd");
        return Task.CompletedTask;

    };

    mqttServer.ApplicationMessageEnqueuedOrDroppedAsync += e =>
    {
        string topic = e.ApplicationMessage.Topic;
        string payload = Encoding.UTF8.GetString(e.ApplicationMessage.Payload);
        Console.WriteLine($"Received message: Topic = {topic}, Payload = {payload}");
        return Task.CompletedTask;
    };

   

    await mqttServer.StartAsync();
    Console.WriteLine("server started");
    //await Task.Delay(-1);

    Console.ReadLine();

    //await mqttServer.StopAsync();



}

