using Todo.Data;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMvc();
builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>();
var app = builder.Build();

app.MapControllerRoute(
    "default",
    "{controller=home}/{action=Index}/{id?}"
);

app.Run();


app.MapControllers();