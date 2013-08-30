#region Using directives
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
#endregion

namespace Pic.DAL
{
    #region Tree processing callback
    /// <summary>
    /// This callback interface is used to show progress
    /// while lengthy operations on database tree are performed
    /// </summary>
    public interface IProcessingCallback
    {
        void Begin();
        void End();
        void Info(string text);
        void Error(string text);
        bool IsAborting { get; }
        bool HadErrors { get; }
    }
    #endregion

    #region TreeProcessingTask (abstract class)
    /// <summary>
    /// encapsulates a task to be processed by worker thread
    /// </summary>
    public abstract class TreeProcessingTask
    {
        #region Abstract properties
        public abstract string Title { get; }
        #endregion
        #region Abstract methods
        public abstract void Execute(IProcessingCallback callback);
        #endregion
    }
    #endregion

    #region Clear database task
    public class TPTClearDatabase:TreeProcessingTask
    {
        #region Constructor
        public TPTClearDatabase()
        {
        }
        #endregion

        #region Override TreeProcessingTask
        /// <summary>
        /// Task name
        /// </summary>
        public override string Title { get { return "Clearing database"; } }
        /// <summary>
        /// Method to be executed by worker thread
        /// </summary>
        public override void Execute(IProcessingCallback callback)
        {
            BackupRestore.ClearDatabase(callback);
        }
        #endregion
    }
    #endregion

    #region Backup branch task
    public class TPTBackupBranch : TreeProcessingTask
    {
        #region Constructor
        public TPTBackupBranch(List<string> treeNodePath, string zipFilePath)
        {
            _treeNodePath = treeNodePath;
            _zipFilePath = zipFilePath;
        }
        #endregion

        #region Override TreeProcessingTask
        /// <summary>
        /// Task name
        /// </summary>
        public override string Title { get { return "Making backup of tree branch"; } }
        /// <summary>
        /// Method to be executed by worker thread
        /// </summary>
        public override void Execute(IProcessingCallback callback)
        {
            try
            {
                BackupRestore.BackupBranch(_treeNodePath, _zipFilePath, callback);
            }
            catch (Exception ex)
            {
                if (null != callback)
                    callback.Error(ex.Message);
            }
        }
        #endregion

        #region Data members
        private List<string> _treeNodePath;
        private string _zipFilePath;
        #endregion
    }
    #endregion

    #region Backup task
    public class TPTBackup : TreeProcessingTask
    { 
        #region Constructor
        public TPTBackup(string destFilePath)
        {
            _destFilePath = destFilePath;
        }
        #endregion

        #region Override TreeProcessingTask
        /// <summary>
        /// Task name
        /// </summary>
        public override  string Title { get { return string.Format("Backup to {0}", _destFilePath); } }
        /// <summary>
        /// Method to be executed by worker thread
        /// </summary>
        public override void Execute(IProcessingCallback callback)
        {
            BackupRestore.BackupFull(_destFilePath, callback);
        }
        #endregion

        #region Data members
        private string _destFilePath;
        #endregion       
    }
    #endregion

    #region Restore task
    public class TPTRestore : TreeProcessingTask
    {
        #region Constructor
        public TPTRestore(string filePath)
        {
            _filePath = filePath;
        }
        #endregion

        #region Override TreeProcessingTask
        /// <summary>
        /// Task name
        /// </summary>
        public override string Title { get { return string.Format("Restoring file {0}", _filePath); } }
        /// <summary>
        /// Method to be executed by worker thread
        /// </summary>
        public override void Execute(IProcessingCallback callback)
        {
            BackupRestore.Restore(_filePath, callback);
        }
        #endregion

        #region Data members
        private string _filePath;
        #endregion
    }
    #endregion

    #region Merge Task
    public class TPTMerge : TreeProcessingTask
    {
        #region Constructor
        public TPTMerge(string filePath)
        {
            _filePath = filePath;
        }
        #endregion

        #region Override TreeProcessingTask
        /// <summary>
        /// Task name
        /// </summary>
        public override string Title { get { return string.Format("Merging file {0}", _filePath); } }
        /// <summary>
        /// Method to be executed by worker thread
        /// </summary>
        public override void Execute(IProcessingCallback callback)
        {
            BackupRestore.Merge(_filePath, callback);
        }
        #endregion
        #region Data members
        private string _filePath;
        #endregion
    }
    #endregion
}
