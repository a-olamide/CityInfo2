namespace CityInfo2.API.Services
{
    public interface IMailService
    {
        void Send(string subject, string message);
    }
}