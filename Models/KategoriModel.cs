namespace TaskManager.Models
{
    public class KategoriModel
    {
        public int KategoriId { get; set; }

        public string KategoriName { get; set; }

        public List<TaskModel> KategoriTasks { get; set; }
    }
}
