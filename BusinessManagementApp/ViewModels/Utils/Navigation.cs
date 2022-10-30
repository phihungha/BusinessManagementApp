using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManagementApp.ViewModels.Utils
{
    public class WorkspaceNavUtils
    {
        public static void ChangeView(WorkspaceViewName viewName)
        {
            WeakReferenceMessenger.Default.Send(new WorkspaceViewChangeMessage(viewName));
        }
    }
}
