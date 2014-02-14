#region Data members
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
#endregion

namespace DesLib4NET
{
    #region Pair class
    public class Pair<T1, T2>
        where T1 : IComparable<T1>
        where T2 : IComparable<T2>
    {
        public Pair(T1 m1, T2 m2)
        {
            first = m1;
            second = m2;
        }

        public T1 first;
        public T2 second;
    }
    #endregion
  
    public abstract class DES_Writer:IDisposable
    {
        #region IDisposable implementation
        public void Dispose()
        {
            if (null != _br)
                Close();
        }
        #endregion

        #region Initialization / Finalization
        protected void Initialize(DES_Header header)
        {
            long iPos = _br.BaseStream.Position;

            byte[] buffer = new byte[36];
            for (int i=0; i<4; ++i)
                _br.Write(buffer);

            // des file header
            string sHeader = "FicDes99, PICADOR est une marque déposée.";
            WriteString(sHeader);

            // des file size
            _addressFileSize = _br.BaseStream.Position;
            _br.Write((uint)_iFileSize);

            // valeur du zoom a l'enregistrement
            _xmin = header._xmin;
            _ymin = header._ymin;
            _xmax = header._xmax;
            _ymax = header._ymax;

            _addressZoom = _br.BaseStream.Position;
            _br.Write(_xmin);
            _br.Write(_ymin);
            _br.Write(_xmax);
            _br.Write(_ymax);

            // 2D or 3D
            _br.Write(header._code2D3D);

            if (253 == header._code2D3D) /* 0xFD */
            {	// 2D -> do not write anything else
            }
            else if (254 == header._code2D3D)
            {	// 3D -> also write transformation matrices
                for (int i=0; i<16; ++i)
                    _br.Write(header._viewMatrix[i]);
                for (int i = 0; i < 16; ++i)
                    _br.Write(header._viewMatrixInverse[i]);
            }
        }

        public void Finish()
        {
            if (_finalized)
                return;

            long addressTable = _br.BaseStream.Position;
            // write table
            // code
            WriteCode(255);
            // number of entities
            _br.Write(_table.Count + 1);
            // write table
            foreach (Pair<uint, uint> pair in _table)
            {
                _br.Write(pair.first);
                _br.Write(pair.second);
            }
            // end entity
            _br.Write(0);
            _br.Write((uint)addressTable);
            // save file size
            uint fileSize = (uint)_br.BaseStream.Position;
            // save table position
            _br.Seek((int)_addressTableRef, SeekOrigin.Begin);
            _br.Write((int)addressTable);

            // write zoom
            _br.Seek((int)_addressZoom, SeekOrigin.Begin);
            _br.Write(_xmin);
            _br.Write(_ymin);
            _br.Write(_xmax);
            _br.Write(_ymax);
            // write file size
            _br.Seek((int)_addressFileSize, SeekOrigin.Begin);
            _br.Write(fileSize);

            _finalized = true;
        }

        public void Close()
        {
            Finish();
            _br.Close();
            _br = null;
        }
        #endregion

        #region Write methods
        public void WriteSuperBaseHeader(DES_SuperBaseHeader header)
        { 
        	// position of first entity
	        WriteCode(90); /* 0x5A */
	        long addressListOfEntitiesRef = _br.BaseStream.Position;
	        _br.Write((uint)header._positionListOfEntities);

            // company name
            WriteCode(200); /* 0xC8 */
            _br.Write((uint)header._companyName.Length);
            WriteString(header._companyName);

	        // user name
	        WriteCode(201); /* 0xC9 */
	        _br.Write((uint)header._userName.Length);
	        WriteString(header._userName);

 	        // comment
	        WriteCode(202);
            _br.Write((uint)header._comment.Length);
	        WriteString(header._comment);

	        // file version
	        WriteCode(92);
            _br.Write(header._fileVersion);

	        // date created
	        WriteCode(203);
	        WriteDate(header._dateCreated);

	        // date modified
	        WriteCode(204);
	        WriteDate(header._dateModified);

            // position file table
            WriteCode(95);
            _addressTableRef = _br.BaseStream.Position;
            _br.Write((uint)header._positionFileTable);

            header._positionListOfEntities = (uint)_br.BaseStream.Position;

            _br.Seek((int)addressListOfEntitiesRef, SeekOrigin.Begin);
            _br.Write((uint)header._positionListOfEntities);

            _br.Seek((int)header._positionListOfEntities, SeekOrigin.Begin);
        }

