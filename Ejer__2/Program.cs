using Ejer__2;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configurar la conexión a la base de datos
builder.Services.AddDbContext<EmployeeContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("EmployeesDB")));

//builder.Services.AddDbContext<EmployeeContext>(options =>
//    options.UseSqlServer(
//        builder.Configuration.GetConnectionString("EmployeesDB"),
//        sqlServerOptions => sqlServerOptions.EnableRetryOnFailure())
//);


// Inyectar el repositorio
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();

var app = builder.Build();

// Endpoint para obtener todos los empleados
app.MapGet("/employees", async (IEmployeeRepository repo) =>
{
	try
	{
        var employees = await repo.GetAllAsync();
        return Results.Ok(employees);
    }
	catch (Exception ex)
	{

        return Results.Problem("erorrrrr");
	}
   
   
});

// Endpoint para agregar un nuevo empleado
app.MapPost("/employees", async (Employee employee, IEmployeeRepository repo) =>
{
    await repo.AddAsync(employee);
    return Results.Created($"/employees/{employee.EmployeeId}", employee);
});

app.Run();
