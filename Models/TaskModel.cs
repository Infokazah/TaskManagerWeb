using System.ComponentModel.DataAnnotations;

namespace TaskManager.Models
{
    public class TaskModel
    {
        [Key]
        public int TaskId {  get; set; }

        public string TaskName { get; set; }

        public string TaskDeadline { get; set; }

        public string TaskStatus { get; set; }

        public int TaskImportance { get; set; }
        public int KategoriId { get; set; }
        public KategoriModel TackKategori { get; set; }
    }
}
