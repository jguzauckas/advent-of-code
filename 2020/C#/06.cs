using System;

namespace Day_6
{
    class Day6
    {
        static void Main(string[] args)
        {
            string[] originalInput = System.IO.File.ReadAllLines(@"E:\OneDrive\Documents\Personal\Learning\Advent of Code\2020\Day 6\input.txt"); //Reading each line of the input to the originalInput array
            int lengthCounter = 1; //Constant to count how many entries are in the input (start at 1 to account for counting blank lines, which would exclude the last entry)
            for (int i = 0; i < originalInput.Length; i++) //Go through each cell of originalInput to determine how many entries/groups there are)
            {
                if (originalInput[i] == "") //Check to see if chosen cell is a blank line (the delimiter between entries/groups)
                {
                    lengthCounter++; //If chosen cell is blank line, increment counter (+1 entry in count)
                }
            }
            string[] cleanedInput = new string[lengthCounter]; //Use lengthCounter to set the right size for cleanedInput (one entry of input per cell)
            int[] peoplePerGroup = new int[lengthCounter];
            int counter = 0; //Counter for do loop to make sure the final entry of originalInput is read without going out of array bounds
            for (int i = 0; i < cleanedInput.Length; i++) //Go through each cell of cleanedInput (one loop for each entry)
            {
                do //See while for information
                {
                    if (cleanedInput[i] == "") //Check to see if this is the first iteration of the do loop for this cell (cell would be blank)
                    {
                        cleanedInput[i] = originalInput[counter]; //Set blank cell to be the first line of the entry
                        peoplePerGroup[i] = 1; //Now that the entry is not empty, set the group size to one person
                    }
                    else //If this is not the first iteration of the do loop for this cell (cell is not blank)
                    {
                        cleanedInput[i] = cleanedInput[i] + ' ' + originalInput[counter]; //Keep original cell contents and add the next line of the entry with a space in between
                        peoplePerGroup[i]++; //Add a person to the group size since we kept track of their input
                    }
                    counter++; //Increment counter to note moving to the next line
                } while (counter < originalInput.Length && originalInput[counter] != ""); //Break the loop when we hit a blank line or exceed the bounds of originalInput
                counter++; //Skip the blank line the do loop ran into before restarting for loop
            }
            int[,] questionTracker = new int[cleanedInput.Length, 26]; //Create a multidimensional array that will keep track of how many of each character (question) each entry/group has
            int[,] countYes = new int[cleanedInput.Length, 2]; //Create a multidimensional array to keep track of how many people answered yes to questions (both total and intersection)
            int totalUnionYes = 0, totalIntersectionYes = 0; //Two variables to total outputs
            for (int i = 0; i < cleanedInput.Length; i++) //Go through each entry/group
            {
                char[] tempArray = cleanedInput[i].ToCharArray(); //Turn the entry into an array of characters to check each question
                foreach (char answer in tempArray) //Go through each character from the entry
                {
                    if (answer != ' ') //Check to make sure the chosen character is not a space
                    {
                        questionTracker[i, answer - 97]++; //Increment the cell that holds the given character for this entry (answer - 97 zero indexes lower case unicode values for array)
                        if (questionTracker[i, answer - 97] == 1) //Check if this was the first character of its kind for the entry (only ++ once)
                        {
                            countYes[i, 0]++; //Mark this question as yes for whole group for part 1
                        }
                        if (questionTracker[i, answer - 97] == peoplePerGroup[i]) //Check if this question was answered yes by every person (value from ++ would be the group size)
                        {                                                         //Not else if because of the potential of group sizes of 1
                            countYes[i, 1]++; //Mark this question as yes for whole group for part 2
                        }
                    }
                }
                totalUnionYes += countYes[i, 0]; //Add the yes for part 1 from this entry to the total
                totalIntersectionYes += countYes[i, 1]; //Add the yes for part 2 from this entry to the total
            }
            Console.WriteLine("The total number of questions answered yes to are {0}", totalUnionYes); //Write out the answer to part 1
            Console.WriteLine("The total number of questions shared by a group are {0}", totalIntersectionYes); //Write out the answer to part 2
        }
    }
}
