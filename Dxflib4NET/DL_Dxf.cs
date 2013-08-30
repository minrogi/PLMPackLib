using System;
using System.Collections.Generic;
using System.Text;

namespace Dxflib4NET
{
    public class DL_Dxf
    {
        #region Data members
        DL_Codes.dxfversion _version;
        #endregion

        #region Constructor
        public DL_Dxf()
        {
            _version = DL_Codes.dxfversion.AC1009;
        }
        #endregion

        #region Write methods
        public void writeHeader(DL_Writer dw)
        {
            dw.comment(string.Concat("dxflib ", DL_Codes.DL_VERSION));
            dw.sectionHeader();

            dw.dxfString(9, "$ACADVER");
            switch (_version)
            {
            case DL_Codes.dxfversion.AC1009:
                dw.dxfString(1, "AC1009");
                break;
            case DL_Codes.dxfversion.AC1012:
                dw.dxfString(1, "AC1012");
                break;
            case DL_Codes.dxfversion.AC1014:
                dw.dxfString(1, "AC1014");
                break;
            case DL_Codes.dxfversion.AC1015:
                dw.dxfString(1, "AC1015");
                break;
            }

            // Newer version require that (otherwise a*cad crashes..)
            if (_version == DL_Codes.VER_2000)
            {
                dw.dxfString(9, "$HANDSEED");
                dw.dxfHex(5, 0xFFFF);
            }
        }

        public void writeVPort(DL_Writer dw)
        {
            dw.dxfString(0, "TABLE");
            dw.dxfString(2, "VPORT");

            if (_version == DL_Codes.VER_2000)
                dw.dxfHex(5, 0x8);

            if (_version == DL_Codes.VER_2000)
                dw.dxfString(100, "AcDbSymbolTable");

            dw.dxfInt(70, 1);
            dw.dxfString(0, "VPORT");

            if (_version == DL_Codes.VER_2000)
            {
                dw.dxfString(100, "AcDbSymbolTableRecord");
                dw.dxfString(100, "AcDbViewportTablerecord");
            }
            dw.dxfString(2, "*Active");
            dw.dxfInt(70, 0);
            dw.dxfReal(10, 0.0);
            dw.dxfReal(20, 0.0);
            dw.dxfReal(11, 1.0);
            dw.dxfReal(21, 1.0);
            dw.dxfReal(12, 286.3055555555555);
            dw.dxfReal(22, 148.5);
            dw.dxfReal(13, 0.0);
            dw.dxfReal(23, 0.0);
            dw.dxfReal(14, 10.0);
            dw.dxfReal(24, 10.0);
            dw.dxfReal(15, 10.0);
            dw.dxfReal(25, 10.0);
            dw.dxfReal(16, 0.0);
            dw.dxfReal(26, 0.0);
            dw.dxfReal(36, 1.0);
            dw.dxfReal(17, 0.0);
            dw.dxfReal(27, 0.0);
            dw.dxfReal(37, 0.0);
            dw.dxfReal(40, 297.0);
            dw.dxfReal(41, 1.92798353909465);
            dw.dxfReal(42, 50.0);
            dw.dxfReal(43, 0.0);
            dw.dxfReal(44, 0.0);
            dw.dxfReal(50, 0.0);
            dw.dxfReal(51, 0.0);
            dw.dxfInt(71, 0);
            dw.dxfInt(72, 100);
            dw.dxfInt(73, 1);
            dw.dxfInt(74, 3);
            dw.dxfInt(75, 1);
            dw.dxfInt(76, 1);
            dw.dxfInt(77, 0);
            dw.dxfInt(78, 0);

            if (_version == DL_Codes.VER_2000)
            {
                dw.dxfInt(281, 0);
                dw.dxfInt(65, 1);
                dw.dxfReal(110, 0.0);
                dw.dxfReal(120, 0.0);
                dw.dxfReal(130, 0.0);
                dw.dxfReal(111, 1.0);
                dw.dxfReal(121, 0.0);
                dw.dxfReal(131, 0.0);
                dw.dxfReal(112, 0.0);
                dw.dxfReal(122, 1.0);
                dw.dxfReal(132, 0.0);
                dw.dxfInt(79, 0);
                dw.dxfReal(146, 0.0);
            }
            dw.dxfString(0, "ENDTAB");
        }

