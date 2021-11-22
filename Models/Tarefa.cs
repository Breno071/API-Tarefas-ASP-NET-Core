namespace GerenciamentoDeTarefas_ASP_NET_Core.Models
{
    public class Tarefa
    {
        public int id { get; set; }
        public string nome { get; set; }
        public bool finalizada { get; set; }
        public DateTime date { get; set; } 

        public Tarefa(string nome)
        {
            this.nome = nome;
            this.finalizada = false;
            this.date = DateTime.Today;
        }
    }
}
