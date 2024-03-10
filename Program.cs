using TutorialMongo.Framewrok;
using TutorialMongo.Framewrok.Contract;
using TutorialMongo.Framewrok.Entity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddTransient<IBaseMongoRepository, BaseMongoRepository>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<ApplicationSettings>(builder.Configuration.GetSection("ApplicationSettings"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