        public void writeLineType(DL_Writer dw, DL_LineTypeData data)
        {
            if (data.name.Length == 0)
                throw new DL_Exception("Line type name must not be empty");

	        // ignore BYLAYER, BYBLOCK for R12
            string sNameUpper = data.name.ToUpper(); 
	        if (_version<DL_Codes.VER_2000)
            {
                if ((sNameUpper.CompareTo("BYBLOCK") == 0) || (sNameUpper.CompareTo("BYLAYER") == 0))
                    return;
            }

        	// write id (not for R12)
            if (sNameUpper.CompareTo("BYBLOCK") == 0) {
                dw.tableLineTypeEntry(0x14);
            } else if (sNameUpper.CompareTo("BYLAYER") == 0) {
                dw.tableLineTypeEntry(0x15);
            } else if (sNameUpper.CompareTo("CONTINUOUS") == 0) {
                dw.tableLineTypeEntry(0x16);
            } else {
                dw.tableLineTypeEntry();
            }

            dw.dxfString(2, data.name);
    	    dw.dxfInt(70, data.flags);

            if (sNameUpper.CompareTo("BYBLOCK") == 0) {
                dw.dxfString(3, "");
                dw.dxfInt(72, 65);
                dw.dxfInt(73, 0);
                dw.dxfReal(40, 0.0);
            } else if (sNameUpper.CompareTo("BYLAYER") == 0 ) {
                dw.dxfString(3, "");
                dw.dxfInt(72, 65);
                dw.dxfInt(73, 0);
                dw.dxfReal(40, 0.0);
            } else if (sNameUpper.CompareTo("CONTINUOUS") == 0) {
                dw.dxfString(3, "Solid line");
                dw.dxfInt(72, 65);
                dw.dxfInt(73, 0);
                dw.dxfReal(40, 0.0);
            } else if (sNameUpper.CompareTo("ACAD_ISO02W100") == 0) {
                dw.dxfString(3, "ISO Dashed __ __ __ __ __ __ __ __ __ __ _");
                dw.dxfInt(72, 65);
                dw.dxfInt(73, 2);
                dw.dxfReal(40, 15.0);
                dw.dxfReal(49, 12.0);
                if (_version>=DL_Codes.VER_R13)
                    dw.dxfInt(74, 0);
                dw.dxfReal(49, -3.0);
                if (_version>=DL_Codes.VER_R13)
                    dw.dxfInt(74, 0);
            } else if (sNameUpper.CompareTo("ACAD_ISO03W100") == 0) {
                dw.dxfString(3, "ISO Dashed with Distance __    __    __    _");
                dw.dxfInt(72, 65);
                dw.dxfInt(73, 2);
                dw.dxfReal(40, 30.0);
                dw.dxfReal(49, 12.0);
                if (_version>=DL_Codes.VER_R13)
                    dw.dxfInt(74, 0);
                    dw.dxfReal(49, -18.0);
                if (_version>=DL_Codes.VER_R13)
                    dw.dxfInt(74, 0);
            } else if (sNameUpper.CompareTo("ACAD_ISO04W100") == 0) {
                dw.dxfString(3, "ISO Long Dashed Dotted ____ . ____ . __");
                dw.dxfInt(72, 65);
                dw.dxfInt(73, 4);
                dw.dxfReal(40, 30.0);
                dw.dxfReal(49, 24.0);
                if (_version>=DL_Codes.VER_R13)
                    dw.dxfInt(74, 0);
                dw.dxfReal(49, -3.0);
                if (_version>=DL_Codes.VER_R13)
                    dw.dxfInt(74, 0);
                dw.dxfReal(49, 0.0);
                if (_version>=DL_Codes.VER_R13)
                    dw.dxfInt(74, 0);
                dw.dxfReal(49, -3.0);
                if (_version>=DL_Codes.VER_R13)
                    dw.dxfInt(74, 0);
            } else if (sNameUpper.CompareTo("ACAD_ISO05W100") == 0) {
                dw.dxfString(3, "ISO Long Dashed Double Dotted ____ .. __");
                dw.dxfInt(72, 65);
                dw.dxfInt(73, 6);
                dw.dxfReal(40, 33.0);
                dw.dxfReal(49, 24.0);
                if (_version>=DL_Codes.VER_R13)
                    dw.dxfInt(74, 0);
                dw.dxfReal(49, -3.0);
                if (_version>=DL_Codes.VER_R13)
                    dw.dxfInt(74, 0);
                dw.dxfReal(49, 0.0);
                if (_version>=DL_Codes.VER_R13)
                    dw.dxfInt(74, 0);
                dw.dxfReal(49, -3.0);
                if (_version>=DL_Codes.VER_R13)
                    dw.dxfInt(74, 0);
                dw.dxfReal(49, 0.0);
                if (_version>=DL_Codes.VER_R13)
                    dw.dxfInt(74, 0);
                dw.dxfReal(49, -3.0);
                if (_version>=DL_Codes.VER_R13)
                    dw.dxfInt(74, 0);
            } else if (sNameUpper.CompareTo("BORDER") == 0) {
                dw.dxfString(3, "Border __ __ . __ __ . __ __ . __ __ . __ __ .");
                dw.dxfInt(72, 65);
                dw.dxfInt(73, 6);
                dw.dxfReal(40, 44.45);
                dw.dxfReal(49, 12.7);
                if (_version>=DL_Codes.VER_R13)
                    dw.dxfInt(74, 0);
                dw.dxfReal(49, -6.35);
                if (_version>=DL_Codes.VER_R13)
                    dw.dxfInt(74, 0);
                dw.dxfReal(49, 12.7);
                if (_version>=DL_Codes.VER_R13)
                    dw.dxfInt(74, 0);
                dw.dxfReal(49, -6.35);
                if (_version>=DL_Codes.VER_R13)
                    dw.dxfInt(74, 0);
                dw.dxfReal(49, 0.0);
                if (_version>=DL_Codes.VER_R13)
                    dw.dxfInt(74, 0);
                dw.dxfReal(49, -6.35);
                if (_version>=DL_Codes.VER_R13)
                    dw.dxfInt(74, 0);
            } else if (sNameUpper.CompareTo("BORDER2") == 0) {
                dw.dxfString(3, "Border (.5x) __.__.__.__.__.__.__.__.__.__.__.");
                dw.dxfInt(72, 65);
                dw.dxfInt(73, 6);
                dw.dxfReal(40, 22.225);
                dw.dxfReal(49, 6.35);
                if (_version>=DL_Codes.VER_R13)
                    dw.dxfInt(74, 0);
                dw.dxfReal(49, -3.175);
                if (_version>=DL_Codes.VER_R13)
                    dw.dxfInt(74, 0);
                dw.dxfReal(49, 6.35);
                if (_version>=DL_Codes.VER_R13)
                    dw.dxfInt(74, 0);
                dw.dxfReal(49, -3.175);
                if (_version>=DL_Codes.VER_R13)
                    dw.dxfInt(74, 0);
                dw.dxfReal(49, 0.0);
                if (_version>=DL_Codes.VER_R13)
                    dw.dxfInt(74, 0);
                dw.dxfReal(49, -3.175);
                if (_version>=DL_Codes.VER_R13)
                    dw.dxfInt(74, 0);
            } else if (sNameUpper.CompareTo("BORDERX2") == 0) {
                dw.dxfString(3, "Border (2x) ____  ____  .  ____  ____  .  ___");
                dw.dxfInt(72, 65);
                dw.dxfInt(73, 6);
                dw.dxfReal(40, 88.9);
                dw.dxfReal(49, 25.4);
                if (_version>=DL_Codes.VER_R13)
                    dw.dxfInt(74, 0);
                dw.dxfReal(49, -12.7);
                if (_version>=DL_Codes.VER_R13)
                    dw.dxfInt(74, 0);
                dw.dxfReal(49, 25.4);
                if (_version>=DL_Codes.VER_R13)
                    dw.dxfInt(74, 0);
                dw.dxfReal(49, -12.7);
                if (_version>=DL_Codes.VER_R13)
                    dw.dxfInt(74, 0);
                dw.dxfReal(49, 0.0);
                if (_version>=DL_Codes.VER_R13)
                    dw.dxfInt(74, 0);
                dw.dxfReal(49, -12.7);
                if (_version>=DL_Codes.VER_R13)
                    dw.dxfInt(74, 0);
            } else if (sNameUpper.CompareTo("CENTER") == 0) {
                dw.dxfString(3, "Center ____ _ ____ _ ____ _ ____ _ ____ _ ____");
                dw.dxfInt(72, 65);
                dw.dxfInt(73, 4);
                dw.dxfReal(40, 50.8);
                dw.dxfReal(49, 31.75);
                if (_version>=DL_Codes.VER_R13)
                    dw.dxfInt(74, 0);
                dw.dxfReal(49, -6.35);
                if (_version>=DL_Codes.VER_R13)
                    dw.dxfInt(74, 0);
                dw.dxfReal(49, 6.35);
                if (_version>=DL_Codes.VER_R13)
                    dw.dxfInt(74, 0);
                dw.dxfReal(49, -6.35);
                if (_version>=DL_Codes.VER_R13)
                    dw.dxfInt(74, 0);
            } else if (sNameUpper.CompareTo("CENTER2") == 0) {
                dw.dxfString(3, "Center (.5x) ___ _ ___ _ ___ _ ___ _ ___ _ ___");
                dw.dxfInt(72, 65);
                dw.dxfInt(73, 4);
                dw.dxfReal(40, 28.575);
                dw.dxfReal(49, 19.05);
                if (_version>=DL_Codes.VER_R13)
                    dw.dxfInt(74, 0);
                dw.dxfReal(49, -3.175);
                if (_version>=DL_Codes.VER_R13)
                    dw.dxfInt(74, 0);
                dw.dxfReal(49, 3.175);
                if (_version>=DL_Codes.VER_R13)
                    dw.dxfInt(74, 0);
                dw.dxfReal(49, -3.175);
                if (_version>=DL_Codes.VER_R13)
                    dw.dxfInt(74, 0);
            } else if (sNameUpper.CompareTo("CENTERX2") == 0) {
                dw.dxfString(3, "Center (2x) ________  __  ________  __  _____");
                dw.dxfInt(72, 65);
                dw.dxfInt(73, 4);
                dw.dxfReal(40, 101.6);
                dw.dxfReal(49, 63.5);
                if (_version>=DL_Codes.VER_R13)
                    dw.dxfInt(74, 0);
                dw.dxfReal(49, -12.7);
                if (_version>=DL_Codes.VER_R13)
                    dw.dxfInt(74, 0);
                dw.dxfReal(49, 12.7);
                if (_version>=DL_Codes.VER_R13)
                    dw.dxfInt(74, 0);
                dw.dxfReal(49, -12.7);
                if (_version>=DL_Codes.VER_R13)
                    dw.dxfInt(74, 0);
            } else if (sNameUpper.CompareTo("DASHDOT") == 0) {
                dw.dxfString(3, "Dash dot __ . __ . __ . __ . __ . __ . __ . __");
                dw.dxfInt(72, 65);
                dw.dxfInt(73, 4);
                dw.dxfReal(40, 25.4);
                dw.dxfReal(49, 12.7);
                if (_version>=DL_Codes.VER_R13)
                    dw.dxfInt(74, 0);
                dw.dxfReal(49, -6.35);
                if (_version>=DL_Codes.VER_R13)
                    dw.dxfInt(74, 0);
                dw.dxfReal(49, 0.0);
                if (_version>=DL_Codes.VER_R13)
                    dw.dxfInt(74, 0);
                dw.dxfReal(49, -6.35);
                if (_version>=DL_Codes.VER_R13)
                    dw.dxfInt(74, 0);
            } else if (sNameUpper.CompareTo("DASHDOT2") == 0) {
                dw.dxfString(3, "Dash dot (.5x) _._._._._._._._._._._._._._._.");
                dw.dxfInt(72, 65);
                dw.dxfInt(73, 4);
                dw.dxfReal(40, 12.7);
                dw.dxfReal(49, 6.35);
                if (_version>=DL_Codes.VER_R13)
                    dw.dxfInt(74, 0);
                dw.dxfReal(49, -3.175);
                if (_version>=DL_Codes.VER_R13)
                    dw.dxfInt(74, 0);
                dw.dxfReal(49, 0.0);
                if (_version>=DL_Codes.VER_R13)
                    dw.dxfInt(74, 0);
                dw.dxfReal(49, -3.175);
                if (_version>=DL_Codes.VER_R13)
                    dw.dxfInt(74, 0);
            } else if (sNameUpper.CompareTo("DASHDOTX2") == 0) {
                dw.dxfString(3, "Dash dot (2x) ____  .  ____  .  ____  .  ___");
                dw.dxfInt(72, 65);
                dw.dxfInt(73, 4);
                dw.dxfReal(40, 50.8);
                dw.dxfReal(49, 25.4);
                if (_version>=DL_Codes.VER_R13)
                    dw.dxfInt(74, 0);
                dw.dxfReal(49, -12.7);
                if (_version>=DL_Codes.VER_R13)
                    dw.dxfInt(74, 0);
                dw.dxfReal(49, 0.0);
                if (_version>=DL_Codes.VER_R13)
                    dw.dxfInt(74, 0);
                dw.dxfReal(49, -12.7);
                if (_version>=DL_Codes.VER_R13)
                    dw.dxfInt(74, 0);
            } else if (sNameUpper.CompareTo("DASHED") == 0) {
                dw.dxfString(3, "Dashed __ __ __ __ __ __ __ __ __ __ __ __ __ _");
                dw.dxfInt(72, 65);
                dw.dxfInt(73, 2);
                dw.dxfReal(40, 19.05);
                dw.dxfReal(49, 12.7);
                if (_version>=DL_Codes.VER_R13)
                    dw.dxfInt(74, 0);
                dw.dxfReal(49, -6.35);
                if (_version>=DL_Codes.VER_R13)
                    dw.dxfInt(74, 0);
            } else if (sNameUpper.CompareTo("DASHED2") == 0) {
                dw.dxfString(3, "Dashed (.5x) _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _");
                dw.dxfInt(72, 65);
                dw.dxfInt(73, 2);
                dw.dxfReal(40, 9.525);
                dw.dxfReal(49, 6.35);
                if (_version>=DL_Codes.VER_R13)
                    dw.dxfInt(74, 0);
                dw.dxfReal(49, -3.175);
                if (_version>=DL_Codes.VER_R13)
                    dw.dxfInt(74, 0);
            } else if (sNameUpper.CompareTo("DASHEDX2") == 0) {
                dw.dxfString(3, "Dashed (2x) ____  ____  ____  ____  ____  ___");
                dw.dxfInt(72, 65);
                dw.dxfInt(73, 2);
                dw.dxfReal(40, 38.1);
                dw.dxfReal(49, 25.4);
                if (_version>=DL_Codes.VER_R13)
                    dw.dxfInt(74, 0);
                dw.dxfReal(49, -12.7);
                if (_version>=DL_Codes.VER_R13)
                    dw.dxfInt(74, 0);
            } else if (sNameUpper.CompareTo("DIVIDE") == 0) {
                dw.dxfString(3, "Divide ____ . . ____ . . ____ . . ____ . . ____");
                dw.dxfInt(72, 65);
                dw.dxfInt(73, 6);
                dw.dxfReal(40, 31.75);
                dw.dxfReal(49, 12.7);
                if (_version>=DL_Codes.VER_R13)
                    dw.dxfInt(74, 0);
                dw.dxfReal(49, -6.35);
                if (_version>=DL_Codes.VER_R13)
                    dw.dxfInt(74, 0);
                dw.dxfReal(49, 0.0);
                if (_version>=DL_Codes.VER_R13)
                    dw.dxfInt(74, 0);
                dw.dxfReal(49, -6.35);
                if (_version>=DL_Codes.VER_R13)
                    dw.dxfInt(74, 0);
                dw.dxfReal(49, 0.0);
                if (_version>=DL_Codes.VER_R13)
                    dw.dxfInt(74, 0);
                dw.dxfReal(49, -6.35);
                if (_version>=DL_Codes.VER_R13)
                    dw.dxfInt(74, 0);
            } else if (sNameUpper.CompareTo("DIVIDE2") == 0) {
                dw.dxfString(3, "Divide (.5x) __..__..__..__..__..__..__..__.._");
                dw.dxfInt(72, 65);
                dw.dxfInt(73, 6);
                dw.dxfReal(40, 15.875);
                dw.dxfReal(49, 6.35);
                if (_version>=DL_Codes.VER_R13)
                    dw.dxfInt(74, 0);
                dw.dxfReal(49, -3.175);
                if (_version>=DL_Codes.VER_R13)
                    dw.dxfInt(74, 0);
                dw.dxfReal(49, 0.0);
                if (_version>=DL_Codes.VER_R13)
                    dw.dxfInt(74, 0);
                dw.dxfReal(49, -3.175);
                if (_version>=DL_Codes.VER_R13)
                    dw.dxfInt(74, 0);
                dw.dxfReal(49, 0.0);
                if (_version>=DL_Codes.VER_R13)
                    dw.dxfInt(74, 0);
                dw.dxfReal(49, -3.175);
                if (_version>=DL_Codes.VER_R13)
                    dw.dxfInt(74, 0);
            } else if (sNameUpper.CompareTo("DIVIDEX2") == 0) {
                dw.dxfString(3, "Divide (2x) ________  .  .  ________  .  .  _");
                dw.dxfInt(72, 65);
                dw.dxfInt(73, 6);
                dw.dxfReal(40, 63.5);
                dw.dxfReal(49, 25.4);
                if (_version>=DL_Codes.VER_R13)
                    dw.dxfInt(74, 0);
                dw.dxfReal(49, -12.7);
                if (_version>=DL_Codes.VER_R13)
                    dw.dxfInt(74, 0);
                dw.dxfReal(49, 0.0);
                if (_version>=DL_Codes.VER_R13)
                    dw.dxfInt(74, 0);
                dw.dxfReal(49, -12.7);
                if (_version>=DL_Codes.VER_R13)
                    dw.dxfInt(74, 0);
                dw.dxfReal(49, 0.0);
                if (_version>=DL_Codes.VER_R13)
                    dw.dxfInt(74, 0);
                dw.dxfReal(49, -12.7);
                if (_version>=DL_Codes.VER_R13)
                    dw.dxfInt(74, 0);
            } else if (sNameUpper.CompareTo("DOT") == 0) {
                dw.dxfString(3, "Dot . . . . . . . . . . . . . . . . . . . . . .");
                dw.dxfInt(72, 65);
                dw.dxfInt(73, 2);
                dw.dxfReal(40, 6.35);
                dw.dxfReal(49, 0.0);
                if (_version>=DL_Codes.VER_R13)
                    dw.dxfInt(74, 0);
                dw.dxfReal(49, -6.35);
                if (_version>=DL_Codes.VER_R13)
                    dw.dxfInt(74, 0);
            } else if (sNameUpper.CompareTo("DOT2") == 0) {
                dw.dxfString(3, "Dot (.5x) .....................................");
                dw.dxfInt(72, 65);
                dw.dxfInt(73, 2);
                dw.dxfReal(40, 3.175);
                dw.dxfReal(49, 0.0);
                if (_version>=DL_Codes.VER_R13)
                    dw.dxfInt(74, 0);
                dw.dxfReal(49, -3.175);
                if (_version>=DL_Codes.VER_R13)
                    dw.dxfInt(74, 0);
            } else if (sNameUpper.CompareTo("DOTX2") == 0) {
                dw.dxfString(3, "Dot (2x) .  .  .  .  .  .  .  .  .  .  .  .  .");
                dw.dxfInt(72, 65);
                dw.dxfInt(73, 2);
                dw.dxfReal(40, 12.7);
                dw.dxfReal(49, 0.0);
                if (_version>=DL_Codes.VER_R13)
                    dw.dxfInt(74, 0);
                dw.dxfReal(49, -12.7);
                if (_version>=DL_Codes.VER_R13)
                    dw.dxfInt(74, 0);
            } else {
                throw new DL_Exception("dxflib warning: DL_Dxf::writeLineType: Unknown Line Type");
            }
        }

