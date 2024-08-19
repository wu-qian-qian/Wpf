using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventModule.Local
{
    public class EventManager
    {
        private readonly Dictionary<string, List<Type>> _handlers;
        public EventManager()
        {
            _handlers = new Dictionary<string, List<Type>>();
        }
        public bool HasSubscriptionsForEvent(string eventName) => _handlers.ContainsKey(eventName);

        public IEnumerable<Type> GetHandlerForEvent(string eventName) => _handlers[eventName];

        /// <summary>
        /// 添加订阅
        /// </summary>
        /// <param name="eventName"></param>
        /// <param name="handler"></param>
        public void AddSubscription(string eventName, Type handler)
        {
            if (!HasSubscriptionsForEvent(eventName))
            {
                List<Type> handlers = new List<Type>();
                handlers.Add(handler);
                _handlers.Add(eventName, handlers);
            }
            if (!_handlers[eventName].Contains(handler))
            {
                _handlers[eventName].Add(handler);
            }
        }
        /// <summary>
        /// 移除订阅
        /// </summary>
        /// <param name="eventName"></param>
        /// <param name="handler"></param>
        public void RemoveSubscription(string eventName, Type handler)
        {
            if (HasSubscriptionsForEvent(eventName))
            {
                _handlers[eventName].Remove(handler);
            }
            if (!_handlers[eventName].Any())
            {
                _handlers.Remove(eventName);
            }
        }
    }
}
