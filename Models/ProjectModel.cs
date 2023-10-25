using System.ComponentModel.DataAnnotations;

namespace TaskManager.Models
{
    public class ProjectModel
    {
        [Key]
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public List<string> ProjectKategories { get; set; }

    }
}
