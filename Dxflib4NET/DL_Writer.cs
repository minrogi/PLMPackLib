#region Using directives
using System;
using System.Text;
using System.Collections.Generic;
#endregion

namespace Dxflib4NET
{
    /// <summary>
    /// Dxf
    /// </summary>
    public class DL_Writer
    {
        #region Data members
        StringBuilder _fileBuilder;
        DL_Codes.dxfversion _version;
        long m_handle;
        ulong _modelSpaceHandle;
        ulong _paperSpaceHandle;
        ulong _paperSpace0Handle;

        #endregion
        /// <summary>
        /// Constructor
        /// </summary>
        public DL_Writer(DL_Codes.dxfversion version)
        {
            _fileBuilder = new StringBuilder();
            _version = version;
            m_handle = 0x30;
            _modelSpaceHandle = 0;
            _paperSpaceHandle = 0;
            _paperSpace0Handle = 0;
        }

        public void close()
        {
        }

        /// <summary>
        /// Generic section for section 'name'
        /// </summary>
        /// <param name="name">Section name</param>
        public void section(string name)
        {
            dxfString(0, "SECTION");
            dxfString(2, name); 
        }
        /// <summary>
        /// Section HEADER.
        /// </summary>
        public void sectionHeader()
        {
            section("HEADER");
        }
        /// <summary>
        /// Section TABLES.
        /// </summary>
        public void sectionTables()
        {
            section("TABLES");
        }
        /// <summary>
        /// Section BLOCKS.
        /// </summary>
        public void sectionBlocks()
        {
            section("BLOCKS");
        }
        /// <summary>
        /// Section ENTITIES.
        /// </summary>
        public void sectionEntities()
        {
            section("ENTITIES");
        }
        /// <summary>
        /// Section CLASSES.
        /// </summary>
        public void sectionClasses()
        {
            section("CLASSES");
        }
        /// <summary>
        /// Section OBJECTS.
        /// </summary>
        public void sectionObjects()
        {
            section("OBJECTS");
        }
        /// <summary>
        /// End of a section.
        /// </summary>
        public void sectionEnd()
        {
            dxfString(0, "ENDSEC");
        }
        /// <summary>
        /// Generic table for table 'name' with 'num' entries
        /// </summary>
        /// <param name="name">Table name</param>
        /// <param name="num">Number</param>
        /// <param name="handle">Handle</param>
        public void table(string name, int num, int handle)
        {
            dxfString(0, "TABLE");
            dxfString(2, name);
            if (_version >= DL_Codes.VER_2000)
            {
                dxfHex(5, handle);
                dxfString(100, "AcDbSymbolTable");
            }
            dxfInt(70, num);
        }
        /// <summary>
        /// Table for layers.
        /// </summary>
        /// <param name="num">Number of layers in total.</param>
        public void tableLayers(int num)
        {
            table("LAYER", num, 2);
        }
        /// <summary>
        /// Table for line types.
        /// </summary>
        /// <param name="num">Number of line types in total.</param>
        public void tableLineTypes(int num)
        {
            table("LTYPE", num, 5);
        }
        /// <summary>
        /// Table for application id.
        /// </summary>
        /// <param name="num">Number of applications registered in total.</param>
        public void tableAppid(int num)
        {
            table("APPID", num, 9);
        }
        /// <summary>
        /// End of a table
        /// </summary>
        public void tableEnd()
        {
            dxfString(0, "ENDTAB");
        }
        /// <summary>
        /// End of DXF file.
        /// </summary>
        public void dxfEOF()
        {
            dxfString(0, "EOF");
        }
        /// <summary>
        /// Comment
        /// </summary>
        /// <param name="text"></param>
        public void comment(string text)
        {
            dxfString(999, text);
        }
        /// <summary>
        /// Entity
        /// </summary>
        /// <param name="entTypeName">Entity type name</param>
        public void entity(string entTypeName)
        {
            dxfString(0, entTypeName);
            if (_version >= DL_Codes.VER_2000)
                handle(5);
        }
        /// <summary>
        /// Attributes of an entity
        /// </summary>
        /// <param name="attrib"></param>
        public void entityAttributes(DL_Attributes attrib)
        { 
            // layer name
            dxfString(8, attrib.Layer);
            // R12 does not accept BYLAYER values.
            // The value has to be missing in that case.
            if (_version >= DL_Codes.VER_2000 || 256 != attrib.Color)
                dxfInt(62, attrib.Color);

            if (_version >= DL_Codes.VER_2000)
                dxfInt(370, attrib.Width);

            if (_version >= DL_Codes.VER_2000 || attrib.LineType.CompareTo("BYLAYER") == 0)
                dxfString(6, attrib.LineType);
        }
        /// <summary>
        /// Subclass
        /// </summary>
        /// <param name="sub">Subclass name</param>
        public void subClass(string sub)
        {
            dxfString(100, sub);
        }
        /// <summary>
        /// Layer (must be in the TABLES section LAYER).
        /// </summary>
        /// <param name="h">Layer handle</param>
        public void tableLayerEntry(long h)
        {
            dxfString(0, "LAYER");
            if (_version >= DL_Codes.VER_2000)
            {
                if (h == 0)
                    handle();
                else
                    dxfHex(5, h);
                dxfString(100, "AcDbSymbolTablerecord");
                dxfString(100, "AcDbLayerTableRecord");
            }
        }
        public void tableLayerEntry()
        { 
            dxfString(0, "LAYER");
            if (_version >= DL_Codes.VER_2000)
            {
                handle();
                dxfString(100, "AcDbSymbolTablerecord");
                dxfString(100, "AcDbLayerTableRecord");
            }
        }
        /// <summary>
        /// Line type (must be in the TABLES section LTYPE)
        /// </summary>
        /// <param name="h">Line type handle</param>
        public void tableLineTypeEntry(long h)
        {
            dxfString(0, "LTYPE");
            if (_version >= DL_Codes.VER_2000)
            {
                if (h == 0)
                    handle();
                else
                    dxfHex(5, h);
                dxfString(100, "AcDbSymbolTableRecord");
                dxfString(100, "AcDbLinetypeTableRecord");
            }
        }
        public void tableLineTypeEntry()
        {
            dxfString(0, "LTYPE");
            if (_version >= DL_Codes.VER_2000)
            {
                handle();
                dxfString(100, "AcDbSymbolTableRecord");
                dxfString(100, "AcDbLinetypeTableRecord");
            }
        }
        /// <summary>
        /// Appid (must be in the TABLES section APPID).
        /// </summary>
        /// <param name="h">APPID</param>
        public void tableAppidEntry(long h)
        {
            dxfString(0, "APPID");
            if (_version>=DL_Codes.VER_2000) {
                if (h==0)
                    handle();
                else
                    dxfHex(5, h);
                dxfString(100, "AcDbSymbolTableRecord");
                dxfString(100, "AcDbRegAppTableRecord");
            }
        }
        public void tableAppidEntry()
        {
            dxfString(0, "APPID");
            if (_version >= DL_Codes.VER_2000)
            {
                handle();
                dxfString(100, "AcDbSymbolTableRecord");
                dxfString(100, "AcDbRegAppTableRecord");
            }
        }       
        /// <summary>
        /// Block (must be in the section BLOCKS).
        /// </summary>
        /// <param name="h"></param>
        public void sectionBlockEntry(int h)
        {
            dxfString(0, "BLOCK");
            if (_version >= DL_Codes.VER_2000)
            {
                if (h == 0)
                    handle(5);
                else
                    dxfHex(5, h);
                dxfString(100, "AcDbEntity");
                if (h == 0x1C)
                    dxfInt(67, 1);
            }
        }
        /// <summary>
        /// End of block (must be in the section BLOCKS).
        /// </summary>
        /// <param name="h"></param>
        public void sectionBlockEntryEnd(int h)
        {
            dxfString(0, "ENDBLK");
            if (_version >= DL_Codes.VER_2000)
            {
                if (h == 0)
                    handle();
                else
                    dxfHex(5, h);
                dxfString(100, "AcDbEntity");
                if (h == 0x1D)
                    dxfInt(67, 1);
                dxfString(8, "0");
                dxfString(100, "AcDbBlockEnd");
            }
        }
        public void sectionBlockEntryEnd()
        {
            dxfString(0, "ENDBLK");
            if (_version >= DL_Codes.VER_2000)
            {
                handle();
                dxfString(100, "AcDbEntity");
                dxfString(8, "0");
                dxfString(100, "AcDbBlockEnd");
            }
        }
        public void color(int col)
        {
            dxfInt(62, col);
        }
        public void lineType(string lt)
        {
            dxfString(6, lt);
        }
        public void lineWeight(int lw)
        {
            dxfInt(370, lw);
        }
        public void coord(uint gc, double x, double y, double z)
        {
            dxfReal(gc, x);
            dxfReal(gc+10, y);
            dxfReal(gc+20, z);
        }
        /// <summary>
        /// Reset handle
        /// </summary>
        public void resetHandle()
        {
            m_handle = 1;        
        }
        /// <summary>
        /// Write a unique handle and returns it.
        /// </summary>
        /// <param name="gc">Group code</param>
        /// <returns></returns>
        public long handle(uint gc)
        {
            // handle has to be Hex
            dxfHex(gc, m_handle);
            return m_handle++;
        }
        /// <summary>
        /// Write a unique handle and returns it.
        /// </summary>
        /// <returns>Handle</returns>
        public long handle()
        {
            dxfHex(5, m_handle);
            return m_handle++;
        }
        /// <summary>
        /// Next handle that will be written
        /// </summary>
        /// <returns>Handle to be written</returns>
        public long getNextHandle()
        {
            return m_handle;
        }
        /// <summary>
        /// Increase handle so that the handle returned remains available.
        /// </summary>
        /// <returns></returns>
        public long incHandle()
        { 
            return m_handle++;
        }
        /// <summary>
        /// Sets the handle of the model space.
        /// Entities refer to this handle.
        /// </summary>
        /// <param name="h">Handle</param>
        public void setModelSpaceHandle(ulong h)
        {
            _modelSpaceHandle = h;
        }
        public ulong getModelSpaceHandle()
        {
            return _modelSpaceHandle;
        }
        /// <summary>
        /// Sets the handle of the paper space.
        /// Some special blocks refer to this handle.
        /// </summary>
        /// <param name="h">Handle</param>
        public void setPaperSpaceHandle(ulong h)
        {
            _paperSpaceHandle = h;
        }
        public ulong getPaperSpaceHandle()
        {
            return _paperSpaceHandle;
        }
        /// <summary>
        /// Sets the handle of the paper space 0;
        /// Some special blocks refer to this handle.
        /// </summary>
        /// <param name="h"></param>
        public void setPaperSpace0Handle(ulong h)
        {
            _paperSpace0Handle = h;
        }
        public ulong getPaperSpace0Handle()
        {
            return _paperSpace0Handle;
        }
        /// <summary>
        /// Writes a hex int variable to the DXF file.
        /// </summary>
        /// <param name="gc">Group code</param>
        /// <param name="value">Int value</param>
        public virtual void dxfHex(uint gc, int value)
        {
            dxfString(gc, string.Format("{0,4:X}", value));
        }
        /// <summary>
        /// Writes a hex int variable to the DXF file.
        /// </summary>
        /// <param name="gc">Group code</param>
        /// <param name="value">Long value</param>
        public virtual void dxfHex(uint gc, long value)
        {
            dxfString(gc, string.Format("{0,4:X}", value));
        }
        /// <summary>
        /// Writes an int variable to the DXF file.
        /// </summary>
        /// <param name="gc">Group code</param>
        /// <param name="value">Int value</param>
        public virtual void dxfInt(uint gc, int value)
        {
            dxfString(gc, value.ToString());
        }

        /// <summary>
        /// Writes a real (double) value to the DXF file.
        /// </summary>
        /// <param name="gc">Group code</param>
        /// <param name="value">Double value</param>
        public virtual void dxfReal(uint gc, double value)
        {
            string str = string.Format("{0}", value);
            // replace "," with "." if any
            str = str.Replace(",", ".");
            // Cut away those zeros at the end:
            if (str.Contains("."))
            {
                str.TrimEnd('0');
                while (str.Length - 1 - str.LastIndexOf('.') < 2)
                    str += '0';
            }
            dxfString(gc, str);
         }
        /// <summary>
        /// Writes a string value to the DXF file
        /// </summary>
        /// <param name="gc">Group code</param>
        /// <param name="value">String value</param>
        public virtual void dxfString(uint gc, string value)
        {
            _fileBuilder.Append(gc < 10 ? "  " : (gc < 100 ? " " : ""));
            _fileBuilder.Append(gc);
            _fileBuilder.AppendLine();
            _fileBuilder.Append(value);
            _fileBuilder.AppendLine();
        }

        public override string ToString()
        {
            return _fileBuilder.ToString();
        }
    }
}
