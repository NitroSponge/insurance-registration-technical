using InsuranceRegistrationTechnical.Api.Mapper;
using InsuranceRegistrationTechnical.Data.Data;
using InsuranceRegistrationTechnical.Data.Extensions;
using InsuranceRegistrationTechnical.Data.Interfaces;
using InsuranceRegistrationTechnical.Data.Repositories;
using InsuranceRegistrationTechnical.Service.Configuration;
using InsuranceRegistrationTechnical.Service.Framework;
using InsuranceRegistrationTechnical.Service.Interfaces;
using InsuranceRegistrationTechnical.Service.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IUserRegistrationService, UserRegistrationService>();
builder.Services.AddScoped<IAgeRestrictionEvaluator, AgeRestrictionEvaluator>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IDateTimeProvider, DateTimeProvider>();
builder.Services.AddDataAccessLayer();
builder.Services.AddAutoMapper(typeof(UserRegistrationMappingProfile));
builder.Services.AddDateOnlyTimeOnlyStringConverters();
builder.Services.Configure<UserRegistrationServiceOptions>(builder.Configuration.GetSection(UserRegistrationServiceOptions.ConfigurationSectionKey));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Services.RunDatabaseMigrations();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
