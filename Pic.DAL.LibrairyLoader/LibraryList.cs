namespace Pic.DAL.LibraryLoader
{
    #region Using directives
    using System.Drawing;
    using System.IO;
    using System.Net;
    #endregion

    public partial class Library
    {
        #region Public properties
        public Image Thumbnail
        {
            get
            {
                string localImageDir = Path.Combine(Path.GetTempPath(), ImageDirectory);
                string localImagePath = Path.Combine(localImageDir, ThumbnailPath);

                // if thumbnail file can not be found, attempt to dowload it
                if (!File.Exists(localImagePath))
                {
                    // build image path
                    string plmPackLibFolder = LibraryLoader.Settings.Default.UriPlmPackLib;
                    if (!plmPackLibFolder.EndsWith("/"))
                        plmPackLibFolder += "/";
                     // download
                    byte[] data;
                    using (WebClient client = new WebClient())
                    { data = client.DownloadData(new System.Uri(plmPackLibFolder + "images/" + ThumbnailPath)); }
                    File.WriteAllBytes(localImagePath, data);                
                }
                return Image.FromFile(localImagePath);
            }
        }

        public static string ImageDirectory
        {
            get { return imageDir; }
        }
        #endregion

        #region Static data
        protected static string imageDir = "PLMPackLib_ThumbCache";
        #endregion
    }
}