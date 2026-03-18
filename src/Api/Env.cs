namespace Api;

public static class Env
{
    public static string POSTGRES_CONNECTION_STRING => GetEnvironmentVariable("POSTGRES_CONNECTION_STRING");

    private static string GetEnvironmentVariable(string key) =>
        Environment.GetEnvironmentVariable(key) ??
            throw new Exception($"Environment variable {key} not found");
}
