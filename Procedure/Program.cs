// See https://aka.ms/new-console-template for more information

using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

var person = new
{
    Name = "Ad",
    SurName = "Soyad",
    Tc = "11111111111",
    Email = "a@a.com"
};

var factory = new ConnectionFactory()
{
    HostName = "localhost",
};
IConnection connection = factory.CreateConnection();
IModel channel = connection.CreateModel();
var queue = "BerkQueue";
channel.QueueDeclare(queue: queue, durable: false, exclusive: false, autoDelete: false, null);

string message = JsonSerializer.Serialize(person);
var body = Encoding.UTF8.GetBytes(message);

channel.BasicPublish(exchange:"",routingKey:queue,body:body);