using MediatR;

namespace EventsModule.Logic.Wrapper
{
    public class RegisterRequest<T> : IRequest<T> where T : class
    {
        public int ID { get; set; }
    }
}
