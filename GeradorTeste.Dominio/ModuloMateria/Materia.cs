using GeradorTeste.Dominio.Compartilhado;
using GeradorTeste.Dominio.ModuloDisciplina;


namespace GeradorTeste.Dominio.ModuloMateria
{
    public class Materia : EntidadeBase<Materia>
    {
        public string Titulo { get; set; }
        public Disciplina Disciplina { get; set; }
        public EnumeradorSerie Serie { get; set; }

        public string NomeDisciplina
        {
            get
            {
                return Disciplina.Nome;
            }
        }


        public enum EnumeradorSerie { Primeira, Segunda }

        public Materia()
        {
            Disciplina = new();
        }

        public Materia(int numero, string titulo)
        {
            Numero = numero;
            Titulo = titulo;
        }

        public override void Atualizar(Materia registro)
        {
            Titulo = registro.Titulo;
        }
    }
}
