using System;
using System.Collections.Generic;
using WebDriverManager.Helpers;

namespace WebDriverManager.DriverConfigs
{
    public interface IDriverConfig
    {
        string GetName();
        string GetUrl32();
        string GetUrl64();
        string GetBinaryName();
        string GetDriverVersion(string browserVersion);
    }
}