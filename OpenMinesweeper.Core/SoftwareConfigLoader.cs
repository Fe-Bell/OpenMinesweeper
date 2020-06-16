using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace OpenMinesweeper.Core
{
    /// <summary>
    /// General file loader for SoftwareConfig files.
    /// </summary>
    public class SoftwareConfigLoader
    {
        #region Constants

        /// <summary>
        /// A default name for the configuration file.
        /// </summary>
        public const string DEFAULTCFGFILENAME = "sw.config";
        /// <summary>
        /// The key to a language entry in the configuration file.
        /// </summary>
        public const string KEY_LANGUAGE = @"LanguageKey";

        #endregion

        #region Properties

        /// <summary>
        /// The full path to the configuration file.
        /// </summary>
        public string FilePath { get; private set; }

        #endregion

        #region Events

        /// <summary>
        /// Event fired once the configuration changes.
        /// </summary>
        public event EventHandler OnConfigurationChanged = null;

        #endregion

        /// <summary>
        /// Constructor.
        /// </summary>
        public SoftwareConfigLoader()
        {

        }
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="filePath">The full path to the configuration file.</param>
        public SoftwareConfigLoader(string filePath = null)
        {
            Initialize(filePath);
        }

        #region Methods

        /// <summary>
        /// Initializes the configuration loader.
        /// </summary>
        /// <param name="filePath">The full path to the configuration file.</param>
        public void Initialize(string filePath = null)
        {
            FilePath = string.IsNullOrEmpty(filePath) ? DEFAULTCFGFILENAME : filePath;
            //If file does not exist, then create.
            if (!File.Exists(FilePath))
            {
                var defaultConfig = Properties.Resources.ResourceManager.GetString("DEFAULT_CFG");
                //Writes the default configuration file to storage
                if(!string.IsNullOrEmpty(defaultConfig))
                {
                    File.WriteAllText(filePath, defaultConfig);
                }
                //Creates an empty config file
                else
                {
                    var softwareConfig = new SoftwareConfig();
                    var xml = softwareConfig.SerializeXML();
                    xml.Save(FilePath);
                }
            }
        }
        /// <summary>
        /// Gets an object representation of the configuration file.
        /// </summary>
        /// <returns></returns>
        public SoftwareConfig GetFullConfig()
        {
            return GetFullConfig<SoftwareConfig>();
        }
        /// <summary>
        /// Gets an object representation of the configuration file. Supports custom comfiguration classess that inherit from the generic SoftwareConfig.
        /// </summary>
        /// <returns></returns>
        public T GetFullConfig<T>() where T : SoftwareConfig
        {
            return SoftwareConfig.DeserializeXML<T>(FilePath);
        }
        /// <summary>
        /// Saves the configuration object to a file.
        /// </summary>
        /// <param name="softwareConfig"></param>
        public void Save<T>(T softwareConfig) where T : SoftwareConfig
        {
            var xml = softwareConfig.SerializeXML();
            xml.Save(FilePath);
            OnConfigurationChanged?.Invoke(this, new EventArgs());
        }
        /// <summary>
        /// Returns the language ID of a software configuration.
        /// </summary>
        /// <param name="swConfig"></param>
        /// <returns></returns>
        public ValueType GetSWLanguage(SoftwareConfig swConfig = null)
        {
            var swconfig = swConfig is null ? GetFullConfig() : swConfig;
            if (swconfig != null)
            {
                if (swconfig.CurrentSettings != null)
                {
                    var currentLanguage = swconfig.CurrentSettings.FirstOrDefault(x => x.Key == KEY_LANGUAGE);
                    if (currentLanguage != null)
                    {
                        if(uint.TryParse(currentLanguage.Value, out uint value))
                        {
                            return value;
                        }
                        else
                        {
                            return 0;
                        }                        
                    }
                }
            }

            var swLanguage = new KeyValuePair<uint, string>(1033, "EN-US");
            return swLanguage;
        }
        /// <summary>
        /// Returns a dictionary of saved resources.
        /// </summary>
        /// <param name="swConfig"></param>
        /// <returns></returns>
        public IDictionary<string, string> GetCurrentTextResources(SoftwareConfig swConfig = null)
        {
            var dict = new Dictionary<string, string>();

            var swconfig = swConfig != null? GetFullConfig() : swConfig;
            if (swconfig != null &&
                swconfig.Resources != null &&
                swconfig.Resources.Any() &&
                swconfig.Resources.Any(x => x is SoftwareConfig.GeneralResources.TextResource))
            {
                var currentLangID = GetSWLanguage(swconfig);
                var res = swconfig.Resources
                    .Where(x => x is SoftwareConfig.GeneralResources.TextResource)
                    .FirstOrDefault(x => x.Key == currentLangID.ToString());

                if (res != null)
                {
                    dict = res.Content.ToDictionary(x => x.Key, x => x.Value);
                }
            }

            return dict;
        }
        /// <summary>
        /// Updates a Key-Value pair in the 'CurrentConfiguration'.
        /// </summary>
        /// <param name="key">The key string of the pair.</param>
        /// <param name="value">The value string of the pair.</param>
        /// <returns></returns>
        public bool UpdateCurrentResource(string key, string value)
        {
            var swConfig = GetFullConfig();
            if(swConfig != null)
            {
                //Find resource
                var resource = swConfig.CurrentSettings.FirstOrDefault(x => x.Key.Equals(key));
                if(resource != null)
                {
                    //Update resource
                    resource.Value = value;
                    Save(swConfig);

                    return true;
                }
            }

            return false;
        }

        #endregion
    }
}
