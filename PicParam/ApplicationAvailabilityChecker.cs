using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PicParam
{
    internal class ApplicationAvailabilityChecker
    {
        /// <summary>
        /// Is application available on this environment
        /// </summary>
        public static bool IsAvailable(string appName)
        {
            if (!_availabilities.ContainsKey(appName))
                _availabilities.Add(appName, GetAvailability(appName));
            return _availabilities[appName];
        }
        /// <summary>
        /// append application to allow checking for availability
        /// </summary>
        public static void AppendApplication(string appName, string appPath)
        {
            _appNameToPath.Add(appName, appPath);
        }
        /// <summary>
        /// actually check application availability
        /// </summary>
        private static bool GetAvailability(string appName)
        {
            if (!_appNameToPath.ContainsKey(appName))
                return false;
            return System.IO.File.Exists(_appNameToPath[appName]);
        }

        private static Dictionary<string, bool> _availabilities = new Dictionary<string,bool>();
        private static Dictionary<string, string> _appNameToPath = new Dictionary<string, string>();
    }
}
