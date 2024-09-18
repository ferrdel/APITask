using Gestion_DataAcccessLayer.Data;
using Gestion_DataAcccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using Task = Gestion_DataAcccessLayer.Models.Task;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.Services.AddDbContext<TaskDbContext>
    (
        options =>
            options.UseSqlServer(
                builder.Configuration.GetConnectionString("DefaultConnection")
                )
    );

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

//Scope de Servicios
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<TaskDbContext>();
    //context.Database.EnsureCreated(); // This will create the database if it does not exist
    context.Database.Migrate(); // This will apply any pending migrations

    //Seed the database
    //Movemos las clases seed a un archivo separado, en el proyecto de DataAccessLayer
    //SeedUsersAndRoles.Initialize(services);
    SeedData.Initialize(context);
}


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

public class SeedData
{
    public static void Initialize(TaskDbContext context)
    {
        if (!context.Users.Any())
        {
            context.AddRange(
                new User
                {
                    Name = "User1",
                    Email = "user1@mail.com",
                    Password = "User123"
                }
            );
            context.SaveChanges();
            
        }

        if (!context.Tasks.Any())
        {
            context.AddRange(
                new Task
                {
                    Name = "Tarea1",
                    Description = "realizar ejercicio practico bootcamps",
                    State = "Pendiente",
                    UserId=1
                },

                new Task
                {
                    Name = "Tarea2",
                    Description = "Ver video clase 12",
                    State = "Pendiente",
                    UserId = 1
                },

                new Task
                {
                    Name = "Tarea3",
                    Description = "realizar ejercicio POO",
                    State = "Completada",
                    UserId = 1
                }
            );
            context.SaveChanges();                        
        }

    }
}