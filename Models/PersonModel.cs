using System.ComponentModel.DataAnnotations;

namespace TaskManager.Models
{
    public class PersonModel
    {
        [Key]
        public int PersonId { get; set; }
        public string PersonName { get; set; }

        public string PersonMailAddress { get; set; }

        public string PersonPassword { get; set; } 

        public List<ProjectModel> PersonProjects { get; set; }
    }
}
