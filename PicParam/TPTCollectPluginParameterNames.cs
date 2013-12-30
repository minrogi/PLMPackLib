#region Using directives
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
// class inherits Pic.DAL.TreeProcessingTask
using Pic.DAL;
using Pic.DAL.SQLite;
#endregion

namespace PicParam
{
    #region TPTCollectPluginParameterNames
    public class TPTCollectPluginParameterNames : TreeProcessingTask
    {
        public override string Title
        {
            get
            {
                return string.Format("Updating localisation file {0}..."
                    , System.IO.Path.GetFileName(LocalizerImpl.Instance.LocalisationFileName));
            }
        }
        public override void Execute(IProcessingCallback callback)
        {
            using (TreeProcessor tnProcessor = new TreeProcessor())
            {
                tnProcessor.ProcessVisitor(new TreeNodeVisitorCollectParameterNames(callback));
            }
        }
    }
    #endregion

    #region TreeNodeVisitorCollectParameterNames
    internal class TreeNodeVisitorCollectParameterNames : TreeNodeVisitor
    {
        #region Constructor
        public TreeNodeVisitorCollectParameterNames(IProcessingCallback callback)
        {
            _callback = callback;
            // instantiate component loader
            _compLoader = new Pic.Plugin.ComponentLoader();
            _compLoader.SearchMethod = new ComponentSearchMethodDB();
        }
        #endregion
        #region TreeNodeVisitor override
        public override void Initialize(PPDataContext db)
        {
        }

        public override bool Process(Pic.DAL.SQLite.PPDataContext db, TreeNode tn)
        {
            if (!tn.IsComponent) return true;
            try
            {
                string filePath = tn.Documents(db)[0].File.Path(db);
                Pic.Plugin.Component comp = _compLoader.LoadComponent(filePath);
                if (null != _callback && null != comp)
                    _callback.Info(string.Format("Successfully loaded component {0}", tn.Name));
                else
                    _callback.Error(string.Format("Failed to load component {0}", tn.Name));

                Pic.Plugin.ParameterStack stack = comp.BuildParameterStack(null);
                foreach (Pic.Plugin.Parameter param in stack)
                {
                    if (param.IsMajoration)
                        continue;
                    // only add parameter description
                    TryAndAddString( param.Description );
                    // ParameterMulti ? 
                    Pic.Plugin.ParameterMulti paramMulti = param as Pic.Plugin.ParameterMulti;
                    if (null != paramMulti)
                    {
                        foreach (string sText in paramMulti.DisplayList)
                            TryAndAddString(sText);
                    }
                }
            }
            catch (Exception ex)
            {
                if (null != _callback)
                    _callback.Error(ex.Message);
            }
            return true;
        }
        public override void Finalize(PPDataContext db)
        {
            // force saving file 
            // (the file might be opened just after collecting parameter strings)
            LocalizerImpl.Instance.Save();
        }
        #endregion

        #region Helpers
        private void TryAndAddString(string sText)
        {
            if (null != _callback && !LocalizerImpl.Instance.HasTerm(sText))
            {
                LocalizerImpl.Instance.AddTerm(sText);
                _callback.Info(string.Format("Adding {0}...", sText));

            }
        }
        #endregion

        #region Data members
        private IProcessingCallback _callback;
        private Pic.Plugin.ComponentLoader _compLoader;
        #endregion
    }
    #endregion
}
