using Application.DependencyInjection;
using Application.DTO.Response.Orders;
using Application.Service.Orders.Queries;
using Application.Service.Products.Queries.Categories;
using Application.Service.Products.Queries.Locations;
using Application.Service.Products.Queries.Products;
using Infrastucture.DependencyInjection;
using Infrastucture.Repository.Orders.Handlers;
using Infrastucture.Repository.Products.Handlers.Categories;
using Infrastucture.Repository.Products.Handlers.Locations;
using Infrastucture.Repository.Products.Handlers.Products;
using MediatR;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor.Services;
using NetcodeHub.Packages.Components.DataGrid;
using Syncfusion.Blazor;
using WebUI.Components;
using WebUI.Components.Layout.Identity;
using WebUI.Hubs;
using WebUI.States;
using WebUI.States.Administration;
using WebUI.States.User;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
	.AddInteractiveServerComponents();
builder.Services.AddInfrastructureService(builder.Configuration);
builder.Services.AddApplicationService();
builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthStateProvider>();
builder.Services.AddSingleton<ChangePasswordState>();
builder.Services.AddScoped<UserActiveOrderCountState>();
builder.Services.AddScoped<AdminActiveOrderCountState>();
builder.Services.AddScoped<GenericHomeHeaderState>();
builder.Services.AddScoped<ConnectionService>();
builder.Services.AddScoped<ICustomAuthorizationService, CustomAuthorizationService>();
builder.Services.AddSyncfusionBlazor();
builder.Services.AddVirtualizationService();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetGenericOrdersCountHandler).Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetAllOrdersQuery).Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetOrdersByIdQuery).Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetOrdersByIdHandler).Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetAllOrdersHandler).Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetProductsQuery).Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetAllLocationsQuery).Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetAllCategoriesQuery).Assembly)); 
Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1NDaF1cX2hIfEx3QHxbf1x0ZFxMZF5bRX5PMyBoS35RckRiW3pfdXZRRmlfUEd1");
builder.Services.AddMudServices();
builder.Services.AddSignalR();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Error", createScopeForErrors: true);
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.MapSignOutEnpoint();app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
	.AddInteractiveServerRenderMode();
app.MapHub<CommunicationHub>("/localhost");

app.Run();
