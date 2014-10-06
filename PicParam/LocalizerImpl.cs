#region Using directives
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
// Path
using System.IO;
// Assembly
using System.Reflection;
// logging
using log4net;
#endregion

namespace PicParam
{
    /// <summary>
    /// Singleton pattern class that allows accessing translated terms
    /// </summary>
    public class LocalizerImpl : Pic.Plugin.ViewCtrl.ILocalizer, IDisposable
    {
        #region Logging
        protected static readonly ILog _log = LogManager.GetLogger(typeof(LocalizerImpl));
        #endregion

        #region Instantiation
        /// <summary>
        /// Static singleton instantiator
        /// </summary>
        public static LocalizerImpl Instance
        {
            get
            {
                if (null == _localizer)
                    _localizer = new LocalizerImpl();
                return _localizer;
            }
        }
        /// <summary>
        /// Private constructor
        /// </summary>
        private LocalizerImpl()
        {
            Load();
        }
        /// <summary>
        /// destructor
        /// </summary>
        ~LocalizerImpl()
        {
            Save();
            Dispose(false);
        }
        #endregion

        #region Implement ILocalizer
        public string GetTranslation(string term)
        {
            string trimmedTerm = term.Trim();
            if (_dict.ContainsKey(trimmedTerm))
            {
                string sText = _dict[trimmedTerm];
                return string.IsNullOrEmpty(sText) ? trimmedTerm : sText;
            }
            else
            {
                if (null != TranslationNotFound)
                {
                    TranslationNotFound(trimmedTerm);
                    if (_dict.Keys.Contains(trimmedTerm) && !string.IsNullOrEmpty(_dict[trimmedTerm]))
                        return _dict[trimmedTerm];
                }
                AddTerm(term);
                return trimmedTerm;                
            }
        }
        public void SetTranslation(string term, string translation)
        {
            if (_dict.Keys.Contains(term.Trim()))
                _dict[term.Trim()] = translation.Trim();
            else
                _dict.Add(term.Trim(), translation.Trim());
        }
        public void AddTerm(string term)
        {
            try
            {
                if (!_dict.ContainsKey(term.Trim()))
                    _dict.Add(term.Trim(), string.Empty);
            }
            catch (Exception)
            {
            }
        }
        public bool HasTerm(string sText)
        {
            return _dict.ContainsKey(sText);
        }
        #endregion

        #region Implement IDisposable
        public void Dispose()
        {
        }
        protected virtual void Dispose(bool disposing)
        { 
            Save();
        }
        #endregion

        #region Load & Save dictionary
        /// <summary>
        /// Localisation file path
        /// One file for each different language
        /// </summary>
        public string LocalisationFileName
        {
            get
            {
                // using variable _localizationFilePath in order to prevent changing culture
                // when new threads are created
                // for .NET framework 4.5 and above set property CultureInfo.DefaultThreadCurrentCulture
                if (string.IsNullOrEmpty(_localizationFilePath))
                {
                    string assemblyFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                    _localizationFilePath = Path.Combine(
                        assemblyFolder
                        , string.Format("Localisation_{0}.txt", System.Globalization.CultureInfo.CurrentCulture.ThreeLetterWindowsLanguageName)
                        );
                }
                return _localizationFilePath;
            }
        }
        /// <summary>
        /// Load string dictionary from file
        /// </summary>
        public void Load()
        {
            try
            {
                string filePath = LocalisationFileName;
                if (!File.Exists(filePath))
                    return;
                using (StreamReader readFile = new StreamReader(filePath, Encoding.Unicode))
                {
                    string line;
                    string[] row;

                    while ((line = readFile.ReadLine()) != null)
                    {
                        try
                        {
                            row = line.Split('=');
                            if (row.Length == 2)
                                SetTranslation(row[0], row[1]);
                        }
                        catch (Exception ex)
                        {
                            _log.Error(string.Format("Translation string dictionnary -> Skipping line: {0} with message: {1}"
                                , line
                                , ex.Message));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _log.Error(string.Format("Failed loading translation strings from  file {0} with message: {1}"
                    , LocalisationFileName
                    ,  ex.Message));
            }
        }
        /// <summary>
        /// Save string dictionary to file
        /// </summary>
        public void Save()
        {
            try
            {
               using (StreamWriter sw = new StreamWriter(LocalisationFileName, false, Encoding.Unicode))
                {
                    // we need to find a cleaner way to sort dictionary on both key and value
                    // first already translated strings
                    foreach (KeyValuePair<string, string> kvPair in _dict.OrderBy(key => key.Key))
                    {
                        if (!string.IsNullOrEmpty( kvPair.Value.Trim() ))
                            sw.WriteLine(string.Format("{0} = {1}", kvPair.Key, kvPair.Value));
                    }
                    // then new (hence not translated yet) strings
                    foreach (KeyValuePair<string, string> kvPair in _dict.OrderBy(key => key.Key))
                    {
                        if (string.IsNullOrEmpty(kvPair.Value.Trim()))
                            sw.WriteLine(string.Format("{0} = {1}", kvPair.Key, kvPair.Value));
                    }
                }
            }
            catch (Exception ex)
            {
                _log.Error(string.Format("Failed saving translation string as file {0} with message : {1}", LocalisationFileName,  ex.Message));
            }
        }
        #endregion

        #region Data members
        /// <summary>
        /// localisation
        /// </summary>
        private string _localizationFilePath; 
        /// <summary>
        /// static singleton instance reference
        /// </summary>
        static LocalizerImpl _localizer = null;
        /// <summary>
        /// term/translation dictionnary
        /// </summary>
        private Dictionary<string, string> _dict = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);
        // delegates
        public delegate void TranslationHandler(string term);
        // events
        public event TranslationHandler TranslationNotFound;
        #endregion
    }
}
