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
        [ConfigurationProperty("dataDirectory", DefaultValue = @"C:\Picador\PicParam", IsRequired = true, IsKey = false)]
        [StringValidator(InvalidCharacters = "~!@#$%^&*[]{};'\"|", MinLength = 1, MaxLength = 255)]
        public string DataDirectory
        {
            get
            {
                return (string)this["dataDirectory"];
            }
            set
            {
                this["dataDirectory"] = value;
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
        [ConfigurationProperty("applicationDxf", DefaultValue = "applicationDxf", IsRequired = false, IsKey = false)]
        [StringValidator(InvalidCharacters = "~!@#$%^&*[]{};'\"|", MinLength = 1, MaxLength = 255)]
        public string ApplicationDxf
        {
            get
            {
                return (string)this["applicationDxf"];
            }
            set
            {
                this["applicationDxf"] = value;
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
        public string DatabasePath
        {
            get
            {
                return Path.Combine(this.DataDirectory, @"Database\PicParam.db");
            }
        }
        public string RepositoryPath
        {
            get
            {
                return Path.Combine(this.DataDirectory, @"Documents\");
            }
        }
        #endregion

        #region Saving
        public static void SaveDataDirectory(string dataDirectory)
        {
            Configuration cfg = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            ApplicationConfiguration section = cfg.GetSection("CustomSection") as ApplicationConfiguration;
            if (section != null)
            {
                section.DataDirectory = dataDirectory;
                cfg.Save(ConfigurationSaveMode.Modified);

                ConfigurationManager.RefreshSection("CustomSection");
            }
        }
        #endregion
    }
}
