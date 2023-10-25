using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManager.Models
{
    public class KategoriModel
    {
        [Key]
        public int KategoriId { get; set; }

        public string KategoriName { get; set; }

        public int Projectid { get; set; }

        public List<TaskModel> KategoriTasks { get; set; }

        [ForeignKey("Projectid")]

        public ProjectModel KategoriProject { get; set; }
    }
}
