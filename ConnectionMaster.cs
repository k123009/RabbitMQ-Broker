using Core;
using RabbitMQ.Client;
using System;
using System.Net;

namespace MessageBroker
{
    public class ConnectionMaster : IConnectionMaster
    {
        ConnectionFactory _connectionFactory;
        IConnection _connection = null;
        IModel _channel = null;

        public string QueueName
        {
            get { return "PaymentRequest"; }
        }

        public IModel Channel
        {
            get
            {
                if (_channel == null || _channel.IsClosed)
                {
                    _connection = GetConnection();
                    _channel = _connection.CreateModel();
                }

                return _channel;
            }
        }

        public ConnectionMaster(IConfig config)
        {
            _connectionFactory = new ConnectionFactory()
            {
                HostName = config.QueueHostName,
                UserName = config.QueueUserName,
                Password = config.QueuePassword
            };

            Channel.QueueDeclare(queue: QueueName,
                                 durable: true,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);
        }

        IConnection GetConnection()
        {
            if (_connection == null || !_connection.IsOpen)
                _connection = _connectionFactory.CreateConnection();

            return _connection;
        }
    }
}
