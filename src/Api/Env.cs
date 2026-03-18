namespace Api;

public static class Env
{
    public static string? POSTGRES_CONNECTION_STRING => 
        Environment.GetEnvironmentVariable("POSTGRES_CONNECTION_STRING");
    public static bool USE_SEED_DATA => 
        bool.Parse(Environment.GetEnvironmentVariable("USE_SEED_DATA") ?? "false");
}
