using Core;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace MessageBroker
{
    public class MessagePublisher : IMessagePublisher
    {
        IConnectionMaster _connectionMaster;

        public MessagePublisher(IConnectionMaster connectionMaster)
        {
            _connectionMaster = connectionMaster;
        }

        public void PublishMessage(object message)
        {
            string serializeMessage = JsonConvert.SerializeObject(message, Formatting.None);
            var body = Encoding.UTF8.GetBytes(serializeMessage);

            IBasicProperties props = _connectionMaster.Channel.CreateBasicProperties();
            props.DeliveryMode = 2;

            _connectionMaster.Channel.BasicPublish(exchange: "",
                                 routingKey: _connectionMaster.QueueName,
                                 basicProperties: props,
                                 body: body);
        }
    }
}
