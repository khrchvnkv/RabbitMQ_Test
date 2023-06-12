using System.Text;
using System.Text.Json;
using RabbitMQ.Client;

namespace FormulaAirline.API.Services
{
    public class MessageProducer : IMessageProducer
    {
        public void SendMessage<T>(T message)
        {
            var factory = new ConnectionFactory()
            {
                HostName = "localhost",
                UserName = "user",
                Password = "mypass",
                VirtualHost = "/"
            };
            var connection = factory.CreateConnection();
            var chanel = connection.CreateModel();
            
            chanel.QueueDeclare("bookings", durable: true, exclusive: false);
            var jsonString = JsonSerializer.Serialize(message);
            var body = Encoding.UTF8.GetBytes(jsonString);
            
            chanel.BasicPublish("", "bookings", body: body);
        }
    }
}