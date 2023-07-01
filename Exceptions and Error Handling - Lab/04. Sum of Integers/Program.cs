string[] nums = Console.ReadLine().Split();
int sum = 0;
int currentNumber = 0;
string currentNumberAsString = string.Empty;
for (int i = 0; i < nums.Length; i++)
{
    try
    {
        currentNumberAsString = nums[i];
        currentNumber = int.Parse(nums[i]);

        sum += currentNumber;
        Console.WriteLine($"Element '{currentNumber}' processed - current sum: {sum}");
    }
    catch (FormatException)
    {
        Console.WriteLine($"The element '{currentNumberAsString}' is in wrong format!");
        Console.WriteLine($"Element '{currentNumberAsString}' processed - current sum: {sum}");
        continue;
    }
    catch (OverflowException)
    {
        Console.WriteLine($"The element '{currentNumberAsString}' is out of range!");
        Console.WriteLine($"Element '{currentNumberAsString}' processed - current sum: {sum}");
        continue;
    }
}



Console.WriteLine($"The total sum of all integers is: {sum}");