        public void writePoint(DL_Writer dw, DL_PointData data, DL_Attributes attrib)
        {
            dw.entity("POINT");
            if (_version == DL_Codes.VER_2000)
            {
                dw.dxfString(100, "AcDbEntity");
                dw.dxfString(100, "AcDbPoint");
            }
            dw.entityAttributes(attrib);
            dw.coord(DL_Codes.POINT_COORD_CODE, data.x, data.y, data.z);
        }

        public void writeLine(DL_Writer dw, DL_LineData data, DL_Attributes attrib)
        {
            dw.entity("LINE");
            if (_version == DL_Codes.VER_2000)
            {
                dw.dxfString(100, "AcDbEntity");
                dw.dxfString(100, "AcDbLine");
            }
            dw.entityAttributes(attrib);
            dw.coord(DL_Codes.LINE_START_CODE, data.x1, data.y1, data.z1);
            dw.coord(DL_Codes.LINE_END_CODE, data.x2, data.y2, data.z2);
            dw.dxfInt(DL_Codes.COLOUR_CODE, data.color);
        }

        public void writeArc(DL_Writer dw, DL_ArcData data, DL_Attributes attrib)
        {
            dw.entity("ARC");
            if (_version == DL_Codes.VER_2000)
                dw.dxfString(100, "AcDbEntity");
            dw.entityAttributes(attrib);
            if (_version == DL_Codes.VER_2000)
                dw.dxfString(100, "AcDbCircle");
            dw.coord(10, data.cx, data.cy, data.cz);
            dw.dxfReal(40, data.radius);
            if (_version == DL_Codes.VER_2000)
                dw.dxfString(100, "AcDbArc");
            dw.dxfReal(50, data.angle1);
            dw.dxfReal(51, data.angle2);
        }
        public void writeLayer(DL_Writer dw, DL_LayerData data, DL_Attributes attrib)
        {
            if (data.lName.Length == 0)
                throw new DL_Exception("DL_Dxf::writeLayer: Layer name must not be empty\n");

            int color = attrib.Color;
            if (color==0) {
                color = 7;
                throw new DL_Exception("Layer color cannot be 0. Corrected to 7.\n");
            }

            if (data.lName == "0") {
                dw.tableLayerEntry(0x10);
            } else {
                dw.tableLayerEntry();
            }

            dw.dxfString(2, data.lName);
            dw.dxfInt(70, data.lFlag);
            dw.dxfInt(62, color);

            dw.dxfString(6, (attrib.LineType.Length==0 ?
                             "CONTINUOUS" : attrib.LineType));

            if (_version>=DL_Codes.VER_2000) {
                // layer defpoints cannot be plotted
                if (data.lName.ToLower().CompareTo("defpoints") == 0) {
                    dw.dxfInt(290, 0);
                }
            }
            if (_version>=DL_Codes.VER_2000 && attrib.Width!=-1) {
                dw.dxfInt(370, attrib.Width);
            }
            if (_version>=DL_Codes.VER_2000) {
                dw.dxfHex(390, 0xF);
            }
        }

