#region Using directives
using System;
using System.Collections.Generic;
using System.Linq;
#endregion

namespace PLMPackLib.DAL
{
    #region CardboardFormat
    public partial class CardboardFormat
    {
        public static bool Exists(PLMPackLibDb db, string name)
        {
            return db.CardboardFormats.Count(cbf => cbf.Name.ToLower() == name.ToLower()) > 0;
        }

        public static CardboardFormat CreateNew(PLMPackLibDb db, string name, string description, float length, float width)
        {
            // is already existing ?
            if (Exists(db, name))
                throw new Exception(string.Format("CardboardFormat with name = {0} already exists", name));
            // build new cardboard format
            CardboardFormat cardboardFormat = new CardboardFormat();
            cardboardFormat.Name = name;
            cardboardFormat.Description = description;
            cardboardFormat.Length = length;
            cardboardFormat.Width = width;
            db.CardboardFormats.Add(cardboardFormat);
            db.SaveChanges();
            return cardboardFormat;
        }

        public static CardboardFormat[] GetAll(PLMPackLibDb db)
        {
            return (from cbf in db.CardboardFormats select cbf).ToArray();
        }

        public static CardboardFormat GetByName(PLMPackLibDb db, string name)
        {
            return db.CardboardFormats.Single(cbf => cbf.Name.ToLower() == name.ToLower());
        }

        public static CardboardFormat GetByID(PLMPackLibDb db, int id)
        {
            return db.CardboardFormats.Single(cbf => cbf.ID == id);
        }

        public void Delete(PLMPackLibDb db)
        {
            db.CardboardFormats.Remove(this);
            db.SaveChanges();
        }

        public override string ToString()
        {
            return string.Format("Name={0} Length={1} Width={2}", Name, Length, Width);
        }
    }
    #endregion

    #region CardboardProfile
    public partial class CardboardProfile
    {
        public static bool Exists(PLMPackLibDb db, string code)
        {
            return db.CardboardProfiles.Count(cbp => cbp.Code.ToLower() == code.ToLower()) > 0;
        }

        public static CardboardProfile CreateNew(PLMPackLibDb db, string code, string name, float thickness)
        {
            // is already exists ?
            if (Exists(db, code))
                throw new Exception(string.Format("Cardboard profile with code = {0} already exists", code));
            // build new cardboard profile
            CardboardProfile cardboardProfile = new CardboardProfile();
            cardboardProfile.Code = code;
            cardboardProfile.Name = name;
            cardboardProfile.Thickness = thickness;
            db.CardboardProfiles.Add(cardboardProfile);
            db.SaveChanges();
            return cardboardProfile;
        }

        public static CardboardProfile[] GetAll(PLMPackLibDb db)
        { 
            return (from cbp in db.CardboardProfiles select cbp).ToArray();
        }

        public static CardboardProfile GetById(PLMPackLibDb db, int id)
        {
            return db.CardboardProfiles.Single(cbp => cbp.ID == id);
        }

        public void Delete(PLMPackLibDb db)
        {
            db.CardboardProfiles.Remove(this);
            db.SaveChanges();
        }

        public override string ToString()
        {
            return string.Format("Code={0} Name={1} Thickness={2}", Code, Name, Thickness);
        }
    }
    #endregion
}