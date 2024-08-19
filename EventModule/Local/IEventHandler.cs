using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventModule.Local
{
    public interface IEventHandler
    {
        Task Handle<T>(T eventData);
    }
}
