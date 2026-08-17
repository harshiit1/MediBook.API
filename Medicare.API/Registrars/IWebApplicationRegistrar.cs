namespace Medicare_API.Registrars
{
    public interface IWebApplicationRegistrar : IRegistrar
    {
        public void RegistrarPipelineComponents(WebApplication app);
    }
}
