using MyWalletAPI.Database;
using MyWalletAPI.Repositories;
using MyWalletAPI.Services;
using MyWalletAPI.Controllers;
using MyWalletAPI.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

DatabaseConfig.Initialize(builder.Configuration.GetSection("Database"));

builder.Services.AddScoped<IWalletRepository<Euro>, WalletRepository<Euro>>();
builder.Services.AddScoped<IWalletService<Euro>, WalletService<Euro>>();
builder.Services.AddScoped<IWalletRepository<Dollar>, WalletRepository<Dollar>>();
builder.Services.AddScoped<IWalletService<Dollar>, WalletService<Dollar>>();
builder.Services.AddScoped<IExchangeService<Dollar,Euro>, ExchangeService<Dollar,Euro>>();
builder.Services.AddScoped<IExchangeService<Euro, Dollar>, ExchangeService<Euro, Dollar>>();

builder.Services.AddScoped<EuroWalletController>();
builder.Services.AddScoped<DollarWalletController>();
builder.Services.AddScoped<ExchangeController>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();