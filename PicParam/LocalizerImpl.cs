#region Using directives
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
// Path
using System.IO;
// Assembly
using System.Reflection; 
#endregion

namespace PicParam
{
    /// <summary>
    /// Singleton pattern class that allows accessing translated terms
    /// </summary>
    public class LocalizerImpl : Pic.Plugin.ViewCtrl.ILocalizer, IDisposable
    {
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
            if (_dict.ContainsKey(term))
            {
                string sText = _dict[term];
                return string.IsNullOrEmpty(sText) ? term : sText;
            }
            else
            {
                if (null != TranslationNotFound)
                {
                    TranslationNotFound(term);
                    if (_dict.Keys.Contains(term) && !string.IsNullOrEmpty(_dict[term]))
                        return _dict[term];
                }
                AddTerm(term);
                return term;                
            }
        }
        public void SetTranslation(string term, string translation)
        {
            _dict.Add(term.Trim(), translation.Trim());
        }
        public void AddTerm(string term)
        {
            if (!_dict.ContainsKey(term))
                _dict.Add(term.Trim(), string.Empty);
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
                string assemblyFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                return Path.Combine(
                    assemblyFolder
                    , string.Format("Localisation_{0}.txt", System.Globalization.CultureInfo.CurrentCulture.ThreeLetterWindowsLanguageName)
                    );            
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
                        row = line.Split('=');
                        if (row.Length == 2 && !_dict.ContainsKey(row[0]))
                            _dict.Add(row[0].Trim(), row[1].Trim());
                    }
                }
            }
            catch (Exception /*ex*/)
            {
            }
        }
        /// <summary>
        /// Save string dictionary to file
        /// </summary>
        public void Save()
        {
            try
            {
                string filePath = string.Empty;
                using (StreamWriter sw = new StreamWriter(LocalisationFileName, false, Encoding.Unicode))
                {
                    foreach (string sKey in _dict.Keys)
                        sw.WriteLine(string.Format("{0} = {1}", sKey, _dict[sKey]));
                }
            }
            catch (Exception /*ex*/)
            { 
            }
        }
        #endregion

        #region Data members
        /// <summary>
        /// static singleton instance reference
        /// </summary>
        static LocalizerImpl _localizer = null;
        /// <summary>
        /// term/translation dictionnary
        /// </summary>
        private Dictionary<string, string> _dict = new Dictionary<string, string>();
        // delegates
        public delegate void TranslationHandler(string term);
        // events
        public event TranslationHandler TranslationNotFound;
        #endregion
    }
}
