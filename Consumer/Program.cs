// See https://aka.ms/new-console-template for more information

using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

var factory = new ConnectionFactory()
{
    HostName = "localhost",
};
IConnection connection = factory.CreateConnection();
IModel channel = connection.CreateModel();

var queue = "BerkQueue";
channel.QueueDeclare(queue: queue, durable: false, exclusive: false, autoDelete: false,null);

var consumer = new EventingBasicConsumer(channel);
consumer.Received += (model, ea) =>
{
    var body = Encoding.UTF8.GetString(ea.Body.ToArray());
    Console.WriteLine(body);
};

channel.BasicConsume(queue, autoAck: true, consumer: consumer);
Console.Read();