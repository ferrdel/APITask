using Gestion_DataAcccessLayer.Models;

namespace WebApp_APIGestionTareas.Infrastructure.Dto
{
    public class TaskDTO
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public string State { get; set; }

        public int UserId { get; set; }

        public User user;
    }
}
