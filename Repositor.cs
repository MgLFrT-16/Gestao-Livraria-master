using System;
using System.IO;
using System.Linq;
using static System.Console;
using System.Collections.Generic;
using LP1_Livraria.Menus;

namespace LP1_Livraria
{
    public class Repositor
    {
        public static void MenuRepositor()
        {
            Title = "Repositor";
            MenuRepositorMetodo();
        }

        public static void MenuRepositorMetodo()
        {
            while (true)
            {
                string prompt = @"

  _____                      _ _             
 |  __ \                    (_) |            
 | |__) |___ _ __   ___  ___ _| |_ ___  _ __ 
 |  _  // _ \ '_ \ / _ \/ __| | __/ _ \| '__|
 | | \ \  __/ |_) | (_) \__ \ | || (_) | |   
 |_|  \_\___| .__/ \___/|___/_|\__\___/|_|   
            | |                              
            |_|                              


Bem Vindo qual das opções deseja selecionar?";
                ForegroundColor = ConsoleColor.White;
                BackgroundColor = ConsoleColor.Black;

                string[] options = { "Consultar Stock", "Adicionar Stock", "Registar Livro", "Enviar Mensagem", "Voltar para o menu principal" };
                NovoMenuRepositor mainMenu = new NovoMenuRepositor(prompt, options);
                int SelectedRepositor = mainMenu.Run3();

