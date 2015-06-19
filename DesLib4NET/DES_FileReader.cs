#region Using directives
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
#endregion

namespace DesLib4NET
{
    public class DES_FileReader : IDisposable
    {
        #region Constructor
        public DES_FileReader()
        {
        }
        #endregion

        #region IDisposable implementation
        public void Dispose()
        {
        }
        #endregion

        #region Read methods
        public bool ReadFile(string filePath, DES_CreationAdapter creationAdapter)
        {
            if (null != _listener)
                _listener.Begin();

            // open file
            BinaryReader br = new BinaryReader(File.Open(filePath, FileMode.Open));

            // get actual file size
            long iActualFileSize = br.BaseStream.Length;
            // check that this is a Des99 file
            br.BaseStream.Position = 4 * 36;
            int sizeToRead = 41;
            byte[] checkStringArray = br.ReadBytes(sizeToRead);
            string checkString = string.Empty;
            foreach (byte b in checkStringArray)
                checkString += (char)b;

            if ("FicDes99, PICADOR est une marque déposée." != checkString)
            {
                br.Close();
                return false;
            }
            // read expected file size
            int iExpectedFileSize = br.ReadInt32();
            if (iExpectedFileSize != iActualFileSize)
            {
                br.Close();
                return false;
            }
            // read float
            float xmin = br.ReadSingle();
            float ymin = br.ReadSingle();
            float xmax = br.ReadSingle();
            float ymax = br.ReadSingle();

            creationAdapter.SetViewWindow(xmin, ymin, xmax, ymax);

            // read 2D/3D flag
            byte c = br.ReadByte();
            if (253 == c)
                creationAdapter.Set2D();
            else if (254 == c)
                creationAdapter.Set3D();
            else
            {
                br.Close();
                return false;
            }

            // read superbase object
            if (!ReadSuperBaseObject(br, creationAdapter))
            {
                br.Close();
                return false;
            }
            br.Close();

            if (null != _listener)
                _listener.End();
            return true;
        }

