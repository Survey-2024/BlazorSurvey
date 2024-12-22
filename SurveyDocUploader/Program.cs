using Azure.Identity;
using SurveyDocUploader;
using SurveyDocUploader.Components;
using SurveyDocUploader.Services;

var builder = WebApplication.CreateBuilder(args);

// Access Key Vault to fetch the app configuration resource's connection string
var keyVaultEndpoint = new Uri("https://kv-surveycreds.vault.azure.net/");
builder.Configuration.AddAzureKeyVault(keyVaultEndpoint, new DefaultAzureCredential());
string connectionString = builder.Configuration["AppConfigConnectionString"]!;

// Authenticate into the App Configuration Resource and bind its Key Vault References
builder.Configuration.AddAzureAppConfiguration(options =>
{
    options.Connect(connectionString)
                    .ConfigureKeyVault(kv =>
                    {
                        kv.SetCredential(new DefaultAzureCredential());
                    });
});

// Bind result of App Configuration to KeyVaultOptions
builder.Services.Configure<KeyVaultOptions>(builder.Configuration.GetSection("KeyVault"));


// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();


builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AllowedCorsOrigins",
        builder =>
        {
            builder
                .SetIsOriginAllowed((_) => true)
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
        });
});

builder.Services.AddBlazorBootstrap();

builder.Services.AddScoped<IBlobService, BlobService>();
builder.Services.AddScoped<IServiceBusService, ServiceBusService>();
builder.Services.AddScoped<IToastService, ToastService>();

builder.Services.AddHttpClient();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
