using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace LP1_Livraria
{
    class MenuLoginNovo
    {
        //Definir Variáveis
        private int SelectedLogin;
        private string[] Options;
        private string Prompt;

        //Construtor
        public MenuLoginNovo(string prompt, string[] options)
        {
            Prompt = prompt;
            Options = options;
            SelectedLogin = 0;
        }

        //Método
        private void DisplayLogin()
        {
            //Exibe Mensagem
            WriteLine(Prompt);

            //Loop com as opções do menu
            for (int i = 0; i < Options.Length; i++)
            {
                //Obtéma opção atual do indice i
                string currentOption = Options[i];
                //Variável do simbolo que aparece atrás de cada opção
                string prefixo;

                //Verifica se a opção atual é a selecionada
                if (i == SelectedLogin)
                {
                    prefixo = "-->";
                    ForegroundColor = ConsoleColor.Black;
                    BackgroundColor = ConsoleColor.White;
                }
                else
                {
                    prefixo = " ";
                    ForegroundColor = ConsoleColor.White;
                    BackgroundColor = ConsoleColor.Black;
                }
                //Exibe a opção selecionada e o prefixo
                WriteLine($"\n{prefixo}  {currentOption}");
            }
            //Restaura as cores após exibir todas as opções
            ResetColor();
        }

        //Método
        public int Run1()
        {
            //Variável de tecla que vai pressionar
            ConsoleKey KeyPressed1;

            //Loop estará sempre em funcionamento até que a tecla ENTER seja pressionada
            do
            {
                //Limpar Consola
                Clear();
                //Método para Exibir Menu de Login
                DisplayLogin();

                //Lê a tecla sem exibir no console
                ConsoleKeyInfo KeyInfo = ReadKey(true);
                KeyPressed1 = KeyInfo.Key;

                //Verifica se a tecla pressionada é a seta para cima
                if (KeyPressed1 == ConsoleKey.UpArrow)
                {
                    //Indice da opção selecionada
                    SelectedLogin--;

                    //Se o indice fôr -1, então ajusta para a ultima opção do menu 
                    if (SelectedLogin == -1)
                    {
                        SelectedLogin = Options.Length - 1;
                    }
                }
                //Verifica se a tecla pressionada é a seta para baixo
                else if (KeyPressed1 == ConsoleKey.DownArrow)
                {
                    //Indice da opção selecionada
                    SelectedLogin++;

                    //Se atingir o comprimento total das opções, volta para a primeira opção do menu 
                    if (SelectedLogin == Options.Length)
                    {
                        SelectedLogin = 0;
                    }
                }

            } while (KeyPressed1 != ConsoleKey.Enter);

            //Retorna o indice da opção selecionada
            return SelectedLogin;
        }
    }
}