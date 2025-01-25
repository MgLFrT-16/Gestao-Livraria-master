using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace LP1_Livraria
{
    class NovoMenuGerente
    {
        //Definir variáveis
        private int SelectedGerente;
        private string[] Options;
        private string Prompt;

        //Construtor
        public NovoMenuGerente(string prompt, string[] options)
        {
            //Valor do parámetro prompt
            Prompt = prompt;

            //Valor do parámetro options
            Options = options;

            //Opção selecionada no menu
            SelectedGerente = 0;
        }

        //Método
        private void DisplayGerente()
        {
            //Exibe mensagem
            WriteLine(Prompt);

            //Loop com as opcões do menu
            for (int i = 0; i < Options.Length; i++)
            {
                //Obtém opção atual no indice i
                string currentOption = Options[i];
                //Variável do simbolo que aparece atrás de cada opção
                string prefixo;

                //Verifica se a opção atual é a selecionada 
                if (i == SelectedGerente)
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
        public int Run()
        {
            //Variável de tecla que vai pressionar
            ConsoleKey KeyPressed;

            //Loop estará sempre em funcionamento até que a tecla ENTER seja pressionada
            do
            {

                //Limpar a consola
                Clear();
                //Método para Exibir Menu de Gerente
                DisplayGerente();

                //Lê a tecla sem exibir no console
                ConsoleKeyInfo KeyInfo = ReadKey(true);
                KeyPressed = KeyInfo.Key;

                //Verifica se a tecla pressionada é a seta para cima 
                if (KeyPressed == ConsoleKey.UpArrow)
                {
                    //Indice da opção selecionada
                    SelectedGerente--;

                    //Se o indice fôr -1, então ajusta para a ultima opção do menu 
                    if (SelectedGerente == -1)
                    {
                        SelectedGerente = Options.Length - 1;
                    }
                }
                //Verifica se a tecla pressionada é a seta para baixo
                else if (KeyPressed == ConsoleKey.DownArrow)
                {
                    //Indice da opção selecionada
                    SelectedGerente++;

                    //Se atingir o comprimento total das opções, volta para a primeira opção do menu 
                    if (SelectedGerente == Options.Length)
                    {
                        SelectedGerente = 0;
                    }
                }

            } while (KeyPressed != ConsoleKey.Enter);

            //Retorna o indice da opção selecionada
            return SelectedGerente;
        }
    }
}