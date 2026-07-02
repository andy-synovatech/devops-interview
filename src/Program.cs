var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// Simulated warmup: the service primes caches / opens pools before it can serve.
// It is alive immediately, but not ready to take traffic for ~30 seconds.
var startedAt = DateTimeOffset.UtcNow;
var warmupPeriod = TimeSpan.FromSeconds(30);
bool IsReady() => DateTimeOffset.UtcNow - startedAt >= warmupPeriod;

// Config comes from the environment (see k8s/configmap.yaml + k8s/secret.yaml).
var apiKey = builder.Configuration["ApiKey"] ?? "unset";
var connectionString = builder.Configuration.GetConnectionString("Default") ?? "unset";
app.Logger.LogInformation(
    "Starting payment-service. ApiKey configured: {HasKey}. DB configured: {HasDb}.",
    apiKey != "unset", connectionString != "unset");

app.MapGet("/", () => Results.Text("payment-service: OK"));

// Liveness: is the process alive? Must NOT depend on warmup or downstreams.
app.MapGet("/health/live", () => Results.Ok(new { status = "alive" }));

// Readiness: can we serve traffic yet? Returns 503 until warmup completes.
app.MapGet("/health/ready", () =>
    IsReady()
        ? Results.Ok(new { status = "ready" })
        : Results.Json(new { status = "warming-up" }, statusCode: 503));

app.Run("http://0.0.0.0:8080");
