using Solid.Rest.Practice.API;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();


//Most of this file was provided by the template, the below row is the only significant thing I've added.
Services.ServiceRegistrations(builder);


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//Added by template, removed for now
//app.UseAuthorization();

app.MapControllers();

app.Run();