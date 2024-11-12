using Microsoft.AspNetCore.OData;
using Microsoft.OData.ModelBuilder;
using BookShopBusiness;
using BookShopRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Microsoft.OData.Edm;
using static System.Reflection.Metadata.BlobBuilder;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
    });
static IEdmModel GetEdmModel()
{
    ODataConventionModelBuilder builder = new();
    builder.EntitySet<Books>("Books");
    builder.EntitySet<Categories>("Categories");
    builder.EntitySet<Shippings>("Shippings");
    builder.EntitySet<Users>("Users");
    return builder.GetEdmModel();
}

var connectionString = builder.Configuration.GetConnectionString("BookStore");
builder.Services.AddDbContext<MyDbContext>();

builder.Services.AddScoped<IBooksRepository, BooksRepository>();
builder.Services.AddScoped<ICategoriesRepository, CategoriesRepository>();
builder.Services.AddScoped<IShippingsRepository, ShippingsRepository>();
builder.Services.AddScoped<IUsersRepository, UsersRepository>();

builder.Services.AddControllers()
    .AddOData(opt => opt.AddRouteComponents("odata", GetEdmModel())
    .Filter().Select().Expand().OrderBy().Count().SetMaxTop(100));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "BookShop API", Version = "v1" });

});

var app = builder.Build();

app.UseODataBatching();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();

    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "BookShop API V1");
    });
}

// Middleware cho HTTPS và xác thực
app.UseHttpsRedirection();
app.UseAuthorization();

// Định tuyến các controller
app.MapControllers();

// Chạy ứng dụng
app.Run();

