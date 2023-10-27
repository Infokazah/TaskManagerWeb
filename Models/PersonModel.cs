using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TaskManager.Models
{
    public class PersonModel
    {
        [Key]
        public int PersonId { get; set; }
        [DisplayName("Имя аккаунта")]
        public string PersonName { get; set; }

        public string PersonMailAddress { get; set; }
        [DisplayName("Пароль")]
        public string PersonPassword { get; set; } 

        public List<ProjectModel> PersonProjects { get; set; }

    }
}
