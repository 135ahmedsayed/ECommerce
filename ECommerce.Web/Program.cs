
using Ecommerce.Domain.Contracts;
using Ecommerce.Persistence.DependancyInjection;
using Ecommerce.Services.DependencyInjection;
using Ecommerce.Services.Service.Exceptions;
using ECommerce.Web.Handler;
using ECommerce.Web.Middlewares;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

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
            //ExceptionHendler
            builder.Services.AddExceptionHandler<ExceptionHendler>();
            builder.Services.AddProblemDetails();
            //_______________________
            //Validation
            builder.Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    var errors = actionContext.ModelState.Where(x => x.Value.Errors.Count > 0)
                    .ToDictionary(x => x.Key,
                                  y=> y.Value.Errors.Select(e => e.ErrorMessage).ToList());

                    var problem = new ProblemDetails
                    {
                        Title = "Validation Errors",
                        Status = StatusCodes.Status400BadRequest,
                        Detail = "One or more validation errors occurred.",
                        Instance = actionContext.HttpContext.Request.Path,
                        Extensions = { { "errors", errors } }
                    };
                    return new BadRequestObjectResult(problem);
                };
            });
            //__________________________
            var app = builder.Build();

            // initialize database
            var scope = app.Services.CreateScope();
            var initializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
            await initializer.InitializeAsync();  //Database Creation (Data Seeding)
            await initializer.InitializeAuthDbAsync(); //AuthDatabase Creation (Data Seeding)
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
            //app.UseCustomExceptionHandler();

            //4 way by built in middleware
            app.UseExceptionHandler();


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
