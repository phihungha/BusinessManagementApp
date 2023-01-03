namespace BusinessManagementApp.ViewModels.Navigation
{
    public class NavBackstackItem
    {
        public WorkspaceViewName nextViewName { get; }
        public ViewModelBase ViewModel { get; }

        public NavBackstackItem(WorkspaceViewName nextViewName, ViewModelBase viewModel)
        {
            this.nextViewName = nextViewName;
            ViewModel = viewModel;
        }
    }
}