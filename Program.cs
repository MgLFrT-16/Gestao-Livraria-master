using System;
using System.Security.Cryptography;
using System.Text;
using static System.Console;

namespace LP1_Livraria
{
    class Program
    {
        private static bool sairPrograma = false;

        public static Login.Cargo cargoAtual = Login.Cargo.Desconhecido;

        public static void Main(string[] args)
        {
            while (!sairPrograma)
            {
                MenuPrincipal();
            }
        }

        public static void MenuPrincipal()
        {
            Title = "Login";
            MetodoMenuLogin();
        }

        public static void MetodoMenuLogin()
        {
            string prompt = @"

  _                 _       
 | |               (_)      
 | |     ___   __ _ _ _ __  
 | |    / _ \ / _` | | '_ \ 
 | |___| (_) | (_| | | | | |
 |______\___/ \__, |_|_| |_|
               __/ |        
              |___/         


Selecione uma das opcões!";
            ForegroundColor = ConsoleColor.White;
            BackgroundColor = ConsoleColor.Black;

            string[] options = { "Login", "Sair" };
            MenuLoginNovo mainMenu = new MenuLoginNovo(prompt, options);
            int SelectedLogin = mainMenu.Run1();

            try
            {

                switch (SelectedLogin)
                {
                    case 0:
                        FazerLogin();
                        break;

                    case 1:
                        sairPrograma = true;
                        break;

                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Erro: Opção inválida.");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                }
            }
            catch (FormatException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Erro: Valor inválido! Escolha um número inteiro.");
                Console.ReadKey();
                Console.Clear();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ReadKey();
                Console.Clear();
            }
        }

        private static void FazerLogin()
        {
            Console.Clear();
            Console.Write("Utilizador: ");
            string utilizador = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(utilizador))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Erro: Por favor, escreva o nome do utilizador.");
                Console.ReadKey();
                Console.Clear();
                return; // Retorna, não permitindo a entrada da senha
            }
            Console.Write("Password: ");
            string senha = LerSenhaOculta();
            if (string.IsNullOrWhiteSpace(senha))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Erro: Por favor, escreva a password do utilizador.");
                Console.ReadKey();
                Console.Clear();
                return; // Retorna, não permitindo a entrada da senha
            }

            // Chame o método VerificarLogin uma vez
            Login.Utilizador usuarioLogado = Login.VerificarLogin(utilizador, senha);

            if (usuarioLogado != null)
            {
                cargoAtual = usuarioLogado.Cargo;

                switch (cargoAtual)
                {
                    case Login.Cargo.Gerente:
                        Gerente.MenuGerenteNovo();
                        break;

                    case Login.Cargo.Caixa:
                        Caixa.MenuCaixa();
                        break;

                    case Login.Cargo.Repositor:
                        Repositor.MenuRepositor();
                        break;

                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Login falhou. Utilizador ou password inválidos.");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Login falhou. Utilizador ou password inválidos.");
                Console.ReadKey();
                Console.Clear();
            }
        }



        private static string LerSenhaOculta()
        {
            string senha = "";
            ConsoleKeyInfo key;

            do
            {
                key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.Backspace && senha.Length > 0)
                {
                    // Se for a tecla Backspace e a senha não estiver vazia, apaga o último caractere
                    senha = senha.Substring(0, senha.Length - 1);
                    Console.Write("\b \b"); // Apaga o caractere da tela
                }
                else if (!char.IsControl(key.KeyChar))
                {
                    // Se não for uma tecla de controle, adiciona o caractere à senha e exibe um asterisco
                    senha += key.KeyChar;
                    Console.Write("*");
                }
            } while (key.Key != ConsoleKey.Enter);

            Console.WriteLine(); // Pular para a próxima linha após a entrada da senha
            return senha;
        }

        public static void Logout()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Logout bem sucedido!");
            Console.ReadKey();
            cargoAtual = Login.Cargo.Desconhecido;
        }
    }
}
