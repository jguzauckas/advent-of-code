using System;

namespace Day_9
{
    class Day9
    {
        static void Main(string[] args)
        {
            string[] originalInput = System.IO.File.ReadAllLines(@"E:\OneDrive\Documents\Personal\Learning\Advent of Code\2020\Day 9\input.txt"); //Reading each line of the input to the originalInput array
            long[] longInput = new long[originalInput.Length]; //Create an array of longs to parse the input into (has to be long due to int32 max)
            for (int i = 0; i < originalInput.Length; i++) //Go through each entry of the input
            {
                longInput[i] = long.Parse(originalInput[i]); //Set the long array entry to the long parse of the entry
            }
            long[,] longSums = new long[originalInput.Length, 2]; //Create a 2D array with two columns, where one holds one number in the sum, and the other holds the other number
            long firstLong = 0; //Variable to store the number that doesn't work
            for (int i = 25; i < originalInput.Length; i++) //Go through each entry, starting at the 26th (index 25) to be able to check the 25 entries before it
            {
                for (int j = 1; j <= 25; j++) //Go through the options 1 to 25
                {
                    for (int k = 1; k <= 25; k++) //Go through the options 1 to 25 again
                    {
                        if (j != k && (longInput[i - j] + longInput[i - k]) == longInput[i]) //Check that our options are not the same (can't sum the same number), and that the sum of those two entries is our input
                        {                                                                    //i - j and i - k take the current entry and rewind to two of the previous 25 entries (different when j != k), so we can check their sum
                            longSums[i, 0] = longInput[i - j]; //Set the first column to the first number in that sum
                            longSums[i, 1] = longInput[i - k]; //Set the second column to the second number in that sum
                        }
                    }
                }
                if (firstLong == 0 && longSums[i, 0] == 0) //If we have not found the broken entry yet, and the first column is 0 (its initial value, hasn't been rewritten) after going through all possibilities
                {
                    firstLong = longInput[i]; //This must be the broken number
                    goto done1; //Exit the loop early since we are done
                }
            }
            done1: //Place to exit the loop when done
            long weaknessSum = 0; //Create variable to store the encryption weakness for part 2
            for (int i = 0; i < originalInput.Length; i++) //Go through each entry as a starting point for the contiguous range
            {
                long tempSum = 0, tempMin = longInput[i], tempMax = 0; //Initialize variables to keep track of important range information (sum, min, and max). Min has to be set to first value in range in order to not be 0
                long count = i; //Create a count independent of i
            back: //Come back to here if the sum has not reached the broken value yet
                tempSum += longInput[count]; //Add the current entry to the tempSum
                tempMin = Math.Min(tempMin, longInput[count]); //Find the minimum of the current value of tempMin and the current entry
                tempMax = Math.Max(tempMax, longInput[count]); //Find the maximum of the current value of tempMax and the current entry
                if (tempSum < firstLong) //If the sum is less than the broken value
                {
                    count++; //Increment counter so we can add the next value
                    goto back; //Go to the back: above to add the next entry to the sum
                } else if (tempSum == firstLong) //If the sum is equal to the broken value then we are all set!
                {
                    weaknessSum = tempMin + tempMax; //Add up the minimum and maximum values of this range to get our part 2 answer
                    goto done2; //Exit the loop early since we are done
                }
            }
            done2: //Place to exit the loop when done
            Console.WriteLine("The first integer that cannot be written as a sum of any two of the previous 25 is {0}", firstLong); //Display part 1 answer
            Console.WriteLine("The sum of the smallest and largest integers in the contiguous range that sum to the answer to part 1 is {0}", weaknessSum); //Display part 2 answer
        }
    }
}