        public void writeObjects(DL_Writer dw)
        {
            dw.dxfString(0, "SECTION");
            dw.dxfString(2, "OBJECTS");
            dw.dxfString(0, "DICTIONARY");
            dw.dxfHex(5, 0xC);                            // C
            dw.dxfString(100, "AcDbDictionary");
            dw.dxfInt(280, 0);
            dw.dxfInt(281, 1);
            dw.dxfString(3, "ACAD_GROUP");
            dw.dxfHex(350, 0xD);          // D
            dw.dxfString(3, "ACAD_LAYOUT");
            dw.dxfHex(350, 0x1A);
            dw.dxfString(3, "ACAD_MLINESTYLE");
            dw.dxfHex(350, 0x17);
            dw.dxfString(3, "ACAD_PLOTSETTINGS");
            dw.dxfHex(350, 0x19);
            dw.dxfString(3, "ACAD_PLOTSTYLENAME");
            dw.dxfHex(350, 0xE);
            dw.dxfString(3, "AcDbVariableDictionary");
            dw.dxfHex(350, dw.getNextHandle());        // 2C
            dw.dxfString(0, "DICTIONARY");
            dw.dxfHex(5, 0xD);
            dw.dxfString(100, "AcDbDictionary");
            dw.dxfInt(280, 0);
            dw.dxfInt(281, 1);
            dw.dxfString(0, "ACDBDICTIONARYWDFLT");
            dw.dxfHex(5, 0xE);
            dw.dxfString(100, "AcDbDictionary");
            dw.dxfInt(281, 1);
            dw.dxfString(3, "Normal");
            dw.dxfHex(350, 0xF);
            dw.dxfString(100, "AcDbDictionaryWithDefault");
            dw.dxfHex(340, 0xF);
            dw.dxfString(0, "ACDBPLACEHOLDER");
            dw.dxfHex(5, 0xF);
            dw.dxfString(0, "DICTIONARY");
            dw.dxfHex(5, 0x17);
            dw.dxfString(100, "AcDbDictionary");
            dw.dxfInt(280, 0);
            dw.dxfInt(281, 1);
            dw.dxfString(3, "Standard");
            dw.dxfHex(350, 0x18);
            dw.dxfString(0, "MLINESTYLE");
            dw.dxfHex(5, 0x18);
            dw.dxfString(100, "AcDbMlineStyle");
            dw.dxfString(2, "STANDARD");
            dw.dxfInt(70, 0);
            dw.dxfString(3, "");
            dw.dxfInt(62, 256);
            dw.dxfReal(51, 90.0);
            dw.dxfReal(52, 90.0);
            dw.dxfInt(71, 2);
            dw.dxfReal(49, 0.5);
            dw.dxfInt(62, 256);
            dw.dxfString(6, "BYLAYER");
            dw.dxfReal(49, -0.5);
            dw.dxfInt(62, 256);
            dw.dxfString(6, "BYLAYER");
            dw.dxfString(0, "DICTIONARY");
            dw.dxfHex(5, 0x19);
            dw.dxfString(100, "AcDbDictionary");
            dw.dxfInt(280, 0);
            dw.dxfInt(281, 1);
            dw.dxfString(0, "DICTIONARY");
            dw.dxfHex(5, 0x1A);
            dw.dxfString(100, "AcDbDictionary");
            dw.dxfInt(281, 1);
            dw.dxfString(3, "Layout1");
            dw.dxfHex(350, 0x1E);
            dw.dxfString(3, "Layout2");
            dw.dxfHex(350, 0x26);
            dw.dxfString(3, "Model");
            dw.dxfHex(350, 0x22);
            dw.dxfString(0, "LAYOUT");
            dw.dxfHex(5, 0x1E);
            dw.dxfString(100, "AcDbPlotSettings");
            dw.dxfString(1, "");
            dw.dxfString(2, "C:\\Program Files\\AutoCAD 2002\\plotters\\DWF ePlot (optimized for plotting).pc3");
            dw.dxfString(4, "");
            dw.dxfString(6, "");
            dw.dxfReal(40, 0.0);
            dw.dxfReal(41, 0.0);
            dw.dxfReal(42, 0.0);
            dw.dxfReal(43, 0.0);
            dw.dxfReal(44, 0.0);
            dw.dxfReal(45, 0.0);
            dw.dxfReal(46, 0.0);
            dw.dxfReal(47, 0.0);
            dw.dxfReal(48, 0.0);
            dw.dxfReal(49, 0.0);
            dw.dxfReal(140, 0.0);
            dw.dxfReal(141, 0.0);
            dw.dxfReal(142, 1.0);
            dw.dxfReal(143, 1.0);
            dw.dxfInt(70, 688);
            dw.dxfInt(72, 0);
            dw.dxfInt(73, 0);
            dw.dxfInt(74, 5);
            dw.dxfString(7, "");
            dw.dxfInt(75, 16);
            dw.dxfReal(147, 1.0);
            dw.dxfReal(148, 0.0);
            dw.dxfReal(149, 0.0);
            dw.dxfString(100, "AcDbLayout");
            dw.dxfString(1, "Layout1");
            dw.dxfInt(70, 1);
            dw.dxfInt(71, 1);
            dw.dxfReal(10, 0.0);
            dw.dxfReal(20, 0.0);
            dw.dxfReal(11, 420.0);
            dw.dxfReal(21, 297.0);
            dw.dxfReal(12, 0.0);
            dw.dxfReal(22, 0.0);
            dw.dxfReal(32, 0.0);
            dw.dxfReal(14, 1.000000000000000E+20);
            dw.dxfReal(24, 1.000000000000000E+20);
            dw.dxfReal(34, 1.000000000000000E+20);
            dw.dxfReal(15, -1.000000000000000E+20);
            dw.dxfReal(25, -1.000000000000000E+20);
            dw.dxfReal(35, -1.000000000000000E+20);
            dw.dxfReal(146, 0.0);
            dw.dxfReal(13, 0.0);
            dw.dxfReal(23, 0.0);
            dw.dxfReal(33, 0.0);
            dw.dxfReal(16, 1.0);
            dw.dxfReal(26, 0.0);
            dw.dxfReal(36, 0.0);
            dw.dxfReal(17, 0.0);
            dw.dxfReal(27, 1.0);
            dw.dxfReal(37, 0.0);
            dw.dxfInt(76, 0);
            dw.dxfHex(330, 0x1B);
            dw.dxfString(0, "LAYOUT");
            dw.dxfHex(5, 0x22);
            dw.dxfString(100, "AcDbPlotSettings");
            dw.dxfString(1, "");
            dw.dxfString(2, "C:\\Program Files\\AutoCAD 2002\\plotters\\DWF ePlot (optimized for plotting).pc3");
            dw.dxfString(4, "");
            dw.dxfString(6, "");
            dw.dxfReal(40, 0.0);
            dw.dxfReal(41, 0.0);
            dw.dxfReal(42, 0.0);
            dw.dxfReal(43, 0.0);
            dw.dxfReal(44, 0.0);
            dw.dxfReal(45, 0.0);
            dw.dxfReal(46, 0.0);
            dw.dxfReal(47, 0.0);
            dw.dxfReal(48, 0.0);
            dw.dxfReal(49, 0.0);
            dw.dxfReal(140, 0.0);
            dw.dxfReal(141, 0.0);
            dw.dxfReal(142, 1.0);
            dw.dxfReal(143, 1.0);
            dw.dxfInt(70, 1712);
            dw.dxfInt(72, 0);
            dw.dxfInt(73, 0);
            dw.dxfInt(74, 0);
            dw.dxfString(7, "");
            dw.dxfInt(75, 0);
            dw.dxfReal(147, 1.0);
            dw.dxfReal(148, 0.0);
            dw.dxfReal(149, 0.0);
            dw.dxfString(100, "AcDbLayout");
            dw.dxfString(1, "Model");
            dw.dxfInt(70, 1);
            dw.dxfInt(71, 0);
            dw.dxfReal(10, 0.0);
            dw.dxfReal(20, 0.0);
            dw.dxfReal(11, 12.0);
            dw.dxfReal(21, 9.0);
            dw.dxfReal(12, 0.0);
            dw.dxfReal(22, 0.0);
            dw.dxfReal(32, 0.0);
            dw.dxfReal(14, 0.0);
            dw.dxfReal(24, 0.0);
            dw.dxfReal(34, 0.0);
            dw.dxfReal(15, 0.0);
            dw.dxfReal(25, 0.0);
            dw.dxfReal(35, 0.0);
            dw.dxfReal(146, 0.0);
            dw.dxfReal(13, 0.0);
            dw.dxfReal(23, 0.0);
            dw.dxfReal(33, 0.0);
            dw.dxfReal(16, 1.0);
            dw.dxfReal(26, 0.0);
            dw.dxfReal(36, 0.0);
            dw.dxfReal(17, 0.0);
            dw.dxfReal(27, 1.0);
            dw.dxfReal(37, 0.0);
            dw.dxfInt(76, 0);
            dw.dxfHex(330, 0x1F);
            dw.dxfString(0, "LAYOUT");
            dw.dxfHex(5, 0x26);
            dw.dxfString(100, "AcDbPlotSettings");
            dw.dxfString(1, "");
            dw.dxfString(2, "C:\\Program Files\\AutoCAD 2002\\plotters\\DWF ePlot (optimized for plotting).pc3");
            dw.dxfString(4, "");
            dw.dxfString(6, "");
            dw.dxfReal(40, 0.0);
            dw.dxfReal(41, 0.0);
            dw.dxfReal(42, 0.0);
            dw.dxfReal(43, 0.0);
            dw.dxfReal(44, 0.0);
            dw.dxfReal(45, 0.0);
            dw.dxfReal(46, 0.0);
            dw.dxfReal(47, 0.0);
            dw.dxfReal(48, 0.0);
            dw.dxfReal(49, 0.0);
            dw.dxfReal(140, 0.0);
            dw.dxfReal(141, 0.0);
            dw.dxfReal(142, 1.0);
            dw.dxfReal(143, 1.0);
            dw.dxfInt(70, 688);
            dw.dxfInt(72, 0);
            dw.dxfInt(73, 0);
            dw.dxfInt(74, 5);
            dw.dxfString(7, "");
            dw.dxfInt(75, 16);
            dw.dxfReal(147, 1.0);
            dw.dxfReal(148, 0.0);
            dw.dxfReal(149, 0.0);
            dw.dxfString(100, "AcDbLayout");
            dw.dxfString(1, "Layout2");
            dw.dxfInt(70, 1);
            dw.dxfInt(71, 2);
            dw.dxfReal(10, 0.0);
            dw.dxfReal(20, 0.0);
            dw.dxfReal(11, 12.0);
            dw.dxfReal(21, 9.0);
            dw.dxfReal(12, 0.0);
            dw.dxfReal(22, 0.0);
            dw.dxfReal(32, 0.0);
            dw.dxfReal(14, 0.0);
            dw.dxfReal(24, 0.0);
            dw.dxfReal(34, 0.0);
            dw.dxfReal(15, 0.0);
            dw.dxfReal(25, 0.0);
            dw.dxfReal(35, 0.0);
            dw.dxfReal(146, 0.0);
            dw.dxfReal(13, 0.0);
            dw.dxfReal(23, 0.0);
            dw.dxfReal(33, 0.0);
            dw.dxfReal(16, 1.0);
            dw.dxfReal(26, 0.0);
            dw.dxfReal(36, 0.0);
            dw.dxfReal(17, 0.0);
            dw.dxfReal(27, 1.0);
            dw.dxfReal(37, 0.0);
            dw.dxfInt(76, 0);
            dw.dxfHex(330, 0x23);
            dw.dxfString(0, "DICTIONARY");
            dw.handle();                           // 2C
            dw.dxfString(100, "AcDbDictionary");
            dw.dxfInt(281, 1);
            dw.dxfString(3, "DIMASSOC");
            dw.dxfHex(350, dw.getNextHandle() + 1);        // 2E
            dw.dxfString(3, "HIDETEXT");
            dw.dxfHex(350, dw.getNextHandle());        // 2D
            dw.dxfString(0, "DICTIONARYVAR");
            dw.handle();                                    // 2E
            dw.dxfString(100, "DictionaryVariables");
            dw.dxfInt(280, 0);
            dw.dxfInt(1, 2);
            dw.dxfString(0, "DICTIONARYVAR");
            dw.handle();                                    // 2D
            dw.dxfString(100, "DictionaryVariables");
            dw.dxfInt(280, 0);
            dw.dxfInt(1, 1);
        }

        public void writeObjectsEnd(DL_Writer dw)
        {
            dw.dxfString(0, "ENDSEC");
        }

        #endregion
    }
}
