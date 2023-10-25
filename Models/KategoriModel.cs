using System.ComponentModel.DataAnnotations;

namespace TaskManager.Models
{
    public class KategoriModel
    {
        [Key]
        public int KategoriId { get; set; }

        public string KategoriName { get; set; }

        public int Projectid { get; set; }

        public List<TaskModel> KategoriTasks { get; set; }

        public ProjectModel KategoriProject { get; set; }
    }
}
