using BusinessLogic.SurveyOptions;
using BusinessLogic.Surveys;
using BusinessLogic.Users;
using UnitOfWork;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IUnitOfWork>(option => new DataAccess.UnitOfWork(builder.Configuration.GetConnectionString("local")));
builder.Services.AddTransient<IUsersLogic, UsersLogic>();
builder.Services.AddTransient<ISurveyoptionsLogic, SurveyOptionsLogic>();
builder.Services.AddTransient<ISurveysLogic, SurveysLogic>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
