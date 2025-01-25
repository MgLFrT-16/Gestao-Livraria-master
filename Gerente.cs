using LP1_Livraria.Menus;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using static System.Console;

namespace LP1_Livraria
{
    public class Gerente
    {
        public static void MenuGerenteNovo()
        {
            Title = "Menu Gerente";
            MenuGerenteMetodo();

        }

        // Método para criar o menu do Gerente
        private static void MenuGerenteMetodo()
        {
            // Ciclo para continuar a correr o menu até alguma opção ser selecionada
            while (true)
            {
                string prompt = @"

   _____                     _       
  / ____|                   | |      
 | |  __  ___ _ __ ___ _ __ | |_ ___ 
 | | |_ |/ _ \ '__/ _ \ '_ \| __/ _ \
 | |__| |  __/ | |  __/ | | | ||  __/
  \_____|\___|_|  \___|_| |_|\__\___|
                                     
                                     

Bem Vindo qual das opções deseja selecionar?";
                ForegroundColor = ConsoleColor.White;
                BackgroundColor = ConsoleColor.Black;

                string[] options = { "Criar novo funcionário", "Eliminar funcionário", "Listar funcionários", "Mudar Credenciais dos Funcionários", "Vender livro", "Enviar Mensagem", "Ver Registo Auditoria", "Visualizar Vendas", "Ver Registos de Stock/Livros", "Voltar para o menu principal", }; // Guarda as opções do menu num array
                NovoMenuGerente mainMenu = new NovoMenuGerente(prompt, options);
                int SelectedGerente = mainMenu.Run();

                try
                {
                    // Switch com os métodos de cada opção do menu
                    switch (SelectedGerente)
                    {
                        case 0:
                            CriarFuncionario();
                            break;

                        case 1:
                            EliminarFuncionario();
                            break;

                        case 2:
                            ListarFuncionarios();
                            break;

                        case 3:
                            EditarFuncionario();
                            break;

                        case 4:
                            VenderLivro();
                            break;

                        case 5:
                            EnviarMensagem();
                            break;

                        case 6:
                            ExibirRegistrosLog();
                            break;

                        case 7:
                            ExibirRegistrosVendas();
                            break;

                        case 8:
                            ExibirRegistrosRepositor();
                            break;

                        case 9:
                            Console.Clear();
                            return; // Sai do método

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

            string prompt = "Escolher Destinatário da Mensagem! ";
            ForegroundColor = ConsoleColor.White;
            BackgroundColor = ConsoleColor.Black;

            string[] options = { "Repositor", "Caixa", "Chat Geral" };
            MenuEnviarMensagemGerente mainMenu = new MenuEnviarMensagemGerente(prompt, options);
            int SelectedMensagemGerente = mainMenu.Run7();

            Console.Clear();
            try
            {
                string remetente = "Gerente" + ((Login.UtilizadorAutenticado != null) ? " " + Login.UtilizadorAutenticado.Nome : " Desconhecido");
                string destinatario = (SelectedMensagemGerente == 0) ? "Repositor" : (SelectedMensagemGerente == 1) ? "Caixa" : (SelectedMensagemGerente == 2) ? "Chat Geral" : "";

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
                    Console.Write("\nAperte ENTER para regressar ao menu!");

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
                Console.WriteLine("Opção inválida. Mensagem não enviada.");
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
                if ((remetente == ("Gerente" + ((Login.UtilizadorAutenticado != null) ? " " + Login.UtilizadorAutenticado.Nome : " Desconhecido")) && destinatario == "Repositor") ||
                (remetente == "Repositor" && destinatario == ("Gerente" + ((Login.UtilizadorAutenticado != null) ? " " + Login.UtilizadorAutenticado.Nome : " Desconhecido"))))
                {
                    return $"..\\..\\Mensagens\\Mensagens_Gerente_Repositor.txt";
                }
                else if ((remetente == ("Gerente" + ((Login.UtilizadorAutenticado != null) ? " " + Login.UtilizadorAutenticado.Nome : " Desconhecido")) && destinatario == "Caixa") ||
                (remetente == "Caixa" && destinatario == ("Gerente" + ((Login.UtilizadorAutenticado != null) ? " " + Login.UtilizadorAutenticado.Nome : " Desconhecido"))))
                {
                    return $"..\\..\\Mensagens\\Mensagens_Gerente_Caixa.txt";
                }
                else if ((remetente == ("Gerente" + ((Login.UtilizadorAutenticado != null) ? " " + Login.UtilizadorAutenticado.Nome : " Desconhecido")) && destinatario == "Chat Geral"))
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

        public static void ExibirRegistrosLog()
        {
            Title = "Registo de Logs";

            try
            {
                string caminhoFicheiroLog = "..\\..\\Logs\\Log.txt";
                if (File.Exists(caminhoFicheiroLog))
                {
                    Console.Clear();
                    Console.WriteLine("Registros do Log:");

                    string[] registros = File.ReadAllLines(caminhoFicheiroLog);

                    foreach (var registro in registros)
                    {
                        Console.WriteLine(registro);
                    }
                }
                else
                {
                    Console.WriteLine("O arquivo de log não existe ou está vazio.");
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Erro ao exibir registros do log: {ex.Message}");
                Console.ResetColor();
            }

            Console.ReadLine(); // Aguarda a entrada do usuário antes de retornar ao menu
        }

        public static void RegistrarLog(string mensagem)
        {
            try
            {
                string caminhoFicheiroLog = "..\\..\\Logs\\Log.txt";
                string logEntry = $"{DateTime.Now}: {mensagem}{Environment.NewLine}";
                File.AppendAllText(caminhoFicheiroLog, logEntry);
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Erro ao registrar log: {ex.Message}");
                Console.ResetColor();
            }
        }


        // Método para criar funcionário
        public static void CriarFuncionario()
        {
            Title = "Criar Funcionário";

            string gerente = "Gerente" + ((Login.UtilizadorAutenticado != null) ? " " + Login.UtilizadorAutenticado.Nome : " Desconhecido");
            Console.Clear();

            string utilizador;
            do
            {
                Console.Write("Nome: ");
                utilizador = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(utilizador))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Erro: O nome não pode estar em branco. Por favor, digite um nome válido.");
                    Console.ForegroundColor = ConsoleColor.White;

                }
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\nAperte ENTER para regressar ao menu!");
                Console.ForegroundColor = ConsoleColor.White;
                Console.ReadKey();
                Console.Clear();

            } while (string.IsNullOrWhiteSpace(utilizador));

            string password;
            do
            {
                Console.Write("Password: ");
                password = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(password))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Erro: A password não pode estar em branco. Por favor, digite uma password válida.");

                }
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\nAperte ENTER para regressar ao menu!");
                Console.ForegroundColor = ConsoleColor.White;
                Console.ReadKey();
                Console.Clear();

            } while (string.IsNullOrWhiteSpace(password));

            string cargo;
            do
            {
                Console.Write("Cargo (Gerente, Repositor, Caixa): ");
                cargo = Console.ReadLine();

                if (!ValidarCargo(cargo))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Erro: Por favor, digite um cargo válido (Gerente, Repositor ou Caixa).");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\nAperte ENTER para regressar ao menu!");
                Console.ReadKey();
                Console.Clear();

            } while (!ValidarCargo(cargo));

            string novaLinha = $"{utilizador},{password},{cargo}";

            string caminhoFicheiro = "..\\..\\Funcionarios\\DadosUtilizadores.txt";
            File.AppendAllText(caminhoFicheiro, novaLinha + Environment.NewLine);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nNovo funcionário criado com sucesso!");
            RegistrarLog($"Novo funcionário criado | Nome: {utilizador}, Cargo: {cargo}, Criado por: {gerente}");
            Console.ReadLine();
        }

        // Método para eliminar funcionário
        public static void EliminarFuncionario()
        {
            Title = "Eliminar Funcionário";

            string gerente = "Gerente" + ((Login.UtilizadorAutenticado != null) ? " " + Login.UtilizadorAutenticado.Nome : " Desconhecido");
            Console.Clear();
            Console.WriteLine("Eliminar funcionário:");

            string caminhoFicheiro = "..\\..\\Funcionarios\\DadosUtilizadores.txt";
            List<string> linhas = File.ReadAllLines(caminhoFicheiro).ToList();

            Console.WriteLine("ID - Dados do Funcionário");
            for (int i = 0; i < linhas.Count; i++)
            {
                if (!string.IsNullOrEmpty(linhas[i]))
                {
                    Console.WriteLine($"{i + 1} - {linhas[i]}");
                }
            }

            Console.Write("\nID do funcionário a ser removido: ");

            if (!int.TryParse(Console.ReadLine(), out int numeroLinha))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Erro: Por favor, insira um ID válido.");
                Console.ReadLine();
                return;
            }

            if (numeroLinha >= 1 && numeroLinha <= linhas.Count)
            {
                // Encontrar a linha real correspondente
                int linhaReal = 0;
                for (int i = 0; i < linhas.Count; i++)
                {
                    if (!string.IsNullOrEmpty(linhas[i]))
                    {
                        linhaReal++;
                        if (linhaReal == numeroLinha)
                        {
                            RegistrarLog($"Funcionário removido | ID: {numeroLinha}, Removido por: {gerente}");
                            linhas.RemoveAt(i);
                            break;
                        }
                    }
                }

                File.WriteAllLines(caminhoFicheiro, linhas);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\nFuncionário removido com sucesso!");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nNúmero de linha inválido. Funcionário não removido.");
            }

