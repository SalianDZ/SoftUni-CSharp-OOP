string[] tokens = Console.ReadLine().Split(", ");
List<Card> cards = new List<Card>();

for (int i = 0; i < tokens.Length; i++)
{
	string[] cardTokens = tokens[i].Split();
	string face = cardTokens[0];
	string suit = cardTokens[1];

	try
	{
		Card card = new(face, suit);

		cards.Add(card);
	}
	catch (ArgumentException ex)
	{
		Console.WriteLine(ex.Message);
		continue;
	}
}

Console.WriteLine(string.Join(" ", cards));


public class Card
{
	private string face;
	private string suit;
	private string utfCode;

	public Card(string face, string suit)
	{
		Face = face;
		Suit = suit;
		UtfCode = UtfCode;
	}

	public string UtfCode
	{
		get { return utfCode; }
		set 
		{
			if (Suit == "S")
			{
				utfCode = "\u2660";
            }
			else if (Suit == "H")
			{
                utfCode = "\u2665";
            }
			else if (Suit == "D")
			{
                utfCode = "\u2666";
            }
			else if (Suit == "C")
			{
                utfCode = "\u2663";
            }
		}
	}


	public string Suit
	{
		get { return suit; }
		set
		{
			if (value != "S" && value != "H" && value != "D" && value != "C")
			{
				throw new ArgumentException("Invalid card!");
			}
			suit = value;
		}
	}


	public string Face
	{
		get { return face; }
		set
		{
			if (value != "2" && value != "3" && value != "4" && value != "5"
                && value != "7" && value != "8" && value != "9"
                && value != "10" && value != "J" && value != "Q"
                && value != "K" && value != "A")
			{
				throw new ArgumentException("Invalid card!");
			}

			face = value;
		}
	}

	public override string ToString()
	{
		return $"[{Face}{UtfCode}]";
	}
}