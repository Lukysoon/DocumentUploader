using DocumentUploader.DocumentService.Data;
using DocumentUploader.DocumentService.Repositories;
using DocumentUploader.DocumentService.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DocumentContext") ?? throw new InvalidOperationException("Connection string 'DocumentContext' not found.")));

builder.Services.AddTransient<IDocService, DocService>();
builder.Services.AddTransient<ITagService, TagService>();
builder.Services.AddTransient<ITagRepository, TagRepository>();
builder.Services.AddTransient<IDocumentRepository, DocumentRepository>();

builder.Services.AddControllers().AddXmlSerializerFormatters();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

app.Run();
