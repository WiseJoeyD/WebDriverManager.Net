using System;
using System.IO;

namespace WebDriverManager.Services.Impl
{
    public class PathVariableService
    {
        protected internal void AddDriverToEnvironmentPathVariable(string path)
        {
            const string name = "PATH";
            var pathVariable = Environment.GetEnvironmentVariable(name, EnvironmentVariableTarget.Process);
            if (pathVariable == null) throw new ArgumentNullException($"Can't get {name} variable");
            path = Path.GetDirectoryName(path);
            var newPathVariable = $"{path}{Path.PathSeparator}{pathVariable}";
            if (path != null && !pathVariable.Contains(path))
                Environment.SetEnvironmentVariable(name, newPathVariable, EnvironmentVariableTarget.Process);
        }
    }
}