        void WriteCode(int code)
        {
            _br.Write((byte)code);
        }

        void WriteString(string s)
        {
            foreach (char c in s.ToCharArray())
                _br.Write((byte)c);
        }

        void WriteDate(DES_Date date)
        {
            _br.Write(date.nDay);
            _br.Write(date.nMonth);
            _br.Write(date.nYear);
            _br.Write(date.nHour);
            _br.Write(date.nMin);
            _br.Write(date.nSec);
        }

        void WriteFloatArray(float[] data)
        {
        }

        void WriteUIntArray(uint[] data)
        { 
        }

        public void WritePoint(DES_Point point)
        {
            // save in table
            _table.Add(new Pair<uint, uint>(2 /*point*/, (uint)_br.BaseStream.Position));

            // entity code
            WriteCode(1);
            _br.Write(2); // point
            // x
            WriteCode(2);
            _br.Write(point._x);
            // y
            WriteCode(3);
            _br.Write(point._y);
            // direction
            WriteCode(4);
            _br.Write(point._dir);
            // dim
            WriteCode(5);
            _br.Write(point._dim);
            // pen
            WriteCode(150);
            _br.Write(point._pen);
            // level
            WriteCode(151);
            _br.Write(point._layer);
            // group
            WriteCode(152);
            _br.Write(point._grp);
            // lock
            WriteCode(153);
            _br.Write(point._lock);
        }

        public void WriteSegment(DES_Segment segment)
        {
            // save in table
            _table.Add(new Pair<uint, uint>(1 /*segment*/, (uint)_br.BaseStream.Position));

            // entity code
            WriteCode(1);
            _br.Write(1); // segment
            // x
            WriteCode(2);
            _br.Write(segment._x); // segment X
            // y
            WriteCode(3);
            _br.Write(segment._y); // segment Y
            // direction
            WriteCode(4);
            _br.Write(segment._dir);
            // dim
            WriteCode(5);
            _br.Write(segment._dim);
            // pen
            WriteCode(150);
            _br.Write(segment._pen);
            // level
            WriteCode(151);
            _br.Write(segment._layer);
            // group
            WriteCode(152);
            _br.Write(segment._grp);
            // lock
            WriteCode(153);
            _br.Write(segment._lock);
        }

        public void WriteArc(DES_Arc arc)
        {
            // save in table
            _table.Add(new Pair<uint, uint>(5 /*arc*/, (uint)_br.BaseStream.Position));
            
            // entity code
            WriteCode(1);
            _br.Write(5); // arc
            // x
            WriteCode(2);
            _br.Write(arc._x);
            // y
            WriteCode(3);
            _br.Write(arc._y);
            // direction
            WriteCode(4);
            _br.Write(arc._dir);
            // dim
            WriteCode(5);
            _br.Write(arc._dim);
            // pen
            WriteCode(150);
            _br.Write(arc._pen);
            // level
            WriteCode(151);
            _br.Write(arc._layer);
            // group
            WriteCode(152);
            _br.Write(arc._grp);
            // lock
            WriteCode(153);
            _br.Write(arc._lock);
            // opening angle
            WriteCode(11);
            _br.Write(arc._angle);
        }

