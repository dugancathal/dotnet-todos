namespace TodoApi.Models
{
    public class TodoItem
    {
        public string Key { get; set; }
        public string Name { get; set; }
        public bool IsComplete { get; set; }

        public TodoItem(string key, string name)
        {
            Key = key;
            Name = name;
        }
    }
}