using Application.Workers.Commands.DeleteWorker;
using WebApi.DependencyInjections;
using WebApiV2.EndPoints;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("conf.json");

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DeleteWorkerCommand).Assembly));
builder.Services.AddDataBase(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
	options.AddPolicy("AllowSpecificPolicy", policy =>
	{
		policy.AllowAnyOrigin();
		policy.AllowAnyHeader();
		policy.AllowAnyMethod();
	});
});

var app = builder.Build();

app.UseHttpsRedirection();
app.UseRouting();

//app.UseMiddleware<ApiKeyMiddleware>();

//if (app.Environment.IsDevelopment())
//{
	app.UseSwagger();
	app.UseSwaggerUI();
//}

app.UseCors("AllowSpecificPolicy");

app.UseClientEndPoints();
app.UseRequestEndPoints();
app.UseExperienceEndPoints();
app.UseReportEndPoints();
app.UseWorkerEndPoints();
app.UseManagerEndPoints();
app.AddStatsEndPoints();

app.Run();