using System;

namespace Day_1
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] originalExpenses = System.IO.File.ReadAllLines(@"E:\OneDrive\Documents\Personal\Learning\Advent of Code\2020\Day 1\input.txt"); //Read each line of text file as string into originalExpenses array
            int[] expenses = new int[originalExpenses.Length]; //Create a new array to store the parsed integer values from originalExpenses array
            for (int i = 0; i < originalExpenses.Length; i++) //Using i to go through each cell of originalExpenses array
            {
                expenses[i] = int.Parse(originalExpenses[i]); //Parsing strings from cell of originalExpenses array into integers in cell of expenses array
            }
            int twoSum = 0, threeSum = 0; //Defining two variables for our outputs, twoSum will be the multiplication of the two numbers that sum to 2020, threeSum will be the multiplication of the three numbers that sum to 2020
            for (int i = 0; i < expenses.Length; i++)
            {
                for (int j = 0; j < expenses.Length; j++) //Using i and j to create all possible pairs of cells in the same array
                {
                    if (i != j && expenses[i] + expenses[j] == 2020) //Making sure the indices will be two unique cells, and checking if the sum is 2020
                    {
                        twoSum = expenses[i] * expenses[j]; //Setting twoSum to the multiplication we want
                        goto done1; //Escaping the for loops early to save time
                    }
                }
            }
            done1:
            for (int i = 0; i < expenses.Length; i++)
            {
                for (int j = 0; j < expenses.Length; j++)
                {
                    for (int k = 0; k < expenses.Length; k++) //Using i, j, and k to create every possible set of three cells in the same array
                    {
                        if ( i != j && i != k && j != k && expenses[i] + expenses[j] + expenses[k] == 2020) //Making sure the indices will be three unique cells, and checking if the sum is 2020
                        {
                            threeSum = expenses[i] * expenses[j] * expenses[k]; //Setting threeSum to the multiplication we want
                            goto done2; //Escaping the for loops early to save time
                        }
                    }
                }
            }
            done2:
            Console.WriteLine("The two entries that sum to 2020 multiplied together are {0}", twoSum);
            Console.WriteLine("The three entries that sum to 2020 multiplied together are {0}", threeSum); //Writing out answers in console
        }
    }
}