                try
                {

                    switch (SelectedRepositor)
                    {
                        case 0:
                            ConsultarStock();
                            break;

                        case 1:
                            AdicionarStock();
                            break;

                        case 2:
                            RegistarLivro();
                            break;

                        case 3:
                            EnviarMensagem();
                            break;

                        case 4:
                            Console.Clear();
                            return; // sai do método 

                        default:
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Erro: Escolha inválida.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Erro: {ex.Message}");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
        }

        public static void EnviarMensagem()
        {
            Title = "Enviar Mensagem";
            Console.Clear();
            string prompt = "Escolher Destinatário da Mensagem! ";
            ForegroundColor = ConsoleColor.White;
            BackgroundColor = ConsoleColor.Black;

            string[] options = { "Gerente", "Caixa", "Chat Geral" };
            MenuEnviarMensagemRepositor mainMenu = new MenuEnviarMensagemRepositor(prompt, options);
            int SelectedMensagemRepositor = mainMenu.Run9();

            Console.Clear();
            try
            {
                string remetente = "Repositor" + ((Login.UtilizadorAutenticado != null) ? " " + Login.UtilizadorAutenticado.Nome : " Desconhecido");
                string destinatario = (SelectedMensagemRepositor == 0) ? "Gerente" : (SelectedMensagemRepositor == 1) ? "Caixa" : (SelectedMensagemRepositor == 2) ? "Chat Geral" : "";

                ExibirMensagens(remetente, destinatario);

                bool continuarEnviando = true;

                while (continuarEnviando)
                {
                    Console.Write($"Escreva a mensagem para {destinatario}: ");
                    string mensagem = Console.ReadLine();

                    Mensagens.EnviarMensagem(remetente, destinatario, mensagem);

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Mensagem enviada com sucesso!");
                    Console.ResetColor();

                    Console.Write("Pressione Enter para enviar outra mensagem ou digite 'exit' para sair: ");
                    string resposta = Console.ReadLine()?.Trim().ToLower();
                    continuarEnviando = resposta != "exit";
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("\nAperte ENTER para regressar ao menu!");
                    Console.ForegroundColor = ConsoleColor.White;

                    if (continuarEnviando)
                    {
                        Console.Clear();
                        ExibirMensagens(remetente, destinatario);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Erro: Mensagem Inválida!");
                Console.ReadKey();
                Console.Clear();
            }

            Console.ReadLine();
        }

        private static void ExibirMensagens(string remetente, string destinatario)
        {
            List<string> mensagens = Mensagens.LerMensagens(remetente, destinatario);

            Console.WriteLine(mensagens.Count > 0 ? "Mensagens existentes:" : "Não há mensagens existentes.");

            foreach (var mensagemExistente in mensagens)
            {
                Console.WriteLine(mensagemExistente);
            }
        }


        public class Mensagens
        {
            public static void EnviarMensagem(string remetente, string destinatario, string mensagem)
            {
                string fileName = ObterNomeArquivo(remetente, destinatario);
                string mensagemFormatada = $"{DateTime.Now}: {remetente} diz para {destinatario} - {mensagem}";
                SalvarMensagem(fileName, mensagemFormatada);
            }

            public static List<string> LerMensagens(string remetente, string destinatario)
            {
                string fileName = ObterNomeArquivo(remetente, destinatario);

                try
                {
                    return new List<string>(File.ReadAllLines(fileName));
                }
                catch (FileNotFoundException)
                {
                    // Se o arquivo não existir, retorna uma lista vazia
                    return new List<string>();
                }
            }

            private static string ObterNomeArquivo(string remetente, string destinatario)
            {
                if ((remetente == ("Repositor" + ((Login.UtilizadorAutenticado != null) ? " " + Login.UtilizadorAutenticado.Nome : " Desconhecido")) && destinatario == "Gerente") ||
                (remetente == "Gerente" && destinatario == ("Repositor" + ((Login.UtilizadorAutenticado != null) ? " " + Login.UtilizadorAutenticado.Nome : " Desconhecido"))))
                {
                    return $"..\\..\\Mensagens\\Mensagens_Gerente_Repositor.txt";
                }
                else if ((remetente == ("Repositor" + ((Login.UtilizadorAutenticado != null) ? " " + Login.UtilizadorAutenticado.Nome : " Desconhecido")) && destinatario == "Caixa") ||
                (remetente == "Caixa" && destinatario == ("Repositor" + ((Login.UtilizadorAutenticado != null) ? " " + Login.UtilizadorAutenticado.Nome : " Desconhecido"))))
                {
                    return $"..\\..\\Mensagens\\Mensagens_Gerente_Caixa.txt";
                }
                else if ((remetente == ("Repositor" + ((Login.UtilizadorAutenticado != null) ? " " + Login.UtilizadorAutenticado.Nome : " Desconhecido")) && destinatario == "Chat Geral"))
                {
                    return $"..\\..\\Mensagens\\Mensagens_Chat_Geral.txt";
                }
                else
                {
                    return $"..\\..\\Mensagens\\Mensagens_{remetente}_{destinatario}.txt";
                }
            }

            public static void SalvarMensagem(string fileName, string mensagem)
            {
                try
                {
                    // Adiciona a mensagem ao arquivo de mensagens
                    File.AppendAllText(fileName, $"{mensagem}\n");
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Erro ao salvar mensagem: {ex.Message}");
                    Console.ResetColor();
                }
            }
        }

        public static void RegistarLivro()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.Clear();
            Console.Title = "Registar Livro";

            string caminhoFicheiro = "..\\..\\Livros\\Livros.txt";

            try
            {
                string novaLinha;
                bool continuarRegisto = true;

                // Carregar as linhas do arquivo para uma lista
                List<string> linhasExistentes = new List<string>();
                if (File.Exists(caminhoFicheiro))
                {
                    linhasExistentes.AddRange(File.ReadAllLines(caminhoFicheiro));
                }

                while (continuarRegisto)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("Código do livro (insira o código 0 para terminar): ");
                    if (!int.TryParse(Console.ReadLine(), out int codigo))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Erro: O código do livro deve ser um número inteiro.");
                        Console.ReadKey();
                        return;
                    }

                    // Se o código for 0, termina o registo
                    if (codigo == 0)
                    {
                        continuarRegisto = false;
                        continue;
                    }

                    // Verificar se o código já existe na lista de linhas
                    if (linhasExistentes.Exists(linha => linha.StartsWith($"{codigo}|")))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Erro: Este código já foi utilizado. Escolha um código diferente.\n");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("Aperte ENTER para digitar um código novo!");
                        Console.ReadKey();
                        Console.Clear();
                        continue;
                    }

                    Console.Write("\nNome do livro: ");
                    string nome = Console.ReadLine();

                    Console.Write("Autor: ");
                    string autor = Console.ReadLine();

                    Console.Write("ISBN: ");
                    if (!long.TryParse(Console.ReadLine(), out long isbn))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Erro: O ISBN do livro deve ser um número longo.");
                        Console.ReadKey();
                        return;
                    }

                    Console.Write("Género: ");
                    string genero = Console.ReadLine();

                    Console.Write("Preço final: € ");
                    if (!double.TryParse(Console.ReadLine(), out double precoFinal))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Erro: O preço final do livro deve ser um número decimal.");
                        Console.ReadKey();
                        return;
                    }

                    double precoIVA = precoFinal * 0.23;
                    precoIVA = Math.Round(precoIVA, 2);
                    Console.Write($"Preço do IVA (23%): € {precoIVA}\n");

                    Console.Write("Quantidade em stock: ");
                    if (!int.TryParse(Console.ReadLine(), out int quantidadeStock))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Erro: A quantidade em stock do livro deve ser um número inteiro.");
                        Console.ReadKey();
                        return;
                    }

                    // Adicionando a nova linha à lista de linhas existentes
                    novaLinha = $"{codigo}|{nome}|{autor}|{isbn}|{genero}|{precoFinal}|{precoIVA}|{quantidadeStock}";
                    linhasExistentes.Add(novaLinha);

