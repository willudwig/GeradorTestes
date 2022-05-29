using GeradorTeste.Dominio.Compartilhado;

namespace GeradorTeste.Dominio.ModuloDisciplina
{
    public class Disciplina : EntidadeBase<Disciplina>
    {
        public string Nome { get; set; }
      
        public Disciplina()
        {

        }
        
        public Disciplina(int numero, string nome) 
        {
            Numero = numero;
            Nome = nome;
        }

        public override void Atualizar(Disciplina registro)
        {
            Nome = registro.Nome;
        }
    }
}
