using System;

namespace Day_13
{
    class Day13
    {
        static void Main(string[] args)
        {
            string[] originalInput = System.IO.File.ReadAllLines(@"E:\OneDrive\Documents\Personal\Learning\Advent of Code\2020\Day 13\input.txt"); //Reading each line of the input to the originalInput array
            int time = int.Parse(originalInput[0]); //Read the first line as the time for part 1
            string[] tempBusIDs = originalInput[1].Split(','); //Split the second line into strings based on the comma as the delimiter
            int length = 0; //Create a counter for length (length being the number of non-empty entries)
            for (int i = 0; i < tempBusIDs.Length; i++) //Go through each string in the split line 2
            {
                if (tempBusIDs[i] != "x") //Check if the entry is not an x
                {
                    length++; //Increment the length because this entry has a bus ID
                }
            }
            int[,] busIDs = new int[length, 2]; //Create a 2D array to store the bus ID number and its position for each non-empty entry
            int[] busArrivals = new int[length]; //Create an array to store bus arrival times
            int counter = 0; //Create a counter for the loop below
            int maxID = 0; //Create a variable to store the maximum ID number (to later be able to determine the latest possible time to find the minimum time)
            for (int i = 0; i < tempBusIDs.Length; i++) //Go through each split entry
            {
                if (tempBusIDs[i] == "x") //Check if the entry is not an ID
                {
                    continue; //Skip this loop iteration
                } else //Otherwise, must be a bus ID
                {
                    busIDs[counter, 0] = int.Parse(tempBusIDs[i]); //Parse the string as an integer for the bus ID
                    busIDs[counter, 1] = i; //Store the location of this bus ID in the list (for part 2)
                    maxID = Math.Max(maxID, busIDs[counter, 0]); //Check if the current ID is larger than the max ID, and store the greater
                    counter++; //Increment the counter so we can store in the right spot of our bus ID array
                }
            }
            int earliestArrival = time + maxID; //Start the earliest arrival time as the maximum time a bus would take to arrive (the time start + the largest bus ID
            int earliestArrivalID = 0; //Create a variable to store the ID of the earliest bus
            for (int i = 0; i < busArrivals.Length; i++) //Go through each bus ID
            {
                do //See while for information
                {
                    busArrivals[i] += busIDs[i, 0]; //Add the bus ID to the arrival time until it reaches the given time
                } while (busArrivals[i] <= time); //Go until we get the first time the bus with this ID would arrive after the given time
                if (busArrivals[i] < earliestArrival) //Check if the new arrival time is sooner than the current saved one
                {
                    earliestArrival = busArrivals[i]; //Save it's arrival time as the new earliest
                    earliestArrivalID = busIDs[i, 0]; //Save it's ID
                }
            }
            int answerPart1 = earliestArrivalID * (earliestArrival - time); //Create a variable for the part 1 answer that multiplies the bus ID by how long you'd wait for the earliest
            Console.WriteLine("{0}", answerPart1); //Write out part 1
            long importantTime = busIDs[0, 0]; //Create a variable to store the important time for part 2
            long mod = 0; //Create a variable to store an intermediate value
            long increment = busIDs[0, 0]; //Create a variable to store our incrementing value
            for (int i = 1; i < length; i++) //Go through each bus ID
            {
                mod = busIDs[i, 0] - (busIDs[i, 1] % busIDs[i, 0]); //Store the bus ID minus its position mod ID
                do //Find important time that works for all previous IDs and new ID
                {
                    importantTime += increment; //Keep incrementing until the new time works
                } while (importantTime % busIDs[i, 0] != mod); //Check if the current time lines up for the new ID
                long a = increment; //Create temporary variables to find GCD
                long b = busIDs[i, 0]; //Create temporary variables to find GCD
                do //Find GCD of increment and ID
                {
                    long t = b; //Create temporary variable to go between a and b
                    b = a % b; //Store b as the remainder of a divided by b
                    a = t; //Store as a as b
                } while (b != 0); //Go until b is 0
                increment = (increment * busIDs[i, 0]) / a; //Find the LCM of increment and the ID for the new increment
            }
            Console.WriteLine("{0}", importantTime); //Write out part 2
        }
    }
}
