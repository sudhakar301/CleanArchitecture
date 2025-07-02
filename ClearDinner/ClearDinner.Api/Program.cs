
using CleanDinner.Application;
using CleanDinner.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddApplication();
    builder.Services.AddInfraStructure(builder.Configuration);
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
}
var app = builder.Build();
{
    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
    app.MapControllers();
    app.Run();
}
