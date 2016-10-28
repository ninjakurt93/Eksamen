using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Morsekoden
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Indtast den tekst du ønsker at få oversat til morsekode:");

               string  input = Console.ReadLine();
                input = input.ToLower().Trim();

                string output = "";

                string space = "  ";

                foreach(char character in input)
                {
                    switch (character)
                    {
                        case 'a':
                            output = output + ".-"+space;
                            break;

						case 'b':
							output = output + "-..." + space;
							break;
						case 'c':
							output = output + "-.-." + space;
							break;
						case 'd':
							output = output + "-.." + space;
							break;
						case 'e':
							output = output + "." + space;
							break;
						case 'f':
							output = output + "..-" + space;
							break;
						case 'g':
							output = output + "--." + space;
							break;
						case 'h':
							output = output + "...." + space;
							break;
						case 'i':
							output = output + ".." + space;
							break;
						case 'j':
							output = output + ".---" + space;
							break;
						case 'k':
							output = output + "-.-" + space;
							break;
						case 'l':
							output = output + ".-.." + space;
							break;
						case 'm':
							output = output + "--" + space;
							break;
						case 'n':
							output = output + "-." + space;
							break;
						case 'o':
							output = output + "---" + space;
							break;
						case 'p':
							output = output + ".--." + space;
							break;
						case 'q':
							output = output + "--.-" + space;
							break;
						case 'r':
							output = output + ".-." + space;
							break;
						case 's':
							output = output + "..." + space;
							break;
						case 't':
							output = output + "-" + space;
							break;
						case 'u':
							output = output + "..-" + space;
							break;
						case 'v':
							output = output + "...-" + space;
							break;
						case 'w':
							output = output + ".--" + space;
							break;
						case 'x':
							output = output + "-..-" + space;
							break;
						case 'y':
							output = output + "-.--" + space;
							break;
						case 'z':
							output = output + "--.." + space;
							break;
						case 'æ':
							output = output + ".-.-" + space;
							break;
						case 'ø':
							output = output + "---." + space;
							break;
						case 'å':
							output = output + ".--.-" + space;
							break;
						case '0':
							output = output + "-----" + space;
							break;
						case '1':
							output = output + ".----" + space;
							break;
						case '2':
							output = output + "..---" + space;
							break;
						case '3':
							output = output + "...--" + space;
							break;
						case '4':
							output = output + "....-" + space;
							break;
						case '5':
							output = output + "....." + space;
							break;
						case '6':
							output = output + "-...." + space;
							break;
						case '7':
							output = output + "--..." + space;
							break;
						case '8':
							output = output + "---.." + space;
							break;
						case '9':
							output = output + "----." + space;
							break;

						case ' ':
							output = output + "/" + space;
							break;


						default:
							Console.WriteLine("\r\n-------\r\nDu har intastet:   {0}",character + "   Dette tegn kendes ikke, start forfra og prøv igen.\r\n-------");
							break;
					}

                } 
				Console.WriteLine(output);
				Console.WriteLine("\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\nTryk på \" ENTER \" for at starte forfra.");
				Console.WriteLine("eller skriv \" !quit! \" for at afslutte.");
				if (Console.ReadLine()=="!quit!")
				{
					break;
				}
				
				Console.Clear();
            }

            
        }
        
    }
}
