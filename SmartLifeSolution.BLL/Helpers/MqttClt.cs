using Microsoft.Extensions.Options;
using MQTTnet;
using MQTTnet.Packets;
using SmartLifeSolution.DAL.Dao.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SmartLifeSolution.BLL.Helpers
{
    public class MqttClt
    {
        private readonly IOptions<MqttSettings> _settings;
        public MqttClt(IOptions<MqttSettings> settings)
        {
            _settings = settings;
        }
        public async Task<MqttClientPublishResult> Publish(string clientId, string topic, string payload)
        {
            var mqttFactory = new MqttClientFactory();

            using (var mqttClient = mqttFactory.CreateMqttClient())
            {
                var mqttClientOptions = new MqttClientOptionsBuilder()
                .WithClientId(clientId)
                .WithTcpServer(_settings.Value.ServerAddress, _settings.Value.Port)
                .WithCredentials(_settings.Value.UserName, _settings.Value.Password)
                .WithCleanSession(true)
            
                .Build();

                await mqttClient.ConnectAsync(mqttClientOptions, CancellationToken.None);

                var applicationMessage = new MqttApplicationMessageBuilder()
                    .WithTopic(topic)
                    .WithPayload(payload)
                    .WithRetainFlag()
                    //.WithQualityOfServiceLevel(MQTTnet.Protocol.MqttQualityOfServiceLevel.AtLeastOnce)
                    .Build();


                var result = await mqttClient.PublishAsync(applicationMessage, CancellationToken.None);

            //    Task.Delay(5000).Wait();

               // await mqttClient.DisconnectAsync();

                return result;
            }
        }

        public async Task<MqttClientSubscribeResult> Subscribe(string clientId, string topic, string payload)
        {
            var factory = new MqttClientFactory();

            var mqttClient = factory.CreateMqttClient();

            var options = new MqttClientOptionsBuilder()
                .WithTcpServer(_settings.Value.ServerAddress, _settings.Value.Port)
                .WithCredentials(_settings.Value.UserName, _settings.Value.Password)
                .WithClientId(clientId)
                .WithCleanSession()
                .Build();

            await mqttClient.ConnectAsync(options, CancellationToken.None);

            var result = await mqttClient.SubscribeAsync(new MqttTopicFilterBuilder()
                .WithTopic(topic)
                .Build());

            mqttClient.ApplicationMessageReceivedAsync += e =>
            {
                Console.WriteLine("we got data");

                Console.WriteLine($"Received message: {Encoding.UTF8.GetString(e.ApplicationMessage.Payload)}");
                return Task.CompletedTask;
            };

            await mqttClient.DisconnectAsync();

            return result;
        }

    }

}


