namespace VirtualNvhAnalyzer.App.Services.Mediator
{
    public class Mediator
    {
        private readonly Dictionary<Type, List<Delegate>> _subscribers = new();

        public void Subscribe<TMessage>(Action<TMessage> handler) where TMessage : class
        {
            var messageType = typeof(TMessage);

            if (!_subscribers.ContainsKey(messageType))
            {
                _subscribers[messageType] = new List<Delegate>();
            }
            _subscribers[messageType].Add(handler);
        }

        public void Unsubscribe<TMessage>(Action<TMessage> handler) where TMessage : class
        {
            var messageType = typeof(TMessage);
            if (_subscribers.ContainsKey(messageType))
            {
                _subscribers[messageType].Remove(handler);

                if (_subscribers[messageType].Count == 0)
                {
                    _subscribers.Remove(messageType);
                }
            }
        }

        public void Publish<TMessage>(TMessage message) where TMessage : class
        {
            var messageType = typeof(TMessage);

            if (_subscribers.ContainsKey(messageType))
            {
                var handlers = _subscribers[messageType];

                foreach (var handler in handlers)
                {

                    ((Action<TMessage>)handler)(message);

                }
            }
        }
    }
}
