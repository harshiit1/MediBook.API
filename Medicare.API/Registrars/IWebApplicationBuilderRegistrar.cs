namespace Medicare_API.Registrars
{
    public interface IWebApplicationBuilderRegistrar : IRegistrar
    {
        void RegisterServices(WebApplicationBuilder builder);
    }
}
