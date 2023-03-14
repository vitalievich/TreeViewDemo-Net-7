using TreeViewDemo.Data;
using Microsoft.AspNetCore.Components;

namespace TreeViewDemo.Pages
{
    public partial class TreeNode 
    {
        [Inject] public DataProvider DataProvider { get; set; }
        [Parameter] public INodeData Data { get; set; }

        [Parameter] public TreeNode Parent { get; set; }

        private readonly List<TreeNode> Childs = new();
        private bool HasChilds => DataProvider.NodeDatas.Any(x => x.ParentId == Data.Id);
        private string HasChildsExists => HasChilds ? !Data.IsHidden ? "oi-chevron-bottom": "oi-chevron-right" : "";
        private string NameStyle => HasChilds ? "" : "tn_capt_no_childs";

        private async void OpenClose()
        {
            Data.IsHidden = !Data.IsHidden;
            await DataProvider.SaveNodeState(Data.Id, Data.IsHidden);
        }
        public void AddChild(TreeNode treeNode) => Childs.Add(treeNode);
        protected override void OnInitialized()
        {
            base.OnInitialized();
            Parent?.AddChild(this);
        }


    }
}
