using System;

namespace Day_2
{
    class Day2
    {
        static void Main(string[] args)
        {
            string[] originalInfo = System.IO.File.ReadAllLines(@"E:\OneDrive\Documents\Personal\Learning\Advent of Code\2020\Day 2\input.txt"); //Read each line of text file as string into originalInfo array
            string[,] splitInfo = new string[originalInfo.Length, 3]; //Creating array to store each line in three pieces
            for (int i = 0; i < originalInfo.Length; i++) //Walk through each entry of originalInfo to fill splitInfo
            {
                string[] tempArray = originalInfo[i].Split(' '); //tempArray holds strings split up based on spaces as delimiters (data is formatted such that there should be three strings)
                splitInfo[i, 0] = tempArray[0]; //Store the amount/location amounts as a string, ex. '16-18'
                splitInfo[i, 1] = tempArray[1]; //Store the character to check as a string, ex. 'h:'
                splitInfo[i, 2] = tempArray[2]; //Store the password itself as a string, ex. 'hhhhhhhhhhhhhhhhhh'
            }
            int[,] letterRequirements = new int[originalInfo.Length, 2]; //Creating array to store the amounts/locations as two integers (first column lower, second column upper)
            char[] letterRequired = new char[originalInfo.Length]; //Creating array to store the character to check as a character
            for (int i = 0; i < originalInfo.Length; i++) //Go through each entry of splitInfo, which has number of rows equal to length of originalInfo
            {
                string[] tempArray = splitInfo[i, 0].Split('-'); //tempArray holds strings split up based on hyphens as delimiters (data is formatted such that there should be two strings)
                letterRequirements[i, 0] = int.Parse(tempArray[0]); //Parse the first string (lower bound) as an integer and store
                letterRequirements[i, 1] = int.Parse(tempArray[1]); //Parse the second string (upper bound) as an integer and store
                letterRequired[i] = char.Parse(splitInfo[i, 1].Remove(1)); //Take the string with the character to be checked (ex. 'h:'), remove the ':' by removing all characters after first, and parsing and storing as character
            }
            int[,] passwordCheck = new int[originalInfo.Length, 3]; //Creating array to store password validation information (first column for how many times checked character shows up, second column for
                                                                    //failing (0)/passing (1) old validation, third column for counting new validation positions with the correct character (1 means passing))
            int oldValidCounter = 0, newValidCounter = 0, bothValidCounter = 0; //Counters for displaying totals that passed old, new, and both validations
            for (int i = 0; i < originalInfo.Length; i++) //Go through each password to check for validation, which has number of rows equal to length of originalInfo
            {
                char[] tempArray = splitInfo[i, 2].ToCharArray(); //tempArray holds the password split into individual characters
                for (int j = 0; j < tempArray.Length; j++) //Go through each cell of tempArray (and therefore each character of the password)
                {
                    if (tempArray[j] == letterRequired[i]) //Check to see if the cell is the letter we are checking for
                    {
                        passwordCheck[i, 0]++; //Count that the cell has the right letter
                    }
                }
                if (passwordCheck[i, 0] >= letterRequirements[i, 0] && passwordCheck[i, 0] <= letterRequirements[i, 1]) //Check that the count of the character we checked falls between the lower and upper bounds
                {
                    passwordCheck[i, 1] = 1; //Set the second column to 1 to know that it passed the old validation
                    oldValidCounter++; //Increment the old counter since this password passed the old validation
                }
                else //if it failed the if, it did not meet the old validation requirements
                {
                    passwordCheck[i, 1] = 0; //Set the second column to 0 to know that it failed the old validation
                }
                for (int j = 0; j < 2; j++) //Go through the two positions we want to check for the new validation
                {
                    if (tempArray[letterRequirements[i, j] - 1] == letterRequired[i]) //Use letterRequirements to get the positions (subtract 1 to account for zero indices) and check that the character is correct
                    {
                        passwordCheck[i, 2]++; //Count that the position had the character we are checking
                    }
                }
                if (passwordCheck[i, 2] == 1) //Check that the character was only in one of the two positions (not neither or both) to pass new validation
                {
                    newValidCounter++; //Increment the new counter since this password passed the new validation
                }
                if (passwordCheck[i, 1] == 1 && passwordCheck[i, 2] == 1) //Check to see if it passed both old and new validations
                {
                    bothValidCounter++; //Increment the both counter since this password passed both validations
                }
            }
            Console.WriteLine("The total number of old valid passwords is {0}", oldValidCounter); //Display number of old valid passwords using old counter
            Console.WriteLine("The total number of new valid passwords is {0}", newValidCounter); //Display number of new valid passwords using new counter
            Console.WriteLine("The total number of both valid passwords is {0}", bothValidCounter); //Display number of both valid passwords using both counter
        }
    }
}
