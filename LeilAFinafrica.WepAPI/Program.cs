using LeilAFinafrica.WepAPI.Confogurations;

var builder = WebApplication.CreateBuilder(args);

// Register services
builder.Services.RegisterServices(builder.Configuration);
builder.Services.AddSwaggerConfiguration();
builder.Services.ConfigureCors(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

app.UseCors(CorsConfig.DEFAULT_POLICY);
app.UseSession();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.UseSwaggerSetup();

app.Run();
