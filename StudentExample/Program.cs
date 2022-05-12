
using Quartz;
using Quartz.Impl;
using StudentExample.Jobs;
using StudentExample.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// register services
builder.Services.AddSingleton<IStudentService, StudentService>();
builder.Services.AddSingleton<IAzureService, AzureService>();
builder.Services.AddSingleton<StudentIncremental>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();///2

app.UseAuthorization();

app.MapControllers();

StdSchedulerFactory factory = new StdSchedulerFactory();
IScheduler scheduler = await factory.GetScheduler();
var serviceProvider = builder.Services.BuildServiceProvider();
scheduler.JobFactory = new JobFactory(serviceProvider);
await scheduler.Start();
IJobDetail job = JobBuilder.Create<StudentIncremental>()
    .WithIdentity("job1", "group1")
    .Build();
ITrigger trigger = TriggerBuilder.Create()
	.WithIdentity("trigger1", "group1")
	.StartNow()
	.WithSimpleSchedule(x => x
        .WithIntervalInMinutes(5)
		.RepeatForever())
	.Build();
await scheduler.ScheduleJob(job, trigger);

app.Run();