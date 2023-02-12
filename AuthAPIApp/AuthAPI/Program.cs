using Auth.Application.Dto.Request;
using Auth.Application.Helper;
using Auth.Application.Service.EmailService;
using Auth.Application.Service.UserService;
using Auth.DataAcces.Persistence;
using Auth.DataAcces.Repository;
using Auth.DataAcces.Repository.Impl;
using AutoMapper;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSwaggerGen(options => {
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Standart auth with bearer scheme",
        In = ParameterLocation.Header,
        Name = "Auhtorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
});

builder.Services.AddControllers(options =>
{
    options.Filters.Add(typeof(ValidateModelAttribute));
}).AddNewtonsoftJson(option =>
{
    option.SerializerSettings.MissingMemberHandling = MissingMemberHandling.Error;
}).AddFluentValidation(fv =>
{
    fv.RegisterValidatorsFromAssemblyContaining<RegisterModel>();
    fv.RegisterValidatorsFromAssemblyContaining<LoginModel>();
    fv.RegisterValidatorsFromAssemblyContaining<UpdateUserModel>();
    //fv.RegisterValidatorsFromAssemblyContaining<LoginDtoValidatior>();
    //fv.RegisterValidatorsFromAssemblyContaining<CustomerUpdateDtoValidator>();
    //fv.RegisterValidatorsFromAssemblyContaining<UserDtoValidator>();
    //fv.RegisterValidatorsFromAssemblyContaining<BookDtoValidator>();
    //fv.RegisterValidatorsFromAssemblyContaining<OrderDtoValidator>();
    //fv.RegisterValidatorsFromAssemblyContaining<OrderItemValidator>();
});


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(builder.Configuration.GetSection("JWT:Secret").Value)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var emailConfig = builder.Configuration
        .GetSection("MailSettings")
        .Get<EmailConfig>();

builder.Services.AddSingleton(emailConfig);

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IEmailService, EmailService>();

IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
