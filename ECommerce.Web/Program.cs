
using Ecommerce.Domain.Contracts;
using Ecommerce.Persistence.DependancyInjection;
using Ecommerce.Services.DependencyInjection;
using ECommerce.Web.Middlewares;

namespace ECommerce.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddPersistenceServices(builder.Configuration);
            builder.Services.AddApplicationServices();
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // initialize database
            var scope = app.Services.CreateScope();
            var initializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
            await initializer.InitializeAsync();  //Database Creation (Data Seeding)

            //Middleware
            //1 way
            /*app.Use(async (context, next) =>
            {
                try
                {
                    await next.Invoke(context);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message); //logging
                    //response
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    await context.Response.WriteAsJsonAsync(new
                    {
                        StatusCode = StatusCodes.Status500InternalServerError,
                        Message = ex.Message,

                    });
                }
            });*/
            //2 way by conventional Middleware
            //app.UseMiddleware<GlobalExceptionHandler>();

            //3 way by extension method CustomExceptionHandler
            app.UseCustomExceptionHandler();


            //___________________
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            //picture
            app.UseStaticFiles();

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
