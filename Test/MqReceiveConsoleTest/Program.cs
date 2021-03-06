﻿using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MqReceiveConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory();
            factory.HostName = "localhost";
            factory.UserName = "guest";
            factory.Password = "guest";

            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    int i = 0;

                   

                    // channel.QueueDeclare("test", false, false, false, null);
                    //channel.BasicQos(0, 1, false);

                    var consumer = new QueueingBasicConsumer(channel);
                    channel.BasicConsume("test", false, consumer);

                    while (true)
                    {
                        var ea = (BasicDeliverEventArgs)consumer.Queue.Dequeue();
                        i++;
                        var body = ea.Body;
                        var message = Encoding.UTF8.GetString(body);

                        channel.BasicAck(ea.DeliveryTag, false);
                        Console.WriteLine(i);
                    }


                }
            }
        }
    }
}
