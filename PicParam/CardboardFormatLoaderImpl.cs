#region Using directives
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Pic.Factory2D;
#endregion

namespace PicParam
{
    class CardboardFormatLoaderImpl : CardboardFormatLoader
    {
        #region Constructor
        public CardboardFormatLoaderImpl()
        {
        }
        #endregion

        #region CardboardFormatLoader implementation
        public override CardboardFormat[] LoadCardboardFormats()
        {
            Pic.DAL.SQLite.PPDataContext db = new Pic.DAL.SQLite.PPDataContext();
            Pic.DAL.SQLite.CardboardFormat[] _cardboardFormats = Pic.DAL.SQLite.CardboardFormat.GetAll(db);

            List<CardboardFormat> listFormat = new List<CardboardFormat>();
            foreach (Pic.DAL.SQLite.CardboardFormat format in _cardboardFormats)
                listFormat.Add(new CardboardFormat(format.ID, format.Name, format.Description, format.Length, format.Width));
            return listFormat.ToArray();
        }
        public override void EditCardboardFormats()
        {
            FormEditCardboardFormats form = new FormEditCardboardFormats();
            if (DialogResult.OK == form.ShowDialog())
            { 
            }
        }
        #endregion
    }
}
