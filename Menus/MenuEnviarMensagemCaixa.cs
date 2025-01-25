using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace LP1_Livraria.Menus
{
    internal class MenuEnviarMensagemCaixa
    {
        private int SelectedMensagemCaixa;
        private string[] Options;
        private string Prompt;

        //Construtor
        public MenuEnviarMensagemCaixa(string prompt, string[] options)
        {
            Prompt = prompt;
            Options = options;
            SelectedMensagemCaixa = 0;
        }

        //Método
        private void DisplayMensagemCaixa()
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
                if (i == SelectedMensagemCaixa)
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
        public int Run6()
        {
            //Variável de tecla que vai pressionar
            ConsoleKey KeyPressed6;

            //Loop estará sempre em funcionamento até que a tecla ENTER seja pressionada
            do
            {
                //Limpar a consola
                Clear();
                //Método para Exibir Menu de Caixa
                DisplayMensagemCaixa();

                //Lê a tecla sem exibir no console
                ConsoleKeyInfo KeyInfo = ReadKey(true);
                KeyPressed6 = KeyInfo.Key;

                //Verifica se a tecla pressionada é a seta para cima 
                if (KeyPressed6 == ConsoleKey.UpArrow)
                {
                    //Indice da opção selecionada
                    SelectedMensagemCaixa--;

                    //Se o indice fôr -1, então ajusta para a ultima opção do menu 
                    if (SelectedMensagemCaixa == -1)
                    {
                        SelectedMensagemCaixa = Options.Length - 1;
                    }
                }
                //Verifica se a tecla pressionada é a seta para baixo
                else if (KeyPressed6 == ConsoleKey.DownArrow)
                {
                    //Indice da opção selecionada
                    SelectedMensagemCaixa++;

                    //Se atingir o comprimento total das opções, volta para a primeira opção do menu 
                    if (SelectedMensagemCaixa == Options.Length)
                    {
                        SelectedMensagemCaixa = 0;
                    }
                }

            } while (KeyPressed6 != ConsoleKey.Enter);

            //Retorna o indice da opção selecionada
            return SelectedMensagemCaixa;
        }
    }
}