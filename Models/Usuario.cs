using GerenciamentoDeTarefas_ASP_NET_Core.Enum;
using System.ComponentModel.DataAnnotations;

namespace GerenciamentoDeTarefas_ASP_NET_Core.Models
{
    public class Usuario
    {
        [Key]
        public int id { get; set; }

        [Required]
        public string login { get; set; }

        [Required]
        [EmailAddress]
        public string email { get; set; }

        [Required]
        public string senha { get; set; }

        public Nivel_Autorizacao nivel_Autorizacao { get; set; }

        public Usuario(string login, string email, string senha)
        {
            this.login = login;
            this.email = email;
            this.senha = senha;
            this.nivel_Autorizacao = Nivel_Autorizacao.CLIENTE;
        }
    }
}
