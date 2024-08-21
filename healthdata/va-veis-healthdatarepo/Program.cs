using Microsoft.AspNetCore.HttpLogging;
using Microsoft.Extensions.Logging.AzureAppServices;
using Serilog;
using System.Net;
using va_veis_healthdatarepo.Middleware;
using va_veis_healthdatarepo.Models;
using va_veis_healthdatarepo.Services;
using va_veis_healthdatarepo.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

#if DEBUG
var logger = new LoggerConfiguration()
  .ReadFrom.Configuration(builder.Configuration)
  .Enrich.FromLogContext()
  .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

#else
    builder.Logging.ClearProviders();
    builder.Logging.AddConsole();
    
#endif

builder.Logging.AddAzureWebAppDiagnostics();

builder.Services.Configure<AzureFileLoggerOptions>(options =>
{
    options.FileName = "azure-diagnostics-";
    options.FileSizeLimit = 50 * 1024;
    options.RetainedFileCountLimit = 5;
});
builder.Services.Configure<AzureBlobLoggerOptions>(options =>
{
    options.BlobName = "log.txt";
});
builder.Services.AddHttpLogging(logging =>
{
    logging.LoggingFields = HttpLoggingFields.Duration;
    if (!builder.Environment.IsProduction())
    {
        logging.LoggingFields = HttpLoggingFields.RequestHeaders;
        logging.LoggingFields = HttpLoggingFields.ResponseHeaders;
    }
    else
    {

    }

    logging.RequestBodyLogLimit = 4096;
    logging.ResponseBodyLogLimit = 4096;
});
builder.Services.AddHttpLoggingInterceptor<AppSvcHttpLoggingInterceptor>();

// This code adds other services for your application.
builder.Services.AddMvc();

builder.Services.AddHttpClient("hdr").ConfigurePrimaryHttpMessageHandler(config => new HttpClientHandler
{
    AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip
}); ;

builder.Services.AddApplicationInsightsTelemetry();

// This code adds other services for your application.
//builder.Services.AddMvc();

builder.Services.AddControllers()
    .AddJsonOptions(opts => opts.JsonSerializerOptions.PropertyNamingPolicy = null);
builder.Services.Configure<Settings>(builder.Configuration.GetSection("Settings"));
builder.Services.Configure<FPDS_Settings>(builder.Configuration.GetSection("FPDS_Settings"));
builder.Services.Configure<PathwaySettings>(builder.Configuration.GetSection("PathwaySettings"));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddSwaggerConfiguration();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped(typeof(ICDSDataService<>), typeof(CDSDataService<>));
builder.Services.AddScoped(typeof(IFPDSDataService<>), typeof(FPDSDataService<>));
builder.Services.AddScoped(typeof(IPWSDataService<>), typeof(PWSDataService<>));
builder.Services.AddScoped<ICDSResponseParser, CDSResponseParser>();
builder.Services.AddTransient(typeof(IDependencyAggregate<>), typeof(DependencyAggregate<>));

var app = builder.Build();
app.Logger.LogInformation("Starting the app");

app.UseHttpLogging();
if (app.Environment.IsDevelopment())
{
    app.UseSwaggerConfiguration();
    app.UseDeveloperExceptionPage();
}
else
{
    //app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
