#region Using directives
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

using Pic.Plugin;
using Pic.Plugin.ViewCtrl;
using Pic.DAL.SQLite;
#endregion

namespace PicParam
{
    public class ProfileLoaderImpl : ProfileLoader
    {
        #region Constructor
        public ProfileLoaderImpl()
        {
        }
        #endregion

        #region Public properties
        public Pic.DAL.SQLite.Component Component
        {
            set
            {
                _comp = value; _majorationList = null;
                BuildCardboardProfile();
            }
        }
        #endregion

        public override void BuildCardboardProfile()
        {
            PPDataContext db = new PPDataContext();
            CardboardProfile[] profiles = CardboardProfile.GetAll(db);
            _cardboardProfiles.Clear();
            foreach (CardboardProfile dbProfile in profiles)
                _cardboardProfiles.Add(dbProfile.Name, dbProfile);

            _majorationList = null;
        }

        #region ProfileLoader overrides
        public override void EditMajorations()
        {
            // show majoration edit form
            FormEditMajorations dlg = new FormEditMajorations(_comp.ID, _selectedProfile, this);
            if (DialogResult.OK == dlg.ShowDialog())  {}
        }

        protected override Profile[] LoadProfiles()
        {
            PPDataContext db = new PPDataContext();
            CardboardProfile[] profiles = CardboardProfile.GetAll(db);
            List<Profile> listProfile = new List<Profile>();
            foreach (CardboardProfile dbProfile in profiles)
                listProfile.Add(new Profile(dbProfile.Name));
            if (listProfile.Count > 0)
                Selected = listProfile[0];
            return listProfile.ToArray();
        }
        protected override Dictionary<string, double> LoadMajorationList()
        {
            if (null == Selected || null == _comp)
                return new Dictionary<string, double>();
            if (null == _majorationList)
            {
                PPDataContext db = new PPDataContext();
                _majorationList = Pic.DAL.SQLite.Component.GetDefaultMajorations(db, _comp.ID, _cardboardProfiles[Selected.ToString()]);
            }
            return _majorationList;
        }
        public override double Thickness
        {
            get
            {
                return _cardboardProfiles[Selected.ToString()].Thickness;
            }
        }
        #endregion

        #region Data members
        private Pic.DAL.SQLite.Component _comp;
        private Dictionary<string, CardboardProfile> _cardboardProfiles= new Dictionary<string,CardboardProfile>();
        #endregion
    }
}
