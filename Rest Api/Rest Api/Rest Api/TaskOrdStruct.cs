using System.Collections.Generic;

namespace Rest_Api
{
    public class OrdStruct
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public List<TaskStruct> Tasks { get; set; }
    }

    public class TaskStruct
    {
        public int Id { get; set; }
        public List<int> PrevTasks { get; set; }
        public int Complexity { get; set; }
        public int TimeConsumption { get; set; }
    }
}
