#region Using directives
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using System.Xml;
using System.Configuration;
#endregion

namespace Pic.DAL
{
    public class ApplicationConfiguration : ConfigurationSection
    {
        #region Static instance accessor
        // ------------------------------------------------------------------
        /// <summary>
        /// Get a global instance.
        /// </summary>
        public static ApplicationConfiguration CustomSection
        {
            get
            {
                lock (typeof(ApplicationConfiguration))
                {
                    Configuration cfg = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                    //if (customSection == null)
                    customSection = cfg.GetSection("CustomSection") as ApplicationConfiguration;
                    if (customSection == null)
                        throw new Exception("No \"CustomSection\" section available in application config file");
                    return customSection;
                }
            }
        }

        private static ApplicationConfiguration customSection;

        public override bool IsReadOnly()
        {
            return false;
        }

        // ------------------------------------------------------------------
        #endregion

        #region Configuration properties
        [ConfigurationProperty("databaseFile", DefaultValue = @"C:\Picador\PicParam\Database\PicParam.db", IsRequired = true, IsKey = false)]
        [StringValidator(InvalidCharacters = "~!@#$%^&*[]{};'\"|", MinLength = 1, MaxLength = 255)]
        public string DatabasePath
        {
            get
            {
                return (string)this["databaseFile"];
            }
            set
            {
                this["databaseFile"] = value;
            }
        }
        [ConfigurationProperty("thumbnailsPath", DefaultValue = @"K:\GitHub\PLMPackLib\Pic.DAL\Thumbnails", IsRequired = true, IsKey = false)]
        [StringValidator(InvalidCharacters = "~!@#$%^&*[]{};'\"|", MinLength = 1, MaxLength = 255)]
        public string ThumbnailsPath
        {
            get
            {
                return (string)this["thumbnailsPath"];
            }
            set
            {
                this["thumbnailsPath"] = value;
                this.CurrentConfiguration.Save(ConfigurationSaveMode.Modified);
            }
        }
        public string RepositoryPath
        {
            get
            {
                return Path.Combine(Path.GetDirectoryName(this.DatabasePath), @"..\Documents\");
            }
        }
        #endregion

        #region Saving
        public static void SaveDatabasePath(string databasePath)
        {
            Configuration cfg = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            ApplicationConfiguration section = cfg.GetSection("CustomSection") as ApplicationConfiguration;
            if (section != null)
            {
                section.DatabasePath = databasePath;
                cfg.Save(ConfigurationSaveMode.Modified);

                ConfigurationManager.RefreshSection("CustomSection");
            }
        }
        #endregion
    }
}
