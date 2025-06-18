namespace VirtualNvhAnalyzer.App.Services.Mediator
{
    public interface IMediator
    {
        void Subscribe<TMessage>(Action<TMessage> handler) where TMessage : class;
        void Unsubscribe<TMessage>(Action<TMessage> handler) where TMessage : class;
        void Publish<TMessage>(TMessage message) where TMessage : class;
    }
}
