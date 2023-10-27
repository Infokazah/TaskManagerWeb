using Microsoft.EntityFrameworkCore;
using TaskManager.Models;


namespace TaskManager.Data
{
    public class TaskManagerDbContext : DbContext
    {
        public DbSet<PersonModel> PersonDb { get; set; }
        public DbSet<KategoriModel> KategoriDb { get; set; }
        public DbSet<TaskModel> TaskDb { get; set; }
        public DbSet<ProjectModel> ProjectDb { get; set; }
        public DbSet<MessengerModel> TaskMessengerDb { get; set; }

        public DbSet<TaskForm> TaskFormDb { get; set; }

        public TaskManagerDbContext(DbContextOptions<TaskManagerDbContext> option) : base(option)
        {

        }
    }
}
