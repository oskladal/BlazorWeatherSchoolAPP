﻿// Kde dochází k zapínání programu - spuštění - přidávání modulů 

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
//using BlazorWeatherSchoolAPP.Data;
using WeatherAPI;
using DBConnect;
using WeatherAPI.Models;
using Radzen;
using Blazorise;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;
using Blazorise.Charts.DataLabels;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

// Builder.Configuration - obsahuji proměnný který jsem si definovali
builder.Services.AddOptions();


builder.Services.AddHttpClient();

//Vytvoření instanci connectAPI a registroval ji do služeb, kde je možné ji vytáhnout v kódu.
builder.Services.AddSingleton<ConnectAPI>();
builder.Services.AddSingleton<WeatherDB>();

//Mudblazor
builder.Services.AddMudServices();


// Pro přidání Radzen
builder.Services.AddScoped<DialogService>();
builder.Services.AddScoped<NotificationService>();
builder.Services.AddScoped<TooltipService>();
builder.Services.AddScoped<ContextMenuService>();

// Přidání Blazorise
builder.Services
    .AddBlazorise(options =>
    {
        options.Immediate = true;
    })
    .AddBootstrapProviders()
    .AddFontAwesomeIcons();


var app = builder.Build();



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();



