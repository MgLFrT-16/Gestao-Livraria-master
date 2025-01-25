using System;
using System.IO;

namespace LP1_Livraria
{
    public class Login
    {
        public class Utilizador
        {
            public string Nome { get; set; }
            public Cargo Cargo { get; set; }

            public Utilizador(string nome, Cargo cargo)
            {
                Nome = nome;
                Cargo = cargo;
            }
        }

        public static Utilizador UtilizadorAutenticado { get; private set; }

        public enum Cargo
        {
            Desconhecido,
            Gerente,
            Caixa,
            Repositor
        }

        public static Utilizador VerificarLogin(string utilizador, string password)
        {
            string caminhoArquivo = "..\\..\\Funcionarios\\DadosUtilizadores.txt";

            try
            {
                if (!File.Exists(caminhoArquivo))
                {
                    return null;
                }

                string[] linhas = File.ReadAllLines(caminhoArquivo);

                foreach (string linha in linhas)
                {
                    string[] dados = linha.Split(',');

                    if (dados.Length == 3 && dados[0] == utilizador && dados[1] == password)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Login bem sucedido! Pressione ENTER para continuar.");
                        Console.ReadKey();

                        if (Enum.TryParse(dados[2], out Cargo cargo))
                        {
                            UtilizadorAutenticado = new Utilizador(dados[0], cargo);
                            return UtilizadorAutenticado;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Erro: Cargo inválido encontrado.");
                            return null;
                        }
                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Erro ao verificar dados de login: {ex.Message}");
                Console.ReadKey();
                Console.Clear();
                return null;
            }
        }
    }
}
