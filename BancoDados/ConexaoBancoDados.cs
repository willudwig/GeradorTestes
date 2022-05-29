using GeradorTeste.Dominio;
using GeradorTeste.Dominio.ModuloDisciplina;
using GeradorTeste.Dominio.ModuloMateria;
using GeradorTeste.Dominio.ModuloTeste;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using static GeradorTeste.Dominio.ModuloMateria.Materia;
using static GeradorTeste.Dominio.ModuloTeste.Teste;
using static GeradorTeste.Dominio.Questao;

namespace BancoDados
{
    public class ConexaoBancoDados
    {
        SqlConnection conexao;
        Disciplina disciplinaMateria;

        string sql;
        public int id;

        public ConexaoBancoDados()
        {
            id = 0;
        }

        private void ConectarBancoDados()
        {
            conexao = new();

            conexao.ConnectionString = @"Data Source=(localDB)\MSSqlLocalDB;Initial Catalog=GeradorTestesDB;Integrated Security=True;Pooling=False";

            conexao.Open();
        }

        private void DesconectarBancoDados()
        {
            conexao.Close();
        }

        #region Disciplina
        public void InserirDisciplinaNoBancoDeDados(string texto)
        {
            ConectarBancoDados();

            SqlCommand cmdInsercao = new();

            sql = @"INSERT INTO [TBDISCIPLINA] ( [NOME] ) VALUES ( @NOME ); SELECT SCOPE_IDENTITY();"; // @NOME é um parâmetro

            cmdInsercao.Connection = conexao;
            cmdInsercao.CommandText = sql;
            cmdInsercao.Parameters.AddWithValue("NOME", texto);

            id = Convert.ToInt32(cmdInsercao.ExecuteScalar());

            DesconectarBancoDados();
        }

        public void EditarDisciplinaNoBancoDados(Disciplina d)
        {
            ConectarBancoDados();

            SqlCommand cmdEdicao = new();

            sql = @"UPDATE [TBDISCIPLINA] SET [NOME] = @NOME WHERE [NUMERO] = @NUMERO";

            cmdEdicao.Connection = conexao;
            cmdEdicao.CommandText = sql;
            cmdEdicao.Parameters.AddWithValue("NUMERO", d.Numero);
            cmdEdicao.Parameters.AddWithValue("NOME", d.Nome);
            cmdEdicao.ExecuteNonQuery();

            DesconectarBancoDados();
        }

        public void ExcluirDisciplinaNoBancoDados(int numero)
        {
            ConectarBancoDados();

            SqlCommand cmdExclusao = new();

            sql = @"DELETE FROM [TBDISCIPLINA] WHERE [NUMERO] = @NUMERO";

            cmdExclusao.Connection = conexao;
            cmdExclusao.CommandText = sql;
            cmdExclusao.Parameters.AddWithValue("NUMERO", numero);
            cmdExclusao.ExecuteNonQuery();

            DesconectarBancoDados();
        }

        public List<Disciplina> SelecionarTodosDisciplina()
        {
            ConectarBancoDados();

            sql = @"SELECT * FROM [TBDISCIPLINA]";

            SqlCommand cmdSelecao = new();

            cmdSelecao.Connection = conexao;
            cmdSelecao.CommandText = sql;

            SqlDataReader leitor = cmdSelecao.ExecuteReader();

            List<Disciplina> disciplinas = new();

            while (leitor.Read())
            {
                int numero = Convert.ToInt32(leitor["NUMERO"]);
                string nome = leitor["NOME"].ToString();

                var disciplina = new Disciplina()
                {
                    Numero = numero,
                    Nome = nome
                };

                disciplinas.Add(disciplina);
            }

            DesconectarBancoDados();

            return disciplinas;
        }

        public Disciplina SelecionarDisciplinaPorNumero(int numeroPesquisado)
        {
            ConectarBancoDados();

            sql = @"SELECT * FROM [TBDISCIPLINA] WHERE [NUMERO] = @NUMERO";

            SqlCommand cmdSelecao = new();

            cmdSelecao.Connection = conexao;
            cmdSelecao.CommandText = sql;
            cmdSelecao.Parameters.AddWithValue("NUMERO", numeroPesquisado);

            SqlDataReader leitor = cmdSelecao.ExecuteReader();

            Disciplina disciplinaSelecionada = null;

            if (leitor.Read())
            {
                int numero = Convert.ToInt32(leitor["NUMERO"]);
                string nome = leitor["NOME"].ToString();

                disciplinaSelecionada = new Disciplina()
                {
                    Numero = numero,
                    Nome = nome
                };
            }

            DesconectarBancoDados();

            return disciplinaSelecionada;
        }
        #endregion

