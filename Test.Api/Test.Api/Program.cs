using FluentValidation;
using Test.Api.Extentsions;
using Test.Core;
using Test.Infrastructure;
using Test.Infrastructure.Database;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddControllers();

    builder.Services.AddDatabase<DatabaseContext, IInfrastructureMarker>(builder.Configuration);

    builder.Services.AddDefaultCors(builder.Configuration);

    builder.Services.AddServices();
    builder.Services.AddRepositories();

    builder.Services.AddValidatorsFromAssembly(typeof(ICoreMarker).Assembly);

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
}

var app = builder.Build();
{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseCors();

    app.MapControllers();
}
app.Run();
