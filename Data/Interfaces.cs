using System.Text.Json.Serialization;

namespace TreeViewDemo.Data
{

    public interface IDataProvider
    {
        // Data for TreeView
        INodeData[] NodeDatas { get; set; }        
        Task GetData();        
        Task SaveNodeState(int id, bool isHidden);
    }
    public interface INodeData
    {
        string Name { get; set; }
        int Id { get; set; }
        int? ParentId { get; set; }
        string Caption { get; set; }
        [JsonIgnore]
        bool IsHidden { get; set; }
        [JsonIgnore]
        List<NodeData> Childs { get; set; }

    }

}
