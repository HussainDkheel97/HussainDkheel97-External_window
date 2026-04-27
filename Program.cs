using Microsoft.EntityFrameworkCore;
using MMSystem.Model;
using External_Party.Services.Admin; // ÊÃßÏ ãä ÇÓÊÏÚÇÁ ãÓÇÑ ÇáÜ Service ÇáÎÇÕ Èß

var builder = WebApplication.CreateBuilder(args);
// ÊÓÌíá AutoMapper æİÍÕ ÇáÜ Profiles İí ÇáãÔÑæÚ
// 1. ÌáÈ ÓØÑ ÇáÇÊÕÇá
var connectionString = builder.Configuration.GetConnectionString("AppContext");

// 2. ÊÓÌíá ÇáÜ DbContext
builder.Services.AddDbContext<AppDbCon>(options =>
    options.UseSqlServer(connectionString));

// 3. ÅÖÇİÉ ÎÏãÇÊ ÇáÜ Controllers æÇáÜ Swagger
builder.Services.AddControllersWithViews(); // ááÜ MVC
builder.Services.AddEndpointsApiExplorer();  // ÖÑæÑí áÜ Swagger
builder.Services.AddSwaggerGen();           // ÊæáíÏ ÇáÜ Swagger

// 4. ÊÓÌíá ÇáÜ Services ÇáÎÇÕÉ Èß (ãåã ÌÏÇğ áßí íÚãá ÇáßäÊÑæá)
builder.Services.AddScoped<IAdmin, AdminMoc>();
// ÊÓÌíá AutoMapper áíÈÍË Úä ÌãíÚ ÇáÜ Profiles İí ÇáãÔÑæÚ ÇáÍÇáí
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
var app = builder.Build();

// 5. ÅÚÏÇÏ ÇáÜ Pipeline
if (app.Environment.IsDevelopment())
{
    // ÊÔÛíá Swagger İí ÈíÆÉ ÇáÊØæíÑ
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        // ÇÎÊíÇÑí: áÌÚá Swagger íİÊÍ ÊáŞÇÆíÇğ ÚäÏ ÊÔÛíá ÇáãÔÑæÚ
        // options.RoutePrefix = string.Empty; 
    });
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// 6. ÎÑÇÆØ ÇáãÓÇÑÇÊ (ÊÃßÏ ãä æÌæÏ ÇáãÓÇÑíä ãÚÇğ)
app.MapControllers(); // ÖÑæÑí áÊÔÛíá ÇáÜ API Controllers
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();