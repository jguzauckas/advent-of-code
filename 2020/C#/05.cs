using System;

namespace Day_5
{
    class Day5
    {
        static void Main(string[] args)
        {
            string[] originalInput = System.IO.File.ReadAllLines(@"E:\OneDrive\Documents\Personal\Learning\Advent of Code\2020\Day 5\input.txt"); //Read each line of the input into the originalInput array as strings
            int[,] binInput = new int[originalInput.Length, 10]; //Create an integer array to convert each char of the entry to binary bits stored as integers
            for (int i = 0; i < originalInput.Length; i++) //Go through each entry of the input
            {
                char[] tempArray = originalInput[i].ToCharArray(); //Turn the given entry into a temporary array of its characters
                for (int j = 0; j < 7; j++) //Go through the first seven characters, which will tell us about the row number
                {
                    if (tempArray[j] == 'F') //Check the given character to see if it is 'F'
                    {
                        binInput[i, j] = 0; //'F' would mean the lower half, which is 0 in a binary representation of a number
                    }
                    else if (tempArray[j] == 'B') //Check the given character to see if it is 'B'
                    {
                        binInput[i, j] = 1; //'B' would mean the upper half, which is 1 in a binary representation of a number
                    }
                }
                for (int j = 7; j < 10; j++)
                {
                    if (tempArray[j] == 'L') //Check the given character to see if it is 'L'
                    {
                        binInput[i, j] = 0; //'L' would mean the lower half, which is 0 in a binary representation of a number
                    }
                    else if (tempArray[j] == 'R') //Check the given character to see if it is 'R'
                    {
                        binInput[i, j] = 1; //'R' would mean the upper half, which is 1 in a binary representation of a number
                    }
                }
            }
            int[,] seatTracking = new int[128, 8]; //Create an array with a cell for each seat to keep track of whether that seat is empty (0 or not initialized), and filled (1)
            int currentMax = 0; //Create an integer variable to keep track of the highest seat ID taken
            for (int i = 0; i < originalInput.Length; i++) //Go through each entry of the input
            {
                int seatRow = 0, seatColumn = 0; //Create integer variables to temporarily store the row and column of the seat of the current entry
                for (int j = 0; j < 7; j++) //Go through the first seven characters of the entry, which will tell us about the row number
                {
                    seatRow += Convert.ToInt32(binInput[i, j] * Math.Pow(2, 6 - j)); //Converts each binary value to a decimal based on place (first corresponds to 6th power, last corresponds to 0th power)
                }                                                                    //and adds to the current seatRow value to get the total after going through all places
                for (int j = 0; j < 3; j++) //Go through the last three characters of the entry, which will tell us about the column number
                {
                    seatColumn += Convert.ToInt32(binInput[i, j + 7] * Math.Pow(2, 2 - j)); //Converts each binary value to a decimal based on place (first corresponds to 2nd power, last corresponds to 0th power)
                }                                                                           //and adds to the current seatColumn value to get the total after going through all places
                seatTracking[seatRow, seatColumn] = 1; //Go to the cell denoted by our calculated seat row and column and store a one to mark it taken by this entry
                currentMax = Math.Max(currentMax, ((seatRow * 8) + seatColumn)); //Set the variable to be the max of its current value and the seat ID of this entry (thereby getting the highest value after loop)
            }
            int yourSeat = -1; //Create a variable to store the ID of your seat (initialized at -1 since it is not a valid seat ID)
            for (int i = 0; i < 128; i++) //Go through each row of the plane
            {
                for (int j = 0; j < 8; j++) //Go through each seat in the chosen row
                {
                    int iLess = 0, jLess = 0, iMore = 0, jMore = 0; //Create variables to check the seat ID's +1 and -1 from the current entry
                    if ((i == 0 && j == 0) || (i == 127 && j == 7)) //Check if we are on the first or last seat of the plane
                    {
                        //Do Nothing, since there is no seat -1 from the first or +1 from the last, it cannot be our seat
                    } else if (j == 0) //Now check to see if we are in the first seat of a row, since j-1 (-1) would not be a seat in the current row
                    {
                        iLess = i - 1; //Set the seat ID -1 from ours to be on the previous row, since j-1 (-1) would not be a seat in the current row
                        jLess = 7; //Set the seat ID -1 from ours to be at the end of the previous, since that ID is -1 from the seat we are checking
                        iMore = i; //Set the seat ID +1 from ours to be in the same row, since j+1 (1) is in the same row as the seat we are checking
                        jMore = j + 1; //Set the seat ID +1 from ours to be the next seat over in the row, since j+1 (1) is in the same row as the seat we are checking
                    } else if (j == 7) //Now check to see if we are in the last seat of a row, since j+1 (8) would not be a seat in the current row
                    {
                        iLess = i; //Set the seat ID -1 from ours to be in the same row, since j-1 (6) is in the same row as the seat we are checking
                        jLess = j - 1; //Set the seat ID -1 from ours to be the previous seat in the row, since j-1 (6) is in the same row as the seat we are checking
                        iMore = i + 1; //Set the seat ID +1 from ours to be in the next row, since j+1 (8) would not be a seat in the current row
                        jMore = 0; //Set the seat ID +1 from ours to be at the beginning of the next, since that ID is +1 from the seat we are checking
                    } else //Now we have eliminated wraparound problems and can set the -1 and +1 IDs more simply
                    {
                        iLess = i; //-1 is the same row as the seat we are checking
                        jLess = j - 1; //-1 is just one column below the seat we are checking
                        iMore = i; //+1 is the same row as the seat we are checking
                        jMore = j + 1; //+1 is just one column above the seat we are checking
                    }
                    if (seatTracking[iLess, jLess] == 1 && seatTracking[i, j] == 0 && seatTracking[iMore, jMore] == 1) //Check that the seat we are checking is empty and the seats +1 and -1 from it are empty
                    {
                        yourSeat = (i * 8) + j; //Since this seat met the requirements of being empty and the seats next to it being taken, set our seat ID to be the ID of the seat we checked
                    }
                }
            }
            Console.WriteLine("The highest seat  ID on a boarding pass is {0}", currentMax); //Write out the highest seat ID on a boarding pass using our currentMax variable from earlier
            Console.WriteLine("Your seat ID is {0}", yourSeat); //Write our our seat ID using the yourSeat variable (if we get -1, we know that we did not find a seat that met the requirements)
        }
    }
}
