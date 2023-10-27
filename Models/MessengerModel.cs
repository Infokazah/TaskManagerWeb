using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManager.Models
{
    public class MessengerModel
    {
        [Key]
        public int MessengerId { get; set; }

        public string Messenge { get; set; }

        public int TaskModelid { get; set; }

        [ForeignKey("TaskModelid")]
        public TaskModel MessengerTaskModel { get; set; }
    }
}
