using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace LP1_Livraria.Menus
{
    internal class NovoMenuEditarFuncionario
    {
        //Definir variáveis
        private int SelectedEditarFuncionario;
        private string[] Options;
        private string Prompt;

        //Construtor
        public NovoMenuEditarFuncionario(string prompt, string[] options)
        {
            //Valor do parámetro prompt
            Prompt = prompt;

            //Valor do parámetro options
            Options = options;

            //Opção selecionada no menu
            SelectedEditarFuncionario = 0;
        }

        //Método
        private void DisplayEditarFuncionario()
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
                if (i == SelectedEditarFuncionario)
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
        public int Run8()
        {
            //Variável de tecla que vai pressionar
            ConsoleKey KeyPressed8;

            //Loop estará sempre em funcionamento até que a tecla ENTER seja pressionada
            do
            {

                //Limpar a consola
                Clear();
                //Método para Exibir Menu de Gerente
                DisplayEditarFuncionario();

                //Lê a tecla sem exibir no console
                ConsoleKeyInfo KeyInfo = ReadKey(true);
                KeyPressed8 = KeyInfo.Key;

                //Verifica se a tecla pressionada é a seta para cima 
                if (KeyPressed8 == ConsoleKey.UpArrow)
                {
                    //Indice da opção selecionada
                    SelectedEditarFuncionario--;

                    //Se o indice fôr -1, então ajusta para a ultima opção do menu 
                    if (SelectedEditarFuncionario == -1)
                    {
                        SelectedEditarFuncionario = Options.Length - 1;
                    }
                }
                //Verifica se a tecla pressionada é a seta para baixo
                else if (KeyPressed8 == ConsoleKey.DownArrow)
                {
                    //Indice da opção selecionada
                    SelectedEditarFuncionario++;

                    //Se atingir o comprimento total das opções, volta para a primeira opção do menu 
                    if (SelectedEditarFuncionario == Options.Length)
                    {
                        SelectedEditarFuncionario = 0;
                    }
                }

            } while (KeyPressed8 != ConsoleKey.Enter);

            //Retorna o indice da opção selecionada
            return SelectedEditarFuncionario;
        }
    }
}
