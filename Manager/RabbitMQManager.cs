using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RabbitMQExample.Manager {
    public class RabbitMQManager {
        ConnectionFactory factory;
        IConnection connection;
        IModel channel;
        public RabbitMQManager() {

            factory = new ConnectionFactory();
            factory.Uri = new Uri("amqps://rlutfbvs:AaXbpeJXLC97vwuc2rSM0rotOlrKURBm@shrimp.rmq.cloudamqp.com/rlutfbvs");

            connection = factory.CreateConnection();
            channel = connection.CreateModel();
        }

        public void Publisher(string quue,string message) {
            Console.WriteLine($"PUBLISING QUUEUE: {quue}, Message : {message}");
            using(IConnection connection = factory.CreateConnection())
            using(IModel channel = connection.CreateModel()) {
                channel.QueueDeclare(quue,false,false,false);
                byte[] bytemessage = Encoding.UTF8.GetBytes(message);
                channel.BasicPublish(exchange: "",routingKey: quue,body: bytemessage);
            }
        }

        public void Consumer(string queue) {
            channel.QueueDeclare(queue,durable: false,exclusive: false,autoDelete: false,arguments: null);

            EventingBasicConsumer consumer = new EventingBasicConsumer(channel);
            channel.BasicConsume(queue,false,consumer);
            consumer.Received += (sender,e) => {
                //e.Body : Kuyruktaki mesajı verir.
                Console.WriteLine("MESSAGE: " + Encoding.UTF8.GetString(e.Body.ToArray()));
            };

            Console.WriteLine("----------------------------");
            Console.Read();
        }
    }
}
