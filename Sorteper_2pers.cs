using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorteper
{
	class Player
	{
		//
		public List<string> deckHand = new List<string>();
		public string name = "";
		public byte playerId;
		public bool npc = true;



		/// <summary>
		/// This is the "constructor". 
		/// </summary>
		/// <param name="player">Player Name?</param>
		/// <param name="tableId">Place on the table for the Player/npc'en?</param>
		/// <param name="npc_status">Is it a npc?</param>y
		public Player(string player, byte tableId, bool npc_status)
		{
			name = player;
			playerId = tableId;
			npc = npc_status;
		}

	}
	class Program
	{



		static void Main(string[] args)
		{

			//Some of the variabels
			Console.Title = "Sorteper";
			List<Player> players = new List<Player>();
			string pressEnterClose = "Tryk på \" ENTER \" for at Lukke spillet.";
			string yesNo = "y	-	Ja/Yes\r\nn	-	Nej/No";
			string pressEnter = "Tryk på \" ENTER \" for at fortsætte.";


			while (true)
			{

				//p = Player (insted of i)
				for (byte p = 1; p < 3; p++)
				{

					Console.Clear();


					Console.WriteLine("Skal spiller " + p + " være en NPC?");
					Console.WriteLine(yesNo);
					string isItANPC = Console.ReadLine().ToLower().Trim();


					//Loop until valid answer about Console.ReadLine(); player ID (Is it a NPC?)
					while (isItANPC != "y" && isItANPC != "n")
					{
						Console.Clear();
						Console.WriteLine("FUCKIN SPASSER MAN!!!\r\nJeg ber dig om noget simpelt!\r\n\"y\" eller \"n\"!!!");
						Console.WriteLine("\r\n\r\n" + "Skal spiller " + p + " være en NPC?" + "\r\n" + yesNo);
						isItANPC = Console.ReadLine().ToLower().Trim();

					}

					if (isItANPC == "y")
					{
						players.Add(new Player("NPC " + p, p, true));
					}
					else
					{
						Console.Clear();
						Console.WriteLine("Indtast navn på Spiller {0}:\r\n", p);
						string playerName = Console.ReadLine();
						players.Add(new Player(playerName, p, false));
					}
				}

				//This is the game loop

				//Here we'er making the cards
				List<string> deck = new List<string> { "!Sorteper!", "Hare", "Hare", "Ko", "Ko", "Kylling", "Kylling", "Hund", "Hund", "Kat", "Kat", "Fugl", "Fugl", "Haj", "Haj", "Hval", "Hval", "Hest", "Hest", "Fisk", "Fisk" };

				Random random = new Random();
				byte playerDealer = 0;

				//Loop until out of cards
				while (deck.Count > 0)
				{
					int deckCardNum = random.Next(0, deck.Count);
					players[playerDealer].deckHand.Add(deck[deckCardNum]);
					deck.RemoveAt(deckCardNum);
					playerDealer++;

					if (playerDealer >= 2)
					{
						playerDealer = 0;
					}
				}

				//You know
				Console.Clear();
				Console.WriteLine("Disse par er blevet fjernet fra følgende personer:\r\n");

				//I'm removing and print doubles
				foreach (Player player in players)
				{
					List<string> doubles;
					player.deckHand = SplitSinglesAndDoublets(player.deckHand, out doubles);
					doubles.Sort();
					Console.WriteLine(player.name + ":	" + string.Join("	-	", doubles) + "\r\n");
				}

				//Printing status
				drawTable(1, players);
				Console.WriteLine("Tryk på \"Enter\" for at starte spillet.");
				Console.ReadKey();

				List<Player> activePlayers = players;
				byte playersTurn = 0;
				byte nextPlayer = 1;

				while (activePlayers.Count > 1)
				{
					int card = -1;
					if (activePlayers[playersTurn].npc)
					{
						
						Console.Clear();
						PrintColoreredText("Det er player: " + activePlayers[playersTurn].name + "'s tur.", ConsoleColor.Black, ConsoleColor.Green);
						drawTable(activePlayers[playersTurn].playerId, activePlayers);


						Console.WriteLine("\r\n---------------------------------------------------------------------------------------------");
						drawPublicHand(activePlayers[nextPlayer].deckHand);
						PrintColoreredText("\r\n" + activePlayers[playersTurn].name + " fra spiller " + activePlayers[nextPlayer].name, ConsoleColor.Black, ConsoleColor.Yellow);
						Console.WriteLine("\r\n");

						Random randomNPCDraw = new Random();
						card = randomNPCDraw.Next(1, activePlayers[nextPlayer].deckHand.Count + 1);
					}



					else
					{
						Console.Clear();
						PrintColoreredText("Det er player: " + activePlayers[playersTurn].name + "'s tur.\r\n", ConsoleColor.Black, ConsoleColor.Green);
						PrintColoreredText("Alle andre kig væk.\r\nNår spiller: " + activePlayers[playersTurn].name + " er klar, Tryk på \"Enter\".", ConsoleColor.Black, ConsoleColor.Red);
						Console.ReadKey();
						Console.Clear();
						PrintColoreredText("Det er player: " + activePlayers[playersTurn].name + "'s tur.", ConsoleColor.Black, ConsoleColor.Green);
						drawTable(activePlayers[playersTurn].playerId, activePlayers);
						Console.Write("Dette er dine kort:\r\n\r\n-	");
						drawHand(activePlayers[playersTurn].deckHand);
						Console.WriteLine("\r\n---------------------------------------------------------------------------------------------");
						drawPublicHand(activePlayers[nextPlayer].deckHand);
						PrintColoreredText("\r\nVælg det kort du vil tage fra spiller " + activePlayers[nextPlayer].name, ConsoleColor.Black, ConsoleColor.Yellow);
						if (activePlayers[playersTurn].name.ToLower() == "cheater")
						{
							Console.Write("Dette er " + activePlayers[nextPlayer].name + " kort:\r\n\r\n-	");
							drawHand(activePlayers[nextPlayer].deckHand);
							Console.WriteLine("\r\n");
						}
						

					}



					//Until valid indput
					while (true && activePlayers[playersTurn].npc == false)
					{
						//If valid indput, break, else inform and loop
						if (Int32.TryParse(Console.ReadLine(), out card) && card > 0 && card <= activePlayers[nextPlayer].deckHand.Count)
						{
							break;
						}

						else
						{
							Console.Clear();
							PrintColoreredText("Det er player: " + activePlayers[playersTurn].name + "'s tur.", ConsoleColor.Black, ConsoleColor.Green);
							drawTable(activePlayers[playersTurn].playerId, activePlayers);
							drawHand(activePlayers[playersTurn].deckHand);
							Console.WriteLine("\r\n---------------------------------------------------------------------------------------------");
							drawPublicHand(activePlayers[nextPlayer].deckHand);
							PrintColoreredText("\r\nVælg det kort du vil tage fra spiller " + activePlayers[nextPlayer].name, ConsoleColor.Black, ConsoleColor.Yellow);
							if (activePlayers[playersTurn].name.ToLower() == "cheater")
							{
								Console.Write("Dette er " + activePlayers[nextPlayer].name + " kort:\r\n\r\n-	");
								drawHand(activePlayers[nextPlayer].deckHand);
								Console.WriteLine("\r\n");
							}
							if (activePlayers[playersTurn].name.ToLower().Contains("op!"))
							{
								Console.Write("Dette er " + activePlayers[nextPlayer].name + " kort:\r\n\r\n-	");
								drawHand(activePlayers[nextPlayer].deckHand);
								Console.WriteLine("\r\n");
							}
							PrintColoreredText("\r\nFUCKIN SPASSER MAN!!!\r\nJeg ber dig om noget simpelt!\r\n\"\"Vælg det kort du vil tage fra spiller " + activePlayers[nextPlayer].name + "!!!\"", ConsoleColor.Black, ConsoleColor.Red);


						}
					}


					if (activePlayers[nextPlayer].deckHand[card - 1] == "!Sorteper!")
					{
						Console.WriteLine("\r\nHAHAHA!!!	-	du har trukket !Sorteper!, du har trukket !Sorteper! HA HA HA HA HA!!! ");
						Console.WriteLine("\r\nBTW, Fik jeg sagt at du har trukket: !Sorteper!\r\n");
					}
					else
					{
						Console.WriteLine("\r\nDu har trukket:	" + activePlayers[nextPlayer].deckHand[card - 1] + "\r\n");
					}

					activePlayers[playersTurn].deckHand.Add(activePlayers[nextPlayer].deckHand[card - 1]);
					activePlayers[nextPlayer].deckHand.RemoveAt(card - 1);
					List<string> doubles;
					activePlayers[playersTurn].deckHand = SplitSinglesAndDoublets(activePlayers[playersTurn].deckHand, out doubles);
					Console.WriteLine(pressEnter);
					Console.ReadKey();




					if (doubles.Count > 0)
					{
						Console.Clear();
						drawTable(activePlayers[playersTurn].playerId, activePlayers);
						doubles.Sort();
						PrintColoreredText("Disse dubletter er blevet fundet:", ConsoleColor.Black, ConsoleColor.Yellow);
						Console.WriteLine(activePlayers[playersTurn].name + ":	" + string.Join("	-	", doubles) + "\r\n");
						Console.WriteLine(pressEnter);
						Console.ReadKey();
						if (activePlayers[playersTurn].deckHand.Count == 0)
						{
							Console.Clear();
							PrintColoreredText("Følgende spiller har vundet!!!:	", ConsoleColor.Black, ConsoleColor.Yellow);
							PrintColoreredText(activePlayers[playersTurn].name, ConsoleColor.Black, ConsoleColor.Yellow);
							PrintColoreredText("\r\nFølgende spiller har vundet!!!:	", ConsoleColor.Black, ConsoleColor.Blue);
							PrintColoreredText(activePlayers[playersTurn].name, ConsoleColor.Black, ConsoleColor.Blue);
							PrintColoreredText("\r\nFølgende spiller har vundet!!!:	", ConsoleColor.Black, ConsoleColor.Green);
							PrintColoreredText(activePlayers[playersTurn].name, ConsoleColor.Black, ConsoleColor.Green);
							PrintColoreredText("\r\nFølgende spiller har vundet!!!:	", ConsoleColor.Black, ConsoleColor.Red);
							PrintColoreredText(activePlayers[playersTurn].name, ConsoleColor.Black, ConsoleColor.Red);

							
							drawTable(activePlayers[playersTurn].playerId, activePlayers);
							Console.WriteLine("\r\n\r\n\r\n" + pressEnterClose);
							break;
						}

					}


					activePlayers[playersTurn].deckHand = activePlayers[playersTurn].deckHand.OrderBy(a => random.Next()).ToList();
					activePlayers[nextPlayer].deckHand = activePlayers[nextPlayer].deckHand.OrderBy(a => random.Next()).ToList();

					if (playersTurn == 0)
					{
						playersTurn = 1;
						nextPlayer = 0;
					}
					else
					{
						playersTurn = 0;
						nextPlayer = 1;
					}



				}


				//Just for test in practice
				Console.ReadLine();
				Console.WriteLine("\r\n\r\nVi tager den lige en gang til. :)");
				Console.WriteLine("\r\n" + pressEnterClose);
				Console.ReadLine();
				break;
			}
		}

		//public static 



		/// <summary>
		/// Method to draw the gamebord in the console
		/// </summary>
		/// <param name="playerID">PlayID of the player who's turn it is</param>
		/// <param name="playersOnTable">The list collection with all of the players</param>
		public static void drawTable(byte playerID, List<Player> playersOnTable)
		{
			Console.WriteLine("\r\n---------------------------------------------------------------------------------------------");
			foreach (Player player in playersOnTable)
			{

				//If is't the players turn, it's change the color of the back and fourground. 
				if (player.playerId == playerID)
				{
					Console.BackgroundColor = ConsoleColor.Green;
					Console.ForegroundColor = ConsoleColor.Black;
				}

				// if not, change it to default
				else
				{
					Console.ResetColor();
				}

				if (player.name.ToLower().Contains("camilla"))
				{
					Console.WriteLine(player.name + "(NOOB): " + player.deckHand.Count);
				}
				else if (player.name.ToLower().Contains("knog"))
				{
					Console.WriteLine(player.name + "(The Champ!): " + player.deckHand.Count);
				}
				else if (player.name.ToLower().Contains("robbin"))
				{
					Console.WriteLine(player.name + "(The Champ!): " + player.deckHand.Count);
				}
				else if (player.name.ToLower().Contains("cheater"))
				{
					Console.WriteLine(player.name + "(Cheater!-Cheater!-Cheater!): " + player.deckHand.Count);
				}
				else
				{
					Console.WriteLine(player.name + ": " + player.deckHand.Count);
				}
			}
			Console.ResetColor();
			Console.WriteLine("---------------------------------------------------------------------------------------------");

		}

		//Here we are showing the cards in the console on my hand
		public static void drawHand(List<string> deckHand)
		{
			foreach (string card in deckHand)
			{

				Console.Write(card + "	-	");
			}
		}

		//Here we are showing the cards in the console on others hand
		public static void drawPublicHand(List<string> deckhand)
		{
			Console.Write("Dette er de kort du skal trække et af:\r\n\r\n-	");
			for (int i = 0; i < deckhand.Count; i++)
			{
				Console.Write("	" + (i + 1) + "	-");
			}
		}

		/// <summary>
		/// How I'm finding the doubles
		/// </summary>
		/// <param name="deckHand">The hand to split into singles and doubles</param>
		/// <param name="dubletsOut">The found doubles</param>
		/// <returns>The players new hand, without doubles</returns>
		public static List<string> SplitSinglesAndDoublets(List<string> deckHand, out List<string> dubletsOut)
		{
			dubletsOut = new List<string>();
			List<string> singles = new List<string>();
			bool single = true;

			//Foreach card in the hand
			for (int i = 0; i < deckHand.Count; i++)
			{

				//To find double
				for (int c = 0; c < deckHand.Count; c++)
				{
					if (c == i)
					{
						continue;
					}
					if (deckHand[i] == deckHand[c])
					{
						single = false;
						dubletsOut.Add(deckHand[i]);
						break;
					}
				}

				// If it's not a double it's a single
				if (single == true)
				{
					singles.Add(deckHand[i]);
				}
				single = true;
			}
			return singles;
		}

		public static void PrintColoreredText(string text, ConsoleColor textColor, ConsoleColor baggroundColor)
		{
			Console.ForegroundColor = textColor;
			Console.BackgroundColor = baggroundColor;
			Console.WriteLine(text);
			Console.ResetColor();
		}

	}
}
