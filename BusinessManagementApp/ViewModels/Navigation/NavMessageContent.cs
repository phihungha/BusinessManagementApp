namespace BusinessManagementApp.ViewModels.Navigation
{
    /// <summary>
    /// Contains the content of a navigation message
    /// including name of view to go to and extra message
    /// </summary>
    public struct NavigationMessageContent
    {
        public WorkspaceViewName? TargetViewName { get; set; }
        public object? Extra { get; set; }
        public bool SaveOnBackstack { get; set; }

        /// <summary>
        /// Contains the content of a navigation message
        /// including name of view to go to and extra message
        /// </summary>
        /// <param name="targetViewName">Name of view to go to</param>
        /// <param name="extra">An object containing extra message</param>
        public NavigationMessageContent(WorkspaceViewName? targetViewName, bool saveOnBackstack, object? extra)
        {
            TargetViewName = targetViewName;
            Extra = extra;
            SaveOnBackstack = saveOnBackstack;
        }
    }
}