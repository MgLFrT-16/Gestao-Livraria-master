using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using static System.Console;
using LP1_Livraria.Menus;

namespace LP1_Livraria
{
    public class Caixa
    {
        public static void MenuCaixa()
        {
            Title = "Menu Caixa";
            MenuCaixaMetodo();

        }

        public static void MenuCaixaMetodo()
        {
            while (true)
            {
                string prompt = @"

   _____      _           
  / ____|    (_)          
 | |     __ _ ___  ____ _ 
 | |    / _` | \ \/ / _` |
 | |___| (_| | |>  < (_| |
  \_____\__,_|_/_/\_\__,_|
                                                    

Bem Vindo qual das opções deseja selecionar ? ";
                ForegroundColor = ConsoleColor.White;
                BackgroundColor = ConsoleColor.Black;

                string[] options = { "Vender Livro", "Listar livros pelo autor", "Listar livros pelo género", "Enviar Mensagem", "Voltar para o menu principal" };
                NovoMenuCaixa mainMenu = new NovoMenuCaixa(prompt, options);
                int SelectedCaixa = mainMenu.Run4();

                try
                {
                    switch (SelectedCaixa)
                    {
                        case 0:
                            VenderLivro();
                            break;

                        case 1:
                            ListarLivrosAutor();
                            break;

                        case 2:
                            ListarLivrosGenero();
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
            string prompt = "Escolher Destinatário da Mensagem ";
            ForegroundColor = ConsoleColor.White;
            BackgroundColor = ConsoleColor.Black;

            string[] options = { "Gerente", "Repositor", "Chat Geral" };
            MenuEnviarMensagemCaixa mainMenu = new MenuEnviarMensagemCaixa(prompt, options);
            int SelectedMensagemCaixa = mainMenu.Run6();

            Console.Clear();
            try
            {
                string remetente = "Caixa" + ((Login.UtilizadorAutenticado != null) ? " " + Login.UtilizadorAutenticado.Nome : " Desconhecido");
                string destinatario = (SelectedMensagemCaixa == 0) ? "Gerente" : (SelectedMensagemCaixa == 1) ? "Repositor" : (SelectedMensagemCaixa == 2) ? "Chat Geral" : "";

                ExibirMensagens(remetente, destinatario);

                bool continuarEnviando = true;

                while (continuarEnviando)
                {
                    Console.Write($"\nEscreva a mensagem para {destinatario}: ");
                    string mensagem = Console.ReadLine();

                    Mensagens.EnviarMensagem(remetente, destinatario, mensagem);

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Mensagem enviada com sucesso!");
                    Console.ResetColor();

                    Console.Write("\nPressione Enter para enviar outra mensagem ou digite 'exit' para sair: ");
                    string resposta = Console.ReadLine()?.Trim().ToLower();
                    continuarEnviando = resposta != "exit";
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\nAperte ENTER para voltar ao menu!");

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
                Console.WriteLine($"Erro Mensagem Inválida!");
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
                if ((remetente == ("Caixa" + ((Login.UtilizadorAutenticado != null) ? " " + Login.UtilizadorAutenticado.Nome : " Desconhecido")) && destinatario == "Gerente") ||
                (remetente == "Gerente" && destinatario == ("Caixa" + ((Login.UtilizadorAutenticado != null) ? " " + Login.UtilizadorAutenticado.Nome : " Desconhecido"))))
                {
                    return $"..\\..\\Mensagens\\Mensagens_Gerente_Caixa.txt";
                }
                else if ((remetente == ("Caixa" + ((Login.UtilizadorAutenticado != null) ? " " + Login.UtilizadorAutenticado.Nome : " Desconhecido")) && destinatario == "Repositor") ||
                (remetente == "Repositor" && destinatario == ("Caixa" + ((Login.UtilizadorAutenticado != null) ? " " + Login.UtilizadorAutenticado.Nome : " Desconhecido"))))
                {
                    return $"..\\..\\Mensagens\\Mensagens_Repositor_Caixa.txt";
                }
                else if ((remetente == ("Caixa" + ((Login.UtilizadorAutenticado != null) ? " " + Login.UtilizadorAutenticado.Nome : " Desconhecido")) && destinatario == "Chat Geral"))
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


        public static void VenderLivro()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.Clear();

            // Ler todos os livros do arquivo
            string caminhoFicheiro = "..\\..\\Livros\\Livros.txt";
            List<string> linhas = new List<string>(File.ReadAllLines(caminhoFicheiro));

            // Verificar se há livros registrados
            if (linhas.Count > 0)
            {
                try
                {
                    Console.WriteLine("Venda de Livros:\n");

                    // Lista os livros disponíveis
                    ListarLivros();

                    // Solicita os códigos dos livros
                    Console.WriteLine("\nIntroduza os códigos dos livros para vender (separados por espaços):");
                    string[] codigosInput = Console.ReadLine().Split(' ');

                    double totalVenda = 0;
                    double totalIVA = 0;
                    int totalLivrosVendidos = 0;

                    foreach (string codigoInput in codigosInput)
                    {
                        int codigo;
                        if (int.TryParse(codigoInput, out codigo))
                        {
                            bool livroEncontrado = false;

                            // Loop até que o livro seja encontrado ou a entrada seja concluída
                            while (!livroEncontrado)
                            {
                                int indexLivro = linhas.FindIndex(l => l.StartsWith($"{codigo}|"));

                                if (indexLivro != -1)
                                {
                                    string[] detalhesLivro = linhas[indexLivro].Split('|');

                                    Console.WriteLine($"\nDetalhes do Livro (Código: {codigo}):");
                                    Console.WriteLine($"Título: {detalhesLivro[1]}");
                                    Console.WriteLine($"Autor: {detalhesLivro[2]}");
                                    Console.WriteLine($"ISBN: {detalhesLivro[3]}");
                                    Console.WriteLine($"Género: {detalhesLivro[4]}");
                                    Console.WriteLine($"Preço: {detalhesLivro[5]}€");
                                    Console.WriteLine($"Stock: {detalhesLivro[7]}");
                                    Console.WriteLine();
                                    Console.Write("Quantidade desejada: ");

                                    if (int.TryParse(Console.ReadLine(), out int quantidadeDesejada))
                                    {
                                        int estoqueAtual = int.Parse(detalhesLivro[7].Trim());

                                        if (quantidadeDesejada <= estoqueAtual)
                                        {
                                            double precoUnitario = double.Parse(detalhesLivro[5].Trim());
                                            double precoVenda = precoUnitario * quantidadeDesejada;
                                            double iva = double.Parse(detalhesLivro[6].Trim()) * quantidadeDesejada;

                                            totalVenda += precoVenda;
                                            totalIVA += iva;
                                            totalLivrosVendidos += quantidadeDesejada;

                                            Console.WriteLine($"IVA (23%): {iva}€");
                                            Console.WriteLine($"Subtotal: {precoVenda}€");

                                            // Atualiza o estoque
                                            linhas[indexLivro] = $"{detalhesLivro[0]}|{detalhesLivro[1]}|{detalhesLivro[2]}|{detalhesLivro[3]}|{detalhesLivro[4]}|{detalhesLivro[5]}|{detalhesLivro[6]}|{estoqueAtual - quantidadeDesejada}";
                                            File.WriteAllLines(caminhoFicheiro, linhas);

                                            livroEncontrado = true;
                                        }
                                        else
                                        {
                                            Console.ForegroundColor = ConsoleColor.Red;
                                            Console.WriteLine("Erro: Quantidade desejada superior ao estoque disponível.");
                                            Console.ForegroundColor = ConsoleColor.White;
                                        }
                                    }
                                    else
                                    {
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine("Erro: Quantidade inválida.");
                                        Console.ForegroundColor = ConsoleColor.White;
                                    }
                                }
                                else
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine($"Erro: Livro com código {codigo} não encontrado.");
                                    livroEncontrado = true;  // Para sair do loop se o livro não for encontrado
                                    Console.ForegroundColor = ConsoleColor.White;
                                }
                                File.AppendAllText("..\\..\\Logs\\RegistoVendas.txt", $"{DateTime.Now}|Código: {codigo}|Preço: {totalVenda}|IVA: {totalIVA}|Quantidade: {totalLivrosVendidos}|Vendido por: Caixa {Login.UtilizadorAutenticado.Nome}\n");
                            }
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"Erro: Código inválido.");
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                    }

                    if (totalVenda > 50)
                    {
                        double desconto = totalVenda * 0.10;
                        totalVenda -= desconto;
                        Console.WriteLine($"\nTotal de Livros Vendidos: {totalLivrosVendidos}");
                        Console.WriteLine($"Total do IVA: {totalIVA}€");
                        Console.WriteLine($"Desconto (10%): -{desconto}€");
                        Console.WriteLine($"Total da Venda: {totalVenda}€");
                    }
                    else
                    {
                        Console.WriteLine($"\nTotal de Livros Vendidos: {totalLivrosVendidos}");
                        Console.WriteLine($"Total do IVA: {totalIVA}€");
                        Console.WriteLine($"Total da Venda: {totalVenda}€");
                    }

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Venda realizada com sucesso!");

                    Console.ReadKey();
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Erro ao realizar a venda: {ex.Message}");
                    Console.ReadKey();
                    Console.Clear();
                    return;
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Não há livros registrados para realizar a venda.");
                Console.ReadKey();
                return;
            }
        }



        public static void ListarLivros()
        {
            try
            {
                string[] linhas = File.ReadAllLines("..\\..\\Livros\\Livros.txt");

                Console.WriteLine("Lista de Livros Disponíveis:\n");

                foreach (string linha in linhas)
                {
                    // Dividir a linha usando o caractere "|" como separador
                    string[] dadosLivro = linha.Split('|');

                    // Certificar-se de que há informações suficientes
                    if (dadosLivro.Length >= 8)
                    {
                        // Extrair o código do livro
                        string codigoLivro = dadosLivro[0].Trim();

                        // Verificar se o código é um número válido
                        if (int.TryParse(codigoLivro, out int codigo))
                        {
                            int estoque;
                            if (int.TryParse(dadosLivro[7].Replace("Stock: ", "").Trim(), out estoque))
                            {
                                if (estoque > 0)
                                {
                                    Console.WriteLine($"Código: {codigo}");
                                    Console.WriteLine($"Título: {dadosLivro[1].Trim()}");
                                    Console.WriteLine($"Autor: {dadosLivro[2].Trim()}");
                                    Console.WriteLine($"ISBN: {dadosLivro[3].Trim()}");
                                    Console.WriteLine($"Gênero: {dadosLivro[4].Trim()}");
                                    Console.WriteLine($"Preço: {dadosLivro[5].Trim()}€");
                                    Console.WriteLine($"Stock: {estoque}\n");
                                }
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine($"Erro: Estoque inválido - {dadosLivro[7]}");
                            }
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"Erro: Código inválido - {codigoLivro}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Erro ao listar livros: {ex.Message}");
            }
        }

        public static void ListarLivrosAutor()
        {
            try
            {
                Console.Clear();
                Console.WriteLine("Introduza o nome do autor que deseja listar os livros:");
                string autorInput = Console.ReadLine();

                string[] linhas = File.ReadAllLines("..\\..\\Livros\\Livros.txt");

                Console.Clear();
                Console.WriteLine($"Livros do autor {autorInput}:\n");

                bool encontrouLivros = false;

                foreach (string linha in linhas)
                {
                    string[] dadosLivro = linha.Split('|');

                    // Verifica se há informações suficientes para um livro
                    if (dadosLivro.Length >= 8)
                    {
                        // Verifica se o autor na linha atual corresponde ao autor introduzido
                        if (dadosLivro[2].Equals(autorInput, StringComparison.OrdinalIgnoreCase))
                        {
                            Console.WriteLine($"Título: {dadosLivro[1]}\n");
                            encontrouLivros = true;
                        }
                    }
                }

                if (!encontrouLivros)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Erro: Nenhum livro encontrado para o autor {autorInput}.");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Erro ao listar livros por autor: {ex.Message}");
                Console.ForegroundColor = ConsoleColor.White;
            }

            Console.ReadKey();
            Console.Clear();
        }



        public static void ListarLivrosGenero()
        {
            try
            {
                Console.Clear();
                Console.WriteLine("Introduza o género de livro que deseja listar:");
                string generoInput = Console.ReadLine().Trim(); // Remova espaços extras

                string[] linhas = File.ReadAllLines("..\\..\\Livros\\Livros.txt");

                Console.Clear();
                Console.WriteLine($"Livros do género {generoInput}:\n");

                bool encontrouLivros = false;

                foreach (string linha in linhas)
                {
                    string[] dadosLivro = linha.Split('|');

                    // Verifica se há informações suficientes para um livro
                    if (dadosLivro.Length >= 8) // Modifiquei para 8, pois parece ser o número correto de campos
                    {
                        // Verifica se o género na linha atual corresponde ao género introduzido
                        if (dadosLivro[4].Trim().Equals(generoInput, StringComparison.OrdinalIgnoreCase)) // Remova espaços extras
                        {
                            Console.WriteLine($"Título: {dadosLivro[1]}");
                            Console.WriteLine($"Autor: {dadosLivro[2]}\n");
                            encontrouLivros = true;
                        }
                    }
                }

                if (!encontrouLivros)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Erro: Nenhum livro encontrado para o género {generoInput}.");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Erro ao listar livros por género: {ex.Message}");
                Console.ForegroundColor = ConsoleColor.White;
            }

            Console.ReadKey();
            Console.Clear();
        }
    }
}