            Console.ReadLine();
        }



        // Método para listar todos os funcionários
        public static void ListarFuncionarios()
        {
            Console.Clear();
            Title = "Lista de Funcionários";

            string caminhoFicheiro = "..\\..\\Funcionarios\\DadosUtilizadores.txt"; // Caminho do ficheiro com as informações dos utilizadores

            try
            {
                string[] linhas = File.ReadAllLines(caminhoFicheiro); // Guarda num array todas as linhas do ficheiro

                // If para verificar se o ficheiro está vazio, se não, mostra uma mensagem de erro
                if (linhas.Length > 0)
                {
                    foreach (string linha in linhas) // Percorre todas as linhas do ficheiro
                    {
                        string[] dados = linha.Split(','); // Guarda num array as informações sem as vírgulas

                        // If para verificar se todos os valores estão preenchidos
                        if (dados.Length == 3)
                        {
                            Console.WriteLine($"Nome: {dados[0]}, Password: {dados[1]}, Cargo: {dados[2]}");

                        }
                    }
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\nAperte ENTER para regressar ao menu!");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Não há funcionários registados.");
                }

                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Erro ao listar funcionários: {ex.Message}");
                Console.ReadLine();
            }
        }
        public static void EditarFuncionario()
        {
            Title = "Editar Funcionário";

            string gerente = "Gerente" + ((Login.UtilizadorAutenticado != null) ? " " + Login.UtilizadorAutenticado.Nome : " Desconhecido");
            Console.Clear();
            Console.WriteLine("Editar funcionário:");

            string caminhoFicheiro = "..\\..\\Funcionarios\\DadosUtilizadores.txt";
            List<string> linhas = File.ReadAllLines(caminhoFicheiro).ToList();

            Console.WriteLine("ID - Dados do Funcionário");

            for (int i = 0; i < linhas.Count; i++)
            {
                if (!string.IsNullOrEmpty(linhas[i]))
                {
                    string[] dados = linhas[i].Split(',');

                    // Verifica se o funcionário NÃO tem o cargo "Gerente"
                    if (!(dados.Length >= 3 && dados[2].Equals("Gerente", StringComparison.OrdinalIgnoreCase)))
                    {
                        Console.WriteLine($"{i + 1} - {linhas[i]}");
                    }
                }
            }

            Console.Write("\nID do funcionário a ser modificado: ");
            if (!int.TryParse(Console.ReadLine(), out int numeroLinha))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Erro: Por favor, insira um ID válido.");
                Console.ReadLine();
                return;
            }

            if (numeroLinha >= 1 && numeroLinha <= linhas.Count)
            {
                string[] dados = linhas[numeroLinha - 1].Split(',');

                // Verifica se o funcionário NÃO tem o cargo "Gerente"
                if (!(dados.Length >= 3 && dados[2].Equals("Gerente", StringComparison.OrdinalIgnoreCase)))
                {
                    string prompt = "Qual das informações quer alterar?";
                    ForegroundColor = ConsoleColor.White;
                    BackgroundColor = ConsoleColor.Black;
                    string[] options = { "Nome", "Password", "Cargo" };
                    NovoMenuEditarFuncionario mainMenu = new NovoMenuEditarFuncionario(prompt, options);
                    int SelectedEditarFuncionario = mainMenu.Run8();

                    Console.Clear();
                    Console.WriteLine($"Funcionário Selecionado: Nome --> {dados[0]}, Password --> {dados[1]}, Cargo --> {dados[2]}");

                    try
                    {
                        switch (SelectedEditarFuncionario)
                        {
                            case 0:
                                Console.Write("Novo nome: ");
                                string novoNome = Console.ReadLine();
                                if (!novoNome.Equals(dados[0], StringComparison.OrdinalIgnoreCase))
                                {
                                    dados[0] = novoNome;
                                }
                                else
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("O novo nome é igual ao nome atual. Funcionário não modificado.");
                                    Console.ReadLine();
                                    return;
                                }
                                break;

                            case 1:
                                Console.Write("Nova password: ");
                                string novaPassword = Console.ReadLine();
                                if (!novaPassword.Equals(dados[1]))
                                {
                                    dados[1] = novaPassword;
                                }
                                else
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("A nova senha é igual à senha atual. Funcionário não modificado.");
                                    Console.ReadLine();
                                    return;
                                }
                                break;

                            case 2:
                                Console.Write("Novo cargo (Gerente, Repositor, Caixa): ");
                                string novoCargo = Console.ReadLine();

                                // Validar o novo cargo
                                if (ValidarCargo(novoCargo))
                                {
                                    dados[2] = novoCargo;
                                }
                                else
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("Cargo inválido. Funcionário não modificado.");
                                    Console.ReadLine();
                                    return;
                                }
                                break;

                            default:
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Opção inválida. Funcionário não modificado.");
                                Console.ReadLine();
                                return;
                        }

                        // Construir a nova linha com as modificações
                        string novaLinha = string.Join(",", dados);

                        // Atualizar a linha no array
                        linhas[numeroLinha - 1] = novaLinha;

                        // Reescrever o arquivo com as modificações
                        File.WriteAllLines(caminhoFicheiro, linhas);

                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("\nFuncionário modificado com sucesso!");
                        RegistrarLog($"Funcionário modificado | ID: {numeroLinha}, Modificado por: {gerente}");
                    }
                    catch (Exception ex)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"Erro Mensagem Inválida!");
                        Console.ReadKey();
                        Console.Clear();
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nFuncionário com cargo 'Gerente' não pode ser modificado.");
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nNúmero de linha inválido. Funcionário não modificado.");
            }

            Console.ReadLine();
        }

        public static void VenderLivro()
        {
            Title = "Vender Livro";

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
                                        }
                                    }
                                    else
                                    {
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine("Erro: Quantidade inválida.");
                                    }
                                }
                                else
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine($"Erro: Livro com código {codigo} não encontrado.");
                                    livroEncontrado = true;  // Para sair do loop se o livro não for encontrado
                                }
                                File.AppendAllText("..\\..\\Logs\\RegistoVendas.txt", $"{DateTime.Now}|Código: {codigo}|Preço: {totalVenda}|IVA: {totalIVA}|Quantidade: {totalLivrosVendidos}|Vendido por: Gerente {Login.UtilizadorAutenticado.Nome}\n");
                            }
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"Erro: Código inválido.");
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
        public static void ExibirRegistrosVendas()
        {
            Title = "Registo de Vendas";

            try
            {
                string caminhoFicheiroVendas = "..\\..\\Logs\\RegistoVendas.txt";
                if (File.Exists(caminhoFicheiroVendas))
                {
                    Console.Clear();
                    Console.WriteLine("Registros de Vendas:");

                    string[] registrosVendas = File.ReadAllLines(caminhoFicheiroVendas);

                    foreach (var registroVenda in registrosVendas)
                    {
                        // Exibir as informações formatadas conforme desejado
                        Console.WriteLine(registroVenda);
                    }
                }
                else
                {
                    Console.WriteLine("O arquivo de registro de vendas não existe ou está vazio.");
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Erro ao exibir registros de vendas: {ex.Message}");
                Console.ResetColor();
            }

            Console.ReadLine(); // Aguarda a entrada do usuário antes de retornar ao menu
        }

        public static void RegistrarVenda(string codigo, double totalVenda, double totalIVA, int totalLivrosVendidos)
        {
            try
            {
                string caminhoFicheiroVendas = "..\\..\\Logs\\RegistoVendas.txt";
                // Formatando a string de registro conforme necessário
                string vendaEntryCaixa = $"{DateTime.Now} Código: {codigo} | Total: {totalVenda} | IVA: {totalIVA} | Quantidade: {totalLivrosVendidos} | Vendido por: Caixa {Login.UtilizadorAutenticado.Nome}{Environment.NewLine}";
                File.AppendAllText(caminhoFicheiroVendas, vendaEntryCaixa);
                string vendaEntryGerente = $"{DateTime.Now} Código: {codigo} | Total: {totalVenda} | IVA: {totalIVA} | Quantidade: {totalLivrosVendidos} | Vendido por: Gerente {Login.UtilizadorAutenticado.Nome}{Environment.NewLine}";
                File.AppendAllText(caminhoFicheiroVendas, vendaEntryGerente);
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Erro ao registrar venda: {ex.Message}");
                Console.ResetColor();
            }
        }
        public static void ExibirRegistrosRepositor()
        {
            try
            {
                string caminhoFicheiroRepositor = "..\\..\\Logs\\RegistoRepositor.txt";
                if (File.Exists(caminhoFicheiroRepositor))
                {
                    Console.Clear();
                    Console.WriteLine("Registros do Repositor:");

                    string[] registrosRepositor = File.ReadAllLines(caminhoFicheiroRepositor);

                    foreach (var registroRepositor in registrosRepositor)
                    {
                        // Exibir as informações formatadas conforme desejado
                        Console.WriteLine(registroRepositor);
                    }
                }
                else
                {
                    Console.WriteLine("O arquivo de registro do repositor não existe ou está vazio.");
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Erro ao exibir registros de repoositor: {ex.Message}");
                Console.ResetColor();
            }

            Console.ReadLine(); // Aguarda a entrada do usuário antes de retornar ao menu
        }

        public static void RegistrarVenda(string codigo, string nome, string autor, int isbn, string genero, double precoFinal, double precoIVA, int quantidadeStock, int quantidadeAdicionar, int novoStock)
        {
            try
            {
                string caminhoFicheiroRepositor = "..\\..\\Logs\\RegistoVendas.txt";
                // Formatando a string de registro conforme necessário
                string repositorv1 = $"{DateTime.Now} |Código: {codigo}|Título: {nome}|Autor: {autor}|ISBN: {isbn}|Género: {genero}|Preço: {precoFinal}|IVA: {precoIVA}|Stock: {quantidadeStock}|Vendido por: Repositor {Login.UtilizadorAutenticado.Nome}";
                File.AppendAllText(caminhoFicheiroRepositor, repositorv1);
                string repositorv2 = $"{DateTime.Now}| No código {codigo}, foram adicionados {quantidadeAdicionar} unidades, dando no total {novoStock} unidades no stock. Foram adicionados pelo Repositor {Login.UtilizadorAutenticado.Nome}";
                File.AppendAllText(caminhoFicheiroRepositor, repositorv2);
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Erro ao registrar venda: {ex.Message}");
                Console.ResetColor();
            }
        }
        private static bool ValidarCargo(string cargo)
        {
            // Lista de cargos válidos
            string[] cargosValidos = { "Gerente", "Repositor", "Caixa" };

            // Verifica se o cargo fornecido está na lista de cargos válidos
            return cargosValidos.Contains(cargo);
        }
    }
}