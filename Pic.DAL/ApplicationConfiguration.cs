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
        [ConfigurationProperty("applicationDes", DefaultValue = "PicGEOM.exe", IsRequired = false, IsKey = false)]
        [StringValidator(InvalidCharacters = "~!@#$%^&*[]{};'\"|", MinLength = 1, MaxLength = 255)]
        public string AppPicGEOM
        {
            get
            {
                return (string)this["applicationDes"];
            }
            set
            {
                this["applicationDes"] = value;
            }
        }
        [ConfigurationProperty("appPicDecoupe", DefaultValue = "applicationPicdecoupe", IsRequired = false, IsKey = false)]
        [StringValidator(InvalidCharacters = "~!@#$%^&*[]{};'\"|", MinLength = 1, MaxLength = 255)]
        public string AppPicdecoup
        {
            get
            {
                return (string)this["appPicDecoupe"];
            }
            set
            {
                this["appPicDecoupe"] = value;
            }
        }
        [ConfigurationProperty("applicationDes3", DefaultValue = "applicationDes3", IsRequired = false, IsKey = false)]
        [StringValidator(InvalidCharacters = "~!@#$%^&*[]{};'\"|", MinLength = 1, MaxLength = 255)]
        public string AppPicador3D
        {
            get
            {
                return (string)this["applicationDes3"];
            }
            set
            {
                this["applicationDes3"] = value;
            }
        }
        [ConfigurationProperty("applicationDXF", DefaultValue = "applicationDXF", IsRequired = false, IsKey = false)]
        [StringValidator(InvalidCharacters = "~!@#$%^&*[]{};'\"|", MinLength = 1, MaxLength = 255)]
        public string ApplicationDXF
        {
            get
            {
                return (string)this["applicationDXF"];
            }
            set
            {
                this["applicationDXF"] = value;
            }
        }
        [ConfigurationProperty("applicationAI", DefaultValue = "applicationAI", IsRequired = false, IsKey = false)]
        [StringValidator(InvalidCharacters = "~!@#$%^&*[]{};'\"|", MinLength = 1, MaxLength = 255)]
        public string ApplicationAI
        {
            get
            {
                return (string)this["applicationAI"];
            }
            set
            {
                this["applicationAI"] = value;
            }
        }
        [ConfigurationProperty("applicationCF2", DefaultValue = "applicationCF2", IsRequired = false, IsKey = false)]
        [StringValidator(InvalidCharacters = "~!@#$%^&*[]{};'\"|", MinLength = 1, MaxLength = 255)]
        public string ApplicationCF2
        {
            get
            {
                return (string)this["applicationCF2"];
            }
            set
            {
                this["applicationCF2"] = value;
            }
        }
        [ConfigurationProperty("thumbnailsPath", DefaultValue = @"K:\Codeplex\PLMPackLib\Pic.DAL\Thumbnails", IsRequired = true, IsKey = false)]
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