        public void WriteCotationDistance(DES_CotationDistance cotation)
        {
             // save in table
            _table.Add(new Pair<uint, uint>(25 /*cot distance*/, (uint)_br.BaseStream.Position));
			//code
            WriteCode(1);
            _br.Write(25); // cot distance
            // x
            WriteCode(2);
            _br.Write(cotation._x);     // cotation X
            // y
            WriteCode(3);
            _br.Write(cotation._y);     // cotation Y
            // direction
            WriteCode(4);
            _br.Write(cotation._dir);   // dir
            // dim
            WriteCode(5);
            _br.Write(cotation._dim);   // dim
            // pen
            WriteCode(150);
            _br.Write(cotation._pen);   // pen
            // level
            WriteCode(151);
            _br.Write(cotation._layer); // layer
            // group
            WriteCode(152);
            _br.Write(cotation._grp);   // grp
            // lock
            WriteCode(153);
            _br.Write(cotation._lock);  // lock
            // espace
            WriteCode(20);
            _br.Write(0);               // FALSE espace
            // text
            WriteCode(21);
            _br.Write(0);               // FALSE text
            // tol
            WriteCode(22);              
            _br.Write(0);               // FALSE tolerance
            // offset
            WriteCode(23);
            _br.Write((float)cotation._offset);     // deport
            // inf
            WriteCode(24);
            _br.Write(0.0f);            // ecart inf
            // sup
            WriteCode(25);
            _br.Write(0.0f);            // ecart sup
            // houv
            WriteCode(155);
            _br.Write((char)2);         // char houv
            // inverse deport
            WriteCode(26);
            _br.Write(0);
            // nb digit
            WriteCode(101);
            _br.Write((short)1);
            // reduction
            WriteCode(27);
            _br.Write(0.0f);
            // texte cote
            WriteCode(217);
            _br.Write(0);
        }

        public void WriteBlockRef(DES_Pose pose)
        {
            // save in table
            _table.Add(new Pair<uint, uint>(16 /*pose*/, (uint)_br.BaseStream.Position));
            // code
            WriteCode(1);
            _br.Write(16); // pose
            // X
            WriteCode(2);
            _br.Write(pose._x);
            // Y
            WriteCode(3);
            _br.Write(pose._y);
            // DIR
            WriteCode(4);
            _br.Write(pose._dir);
            // DIM
            WriteCode(5);
            _br.Write(pose._dim);
            // pen
            WriteCode(150);
            _br.Write(pose._pen);
            // level
            WriteCode(151);
            _br.Write(pose._layer);
            // group
            WriteCode(152);
            _br.Write(pose._grp);
            // lock
            WriteCode(153);
            _br.Write(pose._lock);
            // CBDDPOSE_DX
            WriteCode(38);
            _br.Write(pose._dx);
            // CBDDPOSE_DY
            WriteCode(39);
            _br.Write(pose._dy);
            // CBDDPOSE_MIR
            WriteCode(156);
            _br.Write(pose._mir);
        }

