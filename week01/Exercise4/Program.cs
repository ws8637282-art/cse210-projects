using System;
using System.Collections.Generic;

List<int> numbers = new List<int>();

Console.WriteLine("Enter a list of numbers, type 0 when finished.");

int number = -1;

while (number != 0)
{
    Console.Write("Enter number: ");
    string input = Console.ReadLine();
    number = int.Parse(input);

    if (number != 0)
    {
        numbers.Add(number);
    }
}

int sum = 0;

foreach (int num in numbers)
{
    sum += num;
}

double average = (double)sum / numbers.Count;

int largest = numbers[0];

foreach (int num in numbers)
{
    if (num > largest)
    {
        largest = num;
    }
}

Console.WriteLine($"The sum is: {sum}");
Console.WriteLine($"The average is: {average}");
Console.WriteLine($"The largest number is: {largest}");