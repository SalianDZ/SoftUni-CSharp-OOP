try
{
	int num = int.Parse(Console.ReadLine());
	CheckNumber(num);

	Console.WriteLine(Math.Sqrt(num));
}
catch (ArgumentException ex)
{
	Console.WriteLine(ex.Message);
}
finally
{
	Console.WriteLine("Goodbye.");
}



void CheckNumber(int num)
{
	if (num < 0)
	{
		throw new ArgumentException("Invalid number.");
	}
}