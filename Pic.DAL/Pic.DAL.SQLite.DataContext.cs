namespace Pic.DAL.SQLite
{
    #region Using directives
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.Data;
    using System.Data.SQLite;
    using System.Data.Linq.Mapping;
    using System.Linq;
    using log4net;
    using log4net.Config;
    #endregion

    #region ExceptionDAL
    public class ExceptionDAL : System.Exception
    {
        public ExceptionDAL() { }
        public ExceptionDAL(string message) : base(message) { }
        public ExceptionDAL(string message, System.Exception innerException) : base(message, innerException) { }
    }
    #endregion

    #region PPDataContext
    public partial class PPDataContext
    {
        #region Data members
        protected static readonly ILog _log = LogManager.GetLogger(typeof(PPDataContext));
        private string _repositoryPath;
        #endregion

        #region Constructor
        public PPDataContext()
            : base(new System.Data.SQLite.SQLiteConnection("DbLinqProvider=Sqlite;Data Source=" + Pic.DAL.ApplicationConfiguration.CustomSection.DatabasePath + ";FailIfMissing=false;"))
        {
            _repositoryPath = Pic.DAL.ApplicationConfiguration.CustomSection.RepositoryPath;
            this.OnCreated();
        }
        public PPDataContext(string databaseFile, string repositoryPath)
            : base(new System.Data.SQLite.SQLiteConnection("DbLinqProvider=Sqlite;Data Source=" + databaseFile + ";FailIfMissing=false;"))
        {
            _repositoryPath = repositoryPath;
            this.OnCreated();
        }
        internal PPDataContext(DBDescriptor dbDesc)
            : base(new System.Data.SQLite.SQLiteConnection("DbLinqProvider=Sqlite;Data Source=" + dbDesc.DBFilePath + ";FailIfMissing=false;"))
        {
            _repositoryPath = dbDesc.RepositoryPath;
        }
        #endregion

        #region Public properties
        public string RepositoryPath
        {
            get { return _repositoryPath; }
        }
        #endregion
    }
    #endregion

    #region CardboardProfile
    public partial class CardboardProfile
    {
        public static CardboardProfile[] GetAll(PPDataContext db)
        {
            IEnumerable<CardboardProfile> profiles = from cp in db.CardboardProfiles select cp;
            return profiles.ToArray();
        }

        public static CardboardProfile CreateNew(PPDataContext db, string name, string code, float thickness)
        {
            // is already existing ?
            if (db.CardboardProfiles.Count(cp => cp.Name.ToLower() == name.ToLower()) > 0)
                throw new ExceptionDAL(string.Format("CardboardProfile with name = {0} already exists", name));
            if (db.CardboardProfiles.Count(cp => cp.Code.ToLower() == code.ToLower()) > 0)
                throw new ExceptionDAL(string.Format("CardboardProfile with code = {0} already exists", code));
            // build new cardboard profile
            CardboardProfile profile = new CardboardProfile();
            profile.Name = name;
            profile.Code = code;
            profile.Thickness = thickness;
            // insert
            db.CardboardProfiles.InsertOnSubmit(profile);
            db.SubmitChanges();
            // retrieve new cardboard profile
            return db.CardboardProfiles.Single(cp => cp.Code == code);
        }

        public bool HasMajorationSets
        {
            get { return MajorationSets.Count > 0; }
        }

        public static bool HasByName(PPDataContext db, string name)
        {
            return db.CardboardProfiles.Count(cbp => cbp.Name.ToLower() == name.ToLower()) > 0;
        }

        public static CardboardProfile GetByName(PPDataContext db, string name)
        {
            return db.CardboardProfiles.SingleOrDefault(cbp => cbp.Name.ToLower() == name.ToLower());
        }

        public void Delete(PPDataContext db)
        {
            // check that this profile is not used in majoration sets that the only 
            // check that no component has a single majoration set that depends on this
            // delete majoration sets
            foreach (MajorationSet majoSet in this.MajorationSets)
                majoSet.Delete(db);
            // delete profile itself
            db.CardboardProfiles.DeleteOnSubmit(this);
            db.SubmitChanges();
        }

        public static void DeleteAll(PPDataContext db)
        {
            foreach (CardboardProfile cbp in db.CardboardProfiles)
                cbp.Delete(db);
        }

        public void Update(PPDataContext db)
        {
            // checking validity of changes
            if (db.CardboardProfiles.Count(cbp => (cbp.Name.ToLower() == Name.ToLower()) && (cbp.ID != ID)) > 0)
                throw new ExceptionDAL("Name {0} already used by an other profile");
            if (db.CardboardProfiles.Count(cbp => (cbp.Code.ToLower() == Code.ToLower()) && (cbp.ID != ID)) > 0)
                throw new ExceptionDAL("Code {1} already used by an other profile");
            if (Name.Length < 1)
                throw new ExceptionDAL("Name is empty");
            if (Code.Length < 1)
                throw new ExceptionDAL("Code is empty");
            if (Thickness <= 0.0)
                throw new ExceptionDAL(string.Format("Profile {0}({1}) has an invalid thickness ({2})", Name, Code, Thickness));
            // applying changes
            db.SubmitChanges();
        }

        public override string ToString()
        {
            return string.Format("Name={0} Code={1} Thickness={2}", Name, Code, Thickness);
        }
    }
    #endregion

    #region Cardboard format
    public partial class CardboardFormat
    {
        public static CardboardFormat CreateNew(PPDataContext db, string name, string description, float length, float width)
        {
            // is already existing ?
            if (db.CardboardFormats.Count(cf => cf.Name.ToLower() == name.ToLower()) > 0)
                throw new ExceptionDAL(string.Format("CardboardFormat with name = {0} already exists", name));
            // build new cardboard format
            CardboardFormat cardboardFormat = new CardboardFormat();
            cardboardFormat.Name = name;
            cardboardFormat.Description = description;
            cardboardFormat.Length = length;
            cardboardFormat.Width = width;
            db.CardboardFormats.InsertOnSubmit(cardboardFormat);
            db.SubmitChanges();
            return cardboardFormat;
        }

        public static CardboardFormat[] GetAll(PPDataContext db)
        {
            return (from cbf in db.CardboardFormats select cbf).ToArray();
        }

        public static bool HasByName(PPDataContext db, string name)
        {
            return db.CardboardFormats.Count(cbf => cbf.Name.ToLower() == name.ToLower()) > 0;
        }

        public static CardboardFormat GetByName(PPDataContext db, string name)
        {
            return db.CardboardFormats.Single(cbf => cbf.Name.ToLower() == name.ToLower());
        }

        public static CardboardFormat GetByID(PPDataContext db, int id)
        {
            return db.CardboardFormats.Single(cbf => cbf.ID == id);
        }

        public void Delete(PPDataContext db)
        {
            db.CardboardFormats.DeleteOnSubmit(this);
            db.SubmitChanges();
        }

        public static void DeleteAll(PPDataContext db)
        {
            db.CardboardFormats.DeleteAllOnSubmit(from cbf in db.CardboardFormats select cbf);
            db.SubmitChanges();
        }

        public override string ToString()
        {
            return string.Format("Name={0} Length={1} Width={2}", Name, Length, Width);
        }
    }
    #endregion

    #region MajorationSet
    public partial class MajorationSet
    {
        Dictionary<string, double> Dictionnary
        {
            get
            {
                Dictionary<string, double> dict = new Dictionary<string, double>();
                foreach (Majoration majo in this.Majorations)
                    dict.Add(majo.Name, (double)majo.Value);
                return dict;
            }
        }

        public void Delete(PPDataContext db)
        {
            db.Majoration.DeleteAllOnSubmit(db.Majoration.Where(m => m.MajorationSetID == this.ID));
            db.MajorationSets.DeleteOnSubmit(db.MajorationSets.Single(mjs => mjs.ID == ID));
            db.SubmitChanges();
        }
    }
    #endregion

    #region DocumentType
    public partial class DocumentType
    {
        public static DocumentType CreateNew(PPDataContext db, string name, string description, string application)
        {
            // already exists ?
            if (db.DocumentTypes.Count(dtype => dtype.Name.ToLower() == name.ToLower()) > 0)
                throw new ExceptionDAL(string.Format("Document type with Name = {0} already exists", name));
            // create new document type
            DocumentType docType = new DocumentType();
            docType.Name = name;
            docType.Description = description;
            docType.Application = application;
            db.DocumentTypes.InsertOnSubmit(docType);
            db.SubmitChanges();
            // retrieve newly created document type and return
            return db.DocumentTypes.Single(dtype => dtype.Name == name);
        }

        public static bool HasByName(PPDataContext db, string name)
        {
            // retrieve document type by type
            return db.DocumentTypes.Count(dtype => dtype.Name.ToLower() == name.ToLower()) > 0;
        }

        public static DocumentType GetByName(PPDataContext db, string name)
        {
            // there must be some document types
            if (db.DocumentTypes.Count() == 0) return null;
            // retrieve document type by type
            try { return db.DocumentTypes.Single(dtype => dtype.Name.ToLower() == name.ToLower()); }
            catch (Exception /*ex*/) { return null; }
        }

        public override string ToString()
        {
            return string.Format("Name={0} Description={1} Application={2}", Name, Description, Application);
        }

        public void Delete(PPDataContext db)
        {
            // delete document of this type
            var docQuery = from doc in db.Documents
                           where (doc.DocumentType.ID == this.ID)
                           select doc;
            foreach (Document doc in docQuery)
                doc.Delete(db);
            // delete document type itself
            db.DocumentTypes.DeleteOnSubmit(this);
            db.SubmitChanges();
        }

        public static void DeleteAll(PPDataContext db)
        {
            foreach (DocumentType docType in db.DocumentTypes)
                docType.Delete(db);
        }
    }
    #endregion

    #region File
    public partial class File
    {
        #region Create, copy and delete
        public static File CreateNew(PPDataContext db, string filePath)
        {
            return CreateNew(db, filePath, db.RepositoryPath);
        }

        public static File Copy(PPDataContext dbFrom, string fileDirFrom, File fFrom, PPDataContext dbTo, string fileDirTo)
        {
            return CreateNew(dbTo, fFrom.Path(dbFrom), fileDirTo);
        }

        public static File CreateNew(PPDataContext db, string filePath, string fileDirectory)
        {
            // check if file is actually existing
            if (!System.IO.File.Exists(filePath))
                throw new ExceptionDAL(string.Format("File {0} can not be found.", filePath));
            // get file extension
            string fileExt = System.IO.Path.GetExtension(filePath);
            // copy to repository
            System.Guid guid = System.Guid.NewGuid();
            string repPath = System.IO.Path.Combine(fileDirectory, guid.ToString().Replace('-', '_'));
            repPath = System.IO.Path.ChangeExtension(repPath, fileExt);
            System.IO.File.Copy(filePath, repPath);
            // create File object
            File file = new File();
            file.Extension = fileExt;
            file.Guid = guid;
            file.DateCreated = System.DateTime.Now;
            // insert file
            db.Files.InsertOnSubmit(file);
            db.SubmitChanges();
            // retrieve newly created file
            return db.Files.Single(f => f.Guid == guid);
        }

        internal void Delete(PPDataContext db)
        {
            // delete actual file
            System.IO.File.Delete(Path(db));
            // delete record
            db.Files.DeleteOnSubmit(this);
            db.SubmitChanges();
        }
        #endregion

        #region Additionnal properties and methods
        public static string RepositoryPath
        { get { return ApplicationConfiguration.CustomSection.RepositoryPath; } }

        public string Path(PPDataContext db)
        {
            return GuidToPath(db, this.Guid, this.Extension);
        }

        public static string GuidToPath(PPDataContext db, Guid fileGuid, string extension)
        {
            string filePath = System.IO.Path.Combine(db.RepositoryPath, fileGuid.ToString().Replace('-', '_'));
            return System.IO.Path.ChangeExtension(filePath, extension);
        }

        public string PathWRepo(string repositoryPath)
        {
            string filePath = System.IO.Path.Combine(repositoryPath, Guid.ToString().Replace('-', '_'));
            filePath = System.IO.Path.ChangeExtension(filePath, Extension);
            return filePath;
        }
        #endregion
    }
    #endregion

    #region Thumbnail
    public partial class Thumbnail
    {
        #region Enums
        public enum AnnotateMode
        {
            ANNOTATE_NONE,
            ANNOTATE_WITHBACKGOUND,
            ANNOTATE_NOBACKGROUND
        }
        #endregion

        #region Create and delete
        public static Thumbnail CreateNew(PPDataContext db, string imageFilePath)
        {
            // build new thumbnail object
            Thumbnail thumbnail = new Thumbnail();
            thumbnail.File = File.CreateNew(db, imageFilePath);
            thumbnail.MimeType = GetMimeType(IMAGEFORMAT);
            thumbnail.Width = THUMBNAILSIZE;
            thumbnail.Height = THUMBNAILSIZE;
            thumbnail.ThumbCache = BmpToBytes(Image.FromFile(imageFilePath), ImageFormat.Bmp).ToArray();
            // insertion
            db.Thumbnail.InsertOnSubmit(thumbnail);
            db.SubmitChanges();
            return thumbnail;
        }

        public static Thumbnail GetById(PPDataContext db, int id)
        {
            Thumbnail tb = db.Thumbnail.SingleOrDefault(thumb => thumb.ID == id);
            if (null == tb) throw new ExceptionDAL(string.Format("Failed to load Thumbnail with ID = {0}", id));
            return tb;
        }

        internal void Delete(PPDataContext db)
        {
            // save file ID
            int fileID = this.FileID;

            // delete thumbnail record
            db.Thumbnail.DeleteOnSubmit(this);
            db.SubmitChanges();
            // delete attached file
            File file = db.Files.Single(f => f.ID == fileID);
            file.Delete(db);
        }
        #endregion

        #region Image (Thumbnail) helpers
        /// <summary>
        /// get the thumbnail as stored in the database
        /// </summary>
        /// <returns></returns>
        public Image GetImage()
        {
            return Image.FromStream(new System.IO.MemoryStream(ThumbCache.ToArray()));
        }

        public static void Annotate(Image image, string annotation)
        {
            if (_annotateMode == AnnotateMode.ANNOTATE_NONE) return;

            // graphics
            Graphics grph = Graphics.FromImage(image);
            Font tfont = new Font("Arial", _fontSize);
            Color brushColor = (_annotateMode == AnnotateMode.ANNOTATE_NOBACKGROUND) ? Color.Black : _fontColor;
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Far;
            sf.LineAlignment = StringAlignment.Far;
            Size txtSize = grph.MeasureString(annotation, tfont).ToSize();
            if (_annotateMode == AnnotateMode.ANNOTATE_WITHBACKGOUND)
                grph.FillRectangle(new SolidBrush(_backgroundColor), new Rectangle(image.Width - txtSize.Width - 2, image.Height - txtSize.Height - 2, txtSize.Width + 2, txtSize.Height + 2));
            grph.DrawString(annotation, tfont, new SolidBrush(brushColor), new PointF(image.Width - 2, image.Height - 2), sf);
        }

        /// <summary>
        /// returns a byte[] array which represents the input Image
        /// </summary>
        /// <param name="bmp">The source image to return as byte[] array</param>
        /// <returns>byte[] array which represents the input Image</returns>
        private static System.Data.Linq.Binary BmpToBytes(Image img, ImageFormat imageFormat)
        {
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            // Save to memory using the Jpeg format
            img.Save(ms, imageFormat);
            // read to end
            byte[] bmpBytes = ms.GetBuffer();
            return new System.Data.Linq.Binary(bmpBytes);
        }

        /// <summary>
        /// get the Image Format based on the MimeType
        /// </summary>
        /// <param name="mimeType">Mime type</param>
        /// <returns></returns>
        private static ImageFormat GetFormat(string mimeType)
        {
            ImageFormat result = ImageFormat.Jpeg;
            switch (mimeType)
            {
                case "image/gif":
                    result = ImageFormat.Gif;
                    break;
                case "image/png":
                    result = ImageFormat.Png;
                    break;
                case "image/bmp":
                    result = ImageFormat.Bmp;
                    break;
                case "image/tiff":
                    result = ImageFormat.Tiff;
                    break;
                case "image/x-icon":
                    result = ImageFormat.Icon;
                    break;
                default:
                    result = ImageFormat.Jpeg;
                    break;
            }
            return result;
        }
        /// <summary>
        /// get the MimeType based on the image format
        /// </summary>
        /// <param name="imageFormat">Image format</param>
        /// <returns></returns>
        private static string GetMimeType(ImageFormat imageFormat)
        {
            if (imageFormat == ImageFormat.Gif)
                return "image/gif";
            else if (imageFormat == ImageFormat.Png)
                return "image/png";
            else if (imageFormat == ImageFormat.Bmp)
                return "image/bmp";
            else if (imageFormat == ImageFormat.Tiff)
                return "image/tiff";
            else if (imageFormat == ImageFormat.Icon)
                return "image/x-icon";
            else if (imageFormat == ImageFormat.Jpeg)
                return "image/Jpeg";
            else
                return string.Empty;
        }
        #endregion

        #region Static properties
        public static AnnotateMode AnnotationMode
        {
            get { return _annotateMode; }
            set { _annotateMode = value; }
        }
        public static int FontSize
        {
            get { return _fontSize; }
            set { _fontSize = value; }
        }
        #endregion

        #region Data members
        protected static readonly int THUMBNAILSIZE = 150;
        protected static readonly ImageFormat IMAGEFORMAT = ImageFormat.Jpeg;

        protected static AnnotateMode _annotateMode = AnnotateMode.ANNOTATE_NONE;
        protected static int _fontSize = 10;
        protected static Color _fontColor = Color.White;
        protected static readonly Color _backgroundColor = Color.Black;
        #endregion
    }
    #endregion

    #region Document
    public partial class Document
    {
        #region Create, copy and delete
        internal static Document CreateNew(PPDataContext db, string filePath, string name, string description, string docTypeName, Guid guid)
        {
            return Document.CreateNew(db, db.RepositoryPath, filePath, name, description, docTypeName, guid);
        }

        internal static Document Copy(PPDataContext dbFrom, string fileDirFrom, Document docFrom, PPDataContext dbTo, string fileDirTo)
        {
            return Document.CreateNew(dbTo, docFrom.File.Path(dbFrom), docFrom.Name, docFrom.Description, docFrom.DocumentType.Name, docFrom.Guid);
        }

        internal static Document CreateNew(PPDataContext db, string fileDirectory, string filePath, string name, string description, string docTypeName, Guid guid)
        {
            // check unicity
            if (db.Documents.Count(d => d.Name.ToLower() == name.ToLower() && d.Description.ToLower() == description.ToLower()) > 0)
                throw new ExceptionDAL(string.Format("Document with Name = {0} and Description = {1} already exists", name, description));
            // check that document type exist
            DocumentType docType;
            try { docType = db.DocumentTypes.Single(dt => dt.Name.ToLower() == docTypeName.ToLower()); }
            catch (Exception /*ex*/) { throw new ExceptionDAL(string.Format("There is no document type with name {0}", docTypeName)); }
            // create new file
            File file = File.CreateNew(db, filePath, fileDirectory);
            // build document
            Document doc = new Document();
            doc.FileID = file.ID;
            doc.Name = name;
            doc.Description = description;
            doc.DocumentType = docType;
            doc.Guid = guid.Equals(Guid.Empty) ? Guid.NewGuid() : guid;
            // insert document
            db.Documents.InsertOnSubmit(doc);
            db.SubmitChanges();
            return db.Documents.Single(d => d.Name == name && d.Description == description);
        }

        public void Delete(PPDataContext db)
        {
            // delete TreeNodeDocument
            var tnDocs = db.TreeNodeDocuments.Where(tnd => tnd.DocumentID == this.ID);
            foreach (TreeNodeDocument tnDoc in tnDocs)
            {
                // TreeNodeDocument
                db.TreeNodeDocuments.DeleteOnSubmit(tnDoc);
                // TreeNode
                db.TreeNodes.DeleteOnSubmit(db.TreeNodes.Single(tn => tn.ID == tnDoc.TreeNodeID));
                // Thumbnail
                db.Thumbnail.DeleteOnSubmit(db.Thumbnail.Single(tb => tb.ID == tnDoc.TreeNode.ThumbnailID));
                // File
                File fileTDB = db.Files.Single(f => f.ID == tnDoc.TreeNode.Thumbnail.FileID);
                db.Files.DeleteOnSubmit(fileTDB);
                System.IO.File.Delete(fileTDB.Path(db));
            }
            // delete Component part
            Component component = db.Components.SingleOrDefault(cp => cp.DocumentID == this.ID);
            if (null != component)
                component.Delete(db, false);
            // delete Document
            Document doc = db.Documents.SingleOrDefault(d => d.ID == this.ID);
            db.Documents.DeleteOnSubmit(doc);
            // delete File
            Pic.DAL.SQLite.File fileDB = db.Files.SingleOrDefault(f => f.ID == this.File.ID);
            db.Files.DeleteOnSubmit(fileDB);
            try { System.IO.File.Delete(fileDB.Path(db)); }
            catch (Exception /*ex*/) { }
            // ---> commit changes
            db.SubmitChanges();
        }
        #endregion
        public static Document GetById(PPDataContext db, int id)
        {
            return db.Documents.SingleOrDefault(d => d.ID == id);
        }

        public Component GetComponent(PPDataContext db)
        {
            return db.Components.SingleOrDefault(comp => comp.DocumentID == this.ID);
        }

        public static List<Guid> GetGuids(PPDataContext db)
        {
            List<Guid> guids = new List<Guid>();
            foreach (Document doc in db.Documents)
                guids.Add(doc.Guid);
            return guids;
        }

        public static void DeleteAll(PPDataContext db)
        {
            while (db.Documents.Count() > 0)
                db.Documents.First().Delete(db);
        }
    }
    #endregion

    #region TreeNode
    public partial class TreeNode
    {
        public static TreeNode CreateNew(PPDataContext db, string name, string description, string imageFilePath)
        {
            // check file path
            if (string.IsNullOrEmpty(imageFilePath))
                imageFilePath = System.IO.Path.Combine(Pic.DAL.ApplicationConfiguration.CustomSection.ThumbnailsPath, "Folder.bmp");
            // root node already exist
            if (db.TreeNodes.Count(tn => (null == tn.ParentNodeID) && (name == tn.Name)) > 0)
                throw new ExceptionDAL(string.Format("A root node with Name = {0} already exists", name));
            // create thumbnail
            Thumbnail thumbnail = Thumbnail.CreateNew(db, imageFilePath);
            // build new treenode
            TreeNode treeNode = new TreeNode();
            treeNode.Name = name;
            treeNode.Description = description;
            treeNode.Thumbnail = thumbnail;
            // insert TreeNode
            db.TreeNodes.InsertOnSubmit(treeNode);
            db.SubmitChanges();
            // retrieve newly created thumbnail
            return treeNode;
        }

        public TreeNode CreateChild(PPDataContext db, string name, string description, string imageFilePath)
        {
            // check file path
            if (string.IsNullOrEmpty(imageFilePath))
                imageFilePath = System.IO.Path.Combine(Pic.DAL.ApplicationConfiguration.CustomSection.ThumbnailsPath, "Folder.bmp");
            // parent node already has child node with this name ?
            if (TreeNodes.Count(tn => (tn.Name.ToLower() == name.ToLower()) && tn.ParentNodeID == ID) > 0)
                throw new ExceptionDAL(string.Format("Node {0} already has child node with Name = {1}", this.Name, name));
            // create thumbnail
            Thumbnail thumbnail = Thumbnail.CreateNew(db, imageFilePath);
            // build new treenode
            TreeNode treeNode = new TreeNode();
            treeNode.Name = name;
            treeNode.Description = description;
            treeNode.Thumbnail = thumbnail;
            treeNode.ParentNodeID = ID;
            // insert TreeNode
            db.TreeNodes.InsertOnSubmit(treeNode);
            db.SubmitChanges();
            // retrieve new ly created thumbnail
            return treeNode;
        }

        public bool HasChild(PPDataContext db, string name)
        {
            return (TreeNodes.Count(tn => tn.Name.ToLower() == name.ToLower() && tn.ParentNodeID == ID) > 0);
        }

        public TreeNode GetChild(PPDataContext db, string name)
        {
            TreeNode childNode = db.TreeNodes.SingleOrDefault(tn => (tn.Name.ToLower() == name.ToLower()) && (tn.ParentNodeID == ID));
            if (null == childNode) throw new ExceptionDAL(string.Format("Failed to load child node with name = {0}", name));
            return childNode;
        }

        public void Move(PPDataContext db, TreeNode newParent)
        {
            if (null != newParent)
                ParentNodeID = newParent.ID;
            else
                ParentNodeID = null;
            db.SubmitChanges();
        }

        public static TreeNode GetById(PPDataContext db, int id)
        {
            TreeNode treeNode = db.TreeNodes.SingleOrDefault(tn => tn.ID == id);
            if (null == treeNode) throw new ExceptionDAL(string.Format("Failed to load TreeNode with ID = {0}", id));
            return treeNode;
        }

        public static List<TreeNode> GetByDocumentId(PPDataContext db, int documentId)
        {
            var treeNodeDocuments = db.TreeNodeDocuments.Where(tnd => tnd.DocumentID == documentId);
            List<TreeNode> treeNodes = new List<TreeNode>();
            foreach (TreeNodeDocument tnd in treeNodeDocuments)
                treeNodes.Add(tnd.TreeNode);
            return treeNodes;
        }

        public static List<TreeNode> GetRootNodes(PPDataContext db)
        {
            // retrieve rootnodes
            var nodes = db.TreeNodes.Where(tn => null == tn.ParentNodeID);
            // save in List
            List<TreeNode> listTreeNodes = new List<TreeNode>();
            foreach (TreeNode treeNode in nodes)
                listTreeNodes.Add(treeNode);
            listTreeNodes.Sort(new ComparerTreeNode());
            // if empty list, insert Root treeNode
            if (0 == listTreeNodes.Count)
            {
                TreeNode.CreateNew(db, "Root", "Root node", string.Empty);
                listTreeNodes = GetRootNodes(db);
            }
            return listTreeNodes;
        }
        /// <summary>
        /// Recursively accessing a tree node from its "path" (parent)
        /// </summary>
        /// <param name="db">Data context</param>
        /// <param name="parentNode">Parent node from which to search</param>
        /// <param name="treeNodePath">List of successive parent of searched node</param>
        /// <param name="index">Index in path</param>
        /// <returns>TreeNode</returns>
        public static TreeNode GetNodeByPath(PPDataContext db, TreeNode parentNode, List<string> treeNodePath, int index)
        {
            // child nodes of parent node
            List<TreeNode> childNodes = null;
            if (null == parentNode)
                childNodes = TreeNode.GetRootNodes(db);
            else
                childNodes = parentNode.Childrens(db);

            if (treeNodePath.Count == 0)
                return null;
            // find path name among given children
            TreeNode nodeResult = childNodes.Find(
                delegate(TreeNode node) { return node.Name == treeNodePath[index]; }
            );
            if (null == nodeResult)
                return null; // not found (invalid path)
            else if (index == treeNodePath.Count - 1)
                return nodeResult; // 
            else return TreeNode.GetNodeByPath(db, nodeResult, treeNodePath, index + 1);
        }
        /// <summary>
        /// retrieve parent node
        /// </summary>
        /// <param name="db">Data constext</param>
        /// <returns>parent TreeNode</returns>
        public TreeNode GetParent(PPDataContext db)
        {
            if (null == ParentNodeID)
                return null;
            else
                return TreeNode.GetById(db, this.ParentNodeID.Value);
        }

        public string GetPath(PPDataContext db)
        {
            TreeNode parentNode = GetParent(db);
            string path = string.Empty;
            if (null != parentNode)
                path = parentNode.GetPath(db);
            return path + "\\" + Name;
        }
        public List<string> GetTreeNodePath(PPDataContext db)
        {
            List<string> treeNodePath;
            TreeNode parentNode = GetParent(db);
            if (null != parentNode)
                treeNodePath = parentNode.GetTreeNodePath(db);
            else
                treeNodePath = new List<string>();
            treeNodePath.Add(Name);
            return treeNodePath;
        }

        public bool IsDocument
        {
            get { return (TreeNodeDocuments.Count > 0); }
        }

        public bool IsComponent
        {
            get
            {
                if (0 == TreeNodeDocuments.Count)
                    return false; // -> is branch node
                return string.Equals(TreeNodeDocuments[0].Document.DocumentType.Name, "Parametric component");
            }
        }

        public bool IsDescendantOf(PPDataContext db, TreeNode tn)
        {
            if (tn.ID == ID)
                return true;
            else if (ParentNodeID == null)
                return false;
            else
                return GetParent(db).IsDescendantOf(db, tn);
        }

        public List<TreeNode> Childrens(PPDataContext db)
        {
            // get list of child nodes
            var childNodes = db.TreeNodes.Where(tn => tn.ParentNodeID == this.ID);
            // build list of child nodes
            List<TreeNode> childrens = new List<TreeNode>();
            foreach (TreeNode treeNode in childNodes)
                childrens.Add(treeNode);
            childrens.Sort(new ComparerTreeNode());
            return childrens;
        }

        public bool AllowChildCreation(PPDataContext db, string name)
        {
            return 0 == db.TreeNodes.Count(tn => tn.Name.ToLower() == name.ToLower());
        }

        public List<Document> Documents(PPDataContext db)
        {
            // get list of documents
            List<Document> documents = new List<Document>();
            var treeNodeDocuments = TreeNodeDocuments;
            foreach (TreeNodeDocument treeNodeDoc in TreeNodeDocuments)
                documents.Add(treeNodeDoc.Document);
            return documents;
        }

        public Document InsertDocument(PPDataContext db, string filePath, string name, string description, string docTypeName, string thumbnailPath)
        {
            // check if this TreeNode already has documents with this same name
            if (db.TreeNodeDocuments.Count(tnd => tnd.Document.Name.ToLower() == name) > 0)
                throw new ExceptionDAL(string.Format("TreeNode {0} already has a document with name = {1}", this.Name, name));
            // get list of child nodes
            Document doc = Document.CreateNew(db, filePath, name, description, docTypeName, Guid.Empty);
            // create new TreeNode
            TreeNode tn = CreateChild(db, name, description, thumbnailPath);
            // insertion of document under treenode
            TreeNodeDocument treeNodeDoc = new TreeNodeDocument();
            treeNodeDoc.Document = doc;
            treeNodeDoc.TreeNode = tn;
            db.TreeNodeDocuments.InsertOnSubmit(treeNodeDoc);
            db.SubmitChanges();
            return doc;
        }

        public Component InsertComponent(PPDataContext db, string filePath, Guid guid, string name, string description, string thumbnailPath)
        {
            // check if this TreeNode already has documents with this same name
            if (db.TreeNodeDocuments.Count(tnd => tnd.Document.Name.ToLower() == name) > 0)
                throw new ExceptionDAL(string.Format("TreeNode {0} already has a document with name = {1}", this.Name, name));
            // get list of child nodes
            Document doc = Document.CreateNew(db, filePath, name, description, "Parametric component", Guid.Empty);
            // create new TreeNode
            TreeNode tn = CreateChild(db, name, description, thumbnailPath);
            // insertion of document under treenode
            TreeNodeDocument treeNodeDoc = new TreeNodeDocument();
            treeNodeDoc.Document = doc;
            treeNodeDoc.TreeNode = tn;
            db.TreeNodeDocuments.InsertOnSubmit(treeNodeDoc);
            // create component
            Component component = new Component();
            component.DocumentID = doc.ID;
            component.Guid = guid;
            db.Components.InsertOnSubmit(component);
            db.SubmitChanges();
            return db.Components.Single(c => c.Guid == guid);
        }

        public void Delete(PPDataContext db, bool deleteDocument, IProcessingCallback callback)
        {
            // delete childrens
            foreach (TreeNode childNode in this.Childrens(db))
                childNode.Delete(db, deleteDocument, callback);

            // delete child documents
            if (deleteDocument && this.IsDocument)
            {
                foreach (TreeNodeDocument tnDoc in TreeNodeDocuments)
                {
                    Document doc = tnDoc.Document;
                    if (null != callback)
                        callback.Info(string.Format("Deleting document {0}...", tnDoc.TreeNode.Name));
                    doc.Delete(db);
                }
            }
            else
            {
                // delete link with document
                db.TreeNodeDocuments.DeleteAllOnSubmit(this.TreeNodeDocuments);
                // delete record itself
                if (null != callback)
                    callback.Info(string.Format("Deleting node {0}...", Name));
                db.TreeNodes.DeleteOnSubmit(this);
                db.SubmitChanges();

                // remove thumbnail
                Thumbnail thumbnail = this.Thumbnail;
                thumbnail.Delete(db);
            }
        }

        public static void DeleteAll(PPDataContext db)
        {
            List<TreeNode> treeNodes = TreeNode.GetRootNodes(db);
            foreach (TreeNode treeNode in treeNodes)
                treeNode.Delete(db, true, null);
        }

        public static void ReplaceThumbnail(int nodeID, string imageFilePath)
        {
            int thumbnailToRemoveId = 0, newThumbnailId = 0;
            {
                PPDataContext db = new PPDataContext();
                TreeNode tn = TreeNode.GetById(db, nodeID);
                thumbnailToRemoveId = tn.ThumbnailID;
                newThumbnailId = Thumbnail.CreateNew(db, imageFilePath).ID;
            }
            {
                PPDataContext db = new PPDataContext();
                TreeNode tn = TreeNode.GetById(db, nodeID);
                tn.ThumbnailID = newThumbnailId;
                db.SubmitChanges();
            }
            {
                PPDataContext db = new PPDataContext();
                Thumbnail thumb = Thumbnail.GetById(db, thumbnailToRemoveId);
                db.Thumbnail.DeleteOnSubmit(thumb);
                db.SubmitChanges();
            }
        }
    }
    #endregion

    #region ComparerTreeNode
    public class ComparerTreeNode
        : IComparer<TreeNode>
    {
        #region IComparer<TreeNode> Members
        /// <summary>
        /// Compares two <see cref="TreeNode"/> solutions.
        /// </summary>
        /// <param name="treeNode1">First instance of <see cref="TreeNode"/>.</param>
        /// <param name="treeNode2">First instance of <see cref="TreeNode"/>.</param>
        /// <returns></returns>
        public int Compare(TreeNode treeNode1, TreeNode treeNode2)
        {
            return string.Compare(treeNode1.Name, treeNode2.Name);
        }
        #endregion
    }
    #endregion

    #region Component
    public partial class Component
    {
        #region Enums
        public enum MajoRounding
        { 
            ROUDING_FIRSTDECIMALNEAREST
            , ROUNDING_HALFNEAREST
            , ROUNDING_HALFTOP
            , ROUDING_INT
        }
        #endregion

        public static Component GetById(PPDataContext db, int id)
        {
            return db.Components.SingleOrDefault(c => c.ID == id);
        }

        public static Component GetByGuid(PPDataContext db, Guid guid)
        {
            return db.Components.SingleOrDefault(c => c.Guid == guid);
        }

        public static Component GetByDocumentID(PPDataContext db, int documentId)
        {
            return db.Components.SingleOrDefault(c => c.DocumentID == documentId);
        }

        public static List<Component> GetAllWithDefaultParameters(PPDataContext db)
        {
            List<Component> list = new List<Component>();
            var components = db.Components.Where(comp => comp.ParamDefaultValues.Count > 0);
            foreach (Component comp in components)
                list.Add(comp);
            return list;
        }

        public MajorationSet GetMajorationSetByProfile(PPDataContext db, CardboardProfile profile)
        {
            return db.MajorationSets.SingleOrDefault(mjs => mjs.CardboardProfileID == profile.ID && mjs.ComponentID == this.ID);
        }

        public static Dictionary<string, double> GetDefaultMajorations(PPDataContext db, int componentId, CardboardProfile profile, MajoRounding rounding)
        {
            if (null == profile)
                throw new ExceptionDAL("Profile is null reference!");
            // retrieve component from ID
            Component comp = db.Components.Single(c => c.ID == componentId);
            // find nearest set
            MajorationSet nearestSet = null;
            foreach (MajorationSet majoSet in comp.MajorationSets)
            {
                if (null == nearestSet
                    || (Math.Abs(majoSet.CardboardProfile.Thickness - profile.Thickness) < Math.Abs(nearestSet.CardboardProfile.Thickness - profile.Thickness))
                    )
                    nearestSet = majoSet;
            }

            // build dictionnary
            Dictionary<string, double> dict = new Dictionary<string, double>();
            if (null != nearestSet)
            {
                double coef = (double)(profile.Thickness / nearestSet.CardboardProfile.Thickness);
                dict.Add("th1", profile.Thickness);
                dict.Add("ep1", profile.Thickness);
                foreach (Majoration maj in nearestSet.Majorations)
                {
                    double valueMaj = maj.Value * coef;
                    if (Math.Abs(coef - 1.0) > 1.0e-3)
                    {
                        switch (rounding)
                        {
                            case MajoRounding.ROUDING_FIRSTDECIMALNEAREST:
                                valueMaj = Math.Round(valueMaj * 10) / 10.0;
                                break;
                            case MajoRounding.ROUNDING_HALFNEAREST:
                                valueMaj = Math.Round(valueMaj * 2) / 2.0;
                                break;
                            case MajoRounding.ROUNDING_HALFTOP:
                                valueMaj = Math.Ceiling(valueMaj * 2) / 2;
                                break;
                            case MajoRounding.ROUDING_INT:
                                valueMaj = Math.Round(valueMaj);
                                break;
                            default:
                                break; // no rounding
                        }
                    }
                    dict.Add(maj.Name, valueMaj);
                }
            }
            return dict;
        }

        public Dictionary<string, Dictionary<string, double>> GetAllMajorationSets(PPDataContext db)
        {
            Dictionary<string, Dictionary<string, double>> majorationSets = new Dictionary<string, Dictionary<string, double>>();
            foreach (MajorationSet majoSet in this.MajorationSets)
            {
                Dictionary<string, double> majoDict = new Dictionary<string, double>();
                foreach (Majoration majo in majoSet.Majorations)
                    majoDict.Add(majo.Name, majo.Value);
                majorationSets.Add(majoSet.CardboardProfile.Name, majoDict);
            }
            return majorationSets;
        }

        public static Dictionary<string, double> GetParamDefaultValues(PPDataContext db, Guid guid)
        {
            // retrieve component from ID
            Component comp = Component.GetByGuid(db, guid);
            // find parameters
            Dictionary<string, double> defaultParamValues = new Dictionary<string, double>();
            foreach (ParamDefaultValue pdf in comp.ParamDefaultValues)
                defaultParamValues[pdf.Name] = (double)pdf.Value;
            return defaultParamValues;
        }

        public Dictionary<string, double> GetParamDefaultValues()
        {
            Dictionary<string, double> defaultParamValues = new Dictionary<string, double>();
            foreach (ParamDefaultValue pdf in ParamDefaultValues)
                defaultParamValues[pdf.Name] = (double)pdf.Value;
            return defaultParamValues;
        }

        public double GetParamDefaultValueDouble(PPDataContext db, string name)
        {
            ParamDefaultValue paramDefaultValue = db.ParamDefaultValues.SingleOrDefault(pdf => pdf.ComponentID == this.ID && pdf.Name.ToLower() == name.ToLower());
            if (null == paramDefaultValue)
                throw new ExceptionDAL(string.Format("Database : Failed to load parameter {0} for component {1}({2})", name, this.Document.Name, this.Guid.ToString()));
            return paramDefaultValue.Value;
        }

        public void InsertMajorationSets(PPDataContext db, Dictionary<string, Dictionary<string, double>> majoSets)
        {
            foreach (string profileName in majoSets.Keys)
                InsertNewMajorationSet(db, profileName, majoSets[profileName]);            
        }

        public void InsertNewMajorationSet(PPDataContext db, string profileName, Dictionary<string, double> majorations)
        {
            CardboardProfile profile = db.CardboardProfiles.Single(cbp => cbp.Name.ToLower() == profileName.ToLower());
            if (null == profile)
                throw new ExceptionDAL(string.Format("There is no profile with name = {0}", profileName));
            InsertNewMajorationSet(db, profile, majorations);
        }

        public void InsertNewMajorationSet(PPDataContext db, CardboardProfile profile, Dictionary<string, double> majorations)
        {
            MajorationSet majoSetExisting = GetMajorationSetByProfile(db, profile);
            if (null != majoSetExisting)
                throw new ExceptionDAL(string.Format("A majoration set for component {0} and profile {1} already exists", this.Document.Name, profile.Name));
            MajorationSet majoSet = new MajorationSet();
            majoSet.ComponentID = this.ID;
            majoSet.CardboardProfile = profile;
            db.MajorationSets.InsertOnSubmit(majoSet);
            db.SubmitChanges();

            // get majoset
            MajorationSet majoSetNew = db.MajorationSets.SingleOrDefault(mjs => mjs.CardboardProfileID == profile.ID && mjs.ComponentID == this.ID);
            foreach (KeyValuePair<string, double> pair in majorations)
            {
                Majoration maj = new Majoration();
                maj.MajorationSet = majoSetNew;
                maj.Name = pair.Key;
                maj.Value = (float)pair.Value;
                db.Majoration.InsertOnSubmit(maj);
            }
            db.SubmitChanges();
        }

        public void InsertNewParamDefaultValue(PPDataContext db, string name, double value)
        {
            ParamDefaultValue pdf = new ParamDefaultValue();
            pdf.ComponentID = this.ID;
            pdf.Name = name;
            pdf.Value = (float)value;
            db.ParamDefaultValues.InsertOnSubmit(pdf);
            db.SubmitChanges();
        }

        public void InsertNewParamDefaultValues(PPDataContext db, Dictionary<string, double> paramDefaultValue)
        {
            foreach (KeyValuePair<string, double> pair in paramDefaultValue)
            {
                ParamDefaultValue pdf = new ParamDefaultValue();
                pdf.ComponentID = this.ID;
                pdf.Name = pair.Key;
                pdf.Value = (float)pair.Value;
                db.ParamDefaultValues.InsertOnSubmit(pdf);
            }
            db.SubmitChanges();
        }

        public void UpdateDefaultParamValueSet(PPDataContext db, Dictionary<string, double> dictNameValues)
        {
            var paramDefaultValues = db.ParamDefaultValues.Where(pdf => pdf.ComponentID == this.ID);
            // loop through all parameters found in parameter list
            foreach (string name in dictNameValues.Keys)
            {
                bool found = false;
                foreach (ParamDefaultValue p in paramDefaultValues)
                {
                    if (p.Name == name)
                    {
                        p.Value = (float)dictNameValues[p.Name];
                        found = true;
                    }
                }
                if (!found) // if parameter was not found in the database, create it
                {
                    ParamDefaultValue pdf = new ParamDefaultValue();
                    pdf.ComponentID = this.ID;
                    pdf.Name = name;
                    pdf.Value = (float)dictNameValues[name];
                    db.ParamDefaultValues.InsertOnSubmit(pdf);
                }
            }
            db.SubmitChanges();
        }

        public void UpdateMajorationSet(PPDataContext db, CardboardProfile profile, Dictionary<string, double> majorations)
        {
            MajorationSet majoSet = db.MajorationSets.SingleOrDefault(mjs => mjs.CardboardProfileID == profile.ID && mjs.ComponentID == this.ID);
            if (null != majoSet)
            {
                foreach (Majoration maj in majoSet.Majorations)
                    maj.Value = (float)majorations[maj.Name];
                db.SubmitChanges();
            }
            else
                InsertNewMajorationSet(db, profile.Name, majorations);
        }

        public void Delete(PPDataContext db, bool deleteDocument)
        {
            // delete majoration sets
            foreach (MajorationSet mjs in MajorationSets)
                db.Majoration.DeleteAllOnSubmit(db.Majoration.Where(m => m.MajorationSetID == mjs.ID));
            db.MajorationSets.DeleteAllOnSubmit(db.MajorationSets.Where(mjset => mjset.ComponentID == this.ID));
            db.Components.DeleteOnSubmit(db.Components.Single(cp => cp.ID == this.ID));
            db.SubmitChanges();
            // delete document
            if (deleteDocument)
                Document.Delete(db);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(string.Format("Component Guid = {0},  Name = {1}", this.Guid.ToString(), this.Document.Name));
            foreach (MajorationSet majSet in this.MajorationSets)
            {
                sb.AppendLine(string.Format("\tProfile {0}", majSet.CardboardProfile.Name));
                foreach (Majoration majo in majSet.Majorations)
                    sb.AppendLine(string.Format("\t\t{0} = {1}", majo.Name, majo.Value));
            }
            return sb.ToString();
        }

        public static void DeleteAll(PPDataContext db)
        {
            while (db.Components.Count() > 0)
                db.Components.First().Delete(db, true);
        }
    }
    #endregion
}
