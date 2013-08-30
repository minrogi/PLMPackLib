using System;
using System.Collections.Generic;
using System.Text;

namespace Dxflib4NET
{

    public class DL_Codes
    {
        #region Constants
        public const string DL_VERSION = "2.0.4.6";

         // AutoCAD VERSION aliases
        public const dxfversion VER_R12 = dxfversion.AC1009;
        public const dxfversion VER_LT2 = dxfversion.AC1009;
        public const dxfversion VER_R13 = dxfversion.AC1012;   // not supported yet
        public const dxfversion VER_LT95 = dxfversion.AC1012;   // not supported yet
        public const dxfversion VER_R14 = dxfversion.AC1014;   // not supported yet
        public const dxfversion VER_LT97 = dxfversion.AC1014;   // not supported yet
        public const dxfversion VER_LT98 = dxfversion.AC1014;   // not supported yet
        public const dxfversion VER_2000 = dxfversion.AC1015;
        public const dxfversion VER_2002 = dxfversion.AC1015;

        // Start and endPoints of a line
        public const int LINE_START_CODE    = 10;
        public const int LINE_END_CODE      = 11;
        // Codes
        public const int POINT_COORD_CODE   = 10;

        public const int COLOUR_CODE = 62;
        #endregion

        #region Enums
        /// <summary>
        /// Standard DXF colors.
        /// </summary>
        public enum dxfcolor
        {
            black = 250,
            green = 3,
            red = 1,
            brown = 15,
            yellow = 2,
            cyan = 4,
            magenta = 6
          , gray = 8
          , blue = 5
          , l_blue = 163
          , l_green = 121
          , l_cyan = 131
          , l_red = 23
          , l_magenta = 221
          , l_gray = 252
          , white = 7
          , bylayer = 256
          , byblock = 0
        };

        /// <summary>
        /// Version numbers for the DXF format.
        /// </summary>
        public enum dxfversion
        {
            AC1009, AC1012, AC1014, AC1015
        };
        #endregion
    }
}
