using System.ComponentModel.DataAnnotations;

namespace TaskManager.Models
{
    public class ProjectModel
    {
        [Key]
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string ProjectPassword { get; set; }
        public string CreatorName { get; set; }
        public List<KategoriModel> ProjectKategories { get; set; }
        public List<PersonModel> ProgectPerson { get; set;}
    }
}
