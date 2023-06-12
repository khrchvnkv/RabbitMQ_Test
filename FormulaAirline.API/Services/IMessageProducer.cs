namespace FormulaAirline.API.Services
{
    public interface IMessageProducer
    {
        void SendMessage<T>(T message ); 
    }
}