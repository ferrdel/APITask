using Gestion_DataAcccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System;
using Task = Gestion_DataAcccessLayer.Models.Task;

namespace Gestion_DataAcccessLayer.Data
{
    public class TaskDbContext : DbContext
    {
        public TaskDbContext(DbContextOptions<TaskDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Task> Tasks { get; set; }

    }
}
