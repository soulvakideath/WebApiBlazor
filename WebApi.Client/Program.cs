using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System.Net.Http;
using MudBlazor.Services;
using WebApi.Client;
using WebApi.Client.Services;
using WebApi.Client.Services.IServices;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5225/") });
builder.Services.AddScoped<HttpService>();
builder.Services.AddScoped<ITypeService, TypeService>();
builder.Services.AddScoped<IOperationService, OperationService>();
builder.Services.AddScoped<IReportService, ReportService>();
builder.Services.AddMudServices();

await builder.Build().RunAsync();