using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventModule.Local
{
    public class EventModule : IEventBus
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly EventManager _subManager;


        public EventModule(IServiceProvider serviceProvider, EventManager eventManager)
        {
            _serviceProvider = serviceProvider;
            _subManager = eventManager;

        }

        /// <summary>
        /// 并行触发调用
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="eventName"></param>
        /// <param name="eventData"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task  Publish<T>(string eventName, T eventData,CancellationToken token=default)
        {
            var eventTypes= _subManager.GetHandlerForEvent(eventName);
            Parallel.ForEach(eventTypes,   eventType =>
            {
                var obj= _serviceProvider.CreateScope().ServiceProvider.GetService(eventType) as IEventHandler;
                 obj?.Handle<T>(eventData).GetAwaiter().GetResult();
            });
        }

        public void Subscribe(string eventName, Type handlerType)
        {
            CheckHandlerType(handlerType);
            _subManager.AddSubscription(eventName, handlerType);
        }

        public void UnSubscribe(string eventName, Type handlerType)
        {
            CheckHandlerType(handlerType);
            _subManager.RemoveSubscription(eventName, handlerType);
        }

        private void CheckHandlerType(Type handlerType)
        {
            if ((handlerType.IsAbstract && !(typeof(IEventHandler)).IsAssignableFrom(handlerType)))
            {
                throw new ArgumentException($"{handlerType} doesn't inherit from IIntegrationEventHandler", nameof(handlerType));
            }
        }
    }
}
