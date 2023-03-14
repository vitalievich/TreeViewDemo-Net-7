
using TreeViewDemo.Data;

namespace TreeViewDemo.Shared
{
    public partial class NavMenu
    {
        protected override async Task OnInitializedAsync() => await DataProvider.GetData();
    }
}
