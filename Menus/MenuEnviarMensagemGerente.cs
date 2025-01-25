using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace LP1_Livraria.Menus
{
    internal class MenuEnviarMensagemGerente
    {
        private int SelectedMensagemGerente;
        private string[] Options;
        private string Prompt;

        //Construtor
        public MenuEnviarMensagemGerente(string prompt, string[] options)
        {
            Prompt = prompt;
            Options = options;
            SelectedMensagemGerente = 0;
        }

        //Método
        private void DisplayMensagemGerente()
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
                if (i == SelectedMensagemGerente)
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
        public int Run7()
        {
            //Variável de tecla que vai pressionar
            ConsoleKey KeyPressed7;

            //Loop estará sempre em funcionamento até que a tecla ENTER seja pressionada
            do
            {
                //Limpar a consola
                Clear();
                //Método para Exibir Menu de Caixa
                DisplayMensagemGerente();

                //Lê a tecla sem exibir no console
                ConsoleKeyInfo KeyInfo = ReadKey(true);
                KeyPressed7 = KeyInfo.Key;

                //Verifica se a tecla pressionada é a seta para cima 
                if (KeyPressed7 == ConsoleKey.UpArrow)
                {
                    //Indice da opção selecionada
                    SelectedMensagemGerente--;

                    //Se o indice fôr -1, então ajusta para a ultima opção do menu 
                    if (SelectedMensagemGerente == -1)
                    {
                        SelectedMensagemGerente = Options.Length - 1;
                    }
                }
                //Verifica se a tecla pressionada é a seta para baixo
                else if (KeyPressed7 == ConsoleKey.DownArrow)
                {
                    //Indice da opção selecionada
                    SelectedMensagemGerente++;

                    //Se atingir o comprimento total das opções, volta para a primeira opção do menu 
                    if (SelectedMensagemGerente == Options.Length)
                    {
                        SelectedMensagemGerente = 0;
                    }
                }

            } while (KeyPressed7 != ConsoleKey.Enter);

            //Retorna o indice da opção selecionada
            return SelectedMensagemGerente;
        }
    }
}
