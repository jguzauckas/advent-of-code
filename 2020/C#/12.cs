using System;

namespace Day_12
{
    class Day12
    {
        static void Main(string[] args)
        {
            string[] originalInput = System.IO.File.ReadAllLines(@"E:\OneDrive\Documents\Personal\Learning\Advent of Code\2020\Day 12\input.txt"); //Reading each line of the input to the originalInput array
            string[] directions = new string[originalInput.Length]; //Create a string array to store the directions of each entry (could have used char)
            int[] values = new int[originalInput.Length]; //Create an integer array to store the integer associated with each entry
            for (int i = 0; i < originalInput.Length; i++) //Go through each entry
            {
                directions[i] = originalInput[i].Remove(1); //Remove the integer, and just keep the single character direction
                values[i] = int.Parse(originalInput[i].Substring(1)); //Start after the direction and parse as an integer
            }
            int shipDirection = 0; //0 means east, 90 means south, 180 means west, 270 means north (ship direction will always be modified mod 4)
            int[] shipXY = { 0, 0 }; //First cell is East-West, second cell is North-South
            for (int i = 0; i < originalInput.Length; i++) //Go through each instruction
            {
                if (directions[i] == "F") //Check if the instruction is forward
                {
                    if (shipDirection == 0) //Check if the current direction is East
                    {
                        shipXY[0] += values[i]; //Increment the X cell positively based on the value
                    } else if (shipDirection == 90) //Check if the current direction is South
                    {
                        shipXY[1] -= values[i]; //Increment the Y cell negatively based on the value
                    } else if (shipDirection == 180) //Check if the current direction is West
                    {
                        shipXY[0] -= values[i]; //Increment the X cell negatively based on the value
                    } else if (shipDirection == 270) //Check if the current direction is North
                    {
                        shipXY[1] += values[i]; //Increment the Y cell positively based on the value
                    }
                } else if (directions[i] == "N") //Check if the instruction is North
                {
                    shipXY[1] += values[i]; //Increment the Y cell positively based on the value
                } else if (directions[i] == "S") //Check if the instruction is South
                {
                    shipXY[1] -= values[i]; //Increment the Y cell negatively based on the value
                } else if (directions[i] == "E") //Check if the instruction is East
                {
                    shipXY[0] += values[i]; //Increment the X cell positively based on the value
                } else if (directions[i] == "W") //Check if the instruction is West
                {
                    shipXY[0] -= values[i]; //Increment the X cell negatively based on the value
                } else if (directions[i] == "L") //Check if the instruction is Left
                {
                    shipDirection += (360 - values[i]); //Increment the shipDirection based on the value (translating the left turn into a right turn)
                } else if (directions[i] == "R") //Check if the instruction is Right
                {
                    shipDirection += values[i]; //Increment the shipDirection based on the value
                }
                if (shipDirection > 270) //Check to see if the shipDirection has completed a full loop
                {
                    shipDirection -= 360; //Bring value back within original circle (without losing direction)
                }
            }
            int part1Answer = Math.Abs(shipXY[0]) + Math.Abs(shipXY[1]); //Add the absolute values of the X and Y directions for manhatten distance
            Console.WriteLine("The Manhatten distance of the ship is {0}", part1Answer); //Write out part 1 answer
            shipDirection = 0; //Reset ship direction for part 2
            shipXY[0] = 0; //Reset ship X coordinate for part 2
            shipXY[1] = 0; //Reset ship Y coordinate for part 2
            int[] waypointXY = { 10, 1 }; //First cell is East-West, second cell is North-South
            for (int i = 0; i < originalInput.Length; i++) //Go through each instruction
            {
                if (directions[i] == "F") //Check if the instruction is Forward
                {
                    shipXY[0] += values[i] * waypointXY[0]; //Move ship X to waypoint X the corresponding number of times
                    shipXY[1] += values[i] * waypointXY[1]; //Move ship Y to waypoint Y the corresponding number of times
                } else if (directions[i] == "N") //Check if the instruction is North
                {
                    waypointXY[1] += values[i]; //Increment waypoint Y positively based on value
                } else if (directions[i] == "S") //Check if the instruction is South
                {
                    waypointXY[1] -= values[i]; //Increment waypoint Y negatively based on value
                } else if (directions[i] == "E") //Check if the instruction is East
                {
                    waypointXY[0] += values[i]; //Increment waypoint X positively based on value
                } else if (directions[i] == "W") //Check if the instruction is West
                {
                    waypointXY[0] -= values[i]; //Increment waypoint X negatively based on value
                } else if (directions[i] == "L") //Check if the instruction is Left
                {
                    shipDirection += (360 - values[i]); //Increment the shipDirection based on the value (translating the left turn into a right turn)
                }
                else if (directions[i] == "R") //Check if the instruction is Right
                {
                    shipDirection += values[i]; //Increment the shipDirection based on the value
                }
                if (shipDirection != 0) //Check if the ship turned (direction will be reset to 0 each loop this time, so if it is not 0 it turned this instruction
                {
                    do //Update waypoint coordinates based on the turn of this instruction
                    {
                        int tempInt = waypointXY[0]; //Store the X coordinate of the waypoint so we can write over it
                        waypointXY[0] = waypointXY[1]; //For a 90 degree turn, the new X coordinate of the waypoint is the original Y coordinate (sign stays)
                        waypointXY[1] = (-1) * tempInt; //For a 90 degree turn, the new Y coordinate of the waypoint is the negative of the original X coordinate (sign changes)
                        shipDirection -= 90; //Take one turn off of the direction
                    } while (shipDirection > 0); //Keep going until the shipDirection is back at 0, meaning we've gone through the instructions as many 90 degree right turns as necessary
                }
            }
            int part2Answer = Math.Abs(shipXY[0]) + Math.Abs(shipXY[1]); //Add the absolute values of the X and Y directions for manhatten distance
            Console.WriteLine("The Manhatten distance of the ship with the waypoint is {0}", part2Answer); //Write out answer for part 2
        }
    }
}