        #region Materia
        public void InserirMateriaoBancoDados(Materia materia)
        {
            ConectarBancoDados();

            SqlCommand cmdInsercao = new();

            sql = @"INSERT INTO [TBMATERIA] ( [TITULO], 
                                              [SERIE], 
                                              [DISCIPLINA_NUMERO] ) 
                                 
                                     VALUES ( @TITULO, 
                                              @SERIE, 
                                              @DISC_NUM ); SELECT SCOPE_IDENTITY();"; // com @ é parâmetro

            cmdInsercao.Connection = conexao;
            cmdInsercao.CommandText = sql;
            cmdInsercao.Parameters.AddWithValue("TITULO", materia.Titulo);
            cmdInsercao.Parameters.AddWithValue("SERIE", materia.Serie.ToString());
            cmdInsercao.Parameters.AddWithValue("DISC_NUM", materia.Disciplina.Numero);

            id = Convert.ToInt32(cmdInsercao.ExecuteScalar());

            DesconectarBancoDados();
        }

        public void EditarMateriaNoBancoDados(Materia materia)
        {
            ConectarBancoDados();

            SqlCommand cmdEdicao = new();

            sql = @"UPDATE [TBMATERIA] SET 

                            [TITULO] = @TITULO,
                            [SERIE] = @SERIE,
                            [DISCIPLINA_NUMERO] = @DISC_NUMERO

                    WHERE 
                            [NUMERO] = @NUMERO";

            cmdEdicao.Connection = conexao;
            cmdEdicao.CommandText = sql;
            cmdEdicao.Parameters.AddWithValue("TITULO", materia.Titulo);
            cmdEdicao.Parameters.AddWithValue("SERIE", materia.Serie.ToString());
            cmdEdicao.Parameters.AddWithValue("DISC_NUMERO", materia.Disciplina.Numero);
            cmdEdicao.Parameters.AddWithValue("NUMERO", materia.Numero);
            cmdEdicao.ExecuteNonQuery();

            DesconectarBancoDados();
        }

        public void ExcluirMateriaNoBancoDados(int numero)
        {
            ConectarBancoDados();

            SqlCommand cmdExclusao = new();

            sql = @"DELETE FROM [TBMATERIA] WHERE [NUMERO] = @NUMERO";

            cmdExclusao.Connection = conexao;
            cmdExclusao.CommandText = sql;
            cmdExclusao.Parameters.AddWithValue("NUMERO", numero);
            try
            {
                cmdExclusao.ExecuteNonQuery();
            }
            catch (System.Data.SqlClient.SqlException)
            {
                return;
            }

            DesconectarBancoDados();
        }

        public List<Materia> SelecionarTodosMateria()
        {
            ConectarBancoDados();

            sql = @"SELECT MAT.NUMERO, MAT.TITULO, MAT.DISCIPLINA_NUMERO, MAT.SERIE, DISC.NOME AS DISCIPLINA_NOME FROM [TBMATERIA] AS MAT 
	                    INNER JOIN [TBDISCIPLINA] AS DISC
		                    ON DISC.NUMERO = MAT.DISCIPLINA_NUMERO";

            SqlCommand cmdSelecao = new();

            cmdSelecao.Connection = conexao;
            cmdSelecao.CommandText = sql;

            SqlDataReader leitor = cmdSelecao.ExecuteReader();

            List<Materia> materias = LerMaterias(leitor);

            DesconectarBancoDados();

            return materias;
        }

        public Materia SelecionarMateriaPorNumero(int numeroPesquisado)
        {
            ConectarBancoDados();

            sql = @"SELECT MAT.NUMERO, 
                           MAT.TITULO, 
                           MAT.DISCIPLINA_NUMERO, 
                           MAT.SERIE, 
                           DISC.NOME DISCIPLINA_NOME 
                    FROM [TBMATERIA] AS MAT 
	                       INNER JOIN [TBDISCIPLINA] AS DISC
		                      ON MAT.DISCIPLINA_NUMERO = DISC.NUMERO
                    WHERE 
                          MAT.NUMERO = @NUMERO";

            SqlCommand cmdSelecao = new();

            cmdSelecao.Connection = conexao;
            cmdSelecao.CommandText = sql;
            cmdSelecao.Parameters.AddWithValue("NUMERO", numeroPesquisado);
            SqlDataReader leitor = cmdSelecao.ExecuteReader();

            Materia materiaSelecionada = LerUmaUnicaMateria(leitor);

            DesconectarBancoDados();

            return materiaSelecionada;
        }

