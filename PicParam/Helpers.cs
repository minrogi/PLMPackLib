#region Using directives
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Principal;
#endregion

namespace PicParam
{
    class Helpers
    {
        #region Is user an administrator
        static public bool IsUserAdministrator
        {
            get
            {
                // bool value to hold our return value
                bool isAdmin;
                try
                {
                    //get the currently logged in user
                    WindowsIdentity user = WindowsIdentity.GetCurrent();
                    WindowsPrincipal principal = new WindowsPrincipal(user);
                    isAdmin = principal.IsInRole(WindowsBuiltInRole.Administrator);
                }
                catch (UnauthorizedAccessException /*ex*/)
                {
                    isAdmin = false;
                }
                catch (Exception /*ex*/)
                {
                    isAdmin = false;
                }
                return isAdmin;
            }
        }
        #endregion
    }
}
