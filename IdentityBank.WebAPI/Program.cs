using System.Text.Json.Serialization;
using IdentityBank.Application.Extensions;
using IdentityBank.Domain.DI;
using IdentityBankIdentityBank.API.Extensions;
using IdentityBankIdentityBank.API.Middlewares;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});
            
builder.Services
    .AddMapper() 
    .AddMediatr()
    .AddSettings(builder.Configuration)
    .AddJwtAuthentication()
    .AddIdentity()
    .AddDatabase(builder.Configuration)
    .AddServices();
            
builder.Services.AddMassTransit(configurator =>
{
    configurator.SetKebabCaseEndpointNameFormatter();

    configurator.UsingRabbitMq((context, factoryConfigurator) =>
    {
        factoryConfigurator.Host("amqp://guest:guest@localhost:5672");
        
        factoryConfigurator.ConfigureEndpoints(context);
    });
});

builder.Services.ConfigureBadRequestResponse();
            
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwagger();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Bank.Identity");
    });
}

app.UseHttpsRedirection();

app.UseExceptions();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();