        private Materia LerUmaUnicaMateria(SqlDataReader leitor)
        {
            Materia materiaSelecionada = null;

            if (leitor.Read())
            {
                int numero = Convert.ToInt32(leitor["NUMERO"]);
                string titulo = leitor["TITULO"].ToString();
                string serieString = leitor["SERIE"].ToString();
                int disciplinaNumero = (int)leitor["DISCIPLINA_NUMERO"];
                string disciplinaNome = leitor["DISCIPLINA_NOME"].ToString();

                EnumeradorSerie serie = EnumeradorSerie.Primeira;

                switch (serieString)
                {
                    case "Primeira":
                        serie = EnumeradorSerie.Primeira;
                        break;

                    case "Segunda":
                        serie = EnumeradorSerie.Segunda;
                        break;

                    default:
                        break;
                }

                materiaSelecionada = new Materia()
                {
                    Numero = numero,
                    Titulo = titulo,
                    Serie = serie,
                    Disciplina = new Disciplina()
                    {
                        Numero = disciplinaNumero,
                        Nome = disciplinaNome
                    }
                };
            }

            return materiaSelecionada;
        }

        private List<Materia> LerMaterias(SqlDataReader leitor)
        {
            List<Materia> materias = new();

            while (leitor.Read())
            {
                int numero = Convert.ToInt32(leitor["NUMERO"]);
                string titulo = leitor["TITULO"].ToString();
                string serieString = leitor["SERIE"].ToString();
                int disciplinaNumero = (int)leitor["DISCIPLINA_NUMERO"];
                string disciplinaNome = leitor["DISCIPLINA_NOME"].ToString();

                EnumeradorSerie serie = EnumeradorSerie.Primeira;

                switch (serieString)
                {
                    case "Primeira":
                        serie = EnumeradorSerie.Primeira;
                        break;

                    case "Segunda":
                        serie = EnumeradorSerie.Segunda;
                        break;

                    default:
                        break;
                }

                var materia = new Materia()
                {
                    Numero = numero,
                    Titulo = titulo,
                    Serie = serie,
                    Disciplina = new Disciplina()
                    {
                        Numero = disciplinaNumero,
                        Nome = disciplinaNome
                    }
                };

                materias.Add(materia);

            }

            return materias;
        }

        #endregion

        #region Questao

        public void InserirQuestaoBancoDados(Questao questao)
        {
            ConectarBancoDados();

            SqlCommand cmdInsercao = new();

            sql = @"INSERT INTO [TBQUESTAO] (  
                                              [BIMESTRE], 
                                              [MATERIA_NUMERO],
                                              [PERGUNTA],
                                              [RESPOSTA] 
                                            ) 
                                 
                                     VALUES ( 
                                              @BIMESTRE, 
                                              @MATERIA_NUMERO, 
                                              @PERGUNTA,
                                              @RESPOSTA

                                            ); SELECT SCOPE_IDENTITY();";

            cmdInsercao.Connection = conexao;
            cmdInsercao.CommandText = sql;
            cmdInsercao.Parameters.AddWithValue("BIMESTRE", questao.Bimestre.ToString());
            cmdInsercao.Parameters.AddWithValue("MATERIA_NUMERO", questao.Materia.Numero);
            cmdInsercao.Parameters.AddWithValue("PERGUNTA", questao.Pergunta);
            cmdInsercao.Parameters.AddWithValue("RESPOSTA", questao.Resposta);

            id = Convert.ToInt32(cmdInsercao.ExecuteScalar());

