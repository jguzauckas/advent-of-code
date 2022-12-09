using System;

namespace Day_11
{
    class Day11
    {
        static void Main(string[] args)
        {
            string[] originalInput = System.IO.File.ReadAllLines(@"E:\OneDrive\Documents\Personal\Learning\Advent of Code\2020\Day 11\input.txt"); //Reading each line of the input to the originalInput array
            int[,,] mapInput = new int[originalInput.Length, originalInput[0].Length, 3]; //Create 3D array to store whether each spot (on a 2D grid) is a floor or seat, if it is occupied, and an extra cell for counting
            for (int i = 0; i < originalInput.Length; i++) //Go through each entry of the input
            {
                char[] tempArray = originalInput[i].ToCharArray(); //Turn the entry into an array of characters
                for (int j = 0; j < originalInput[0].Length; j++) //Go through each character
                {
                    if (tempArray[j] == 'L') //If the character was 'L', then it is a seat
                    {
                        mapInput[i, j, 0] = 1; //1 in the first cell for seat
                        mapInput[i, j, 1] = 0; //0 in the second cell for not occupied
                        mapInput[i, j, 2] = 0; //0 in the third cell to start the counter at 0
                    }
                    else //If not seat, then floor
                    {
                        mapInput[i, j, 0] = 0; //0 in the first cell for floor
                        mapInput[i, j, 1] = 0; //0 in the second cell for not occupied
                        mapInput[i, j, 2] = 0; //0 in the third cell to start the counter at 0
                    }
                }
            }
            int unchangedCounter = 0, seatsOccupied = 0; //Create a counter to keep track of how many seats are unchanged, and how many seats are occupied
            do //See while, want to go until all seats are not changing their occupied status
            {
                for (int i = 0; i < originalInput.Length; i++) //Go through each line of the input
                {
                    for (int j = 0; j < originalInput[0].Length; j++) //Go through each column of the chosen line
                    {
                        //Note that if the spot is floor, then it will increment the counter by 0, and so it doesn't matter if we check whether or not the given spot is a floor or seat
                        mapInput[i, j, 2] = 0; //Reset the counter to 0 (important after first iteration of do loop)
                        if (i == 0 && j == 0) //Check if we are in the top left (can't go left or up)
                        {
                            mapInput[i, j, 2] += mapInput[i, j + 1, 1]; //Increment based on status of seat to right
                            mapInput[i, j, 2] += mapInput[i + 1, j, 1] + mapInput[i + 1, j + 1, 1]; //Increment based on status of seats in the next row
                        } else if (i == 0 && j == originalInput[0].Length - 1) //Check if we are in the top right (can't go right or up)
                        {
                            mapInput[i, j, 2] += mapInput[i, j - 1, 1]; //Increment based on status of seat to left
                            mapInput[i, j, 2] += mapInput[i + 1, j - 1, 1] + mapInput[i + 1, j, 1]; //Increment based on status of seats in the next row
                        } else if (i == 0) //Check if we are just in the top row (not top left or right) (can't go up)
                        {
                            mapInput[i, j, 2] += mapInput[i, j - 1, 1] + mapInput[i, j + 1, 1]; //Increment based on status of seats to left and right
                            mapInput[i, j, 2] += mapInput[i + 1, j - 1, 1] + mapInput[i + 1, j, 1] + mapInput[i + 1, j + 1, 1]; //Increment based on status of seats in the next row
                        } else if (i == originalInput.Length - 1 && j == 0) //Check if we are in the bottom left (can't go left or down)
                        {
                            mapInput[i, j, 2] += mapInput[i - 1, j, 1] + mapInput[i - 1, j + 1, 1]; //Increment based on status of seats in the previous row
                            mapInput[i, j, 2] += mapInput[i, j + 1, 1]; //Increment based on status of seat to right
                        } else if (i == originalInput.Length - 1 && j == originalInput[0].Length - 1) //Check if we are in the bottom right (can't go right or down)
                        {
                            mapInput[i, j, 2] += mapInput[i - 1, j - 1, 1] + mapInput[i - 1, j, 1]; //Increment based on status of seats in the previous row
                            mapInput[i, j, 2] += mapInput[i, j - 1, 1]; //Increment based on status of seat to left
                        } else if (i == originalInput.Length - 1) //Check if we are just in the bottom row (not bottom left or right) (can't go down)
                        {
                            mapInput[i, j, 2] += mapInput[i - 1, j - 1, 1] + mapInput[i - 1, j, 1] + mapInput[i - 1, j + 1, 1]; //Increment based on status of seats in the previous row
                            mapInput[i, j, 2] += mapInput[i, j - 1, 1] + mapInput[i, j + 1, 1]; //Increment based on status of seats to left and right
                        } else if (j == 0) //Check if we are just on the left (not top or bottom left) (can't go left)
                        {
                            mapInput[i, j, 2] += mapInput[i - 1, j, 1] + mapInput[i - 1, j + 1, 1]; //Increment based on status of seats in the previous row
                            mapInput[i, j, 2] += mapInput[i, j + 1, 1]; //Increment based on status of seat to right
                            mapInput[i, j, 2] += mapInput[i + 1, j, 1] + mapInput[i + 1, j + 1, 1]; //Increment based on status of seats in the next row
                        } else if (j == originalInput[0].Length - 1) //Check if we are just on the right (not top or bottom right) (can't go right)
                        {
                            mapInput[i, j, 2] += mapInput[i - 1, j - 1, 1] + mapInput[i - 1, j, 1]; //Increment based on status of seats in the previous row
                            mapInput[i, j, 2] += mapInput[i, j - 1, 1]; //Increment based on status of seat to left
                            mapInput[i, j, 2] += mapInput[i + 1, j - 1, 1] + mapInput[i + 1, j, 1]; //Increment based on status of seats in the next row
                        } else //Now we have no restrictions for the 8 slots surrounding the current seat
                        {
                            mapInput[i, j, 2] += mapInput[i - 1, j - 1, 1] + mapInput[i - 1, j, 1] + mapInput[i - 1, j + 1, 1]; //Increment based on status of seats in the previous row
                            mapInput[i, j, 2] += mapInput[i, j - 1, 1] + mapInput[i, j + 1, 1]; //Increment based on status of seats to left and right
                            mapInput[i, j, 2] += mapInput[i + 1, j - 1, 1] + mapInput[i + 1, j, 1] + mapInput[i + 1, j + 1, 1]; //Increment based on status of seats in the next row
                        }
                    }
                }
                unchangedCounter = 0; //Reset the counter for unchanged seats to 0 to recheck as we update seats
                for (int i = 0; i < originalInput.Length; i++) //Go through each row
                {
                    for (int j = 0; j < originalInput[0].Length; j++) //Go through each column of each row
                    {
                        if (mapInput[i, j, 0] == 1 && mapInput[i, j, 1] == 1 && mapInput[i, j, 2] >= 4) //Check that the spot is a seat, is currently occupied, and the neighboring seats are too full
                        {
                            mapInput[i, j, 1] = 0; //Empty the seat
                            seatsOccupied--; //Remove from current count of how many seats are taken
                        } else if (mapInput[i, j, 0] == 1 && mapInput[i, j, 1] == 0 && mapInput[i, j, 2] == 0) //Check that the spot is a seat, is not occupied, and the neighboring seats are empty
                        {
                            mapInput[i, j, 1] = 1; //Occupy the seat
                            seatsOccupied++; //Add this seat to the current count of how many seats are taken
                        } else //If it is floor or is a seat that didn't empty/become occupied then it is unchanged and can be counted towards that
                        {
                            unchangedCounter++; //Increment unchangedCounter to show that the spot did not change
                        }
                    }
                }
            } while (unchangedCounter < originalInput.Length * originalInput[0].Length); //Check if the unchangedCounter value is the same as columns * rows (all cells), which would mean that none changed
            Console.WriteLine("{0} end up occupied with the rules from part 1", seatsOccupied); //Write part 1 answer
            for (int i = 0; i < originalInput.Length; i++) //Go through each row of the input
            {
                for (int j = 0; j < originalInput[0].Length; j++) //Go through each cell of the row
                {
                    mapInput[i, j, 1] = 0; //Reset occupation for part 2
                    mapInput[i, j, 2] = 0; //Reset counter for part 2
                }
            }
            seatsOccupied = 0; //Reset how many seats are occupied for part 2
            do
            {
                for (int i = 0; i < originalInput.Length; i++) //Go through each row of the input
                {
                    for (int j = 0; j < originalInput[0].Length; j++) //Go through each cell of the chosen row
                    {
                        mapInput[i, j, 2] = 0; //Reset the counter for the chosen cell (doesn't matter for first loop through)
                        if (j != originalInput[0].Length - 1) //Check that we are not on the right side (wouldn't be able to go right)
                        {
                            for (int k = j + 1; k < originalInput[0].Length; k++) //Start at the next cell to the right and go until we hit the right side
                            {
                                if (mapInput[i, k, 0] == 1) //Check if the next cell in the right direction is a seat
                                {
                                    mapInput[i, j, 2] += mapInput[i, k, 1]; //Increment the counter for the current seat based on the occupation status of the checked seat
                                    break; //Exit this loop since we came across the first seat in that direction
                                }
                            }
                        }
                        if (i != originalInput.Length - 1 && j != originalInput[0].Length - 1) //Check that we are not on the bottom and right (wouldn't be able to go down and to the right)
                        {
                            for (int k = i + 1; k < originalInput.Length; k++) //Start at the next cell down and go until we hit the bottom
                            {
                                if (j + (k - i) >= originalInput[0].Length) //Use the modified row index to shift column index, and check that it doesn't go off the right side
                                {
                                    break; //Exit this loop since we checked all seats we could in that direction
                                }
                                else if (mapInput[k, j + (k - i), 0] == 1) //Check if the next cell in the down-right direction is a seat
                                {
                                    mapInput[i, j, 2] += mapInput[k, j + (k - i), 1]; //Increment the counter for the current seat based on the occupation status of the checked seat
                                    break; //Exit this loop since we came across the first seat in that direction
                                }
                            }
                        }
                        if (i != originalInput.Length - 1) //Check that we are not on the bottom side (wouldn't be able to go down)
                        {
                            for (int k = i + 1; k < originalInput.Length; k++) //Start at the next cell down and go until we hit the bottom side
                            {
                                if (mapInput[k, j, 0] == 1) //Check if the next cell in the down direction is a seat
                                {
                                    mapInput[i, j, 2] += mapInput[k, j, 1]; //Increment the counter for the current seat based on the occupation status of the checked seat
                                    break; //Exit this loop since we came across the first seat in that direction
                                }
                            }
                        }
                        if (i != originalInput.Length - 1 && j != 0) //Check that we are not on the bottom and left (wouldn't be able to go down and to the left)
                        {
                            for (int k = i + 1; k < originalInput.Length; k++) //Start at the next cell down and go until we hit the bottom
                            {
                                if (j - (k - i) < 0) //Use the modified row index to shift column index, and check that it doesn't go off the left side
                                {
                                    break; //Exit this loop since we checked all seats we could in that direction
                                }
                                else if (mapInput[k, j - (k - i), 0] == 1) //Check if the next cell in the down-left direction is a seat
                                {
                                    mapInput[i, j, 2] += mapInput[k, j - (k - i), 1]; //Increment the counter for the current seat based on the occupation status of the checked seat
                                    break; //Exit this loop since we came across the first seat in that direction
                                }
                            }
                        }
                        if (j != 0) //Check that we are not on the left side (wouldn't be able to go left)
                        {
                            for (int k = j - 1; k >= 0; k--) //Start at the next cell to the left and go until we hit the left side
                            {
                                if (mapInput[i, k, 0] == 1) //Check if the next cell in the left direction is a seat
                                {
                                    mapInput[i, j, 2] += mapInput[i, k, 1]; //Increment the counter for the current seat based on the occupation status of the checked seat
                                    break; //Exit this loop since we came across the first seat in that direction
                                }
                            }
                        }
                        if (i != 0 && j != 0) //Check that we are not on the top and left (wouldn't be able to go up and to the left)
                        {
                            for (int k = i - 1; k >= 0; k--) //Start at the next cell up and go until we hit the top
                            {
                                if (j + (k - i) < 0) //Use the modified row index to shift column index, and check that it doesn't go off the left side
                                {
                                    break; //Exit this loop since we checked all seats we could in that direction
                                }
                                else if (mapInput[k, j + (k - i), 0] == 1) //Check if the next cell in the up-left direction is a seat
                                {
                                    mapInput[i, j, 2] += mapInput[k, j + (k - i), 1]; //Increment the counter for the current seat based on the occupation status of the checked seat
                                    break; //Exit this loop since we came across the first seat in that direction
                                }
                            }
                        }
                        if (i != 0) //Check that we are not on the top side (wouldn't be able to go up)
                        {
                            for (int k = i - 1; k >= 0; k--) //Start at the next cell up and go until we hit the top side
                            {
                                if (mapInput[k, j, 0] == 1) //Check if the next cell in the up direction is a seat
                                {
                                    mapInput[i, j, 2] += mapInput[k, j, 1]; //Increment the counter for the current seat based on the occupation status of the checked seat
                                    break; //Exit this loop since we came across the first seat in that direction
                                }
                            }
                        }
                        if (i != 0 && j != originalInput[0].Length - 1) //Check that we are not on the top and right (wouldn't be able to go up and to the right)
                        {
                            for (int k = i - 1; k >= 0; k--) //Start at the next cell up and go until we hit the top
                            {
                                if (j - (k - i) >= originalInput[0].Length) //Use the modified row index to shift column index, and check that it doesn't go off the right side
                                {
                                    break; //Exit this loop since we checked all seats we could in that direction
                                }
                                else if (mapInput[k, j - (k - i), 0] == 1) //Check if the next cell in the up-right direction is a seat
                                {
                                    mapInput[i, j, 2] += mapInput[k, j - (k - i), 1]; //Increment the counter for the current seat based on the occupation status of the checked seat
                                    break; //Exit this loop since we came across the first seat in that direction
                                }
                            }
                        }
                    }
                }
                unchangedCounter = 0; //Reset the counter for unchanged seats to 0 to recheck as we update seats
                for (int i = 0; i < originalInput.Length; i++) //Go through each row
                {
                    for (int j = 0; j < originalInput[0].Length; j++) //Go through each column of each row
                    {
                        if (mapInput[i, j, 0] == 1 && mapInput[i, j, 1] == 1 && mapInput[i, j, 2] >= 5) //Check that the spot is a seat, is currently occupied, and the neighboring seats are too full
                        {
                            mapInput[i, j, 1] = 0; //Empty the seat
                            seatsOccupied--; //Remove from current count of how many seats are taken
                        }
                        else if (mapInput[i, j, 0] == 1 && mapInput[i, j, 1] == 0 && mapInput[i, j, 2] == 0) //Check that the spot is a seat, is not occupied, and the neighboring seats are empty
                        {
                            mapInput[i, j, 1] = 1; //Occupy the seat
                            seatsOccupied++; //Add this seat to the current count of how many seats are taken
                        }
                        else //If it is floor or is a seat that didn't empty/become occupied then it is unchanged and can be counted towards that
                        {
                            unchangedCounter++; //Increment unchangedCounter to show that the spot did not change
                        }
                    }
                }
            } while (unchangedCounter < originalInput.Length * originalInput[0].Length); //Check if the unchangedCounter value is the same as columns * rows (all cells), which would mean that none changed
            Console.WriteLine("{0} end up occupied with the rules of part 2", seatsOccupied); //Write the answer for part 2
        }
    }
}
