
using System.Text;
using Ecommerce.Domain.Contracts;
using Ecommerce.Persistence.DependancyInjection;
using Ecommerce.Services.DependencyInjection;
using Ecommerce.Services.Service.Exceptions;
using ECommerce.Infrastructure.Service;
using ECommerce.Web.Handler;
using ECommerce.Web.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace ECommerce.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddApplicationServices()
                .AddPersistenceServices(builder.Configuration)
                .AddInfrastructureServices(builder.Configuration);

            builder.Services.Configure<JWTOptions>(builder.Configuration.GetSection(JWTOptions.SectionName));


            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v1", new OpenApiInfo { Title = "ECommerce API", Version = "v1" });
                option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n ",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT"
                });
                option.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type= ReferenceType.SecurityScheme,
                                Id= "Bearer"
                            }
                        },
                        new string[]{}
                    }
                });
            });

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

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options => 
                {
                    var jwt = builder.Configuration.GetSection(JWTOptions.SectionName)
                    .Get<JWTOptions>();
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwt.Issure,
                        ValidAudience = jwt.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key)),
                    };
                } );
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
                app.UseSwaggerUI(c =>
                {
                    c.DisplayRequestDuration();
                    c.EnableFilter();
                    c.DocExpansion(DocExpansion.None);
                });
            }
            //picture
            app.UseStaticFiles();

            app.UseHttpsRedirection();

            app.UseAuthentication(); 
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