        public void WriteCardboardFormat(DES_CardboardFormat cardboardFormat)
        {
            // save in table
            _table.Add(new Pair<uint, uint>(18 /* CBddCartImpose */, (uint)_br.BaseStream.Position));
            // code
            WriteCode(1);
            _br.Write(18); // CBddCartImpose
            // X
            WriteCode(2);
            _br.Write(cardboardFormat._x);
            // Y
            WriteCode(3);
            _br.Write(cardboardFormat._y);
            // DIR
            WriteCode(4);
            _br.Write(cardboardFormat._dir);
            // DIM
            WriteCode(5);
            _br.Write(cardboardFormat._dim);
            // pen
            WriteCode(150);
            _br.Write(cardboardFormat._pen);
            // level
            WriteCode(151);
            _br.Write(cardboardFormat._layer);
            // group
            WriteCode(152);
            _br.Write(cardboardFormat._grp);
            // lock
            WriteCode(153);
            _br.Write(cardboardFormat._lock);
            // ep
            WriteCode(47);
            _br.Write(cardboardFormat._thickness);
            // fmtcartx
            WriteCode(48);
            _br.Write(cardboardFormat._width);
            // fmtcarty
            WriteCode(49);
            _br.Write(cardboardFormat._height);
            // fmthtx
            WriteCode(50);
            _br.Write(cardboardFormat._width);
            // fmthty
            WriteCode(51);
            _br.Write(cardboardFormat._height);
            // pertefc
            WriteCode(52);
            _br.Write(0.0);
            // pertefht
            WriteCode(53);
            _br.Write(0.0);

            /*
            // Verrouillage
			code=CBDD_VER; ficdes->Write(&code,1);
			ficdes->Write((char*)&m_masque_ver,sizeof(m_masque_ver));
			// Association
			listasso=GetListAsso();
			if(nbasso=listasso.GetLength())
			{
				int err;
				if(err=listasso.Serialize(ficdes,mode,posfin,TableAsso,this))
					return err;
			}
			code=CBDDCARTIMPOSE_EP; ficdes->Write(&code,1);
			ficdes->Write((char*)&m_ep,sizeof(m_ep));
			// 
			code=CBDDCARTIMPOSE_FMTCARTX; ficdes->Write(&code,1);
			ficdes->Write((char*)&m_fmtcartx,sizeof(m_fmtcartx));
			// 
			code=CBDDCARTIMPOSE_FMTCARTY; ficdes->Write(&code,1);
			ficdes->Write((char*)&m_fmtcarty,sizeof(m_fmtcartx));
			// 
			code=CBDDCARTIMPOSE_FMTHTX; ficdes->Write(&code,1);
			ficdes->Write((char*)&m_fmthtx,sizeof(m_fmthtx));
			// 
			code=CBDDCARTIMPOSE_FMTHTY; ficdes->Write(&code,1);
			ficdes->Write((char*)&m_fmthty,sizeof(m_fmthty));
			// 
			code=CBDDCARTIMPOSE_PERTEFC; ficdes->Write(&code,1);
			ficdes->Write((char*)&m_pertefc,sizeof(m_pertefc));
			// 
			code=CBDDCARTIMPOSE_PERTEFHT; ficdes->Write(&code,1);
			ficdes->Write((char*)&m_pertefht,sizeof(m_pertefht));
            */
            // #define CBDDCARTIMPOSE_EP		47	// 
            // #define CBDDCARTIMPOSE_FMTCARTX	48	// X
            // #define CBDDCARTIMPOSE_FMTCARTY	49	// Y
            // #define CBDDCARTIMPOSE_FMTHTX	50	// Format Hors Tout en X
            // #define CBDDCARTIMPOSE_FMTHTY	51	// Format Hors Tout en X
            // #define CBDDCARTIMPOSE_PERTEFC	52	// 
            // #define CBDDCARTIMPOSE_PERTEFHT	53	// 
        }
        #endregion

        #region Data members
        protected System.IO.BinaryWriter _br;
        long _addressFileSize = 0;
        long _addressTableRef = 0;
        long _addressZoom = 0;
        long _iFileSize = 0;
        float _xmin = 0.0f, _ymin = 0.0f, _xmax = 0.0f, _ymax = 0.0f;
        List<Pair<uint, uint>> _table = new List<Pair<uint, uint>>();
        bool _finalized = false;
        #endregion
    }

    public class DES_WriterFile : DES_Writer
    { 
        #region Constructor
        public DES_WriterFile(DES_Header header, string filePath)
        {
            _br = new System.IO.BinaryWriter(File.Open(filePath, FileMode.Create));
            Initialize(header);
        }
        #endregion
    }

    public class DES_WriterMem : DES_Writer
    { 
        #region Constructor
        public DES_WriterMem(DES_Header header)
        {
            _br = new System.IO.BinaryWriter(_memStream);
            Initialize(header);
        }
        #endregion

        #region Output
        public byte[] GetResultByteArray()
        {
            Finish();
            return _memStream.ToArray();
        }
        #endregion

        #region Data members
        System.IO.MemoryStream _memStream = new MemoryStream();
        #endregion
    }
}