            DesconectarBancoDados();
        }

        public void EditarQuestaoNoBancoDados(Questao questao)
        {
            ConectarBancoDados();

            SqlCommand cmdEdicao = new();

            sql = @"UPDATE [TBQUESTAO] SET 

                                         [BIMESTRE] = @BIMESTRE, 
                                         [MATERIA_NUMERO] = @MATERIA_NUMERO,
                                         [PERGUNTA] = @PERGUNTA,
                                         [RESPOSTA] = @RESPOSTA

                                   WHERE 

                                         [NUMERO] = @NUMERO";

            cmdEdicao.Connection = conexao;
            cmdEdicao.CommandText = sql;
            cmdEdicao.Parameters.AddWithValue("NUMERO", questao.Numero);
            cmdEdicao.Parameters.AddWithValue("BIMESTRE", questao.Bimestre.ToString());
            cmdEdicao.Parameters.AddWithValue("MATERIA_NUMERO", questao.Materia.Numero);
            cmdEdicao.Parameters.AddWithValue("PERGUNTA", questao.Pergunta);
            cmdEdicao.Parameters.AddWithValue("RESPOSTA", questao.Resposta);
            cmdEdicao.ExecuteNonQuery();

            DesconectarBancoDados();
        }

        public void EditarAlternativaBancoDados(Questao questao)
        {
            if (VerificarTabelaAlternativaVazia() == true)
            {
                AdicionarAlternativas(questao);
            }
            else
            {
                if (questao.alternativas.Count == 3)

                    AdicionarAlternativas(questao);

                else
                {
                    for (int i = 0; i < 3; i++)
                    {
                        questao.alternativas.Remove(questao.alternativas[0]);
                    }

                    ExcluirAlternativasQuestao(questao.Numero);

                    AdicionarAlternativas(questao);
                }
            }
        }
       
        private bool VerificarTabelaAlternativaVazia()
        {
            ConectarBancoDados();

            sql = @"SELECT COUNT(*) FROM TBALTERNATIVA";

            SqlCommand cmdVerifica = new(sql, conexao);

            int dados = Convert.ToInt32(cmdVerifica.ExecuteScalar());

            DesconectarBancoDados();

            if (dados == 0)

                return true;

            else

                return false;
        }

        public void AdicionarAlternativas(Questao questao)
        {
            ConectarBancoDados();

            sql = @"INSERT INTO [TBALTERNATIVA] (
                                                  [QUESTAO_NUMERO],
                                                  [DESCRICAO]  
                                                )
                                         VALUES (
                                                  @QUESTAO_NUMERO,
                                                  @DESCRICAO
                                                ); 

                                                SELECT SCOPE_IDENTITY();";

            foreach (string item in questao.alternativas)
            {
                SqlCommand cmdInserirAlternativa = new(sql, conexao);

                cmdInserirAlternativa.Parameters.AddWithValue("DESCRICAO", item);
                cmdInserirAlternativa.Parameters.AddWithValue("QUESTAO_NUMERO", questao.Numero);

                cmdInserirAlternativa.ExecuteNonQuery();
            }

            DesconectarBancoDados();
        }

        public void ExcluirQuestaoNoBancoDados(int numero)
        {
            ExcluirAlternativasQuestao(numero);

            ConectarBancoDados();

            SqlCommand cmdExclusao = new();

            sql = @"DELETE FROM [TBQUESTAO] WHERE [NUMERO] = @NUMERO";

            cmdExclusao.Connection = conexao;
            cmdExclusao.CommandText = sql;
            cmdExclusao.Parameters.AddWithValue("NUMERO", numero);
            cmdExclusao.ExecuteNonQuery();

            DesconectarBancoDados();
        }

        private void ExcluirAlternativasQuestao(int numeroQuestao)
        {
            ConectarBancoDados();

            SqlCommand cmdExclusao = new();

            sql = @"DELETE FROM [TBALTERNATIVA] WHERE [QUESTAO_NUMERO] = @NUMERO";

            cmdExclusao.Connection = conexao;
            cmdExclusao.CommandText = sql;
            cmdExclusao.Parameters.AddWithValue("NUMERO", numeroQuestao);
            cmdExclusao.ExecuteNonQuery();

            DesconectarBancoDados();
        }

        public List<Questao> SelecionarTodosQuestao()
        {
            ConectarBancoDados();

            sql = @"SELECT 
                            Q.NUMERO,
                            Q.BIMESTRE,
                            Q.PERGUNTA,
                            Q.RESPOSTA,
                            Q.MATERIA_NUMERO,

                            M.TITULO AS MATERIA_TITULO,
                            M.SERIE AS MATERIA_SERIE,
                            M.DISCIPLINA_NUMERO AS DISCIPLINA_NUMERO
                    FROM 
                            [TBQUESTAO] AS Q

                    INNER JOIN [TBMATERIA] AS M

                            ON Q.MATERIA_NUMERO = M.NUMERO";

            SqlCommand cmdSelecao = new();

            cmdSelecao.Connection = conexao;
            cmdSelecao.CommandText = sql;

            SqlDataReader leitor = cmdSelecao.ExecuteReader();

            List<Questao> questoes = LerQuestoes(leitor);

            DesconectarBancoDados();

            foreach  (Questao q in questoes)
            {
                q.Materia.Disciplina = SelecionarDisciplinaPorNumero(q.Materia.Disciplina.Numero);
                SelecionarAlternativas(q);  
            }

            return questoes;
        }

        public Questao SelecionarQuestaoPorNumero(int numero)
        {
            ConectarBancoDados();

            sql = @"SELECT 
                            Q.NUMERO,
                            Q.BIMESTRE,
                            Q.PERGUNTA,
                            Q.RESPOSTA,
                            Q.MATERIA_NUMERO,

                            M.TITULO AS MATERIA_TITULO,
                            M.SERIE AS MATERIA_SERIE,
                            M.DISCIPLINA_NUMERO AS DISCIPLINA_NUMERO

                   FROM [TBQUESTAO] AS Q 

                   INNER JOIN [TBMATERIA] AS M 
                    
                            ON Q.MATERIA_NUMERO = M.NUMERO

	                WHERE 
                            Q.NUMERO = @NUMERO";

            SqlCommand cmdSelecao = new();

            cmdSelecao.Connection = conexao;
            cmdSelecao.CommandText = sql;
            cmdSelecao.Parameters.AddWithValue("NUMERO", numero);
            SqlDataReader leitor = cmdSelecao.ExecuteReader();

            Questao questSelecionada = LerUmaUnicaQuestao(leitor);

            DesconectarBancoDados();

            SelecionarAlternativas(questSelecionada);

            questSelecionada.Materia.Disciplina = SelecionarDisciplinaPorNumero(questSelecionada.Materia.Disciplina.Numero);

            return questSelecionada;
        }

        private Questao LerUmaUnicaQuestao(SqlDataReader leitor)
        {
            Questao questaoSelecionada = null;

            if (leitor.Read())
            {
                int numero = Convert.ToInt32(leitor["NUMERO"]);
                string bimestreString = Convert.ToString(leitor["BIMESTRE"]);
                string pergunta = Convert.ToString(leitor["PERGUNTA"]);
                string resposta = Convert.ToString(leitor["RESPOSTA"]);

                int materiaNumero = Convert.ToInt32(leitor["MATERIA_NUMERO"]);
                string materiaTitulo = Convert.ToString(leitor["MATERIA_TITULO"]);
                string materiaSerie = Convert.ToString(leitor["MATERIA_SERIE"]);

                int materiaDisciplinaNumero = (int)leitor["DISCIPLINA_NUMERO"];
               // string materiaDisciplinaNome = leitor["DISCIPLINA_NOME"].ToString();

                EnumeradorSerie serie = EnumeradorSerie.Primeira;
                EnumeradorBimestre bimestre = EnumeradorBimestre.Primeiro;

                switch (materiaSerie)
                {
                    case "Primeira":
                        serie = EnumeradorSerie.Primeira;
                        break;

                    case "Segunda":
                        serie = EnumeradorSerie.Segunda;
                        break;

                    default:
                        break;
                }

                switch (bimestreString)
                {
                    case "Primeiro":
                        bimestre = EnumeradorBimestre.Primeiro;
                        break;

                    case "Segundo":
                        bimestre = EnumeradorBimestre.Segundo;
                        break;

                    case "Terceiro":
                        bimestre = EnumeradorBimestre.Terceiro;
                        break;

                    case "Quarto":
                        bimestre = EnumeradorBimestre.Quarto;
                        break;

                    default:
                        break;
                }

                questaoSelecionada = new Questao()
                {
                    Numero = numero,
                    Bimestre = bimestre,
                    Pergunta = pergunta,
                    Resposta = resposta,

                    //Materia = new()
                    //{
                    //    Numero = materiaNumero,
                    //    Titulo = materiaTitulo,
                    //    Serie = serie,

                    //    Disciplina = new()
                    //    {
                    //        Numero = materiaDisciplinaNumero,
                    //       // Nome = materiaDisciplinaNome
                    //    }
                    //}
                };

                questaoSelecionada.Materia.Numero = materiaNumero;
                questaoSelecionada.Materia.Titulo = materiaTitulo;
                questaoSelecionada.Materia.Serie = serie;
                questaoSelecionada.Materia.Disciplina.Numero = materiaDisciplinaNumero;
            }

            return questaoSelecionada;
        }

        private List<Questao> LerQuestoes(SqlDataReader leitor)
        {
            List<Questao> questoes = new();

            while (leitor.Read())
            {
                int numero = Convert.ToInt32(leitor["NUMERO"]);
                string bimestreString = Convert.ToString(leitor["BIMESTRE"]);
                string pergunta = Convert.ToString(leitor["PERGUNTA"]);
                string resposta = Convert.ToString(leitor["RESPOSTA"]);

                int materiaNumero = Convert.ToInt32(leitor["MATERIA_NUMERO"]);
                string materiaTitulo = Convert.ToString(leitor["MATERIA_TITULO"]);
                string materiaSerie = Convert.ToString(leitor["MATERIA_SERIE"]);

                int materiaDisciplinaNumero = (int)leitor["DISCIPLINA_NUMERO"];
             //   string materiaDisciplinaNome = leitor["M.DISCIPLINA_NOME"].ToString();

                EnumeradorSerie serie = EnumeradorSerie.Primeira;
                EnumeradorBimestre bimestre = EnumeradorBimestre.Primeiro;

                switch (materiaSerie)
                {
                    case "Primeira":
                        serie = EnumeradorSerie.Primeira;
                        break;

                    case "Segunda":
                        serie = EnumeradorSerie.Segunda;
                        break;

                    default:
                        break;
                }

                switch (bimestreString)
                {
                    case "Primeiro":
                        bimestre = EnumeradorBimestre.Primeiro;
                        break;

                    case "Segundo":
                        bimestre = EnumeradorBimestre.Segundo;
                        break;

                    case "Terceiro":
                        bimestre = EnumeradorBimestre.Terceiro;
                        break;

                    case "Quarto":
                        bimestre = EnumeradorBimestre.Quarto;
                        break;

                    default:
                        break;
                }

                var questao = new Questao()
                {
                    Numero = numero,
                    Bimestre = bimestre,
                    Pergunta = pergunta,
                    Resposta = resposta,

                    //Materia = new()
                    //{
                    //    Numero = materiaNumero,
                    //    Titulo = materiaTitulo,
                    //    Serie = serie,

                    //    Disciplina = new()
                    //    {
                    //        Numero = materiaDisciplinaNumero,
                    //        Nome = null
                    //    }
                    //}
                };

                questao.Materia.Numero = materiaNumero;
                questao.Materia.Titulo = materiaTitulo;
                questao.Materia.Serie = serie;
                questao.Materia.Disciplina.Numero = materiaDisciplinaNumero;

                questoes.Add(questao);
            }

            return questoes;
        }

        public void SelecionarAlternativas(Questao questao)
        {
            ConectarBancoDados();

            sql = @"SELECT * FROM [TBALTERNATIVA] AS ALT
                            WHERE ALT.QUESTAO_NUMERO = @QUESTAO_NUMERO";

            SqlCommand cmdSelecao = new();

            cmdSelecao.Connection = conexao;
            cmdSelecao.CommandText = sql;
            cmdSelecao.Parameters.AddWithValue("QUESTAO_NUMERO", questao.Numero);

            SqlDataReader leitor = cmdSelecao.ExecuteReader();

            LerAlternativas(leitor, questao);

            DesconectarBancoDados();
        }

        private void LerAlternativas(SqlDataReader leitor, Questao questao)
        {
            questao.alternativas = new();

            while (leitor.Read())
            {
                questao.alternativas.Add(leitor["DESCRICAO"].ToString());
            }

        }

        #endregion

        #region Teste

        public void InserirtTesteBancoDados(Teste teste)
        {
            ConectarBancoDados();

            SqlCommand cmdInsercao = new();

            sql = @"INSERT INTO [TBTESTE] (
                                              [DISCIPLINA_NUMERO],
                                              [PROVA], 
                                              [NUMEROQUESTOES],
                                              [GABARITO],
                                              [DATA] 
                                           ) 

                                    VALUES (
                                              @DISCIPLINA_NUMERO, 
                                              @PROVA, 
                                              @NUMEROQUESTOES,
                                              @GABARITO,
                                              @DATA

                                           ); SELECT SCOPE_IDENTITY();";

            cmdInsercao.Connection = conexao;
            cmdInsercao.CommandText = sql;
            cmdInsercao.Parameters.AddWithValue("DISCIPLINA_NUMERO", teste.DisciplinaTeste.Numero);
            cmdInsercao.Parameters.AddWithValue("PROVA", teste.Prova.ToString());
            cmdInsercao.Parameters.AddWithValue("NUMEROQUESTOES", teste.NumeroQuestoes.ToString());
            cmdInsercao.Parameters.AddWithValue("GABARITO", teste.gabarito.ToString());
            cmdInsercao.Parameters.AddWithValue("DATA", teste.data);

            id = Convert.ToInt32(cmdInsercao.ExecuteScalar());

            DesconectarBancoDados();
        }

        public void EditarTesteNoBancoDados(Teste teste)
        {
            ConectarBancoDados();

            SqlCommand cmdEdicao = new();

            sql = @"UPDATE [TBTESTE] SET 

                                        [DISCIPLINA_NUMERO] = @DISCIPLINA_NUMERO,
                                        [PROVA] = @PROVA, 
                                        [NUMEROQUESTOES] = @NUMEROQUESTOES,
                                        [GABARITO] = @GABARITO,
                                        [DATA] = @DATA

                                       WHERE 

                                         [NUMERO] = @NUMERO";

            cmdEdicao.Connection = conexao;
            cmdEdicao.CommandText = sql;
            cmdEdicao.Parameters.AddWithValue("DISCIPLINA_NUMERO", teste.DisciplinaTeste.Numero);
            cmdEdicao.Parameters.AddWithValue("PROVA", teste.Prova.ToString());
            cmdEdicao.Parameters.AddWithValue("NUMEROQUESTOES", teste.NumeroQuestoes.ToString());
            cmdEdicao.Parameters.AddWithValue("GABARITO", teste.gabarito.ToString());
            cmdEdicao.Parameters.AddWithValue("NUMERO", teste.Numero);
            cmdEdicao.Parameters.AddWithValue("DATA", Convert.ToDateTime(teste.DataString));

            cmdEdicao.ExecuteNonQuery();

            DesconectarBancoDados();
        }

        public void ExcluirTesteNoBancoDados(int numero)
        {
            ConectarBancoDados();

            SqlCommand cmdExclusao = new();

            sql = @"DELETE FROM [TBTESTE] WHERE [NUMERO] = @NUMERO";

            cmdExclusao.Connection = conexao;
            cmdExclusao.CommandText = sql;
            cmdExclusao.Parameters.AddWithValue("NUMERO", numero);
            cmdExclusao.ExecuteNonQuery();

            DesconectarBancoDados();
        }

        public List<Teste> SelecionarTodosTeste()
        {
            ConectarBancoDados();

            sql = @"SELECT 
                            T.[NUMERO],
                            T.[DISCIPLINA_NUMERO],
                            T.[PROVA], 
                            T.[NUMEROQUESTOES],
                            T.[GABARITO],
                            T.[DATA],

                            D.NOME AS DISCIPLINA_NOME
                    FROM 
                            [TBTESTE] AS T

                    INNER JOIN [TBDISCIPLINA] AS D

                            ON T.DISCIPLINA_NUMERO = D.NUMERO";


            SqlCommand cmdSelecao = new();

            cmdSelecao.Connection = conexao;
            cmdSelecao.CommandText = sql;

            SqlDataReader leitor = cmdSelecao.ExecuteReader();

            List<Teste> testes = LerTestes(leitor);

            DesconectarBancoDados();

            foreach (Teste t in testes)
            {
                t.DisciplinaTeste = SelecionarDisciplinaPorNumero(t.DisciplinaTeste.Numero);
            }

            return testes;
        }

        public Teste SelecionarTestePorNumero(int numero)
        {
            ConectarBancoDados();

            sql = @"SELECT 
                            
                            T.[NUMERO],
                            T.[DISCIPLINA_NUMERO],
                            T.[PROVA], 
                            T.[NUMEROQUESTOES],
                            T.[GABARITO],
                            T.[DATA],

                            D.NOME AS DISCIPLINA_NOME
                    FROM 
                            [TBTESTE] AS T

                    INNER JOIN [TBDISCIPLINA] AS D

                            ON T.DISCIPLINA_NUMERO = D.NUMERO

                     WHERE 
                            T.NUMERO = @NUMERO";

            SqlCommand cmdSelecao = new();

            cmdSelecao.Connection = conexao;
            cmdSelecao.CommandText = sql;
            cmdSelecao.Parameters.AddWithValue("NUMERO", numero);
            SqlDataReader leitor = cmdSelecao.ExecuteReader();

            Teste testeSelecionado = LerUmUnicoTeste(leitor);

            DesconectarBancoDados();

            testeSelecionado.DisciplinaTeste = SelecionarDisciplinaPorNumero(testeSelecionado.DisciplinaTeste.Numero);

            return testeSelecionado;
        }

        private Teste LerUmUnicoTeste(SqlDataReader leitor)
        {
            Teste teste = new();

            if (leitor.Read())
            {
                int numero = Convert.ToInt32(leitor["NUMERO"]);
                string numeroQuestoesString = Convert.ToString(leitor["NUMEROQUESTOES"]);
                string prova = Convert.ToString(leitor["PROVA"]);
                string gabaritoString = Convert.ToString(leitor["GABARITO"]);
                DateTime dataSetada = Convert.ToDateTime(leitor["DATA"]);

                int disciplinaTesteNumero = (int)leitor["DISCIPLINA_NUMERO"];
                //  nome da disciplina pega por fora

                EnumNumeroQuestoes numeroQuestoes = EnumNumeroQuestoes.cinco;

                switch (numeroQuestoesString)
                {
                    case "5":
                        numeroQuestoes = EnumNumeroQuestoes.cinco;
                        break;

                    case "10":
                        numeroQuestoes = EnumNumeroQuestoes.dez;
                        break;

                    case "15":
                        numeroQuestoes = EnumNumeroQuestoes.quinze;
                        break;

                    case "20":
                        numeroQuestoes = EnumNumeroQuestoes.vinte;
                        break;

                    default:
                        break;
                }

                teste = new Teste()
                {
                    Numero = numero,
                    NumeroQuestoes = numeroQuestoes,
                    Prova = prova,
                    gabarito = gabaritoString,
                    data = dataSetada
                };

                teste.DisciplinaTeste.Numero = disciplinaTesteNumero;
                teste.DisciplinaTeste.Nome = null;
            }

            return teste;
        }

        private List<Teste> LerTestes(SqlDataReader leitor)
        {
            List<Teste> testes = new();

            while (leitor.Read())
            {
                int numero = Convert.ToInt32(leitor["NUMERO"]);
                string numeroQuestoesString = Convert.ToString(leitor["NUMEROQUESTOES"]);
                string prova = Convert.ToString(leitor["PROVA"]);
                string gabaritoString = Convert.ToString(leitor["GABARITO"]);
                DateTime dataSetada = Convert.ToDateTime(leitor["DATA"]);

                int disciplinaTesteNumero = (int)leitor["DISCIPLINA_NUMERO"];
                //  nome da disciplina pega por fora

                EnumNumeroQuestoes numeroQuestoes = EnumNumeroQuestoes.cinco;

                switch (numeroQuestoesString)
                {
                    case "5":
                        numeroQuestoes = EnumNumeroQuestoes.cinco;
                        break;

                    case "10":
                        numeroQuestoes = EnumNumeroQuestoes.dez;
                        break;

                    case "15":
                        numeroQuestoes = EnumNumeroQuestoes.quinze;
                        break;

                    case "20":
                        numeroQuestoes = EnumNumeroQuestoes.vinte;
                        break;

                    default:
                        break;
                }

                var teste = new Teste()
                {
                    Numero = numero,
                    NumeroQuestoes = numeroQuestoes,
                    Prova = prova,
                    gabarito = gabaritoString,
                    data = dataSetada
                };

                teste.DisciplinaTeste.Numero = disciplinaTesteNumero;
                teste.DisciplinaTeste.Nome = null;

                testes.Add(teste);
            }

            return testes;
        }

        #endregion
    }
}
