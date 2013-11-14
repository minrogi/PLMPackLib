using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pic.Plugin.ViewCtrl
{
    /// <summary>
    /// Localizer
    /// </summary>
    public interface ILocalizer
    {
        /// <summary>
        /// get translation string
        /// </summary>
        /// <param name="text">string to be translated</param>
        /// <returns>translated string</returns>
        string GetTranslation(string term);
        /// <summary>
        /// save translation string
        /// </summary>
        /// <param name="text">string to be translated</param>
        /// <param name="translation">string proposed translation</param>
        void SetTranslation(string term, string translation);
        /// <summary>
        /// add string to be translated
        /// </summary>
        /// <param name="term">term that needs to be translated</param>
        void AddTerm(string term);
    }
}
