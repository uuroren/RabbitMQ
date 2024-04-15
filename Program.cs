using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQExample.Manager;
using System.Text;


RabbitMQManager rabbit = new RabbitMQManager();

#region Queue Publishler
Console.WriteLine("QUEUE PUBLİSH");
Console.WriteLine("-------------------------");

rabbit.Publisher("test_queue","test");

#endregion

#region QeueConsumer 
Console.WriteLine("-------------------------");
Console.WriteLine("QUEUE CONSUMER");
 
rabbit.Consumer("test_queue");

#endregion