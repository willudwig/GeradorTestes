using GeradorTeste.Dominio.Compartilhado;
using GeradorTeste.Dominio.ModuloDisciplina;
using System;

namespace GeradorTeste.Dominio.ModuloMateria
{
    public class Materia : EntidadeBase<Materia>
    {
        public string Titulo { get; set; }
        public Disciplina Disciplina { get; set; }
        public EnumeradorSerie Serie { get; set; }

        public string NomeDisciplinaMateria
        {
            get
            {
                return Disciplina.Nome;
            }
        }

        public string SerieString
        {
            get
            {
                switch (Serie)
                {
                    case EnumeradorSerie.Primeira:
                        return "Primeira";
                        break;

                    case EnumeradorSerie.Segunda:
                        return "Segunda";
                        break;

                    default:
                        break;
                }

                return "";
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
