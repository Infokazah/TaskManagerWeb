using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManager.Models
{
    public class TaskForm
    {
        [Key]
        public int TaskFormId { get; set; }

        public string TaskFormName { get; set; }
        public string TaskFormDescription { get; set; }
        public string TaskFormData { get; set; }
        public string TaskFormPersonName { get; set;}
        public int TaskModelFormId { get; set; }
        [ForeignKey("TaskModelFormId")]
        public TaskModel TaskModelForm { get; set; }
    }
}
