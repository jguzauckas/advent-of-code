using System;

namespace Day_7
{
    class Day7
    {
        static void Main(string[] args)
        {
            string[] originalInput = System.IO.File.ReadAllLines(@"E:\OneDrive\Documents\Personal\Learning\Advent of Code\2020\Day 7\input.txt"); //Reading each line of the input to the originalInput array
            string[][] splitInput = new string[originalInput.Length][]; //Create an array for strings from each entry and store them as an array of arrays
            int[][,] intSplitInput = new int[originalInput.Length][,]; //Create a 2D array for numbers from each entry and store them as an array of arrays
            for (int i = 0; i < originalInput.Length; i++) //Go through each entry of the input
            {
                string[] tempArray = originalInput[i].Remove(originalInput[i].Length - 1).Split(" contain "); //Split the bag the entry is about (before "contain") and what is in it (after "contain")
                string[] tempArray2 = tempArray[1].Split(", "); //Split what is in the bag into separate pieces
                splitInput[i] = new string[tempArray2.Length + 1]; //Initialize the array that will hold the strings for the entry to have the right number of cells
                intSplitInput[i] = new int[tempArray2.Length + 1, 2]; //Initialize the array that will hold the integers for the entry to have the right number of cells (column one for integers, column two for locations)
                splitInput[i][0] = tempArray[0].Remove(tempArray[0].Length - 1); //Set the first cell of the string array to be the bag that the entry is about (removing the 's' from "bags")
                for (int j = 0; j < splitInput[i].Length - 1; j++) //Go through each component of the chosen bag
                {
                    if (tempArray2[j].Substring(tempArray2[j].Length - 1) == "s") //Check to see if the number of bags is plural ("bags" instead of "bag")
                    {
                        splitInput[i][j + 1] = tempArray2[j].Remove(tempArray2[j].Length - 1).Substring(2); //Store the chosen bag component without the 's' in "bags" and without the leading number
                    } else //Otherwise it must not be plural ("bag" instead of "bags")
                    {
                        splitInput[i][j + 1] = tempArray2[j].Substring(2); //Store the chosen bag component without the leading number
                    }
                    if (splitInput[i][j + 1] != " other bag") //Check to make sure the chosen bag holds bags
                    {
                        intSplitInput[i][j + 1, 0] = int.Parse(tempArray2[j].Remove(1)); //Store the leading number of the bag component
                    }
                }
            }
            for (int i = 0; i < originalInput.Length; i++) //Go through each entry
            {
                for (int j = 1; j < splitInput[i].Length; j++) //Go through each bag component of the chosen entry
                {
                    for (int k = 0; k < originalInput.Length; k++) //Check every entry against the chosen bag component of the chosen entry
                    {
                        if (splitInput[i][j] == " other bag") //Check if the chosen bag component of the chosen entry is empty
                        {
                            intSplitInput[i][j, 1] = -1; //Mark the location of the empty bag as -1
                        } else if (splitInput[i][j] == splitInput[k][0]) //Empty bag components have been eliminated, now check that the bag component matches the entry
                        {
                            intSplitInput[i][j, 1] = k; //Store the location of the entry matching this bag component as the pair of the component value
                            goto done1; //Exit the k loop early to save time
                        }
                    }
                done1:;
                }
            }
            int count = 0; //Create counting variable for do while loops
            int[,] holdTracking = new int[originalInput.Length, 2]; //Create a 2D array where the first column keeps track of whether the chosen bag can contain a shiny gold and second column
                                                                    //is a counter for if its subcomponents don't contain a shiny gold bag
            do //See while for loop information
            {
                count = 0; //Reset counter to 0 each loop, to be incremented if a bag has not been determined to hold a shiny gold bag or not
                for (int i = 0; i < originalInput.Length; i++) //Go through each entry
                {
                    holdTracking[i, 1] = 0; //Reset the counter for subcomponents not containing shiny gold bag each loop
                    if (holdTracking[i, 0] == 0) //Check that the bag has not been determined
                    {
                        for (int j = 1; j < splitInput[i].Length; j++) //Go through each bag component of the entry
                        {
                            if (splitInput[i][j] == "shiny gold bag") //Check if the bag component is a shiny gold bag
                            {
                                holdTracking[i, 0] = 2; //2 means that the entry does contain a shiny gold bag
                                goto done2; //Exit this entry of the loop since it has been determined
                            } else if (splitInput[i][j] == " other bag") //Check if the entry is empty
                            {
                                holdTracking[i, 1]++; //Increment the subcomponent counter since it does not contain any other bags
                            } else //Down to just other bag subcomponents
                            {
                                for (int k = 0; k < originalInput.Length; k++) //Go through each entry to check against current subcomponent
                                {
                                    if (splitInput[i][j] == splitInput[k][0] && holdTracking[k, 0] == 2) //Check that current bag component matches the entry, and if the chosen checked entry contains a shiny golf
                                    {
                                        holdTracking[i, 0] = 2; //Component bag contains shiny gold bag so the chosen entry does by transitive property
                                        goto done2; //Exit this entry of the loop since it has been determined
                                    } else if (splitInput[i][j] == splitInput[k][0] && holdTracking[k, 0] == 1) //Check that current bag component matches the entry, and if the chosen checked entry does not contain a shiny gold
                                    {
                                        holdTracking[i, 1]++; //Increment the subcomponent counter since the component bag does not contain a shiny gold bag
                                    }
                                }
                            }
                            if (holdTracking[i, 1] == splitInput[i].Length - 1) //Check if the subcomponent counter has reached the number of subcomponents (every subcomponent does not contain a shiny gold bag
                            {
                                holdTracking[i, 0] = 1; //1 means that the entry does not contain a shiny gold bag
                                goto done2; //Exit this entry of the loop since it has been determined
                            }
                        }
                    }
                    if (holdTracking[i, 0] == 0) //Check if the chosen entry is not determined yet
                    {
                        count++; //Increment counter so we know not all bags are determined
                    }
                done2:;
                }
            } while (count > 0); //Go until all bags are determined to hold a shiny gold bag or not (will not get incremented if all determined)
            count = 0; //Reset counter for next loop
            //Determine how many bags each bag contains to be able to calculate how many bags a shiny gold bag contains
            do //See while for loop information
            {
                for (int i = 0; i < originalInput.Length; i++) //Go through each entry
                {
                    intSplitInput[i][0, 0] = 0; //Reset the number of bags the entry holds to zero to recalculate
                    for (int j = 1; j < splitInput[i].Length; j++) //Go through each component bag of the entry
                    {
                        if (intSplitInput[i][j, 1] == -1) //Check if the component bag is "no other bags"
                        {
                            intSplitInput[i][0, 0] += intSplitInput[i][j, 0]; //Since it was no other bags, this adds 0 to the count
                        } else //Component bag must now be an actual bag
                        {
                            intSplitInput[i][0, 0] += intSplitInput[i][j, 0] * (1 + intSplitInput[intSplitInput[i][j, 1]][0, 0]); //Add the number of this component bag times the number of bags it holds
                        }                                                                                                         //(+1 to account for component bag itself)
                    }
                }
                count++; //Increment counter for one full cycle of calculating
            } while (count < originalInput.Length); //Since bags can't be defined recursively, the number of entries is the highest number of times that this needs to be done to
                                                    //successfully define how many bags every bag holds
            int totalHold = 0; //Create variable to find out how many bags contain shiny gold bags
            int totalGoldHold = 0; //Create a variable to find out how many bags each shiny gold bag contains
            for (int i = 0; i < originalInput.Length; i++) //Go through each entry
            {
                if (holdTracking[i, 0] == 2) //Check if the entry holds a shiny gold bag
                {
                    totalHold++; //Increment the counter for how many bags contain a shiny gold bag
                }
                if (splitInput[i][0] == "shiny gold bag") //Check if the entry is the shiny gold bag
                {
                    totalGoldHold = intSplitInput[i][0, 0]; //Take how many bags that the shiny gold bag was calculated to hold
                }
            }
            Console.WriteLine("The number of bags that can ultimately hold a shiny gold bag is {0}", totalHold); //Report the number of bags containing the shiny gold bag
            Console.WriteLine("The number of bags that a single gold bag can hold is {0}", totalGoldHold); //Report the number of bags that a shiny gold bag contains
        }
    }
}
