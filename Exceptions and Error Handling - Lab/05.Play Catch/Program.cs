int[] nums = Console.ReadLine().Split().Select(int.Parse).ToArray();
int exceptionCounter = 0;

while (exceptionCounter < 3)
{
    string[] command = Console.ReadLine().Split();

    try
    {
        if (command[0] == "Replace")
        {
            int index = int.Parse(command[1]);
            int element = int.Parse(command[2]);

            nums[index] = element;
        }
        else if (command[0] == "Print")
        {
            int firstIndex = int.Parse(command[1]);
            int secondIndex = int.Parse(command[2]) + 1;

            Console.WriteLine(String.Join(", ", nums[firstIndex..secondIndex]));
        }
        else if (command[0] == "Show")
        {
            int index = int.Parse(command[1]);
            Console.WriteLine(nums[index]);
        }
    }
    catch (FormatException)
    {
        Console.WriteLine("The variable is not in the correct format!");
        exceptionCounter++;
    }
    catch (ArgumentOutOfRangeException)
    {
        Console.WriteLine("The index does not exist!");
        exceptionCounter++;
    }
    catch (IndexOutOfRangeException)
    {
        Console.WriteLine("The index does not exist!");
        exceptionCounter++;

}
Console.WriteLine(String.Join(", ", nums));