using System;

namespace Day_10
{
    class Day10
    {
        static void Main(string[] args)
        {
            string[] originalInput = System.IO.File.ReadAllLines(@"E:\OneDrive\Documents\Personal\Learning\Advent of Code\2020\Day 10\input.txt"); //Reading each line of the input to the originalInput array
            int[] intInput = new int[originalInput.Length]; //Create an array of ints to parse the input into
            int joltMax = 0;
            for (int i = 0; i < originalInput.Length; i++) //Go through each entry of the input
            {
                intInput[i] = int.Parse(originalInput[i]); //Set the long array entry to the long parse of the entry
                joltMax = Math.Max(joltMax, intInput[i]); //Find the maximum of the current max and the current value to see what the highest jolt is (to determine device jolts)
            }
            int[] orderedIntInput = new int[intInput.Length + 2]; //Create an integer array to hold the adapter jolts in order (and a leading 0 for the outlet and the device at the end)
            orderedIntInput[0] = 0; //Set the first value to the outlet jolts of 0
            orderedIntInput[intInput.Length + 1] = joltMax + 3; //Set the last value to the device jolts of the highest adapter + 3
            int[] joltDifferences = new int[3]; //Create an array to keep track of how many differences of 1, 2, 3 jolts there are
            int currentJolts = 0, counter = 0; //Create two counters, one for the current jolts as we add adapters, and the other for keeping track of how many adapters we've used
            do
            {
                for (int i = 0; i < intInput.Length; i++) //Go through each adapter to look for 1 jolt difference
                {
                    if (intInput[i] - currentJolts == 1) //If the adapter is 1 jolt above the current jolts (starts at 0, so if its 1)
                    {
                        currentJolts = intInput[i]; //Set the adapter jolts as the new current jolts
                        joltDifferences[0]++; //Increment the 1 jolt difference cell
                        orderedIntInput[counter + 1] = intInput[i]; //Set the ordered list value to this adapter (prioritizing 1 jolt difference over 2 or 3)
                        goto done; //Exit the for loop to restart and look for the next adapter
                    }
                }
                for (int i = 0; i < intInput.Length; i++) //Go through each adapter to look for 2 jolt difference
                {
                    if (intInput[i] - currentJolts == 2) //If the adapter is 2 jolts above the current jolts (starts at 0, so if its 2)
                    {
                        currentJolts = intInput[i]; //Set the adapter jolts as the new current jolts
                        joltDifferences[1]++; //Increment the 2 jolt difference cell
                        orderedIntInput[counter + 1] = intInput[i]; //Set the ordered list value to this adapter (prioritizing 2 jolts difference over 3)
                        goto done; //Exit the for loop to restart and look for the next adapter
                    }
                }
                for (int i = 0; i < intInput.Length; i++) //Go through each adapter to look for 3 jolt difference
                {
                    if (intInput[i] - currentJolts == 3) //If the adapter is 3 jolts above the current jolts (starts at 0, so if its 3)
                    {
                        currentJolts = intInput[i]; //Set the adapter jolts as the new current jolts
                        joltDifferences[2]++; //Increment the 3 jolt difference cell
                        orderedIntInput[counter + 1] = intInput[i]; //Set the ordered list value to this adapter (prioritizing 3 jolts difference last)
                        goto done; //Exit the for loop to restart and look for the next adapter
                    }
                }
            done: //Where the for loops exit to
                counter++; //Increment the counter to look for the next adapter in the list (using as index)
            } while (counter < intInput.Length); //Go until we've used every adapter
            int answer1 = joltDifferences[0] * (joltDifferences[2] + 1); //Calculate part 1 answer based on number of adapters with a jolt difference of one times jolt difference of 3
            int[] joltJumps = new int[intInput.Length + 1]; //Create integer array to look at the jolt difference between each adapter in the list and its precursor
            for (int i = 0; i < intInput.Length + 1; i++) //Go through each entry of the ordered list
            {
                joltJumps[i] = orderedIntInput[i + 1] - orderedIntInput[i]; //Calculate the jolt difference between the current entry and the previous in the ordered list
            }
            int[] joltGroups = new int[intInput.Length]; //Create integer array to count the size of any groups of sequential 1 jolt differences (there are no 2's, and 3's won't affect the unique arrangements)
            int j = 0; //Create counting variable to keep track of what entries we've considered already
            counter = 0; //Reset counter variable to 0 to reuse for next do loop
            do
            {
                for (int i = j; i < joltJumps.Length; i++) //Go through each entry of joltJumps, starting from various points based on j (starts at 0)
                {
                    if (joltJumps[i] == 1) //Check if the entry is a jolt difference of 1
                    {
                        joltGroups[counter]++; //Increment the size of this particular group by 1
                    } else if (joltJumps[i] == 3 && joltGroups[counter] == 0) //Check if the entry is a jolt difference of 3 and we have not added any 1s to the current group
                    {
                        j = i + 1; //Set the counting variable to the next entry
                        goto loop; //Exit the for loop to be able to go through again starting from the next entry
                    } else if (joltJumps[i] == 3 && joltGroups[counter] > 0) //Check if the entry is a jolt difference of 3 and we have a group of some size
                    {
                        j = i + 1; //Set the counting variable to the next entry
                        counter++; //Increment the counter to note that the current group has ended and any future entries will have to be in future groups
                        goto loop; //Exit the for loop to be able to go through again starting from the next entry
                    }
                }
            loop:; //Where we exit the for loop
            } while (j < intInput.Length); //Go until the counting variable j is set to some index outside of the bounds of the number of entries
            long answer2 = 1; //Create a long to store the answer for part 2 (will be beyond int32 limit)
            for (int i = 0; i < intInput.Length; i++) //Go through each entry of the joltGroups array
            {
                if (joltGroups[i] != 0) //Check to make sure the entry has a group size that is bigger than 0
                {
                    if (joltGroups[i] == 1) //Check if the group size is 1 (only one 1 surrounded by 3's)
                    {
                        answer2 *= 1; //Multiply the current answer2 by how many options this group creates for our arrangements
                    }                 //A group of 1 cannot have any adapters eliminated because then the outer 3's would be too far apart
                    else if (joltGroups[i] == 2) //Check if the group size is 2 (two 1's surrounded by 3's)
                    {
                        answer2 *= 2; //Multiply the current answer2 by how many options this group creates for our arrangements
                    }                 //A group of 2 can only have one adapter potentially eliminated, since otherwise the outer 3's would be too far apart
                    else if (joltGroups[i] == 3) //Check if the group size is 3 (three 1's surrounded by 3's)
                    {
                        answer2 *= 4; //Multiply the current answer2 by how many options this group creates for our arrangements
                    }                 //A group of 3 can only have two adapters potentially eliminated, since otherwise the outer 3's would be too far apart, then look at all individual adapters and the pairs
                    else if (joltGroups[i] == 4) //Check if the group size is 4 (four 1's surrounded by 3's), we don't have more than four 1's in this case
                    {
                        answer2 *= 7; //Multiply the current answer2 by how many options this group creates for our arrangements
                    }                 //A group of 4 can only have three adapters potentially eliminated, since otherwise the outer 3's would be too far apart, then look at all individual adapters and the pairs
                }
            }
            Console.WriteLine("The number of 1-jolt differences multiplied by the number of 3-jolt differences is {0}", answer1); //Write out answer for part 1
            Console.WriteLine("The total number of distinct ways you can arrange the adapters to connect the charing outlet to your device is {0}", answer2); //Write out answer for part 2
        }
    }
}
