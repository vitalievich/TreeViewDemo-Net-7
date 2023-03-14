
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace TreeViewDemo.Data
{
    public class DataProvider : IDataProvider   
    {
        public INodeData[] NodeDatas { get; set; }
        private readonly HttpClient Http;
        private readonly IJsWorker JsWorker;
        private List<NodeState> NodeStates { get; set; }
        public DataProvider(IJsWorker jsWorker, HttpClient http)
        {
            Http = http;
            JsWorker = jsWorker;    
        }
        public async Task GetData()
        {
            var nodeStates = await JsWorker.GetStringAsync("nodeStates");
            NodeDatas = await Http.GetFromJsonAsync<NodeData[]>("sample-data/DataTree1.json");
            if (string.IsNullOrEmpty(nodeStates))
            {
                nodeStates = JsonSerializer.Serialize(from x in NodeDatas select new NodeState() { Id = x.Id, IsHidden = true });
            }
            NodeStates = JsonSerializer.Deserialize<List<NodeState>>(nodeStates);
            foreach(var nd in NodeDatas)
            {
                var ns = NodeStates.SingleOrDefault(x => x.Id == nd.Id);
                nd.IsHidden = (ns == null) || ns.IsHidden;
            }
        }

        public async Task SaveNodeState(int id, bool isHidden)
        {
            NodeStates.Single(x => x.Id == id).IsHidden = isHidden;
            var nodeStates = JsonSerializer.Serialize(NodeStates);
            await JsWorker.SetStringAsync("nodeStates", nodeStates);

        }
    }
    public class NodeData : INodeData
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public int? ParentId { get; set; }
        public string Caption { get; set; }
        [JsonIgnore]
        public bool IsHidden { get; set; } = true;
        [JsonIgnore]
        public List<NodeData> Childs { get; set; } 

    }
    public class NodeState 
    {
        public int Id { get; set; }
        public bool IsHidden { get; set; }
    }
}
