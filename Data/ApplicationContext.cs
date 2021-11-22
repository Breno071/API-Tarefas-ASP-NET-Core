using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GerenciamentoDeTarefas_ASP_NET_Core.Models;
using GerenciamentoDeTarefas_ASP_NET_Core.ViewModel;

namespace GerenciamentoDeTarefas_ASP_NET_Core.Data
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext (DbContextOptions<ApplicationContext> options)
            : base(options)
        {
        }

        public DbSet<Tarefa> Tarefas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        
    }
}
