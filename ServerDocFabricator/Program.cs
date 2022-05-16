using ServerDocFabricator.DAL;
using ColoredLive.DAL;
using ServerDocFabricator.DAL.Entities;
using ServerDocFabricator.Utils;
using ColoredLive.Service.Core.Middlewares;

var builder = WebApplication.CreateBuilder(args);


var section = builder.Configuration.GetSection("AppSettings");
builder.Services.Configure<AppSettings>(section);
builder.Services.AddDbContext<AppDbContext>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

//builder.Services.AddSwaggerGen(c => c.SwaggerDoc("v1", new OpenApiInfo{ 
//    Version = "v1",
//    Title = "Test API",
//    Description = "Best doc fabricator api Description",
//}));
BlAutoImplementor.Implement<UserEntity>(builder.Services);

builder.Services.AddControllers();

var app = builder.Build();
//app.UseSwagger();
//app.UseSwaggerUI(builder => {
//    builder.SwaggerEndpoint("/swagger", "API");
//});

app.UseCors(cors =>     
{
    cors.AllowAnyHeader();
    cors.AllowAnyMethod();
    cors.AllowAnyOrigin();
});

app.UseMiddleware<JwtMiddleware>();
app.MapControllers();
app.MapSwagger();
app.Run();