                    // Escrevendo as linhas atualizadas de volta para o arquivo

                    File.WriteAllLines(caminhoFicheiro, linhasExistentes);

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Novo livro registado com sucesso!\n");
                    Console.WriteLine("Aperte ENTER para digitar um código novo!");
                    File.AppendAllText("..\\..\\Logs\\RegistoRepositor.txt", $"{DateTime.Now}|Código: {codigo}|Título: {nome}|Autor: {autor}|ISBN: {isbn}|Género: {genero}|Preço: {precoFinal}|IVA: {precoIVA}|Stock: {quantidadeStock}|Adicionado por: Repositor {Login.UtilizadorAutenticado.Nome}\n");
                    Console.ReadKey();
                    Console.Clear();
                }

                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Erro ao registar livros: {ex.Message}");
                Console.ReadKey();
                Console.Clear();
            }
        }

        public static void AdicionarStock()
        {
            Console.Clear();

            Console.Write("Introduza o código do livro para adicionar stock: ");
            if (!int.TryParse(Console.ReadLine(), out int codigoLivro))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Erro: Por favor, insira um código de livro válido (número inteiro).");
                Console.ReadKey();
                return;
            }

            string caminhoFicheiro = "..\\..\\Livros\\Livros.txt";

            try
            {
                string[] linhas = File.ReadAllLines(caminhoFicheiro);

                bool livroEncontrado = false;

                for (int i = 0; i < linhas.Length; i++)
                {
                    string[] dadosLivro = linhas[i].Split('|');

                    // Verifica se o código do livro corresponde ao código fornecido
                    if (dadosLivro.Length > 0 && int.TryParse(dadosLivro[0], out int codigo) && codigo == codigoLivro)
                    {
                        livroEncontrado = true;

                        // Exibe as informações do livro, incluindo o stock
                        Console.WriteLine($"Livro: {dadosLivro[1].Trim()}\nStock atual: {dadosLivro[7].Trim()}");

                        Console.Write("Quantidade a adicionar ao stock: ");
                        if (int.TryParse(Console.ReadLine(), out int quantidadeAdicionar))
                        {
                            int stockAtual = int.Parse(dadosLivro[7].Trim());
                            int novoStock = stockAtual + quantidadeAdicionar;

                            // Atualiza a linha do stock no array
                            dadosLivro[7] = novoStock.ToString();

                            // Atualiza o ficheiro com as novas linhas
                            linhas[i] = string.Join("|", dadosLivro);
                            File.WriteAllLines(caminhoFicheiro, linhas);

                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine($"\nStock atualizado com sucesso. Novo stock: {novoStock}");
                            File.AppendAllText("..\\..\\Logs\\RegistoRepositor.txt", $"{DateTime.Now}| No código {codigoLivro}, foram adicionados {quantidadeAdicionar} unidades, dando no total {novoStock} unidades no stock. Foram adicionados pelo Repositor {Login.UtilizadorAutenticado.Nome}\n");
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Erro: A quantidade a adicionar deve ser um número inteiro.");
                        }

                        break;
                    }
                }

                if (!livroEncontrado)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Erro: Livro com código {codigoLivro} não encontrado.");
                }

                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Erro ao adicionar stock: {ex.Message}");
                Console.ReadKey();
                Console.Clear();
            }
        }


        public static void ConsultarStock()
        {
            Console.Clear();

            Console.Write("Introduza o código do livro que deseja consultar stock: ");
            if (!int.TryParse(Console.ReadLine(), out int codigoLivro))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Erro: Por favor, insira um código de livro válido (número inteiro).");
                Console.ReadKey();
                return;
            }

            string caminhoFicheiro = "..\\..\\Livros\\Livros.txt";

            try
            {
                string[] linhas = File.ReadAllLines(caminhoFicheiro);

                bool livroEncontrado = false;

                for (int i = 0; i < linhas.Length; i++)
                {
                    string[] dadosLivro = linhas[i].Split('|');

                    // Verifica se o código do livro corresponde ao código fornecido
                    if (dadosLivro.Length > 0 && int.TryParse(dadosLivro[0], out int codigo) && codigo == codigoLivro)
                    {
                        livroEncontrado = true;

                        // Exibe as informações do livro, incluindo o stock
                        Console.WriteLine($"Livro: {dadosLivro[1].Trim()}\nStock: {dadosLivro[7].Trim()}");
                        break;
                    }
                }

                if (!livroEncontrado)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Erro: Livro com código {codigoLivro} não encontrado.");
                }

                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Erro ao consultar o stock: {ex.Message}");
                Console.ReadKey();
                Console.Clear();
            }
        }
    }
}
