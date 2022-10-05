using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using RP_IndustriPark;
using RP_IndustriPark.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddHttpClient<IIndustryClient, IndustryClient>(client => client.BaseAddress = new Uri("http://localhost:7277"));

await builder.Build().RunAsync();
