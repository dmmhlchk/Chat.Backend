using Chat.API.Extensions.Dependencies;
using Chat.API.Extensions.Middlewares;
using Chat.Application.Dependencies;
using Chat.Persistence.Dependencies;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddApplication(builder.Configuration);
builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddApi(builder.Configuration);



var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseApi();
    
    

app.Run();
