using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace LP1_Livraria
{
    class NovoMenuRepositor
    {
        //Definir variáveis
        private int SelectedRepositor;
        private string[] Options;
        private string Prompt;

        //Construtor
        public NovoMenuRepositor(string prompt, string[] options)
        {
            Prompt = prompt;
            Options = options;
            SelectedRepositor = 0;
        }

        //Método
        private void DisplayRepositor()
        {
            //Exibe a mensagem
            WriteLine(Prompt);
            //Loop com as opções do menu
            for (int i = 0; i < Options.Length; i++)
            {
                //Obtéma opção atual do indice i
                string currentOption = Options[i];
                //Variável do simbolo que aparece atrás de cada opção
                string prefixo;

                //Verifica se a opção atual é a selecionada
                if (i == SelectedRepositor)
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
                WriteLine($"\n{prefixo}  {currentOption} \n");
            }
            //Restaura as cores após exibir todas as opções
            ResetColor();
        }

        //Método
        public int Run3()
        {
            //Variável de tecla que vai pressionar
            ConsoleKey KeyPressed2;

            //Loop estará sempre em funcionamento até que a tecla ENTER seja pressionada
            do
            {
                //Limpar a consola
                Clear();
                //Método para Exibir Menu de Repositor
                DisplayRepositor();

                //Lê a tecla sem exibir no console
                ConsoleKeyInfo KeyInfo = ReadKey(true);
                KeyPressed2 = KeyInfo.Key;

                //Verifica se a tecla pressionada é a seta para cima 
                if (KeyPressed2 == ConsoleKey.UpArrow)
                {
                    //Indice da opção selecionada
                    SelectedRepositor--;

                    //Se o indice fôr -1, então ajusta para a ultima opção do menu 
                    if (SelectedRepositor == -1)
                    {
                        SelectedRepositor = Options.Length - 1;
                    }
                }
                //Verifica se a tecla pressionada é a seta para baixo
                else if (KeyPressed2 == ConsoleKey.DownArrow)
                {
                    //Indice da opção selecionada
                    SelectedRepositor++;

                    //Se atingir o comprimento total das opções, volta para a primeira opção do menu
                    if (SelectedRepositor == Options.Length)
                    {
                        SelectedRepositor = 0;
                    }
                }

            } while (KeyPressed2 != ConsoleKey.Enter);

            //Retorna o indice da opção selecionada
            return SelectedRepositor;
        }
    }
}
