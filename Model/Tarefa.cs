namespace Tarefas.API.Model
{
    public class Tarefa
    {
        public Tarefa() //metodo construtor tambem poderia esta direto na propriedade "Id" abaixo (igual no DataCadastro e no Concluida
        { 
            Id = Guid.NewGuid();
        }
        public Guid Id { get; private set; }
        public string Nome { get; set; }
        public string Detalhe { get; set; }
        public bool Concluida { get; set; } = false;
        public DateTime DataCadastro { get; private set; } = DateTime.Now;
        public DateTime? DataConclusao { get; private set; } = null;
        public int CategoriId { get; set; }
        public Categoria Categoria { get; private set; }        
    }
}