        private bool ReadSuperBaseObject(BinaryReader br, DES_CreationAdapter creationAdapter)
        {
            DES_SuperBaseHeader header = new DES_SuperBaseHeader();

            // read superbase header
            if (br.ReadByte() != 90)
                return false;
            header._positionListOfEntities = (uint)br.ReadInt32();
            if (br.ReadByte() != 200)
                return false;
            header._companyName = ReadString(br);
            if (br.ReadByte() != 201)
                return false;
            header._userName = ReadString(br);
            if (br.ReadByte() != 202)
                return false;
            header._comment = ReadString(br);
            if (br.ReadByte() != 92)
                return false;
            header._fileVersion = br.ReadUInt32();
            if (br.ReadByte() != 203)
                return false;
            ReadDate(br);
            if (br.ReadByte() != 204)
                return false;
            ReadDate(br);
            if (br.ReadByte() != 95)
                return false;
            header._positionFileTable = (uint)br.ReadInt32();

            creationAdapter.AddSuperBaseHeader(header);

            // move to superbase table
            br.BaseStream.Position = header._positionFileTable;
            // read table code
            if (br.ReadByte() != 255)
                return false;
            // read number of entities
            int noEntities = br.ReadInt32();
            if (null != _listener)
                _listener.SetRange(0, (uint)noEntities);
            // read table
            List<Pair<uint, uint>> table = new List<Pair<uint, uint>>();
            for (int i = 0; i < noEntities; ++i)
            {
                Pair<uint, uint> pair = new Pair<uint, uint>(0,0);
                pair.first = (uint)br.ReadInt32();
                pair.second = (uint)br.ReadInt32();
                table.Add(pair);
            }

            uint pos = 0;
            foreach (Pair<uint, uint> pair in table)
            {
                if (null != _listener)
                    _listener.SetPosition(pos++);

                br.BaseStream.Position = pair.second;

                switch (pair.first)
                {
                    case 0: // end entity -> read nothing
                        break;
                    case 1: // segment
                        {
                            // entity code
                            if (br.ReadByte() != 1) continue;
                            int code = br.ReadInt32();
                            // x
                            if (br.ReadByte() != 2) continue;
                            float x = br.ReadSingle();
                            // y
                            if (br.ReadByte() != 3) continue;
                            float y = br.ReadSingle();
                            // direction
                            if (br.ReadByte() != 4) continue;
                            float dir = br.ReadSingle();
                            // dimension
                            if (br.ReadByte() != 5) continue;
                            float dim = br.ReadSingle();
                            // pen
                            if (br.ReadByte() != 150) continue;
                            byte pen = br.ReadByte();
                            // level
                            if (br.ReadByte() != 151) continue;
                            byte layer = br.ReadByte();
                            // group
                            if (br.ReadByte() != 152) continue;
                            byte grp = br.ReadByte();
                            // lock
                            if (br.ReadByte() != 153) continue;
                            byte loc = br.ReadByte();

		                    float x1 = x - dim * (float)Math.Cos(dir * Math.PI / 180.0f);
                            float y1 = y - dim * (float)Math.Sin(dir * Math.PI / 180.0f);
                            float x2 = x + dim * (float)Math.Cos(dir * Math.PI / 180.0f);
                            float y2 = y + dim * (float)Math.Sin(dir * Math.PI / 180.0f);

                            creationAdapter.AddSegment(new DES_Segment(x1, y1, x2, y2, pen, grp, layer));
                        }
                        break;
                    case 2: // point
                        {
                            // entity code
                            if (br.ReadByte() != 1) continue;
                            int code = br.ReadInt32();
                            // x
                            if (br.ReadByte() != 2) continue;
                            float x = br.ReadSingle();
                            // y
                            if (br.ReadByte() != 3) continue;
                            float y = br.ReadSingle();
                            // direction
                            if (br.ReadByte() != 4) continue;
                            float dir = br.ReadSingle();
                            // dimention
                            if (br.ReadByte() != 5) continue;
                            float dim = br.ReadSingle();
                            // pen
                            if (br.ReadByte() != 150) continue;
                            byte pen = br.ReadByte();
                            // level
                            if (br.ReadByte() != 151) continue;
                            byte layer = br.ReadByte();
                            // group
                            if (br.ReadByte() != 152) continue;
                            byte grp = br.ReadByte();
                            // lock
                            if (br.ReadByte() != 153) continue;
                            byte loc = br.ReadByte();

                            creationAdapter.AddPoint(new DES_Point(x,y,pen, grp, layer));
                        }
                        break;
                    case 5: // arc
                        {
                            // entity code
                            if (br.ReadByte() != 1) continue;
                            int code = br.ReadInt32();
                            // x
                            if (br.ReadByte() != 2) continue;
                            float x = br.ReadSingle();
                            // y
                            if (br.ReadByte() != 3) continue;
                            float y = br.ReadSingle();
                            // direction
                            if (br.ReadByte() != 4) continue;
                            float dir = br.ReadSingle();
                            // dimension
                            if (br.ReadByte() != 5) continue;
                            float dim = br.ReadSingle();
                            // pen
                            if (br.ReadByte() != 150) continue;
                            byte pen = br.ReadByte();
                            // layer
                            if (br.ReadByte() != 151) continue;
                            byte layer = br.ReadByte();
                            // group
                            if (br.ReadByte() != 152) continue;
                            byte grp = br.ReadByte();
                            // lock
                            if (br.ReadByte() != 153) continue;
                            byte loc = br.ReadByte();
                            // association
                            byte byteCode = br.ReadByte();
                            if (byteCode == 200)
                            {
                                int size = br.ReadInt32();
                                int noAssociations = br.ReadInt32();
                                for (int i = 0; i < noAssociations; ++i)
                                {
                                    br.ReadChar();
                                    br.ReadInt32();
                                }
                                // next
                                byteCode = br.ReadByte();
                            }
                            // read gaps
                            if (byteCode == 217)
                            {
                                int size = br.ReadInt32();
						        float fGaps = br.ReadSingle();
						        int noGaps = (int)fGaps;
						        for (int i=0; i<noGaps; ++i)
						        {
                                    br.ReadSingle();
                                    br.ReadSingle();
						        }
						        // next 
                                byteCode = br.ReadByte();
                           }
                            if (byteCode == 170)
                            {
                                byteCode = br.ReadByte(); // read code = 170 a second time ?
						        byteCode = br.ReadByte();
						        if (byteCode == 71 )
						        {
                                    int saut = br.ReadInt32();
							        byteCode =  br.ReadByte();
						        }
						        if ( byteCode == 72 )
						        {
                                    int coupe = br.ReadInt32();
							        byteCode =  br.ReadByte();
						        }
                            }
                            // opening angle
                            if (11 != byteCode) continue;
                            float angle = br.ReadSingle();
                            
                            creationAdapter.AddArc(new DES_Arc(x, y, dim, dir, dir+angle, pen, grp, layer));
                        }
                        break;
                    case 7: // bezier
                        break;
                    case 8: // ellipse
                        break;
                    case 15: // surface
                        break;
                    case 16: // pose
                        break;
                    case 20: // C_COT_RAYINT
                        {
                            creationAdapter.AddDimensionInnerRadius();
                        }
                        break;
                    case 21: // C_COT_RAYEXT
                        {
                            creationAdapter.AddDimensionOuterRadius();
                        }
                        break;
                    case 22: // C_COT_DIAINT
                        {
                            creationAdapter.AddDimensionInnerDiameter();
                        }
                        break;
                    case 23: // C_COT_DIAEXT
                        {
                            creationAdapter.AddDimensionOuterDiameter();
                        }
                        break;
                    case 24: // C_COT_CYL
                        {

                        }
                        break;
                    case 25: // C_COT_DIS
                        {
                            // entity code
                            if (br.ReadByte() != 1) continue;
                            int code = br.ReadInt32();
                            // x
                            if (br.ReadByte() != 2) continue;
                            float x = br.ReadSingle();
                            // y
                            if (br.ReadByte() != 3) continue;
                            float y = br.ReadSingle();
                            // direction
                            if (br.ReadByte() != 4) continue;
                            float dir = br.ReadSingle();
                            // dimension
                            if (br.ReadByte() != 5) continue;
                            float dim = br.ReadSingle();
                            // pen
                            if (br.ReadByte() != 150) continue;
                            byte pen = br.ReadByte();
                            // layer
                            if (br.ReadByte() != 151) continue;
                            byte layer = br.ReadByte();
                            // group
                            if (br.ReadByte() != 152) continue;
                            byte grp = br.ReadByte();
                            // lock
                            if (br.ReadByte() != 153) continue;
                            byte loc = br.ReadByte();
                            // association
                            byte byteCode = br.ReadByte();
                            if (byteCode == 200)
                            {
                                int size = br.ReadInt32();
                                int noAssociations = br.ReadInt32();
                                for (int i = 0; i < noAssociations; ++i)
                                {
                                    br.ReadChar();
                                    br.ReadInt32();
                                }
                                // next
                                byteCode = br.ReadByte();
                            }
                            // aEspace
                            if (byteCode != 20) continue;
                            bool aEspace = (br.ReadInt32() == 1);
                            // aText
                            if (br.ReadByte() != 21) continue;
                            bool aText = (br.ReadInt32() == 1);
                            // aTolerance
                            if (br.ReadByte() != 22) continue;
                            bool aTolerance = (br.ReadInt32() == 1);
                            // offset
                            if (br.ReadByte() != 23) continue;
                            float offset = br.ReadSingle();
                            // ecart inf
                            if (br.ReadByte() != 24) continue;
                            float ecartInf = br.ReadSingle();
                            // ecart sup
                            if (br.ReadByte() != 25) continue;
                            float ecartSup = br.ReadSingle();
                            // houv
                            if (br.ReadByte() != 155) continue;
                            char houv = br.ReadChar();
                            // invDep
                            if (br.ReadByte() != 26) continue;
                            bool invDep = (br.ReadInt32() == 1);
                            // noDecimals
                            if (br.ReadByte() != 101) continue;
                            short noDecimals = br.ReadInt16();
                            // reduction
                            if (br.ReadByte() != 27) continue;
                            float reduction = br.ReadSingle();
                            // text
                            if (br.ReadByte() != 217) continue;
                            string text = ReadString(br);

                            DES_CotationDistance dimension = new DES_CotationDistance(
                                x, y, dir, dim
                                , pen, grp, layer
                                , offset, reduction
                                , ecartSup, ecartInf, invDep
                                , aText, aTolerance, aEspace
                                , noDecimals, text, houv);
                            creationAdapter.AddDimensionDistance(dimension);
                        }
                        break;
                    case 26: // C_COT_ANGLE
                        {
                            creationAdapter.AddDimensionAngle();
                        }
                        break;
                    case 29: // C_COT_FLECHE
                        {
                            creationAdapter.AddDimensionArrow();
                        }
                        break;
                    case 34: // C_TEXTE
                        {
                            // entity code
                            if (br.ReadByte() != 1) continue;
                            int code = br.ReadInt32();
                            // x
                            if (br.ReadByte() != 2) continue;
                            float x = br.ReadSingle();
                            // y
                            if (br.ReadByte() != 3) continue;
                            float y = br.ReadSingle();
                            // direction
                            if (br.ReadByte() != 4) continue;
                            float dir = br.ReadSingle();
                            // dimention
                            if (br.ReadByte() != 5) continue;
                            float dim = br.ReadSingle();
                            // pen
                            if (br.ReadByte() != 150) continue;
                            byte pen = br.ReadByte();
                            // level
                            if (br.ReadByte() != 151) continue;
                            byte layer = br.ReadByte();
                            // group
                            if (br.ReadByte() != 152) continue;
                            byte grp = br.ReadByte();


                            DES_Text text = new DES_Text(pen, grp, layer);
                            creationAdapter.AddText(text);
                        }
                        break;
                    case 4000: // questionnaire
                        {
                            // entity code
                            if (br.ReadByte() != 1) continue;
                            int code = br.ReadInt32();
                            // x
                            if (br.ReadByte() != 2) continue;
                            float x = br.ReadSingle();
                            // y
                            if (br.ReadByte() != 3) continue;
                            float y = br.ReadSingle();
                            // direction
                            if (br.ReadByte() != 4) continue;
                            float dir = br.ReadSingle();
                            // dimention
                            if (br.ReadByte() != 5) continue;
                            float dim = br.ReadSingle();
                            // assos
                            byte byteCode = br.ReadByte();
                            if (byteCode == 200)
                            {
                                int size = br.ReadInt32();
                                int noAssociations = br.ReadInt32();
                                for (int i = 0; i < noAssociations; ++i)
                                {
                                    br.ReadChar();
                                    br.ReadInt32();
                                }
                                // next
                                byteCode = br.ReadByte();
                            }
                            // number of questions
                            if (byteCode != 29) continue;
                            int iNoQuest = br.ReadInt32();
                            if (iNoQuest > 200) continue;

                            Dictionary<string, string> questions = new Dictionary<string,string>();
                            for (int i = 0; i < iNoQuest; ++i)
                            {
                                if (br.ReadByte() != 218) continue;
                                string quest = ReadString(br);
                                if (br.ReadByte() != 219) continue;
                                string answer = ReadString(br);
                                if (br.ReadByte() != 30) continue;
                                int index = br.ReadInt32();
                                questions.Add(quest, answer);
                            }
                            creationAdapter.UpdateQuestionnaire(questions);
                        }
                        break;
                    default:
                        break;
                }
            }
            return true;
        }

        string ReadString(BinaryReader br)
        {
            string s = string.Empty;
            int iLength = br.ReadInt32();
            for (int i=0; i<iLength; ++i)
                s += (char)br.ReadByte();
            return s;
        }

        DES_Date ReadDate(BinaryReader br)
        {
            DES_Date date = new DES_Date();
            date.nDay = br.ReadInt32();
            date.nMonth = br.ReadInt32();
            date.nYear = br.ReadInt32();
            date.nHour = br.ReadInt32();
            date.nMin = br.ReadInt32();
            date.nSec = br.ReadInt32();
            return date;
        }

        public DES_FileReaderListener Listener
        {
            set
            {
                _listener = value;
            }
        }
        #endregion

        #region Data members
        protected DES_FileReaderListener _listener;
        #endregion
    }
}
