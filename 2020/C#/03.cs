using System;

namespace Day3
{
    class Day3
    {
        static void Main(string[] args)
        {
            string[] originalMap = System.IO.File.ReadAllLines(@"E:\OneDrive\Documents\Personal\Learning\Advent of Code\2020\Day 3\input.txt"); //Read each line of the input as a string into a cell of this array
            int[,] slopes = new int[,] { { 1, 1 }, { 3, 1 }, { 5, 1 }, { 7, 1 }, { 1, 2 } }; //Storing the set of slopes for part 2 in the form { right, down }, { 3, 1 } is the slope from part 1
            char[,] fullMap = new char[originalMap.Length, originalMap[0].Length]; //Creating char array to store a full map of chars based on the original map
            for (int i = 0; i < originalMap.Length; i++) //Going through each line of the originalMap
            {
                for (int j = 0; j < originalMap[i].Length; j++) //Going through each character of the line
                {
                    fullMap[i, j] = originalMap[i][j]; //Storing the chosen character from the chosen line in the array
                }
            }
            int currentX, currentY; //Variables to keep track of where we are on the map
            long[] treeCounter = new long[slopes.Length / 2]; //Creating long array to store trees hit (using long so it can multiply without going over Int32 limit!)
            long treeTotal = 1; //Creating variable to multiply total trees hit per slope together (set to 1 so multiplication of first value yields just the first value)
            for (int i = 0; i < slopes.Length / 2; i++) //Going through each of the slopes (length is rows * columns, so by dividing by columns we just get the right number of slopes)
            {
                currentX = 0; //Making sure for each slope we start our x-value at 0
                currentY = 0; //Making sure for each slope we start our y-value at 0
                do //see while for information
                {
                    if (fullMap[currentY, currentX % originalMap[0].Length] == '#') //Checking to see if the spot at the current location is a tree
                    {
                        treeCounter[i]++; //Incrementing treeCounter since we hit a tree
                    }
                    currentX += slopes[i, 0]; //Moving based on x slope
                    currentY += slopes[i, 1]; //Moving based on y slope
                } while (currentY < originalMap.Length); //Stopping the loop once we get to the bottom of the map. Did loop with currentY, stops if currentY + the y slope is off the map
                Console.WriteLine("With a slope of right {0} and down {1}, you hit {2} trees", slopes[i, 0], slopes[i, 1], treeCounter[i]); //Writing a line detailing each slope and number of trees hit
                treeTotal *= treeCounter[i]; //Multiplying total number of trees from the slope into our multiplied total (long to prevent Int32 wraparound)
            }
            Console.WriteLine("Multiplying all trees encountered gives {0}", treeTotal); //Writing the final number of trees multiplied together
        }
    }
}
