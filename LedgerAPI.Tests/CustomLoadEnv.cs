namespace LedgerAPI.Tests;

using DotNetEnv;

public static class CustomLoadEnv
{
    public static void LoadEnv(){

        var currentDirectory = Environment.CurrentDirectory;
        var env = ".env";

        // Traverse up the directory tree to find the .env file
        while(currentDirectory != null)
        {
            string fullPath = Path.Combine(currentDirectory, env);
            if(File.Exists(fullPath))
            {
                // Load environment variables from .env file
                Env.Load(fullPath);
                break;
            }
            currentDirectory = Directory.GetParent(currentDirectory)?.FullName;
        }
    }
}