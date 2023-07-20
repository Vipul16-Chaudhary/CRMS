using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCMS._4.O.Interfaces.RabbitMQInterface
{
    public interface IRabitMQProducerInterface
    {
        public  Task<bool> SendProductMessage<T>(T message);
    }
}
