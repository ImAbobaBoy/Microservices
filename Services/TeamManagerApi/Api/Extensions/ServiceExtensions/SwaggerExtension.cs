namespace API.Extensions
{
    /// <summary>
    /// Расширение для добавления сваггера
    /// </summary>
    public static class SwaggerExtensions
    {
        public static void AddSwaggerDocumentation(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }

        public static void UseSwaggerDocumentation(this WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "TeamManager API V1");
                c.RoutePrefix = "swagger"; 
            });
        }
    }
}