#region Using directives
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;

using Plossum.CommandLine;
using ICSharpCode.SharpZipLib.Zip;

#endregion

namespace Pic.Data.DataAppender
{
    #region Command line manager : class Options
    [CommandLineManager(ApplicationName = "DataAppender.exe", Copyright = "Copyright (c) TreeDim")]
    class Options
    {
        [CommandLineOption(Description = "Displays this help text")]
        public bool Help = false;

        [CommandLineOption(Description = "Specifies the component tree file (.zip) to install", MinOccurs = 1)]
        public string i
        {
            get { return zipDataFile; }
            set
            {
                if (String.IsNullOrEmpty(value))
                    throw new InvalidOptionValueException("The data file path must not be empty", false);
                if (!System.IO.File.Exists(value))
                    throw new InvalidOptionValueException(string.Format("The file {0} could not be found.", value), false);
                zipDataFile = value;
            }
        }
        [CommandLineOption(Description = "Clear database before inserting data", MinOccurs = 0, BoolFunction = BoolFunction.TrueIfPresent)]
        public bool c
        {
            get { return clearDatabase; }
            set { clearDatabase = value; }
        }

        private string zipDataFile;
        private bool clearDatabase;
    }
    #endregion
    class Program
    {
        #region Constants
        public static int LINE_WIDTH = 78;
        #endregion

        #region Static data members
        private static string _dstDirectory;
        #endregion

        #region Main
        static int Main(string[] args)
        {
            try
            {
                // --- get zip file path --------------------------------------
                Options options = new Options();
                CommandLineParser parser = new CommandLineParser(options);
                parser.Parse();

                Console.WriteLine(parser.UsageInfo.GetHeaderAsString(LINE_WIDTH));
                if (options.Help)
                {
                    Console.WriteLine(parser.UsageInfo.GetOptionsAsString(LINE_WIDTH));
                    MessageBox.Show()
                    return 0;
                }
                else if (parser.HasErrors)
                {
                    Console.WriteLine(parser.UsageInfo.GetErrorsAsString(LINE_WIDTH));
                    return 0;
                }

                string zipDataFile = options.i;
                Console.WriteLine(string.Format("Processing data file {0}", zipDataFile));

                // --- unzip file content -------------------------------------
                _dstDirectory = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString().Replace('-', '_'));
                Directory.CreateDirectory(_dstDirectory);
                
                FileStream fileStreamIn = new FileStream(zipDataFile, FileMode.Open, FileAccess.Read);
                ZipInputStream zipInStream = new ZipInputStream(fileStreamIn);
                ZipEntry entry;
                while ((entry = zipInStream.GetNextEntry()) != null)
                {
                    Console.WriteLine(string.Format("Extracting {0}", entry.Name));
                    FileStream fileStreamOut = new FileStream(_dstDirectory + @"\" + entry.Name, FileMode.Create, FileAccess.Write);
                    int size = 2048;
                    byte[] buffer = new byte[size];
                    do
                    {
                        size = zipInStream.Read(buffer, 0, buffer.Length);
                        fileStreamOut.Write(buffer, 0, size);
                    } while (size > 0);
                    fileStreamOut.Close();
                }
                zipInStream.Close();
                fileStreamIn.Close();

                // --- find DataTree.xml --------------------------------------
                string dataTreeFileName = "DataTree.xml";
                string xmlFilePath = Path.Combine(_dstDirectory, dataTreeFileName);
                if (System.IO.File.Exists(xmlFilePath))
                {
                    Console.WriteLine(string.Format("Processing file {0}", dataTreeFileName));
                    using (XmlTextReader reader = new XmlTextReader(xmlFilePath))
                    {
                        XmlDocument doc = new XmlDocument();
                        doc.Load(reader);
                        ProcessNode(doc, "", null);
                    }
                }
                else
                    Console.WriteLine("DataTree.xml file does not exist -> Exiting...");
                // --- delete temp files --------------------------------------
                DirectoryInfo dirInfo = new DirectoryInfo(_dstDirectory);
                foreach (FileInfo fileInfo in dirInfo.GetFiles())
                {
                    Console.WriteLine(string.Format("Deleting file {0}", fileInfo.Name));
                    fileInfo.Delete();
                }
                dirInfo.Delete();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception caught:");
                Console.WriteLine(ex.ToString());
            }
            return 0;
        }
        #endregion

        #region XmlNode processing
        static private void ProcessNode(XmlNode node, string offset, DocTreeNodeBranch parentBranch)
        {
            if ("xml" == node.Name)
            {   // parent node -> do nothing
            }
            else if ("Branch" == node.Name)
            {
                // create new branch
                string name = node.Attributes["Name"].Value;
                Console.WriteLine( offset + string.Format("Branch : {0}", name));
                string description = node.Attributes["Description"].Value;
                string bmpFileName = node.Attributes["BmpFileName"].Value;
                string filePath = bmpFileName.Length == 0 ? string.Empty : Path.Combine(_dstDirectory, bmpFileName);
                DocTreeNodeBranch newBranch = null;
                if (null == parentBranch)
                    newBranch = DocTreeNodeBranch.AddNewRootNode(name, description, filePath);
                else
                    newBranch = parentBranch.AddChildBranch(name, description, filePath);
                // recursively create child nodes
                foreach (XmlNode childNode in node.ChildNodes)
                {
                    try
                    {
                        ProcessNode(childNode, offset + "    ", newBranch);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                }
            }
            else if ("Leaf" == node.Name)
            {
                // create new leaf
                string name = node.Attributes["Name"].Value;
                Console.WriteLine(offset + string.Format("Leaf : {0}", name));
                string description = node.Attributes["Description"].Value;
                string filePath = Path.Combine( _dstDirectory, node.Attributes["FileName"].Value );
                Document childDocument = Document.create(DocumentType.getByName("ParametricComponent"), name, description, filePath);
                parentBranch.AddChildDocument(childDocument);
            }
        }
        #endregion
    }
}
