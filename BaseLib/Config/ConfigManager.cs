using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nini.Config;
using BaseLib.Helpers;

namespace BaseLib.Configs
{
    public sealed class ConfigManager
    {
        private static readonly IniConfigSource Parser; // the ini parser.
        private static readonly string ConfigFile;
        private static bool _fileExists = false; // does the ini file exists?

        static ConfigManager()
        {
            try
            {
                ConfigFile = string.Format("{0}/{1}", FileHelpers.AssemblyRoot, "config.ini"); // the config file's location.
                Parser = new IniConfigSource(ConfigFile); // see if the file exists by trying to parse it.
                _fileExists = true;
            }
            catch (Exception)
            {
                Parser = new IniConfigSource(); // initiate a new .ini source.
                _fileExists = false;
                Console.WriteLine("Error loading settings config.ini, will be using default settings.");
            }
            finally
            {
                // adds aliases so we can use On and Off directives in ini files.
                Parser.Alias.AddAlias("On", true); 
                Parser.Alias.AddAlias("Off", false);
            }

            Parser.ExpandKeyValues();
        }

        static internal IConfig Section(string section) // Returns the asked config section.
        {
            return Parser.Configs[section];
        }

        static internal IConfig AddSection(string section) // Adds a config section.
        {
            return Parser.AddConfig(section);
        }

        static internal void Save() //  Saves the settings.
        {
            if (_fileExists) Parser.Save();
            else
            {
                Parser.Save(ConfigFile);
                _fileExists = true;
            }
        }
    }
}
