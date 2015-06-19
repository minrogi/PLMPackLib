using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace treeDiM.DrawingQuery
{
    public class DESQuerierException : Exception
    {
        // constructor
        public DESQuerierException(string message)
            : base(message)
        { 
        }
    }

    public class DESQuerierFileNotFoundException : DESQuerierException
    {
        public DESQuerierFileNotFoundException(string filePath)
            : base(string.Format("File {0} does not exist", filePath))
        {}
    }

    public class DESQuerierQuestionsNotFound : DESQuerierException
    {
        public DESQuerierQuestionsNotFound(string question)
            : base(string.Format("Question {0} not found", question))
        {
        }
    }
}
