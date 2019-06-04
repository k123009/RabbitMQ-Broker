using RabbitMQ.Client;
using System;

namespace MessageBroker
{
    public class MessageConsumer : IMessageConsumer
    {
        IConnectionMaster _connectionMaster;

        public MessageConsumer(IConnectionMaster connectionMaster)
        {
            _connectionMaster = connectionMaster;
        }

        public BasicGetResult GetMessage()
        {
            return _connectionMaster.Channel.BasicGet(_connectionMaster.QueueName, false);
        }

        public void SendAck(ulong deliveryTag, bool multiple)
        {
            _connectionMaster.Channel.BasicAck(deliveryTag, multiple);
        }

        public void SendNack(ulong deliveryTag, bool multiple, bool requeue)
        {
            _connectionMaster.Channel.BasicNack(deliveryTag, multiple, requeue);
        }
    }
}
