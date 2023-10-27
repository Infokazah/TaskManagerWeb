using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManager.Models
{
    public class TaskModel
    {
        [Key]
        public int TaskId { get; set; }

        public string TaskName { get; set; }

        public string TaskDeadline { get; set; }

        public string TaskStatus { get; set; }
        [Range(1, 9, ErrorMessage = "Приоритет не может привышать 9 и быть меньше 1")]

        public int TaskImportance { get; set; }
        public string TaskDiscription { get; set; }
        public int KategoriId { get; set; }
        [ForeignKey("KategoriId")]
        public KategoriModel TackKategori { get; set; }

        public List<MessengerModel> MessengerList { get; set; }


    }
}
