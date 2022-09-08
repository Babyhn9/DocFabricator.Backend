using AutoMapper;
using ServerDocFabricator.DAL;
using ColoredLive.DAL;
using ServerDocFabricator.DAL.Entities;
using ServerDocFabricator.Utils;
using ColoredLive.Service.Core.Middlewares;
using ServerDocFabricator.BL.Mapper;
using ServerDocFabricator.BL.Services.Interfaces;
using AutoMapper;
var builder = WebApplication.CreateBuilder(args);


var section = builder.Configuration.GetSection("root:settings");
builder.Services.Configure<AppSettings>(section);
builder.Services.AddDbContext<AppDbContext>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

//builder.Services.AddSwaggerGen(c => c.SwaggerDoc("v1", new OpenApiInfo{ 
//    Version = "v1",
//    Title = "Test API",
//    Description = "Best doc fabricator api Description",
//}));
BlAutoImplementor.Implement<ITemplateBl>(builder.Services);
BlAutoImplementor.ImplementMapper<ITemplateBl>(builder.Services);

builder.Services.AddSingleton(
    new MapperConfiguration(ctg =>
        ctg.AddProfile<ProjectProfile>())
        .CreateMapper()
    );

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

