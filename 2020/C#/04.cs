using System;

namespace Day_4
{
    class Day4
    {
        static void Main(string[] args)
        {
            string[] originalInput = System.IO.File.ReadAllLines(@"E:\OneDrive\Documents\Personal\Learning\Advent of Code\2020\Day 4\input.txt"); //Reading each line of the input to the originalInput array
            string[] fields = new string[8] { "byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid", "cid" }; //Entering all eight potential fields into array to read later
            string[] eyeColor = new string[7] { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" }; //Entering all seven eye colors into array to read later
            int lengthCounter = 1; //Constant to count how many entries are in the input (start at 1 to account for counting blank lines, which would exclude the last entry)
            for (int i = 0; i < originalInput.Length; i++) //Go through each cell of originalInput
            {
                if (originalInput[i] == "") //Check to see if chosen cell is a blank line
                {
                    lengthCounter++; //If chosen cell is blank line, increment counter (+1 entry in count)
                }
            }
            string[] cleanedInput = new string[lengthCounter]; //Use lengthCounter to set the right size for cleanedInput (one entry of input per cell)
            int counter = 0; //Counter for do loop to make sure the final entry of originalInput is read without going out of array bounds
            for (int i = 0; i < cleanedInput.Length; i++) //Go through each cell of cleanedInput (one loop for each entry)
            {
                do //See while for information
                {
                    if (cleanedInput[i] == "") //Check to see if this is the first iteration of the do loop for this cell (cell would be blank)
                    {
                        cleanedInput[i] = originalInput[counter]; //Set blank cell to be the first line of the entry
                    }
                    else //If this is not the first iteration of the do loop for this cell (cell is not blank)
                    {
                        cleanedInput[i] = cleanedInput[i] + " " + originalInput[counter]; //Keep original cell contents and add the next line of the entry with a space in between
                    }
                    counter++; //Increment counter to note moving to the next line
                } while (counter < originalInput.Length && originalInput[counter] != ""); //Break the loop when we hit a blank line or exceed the bounds of originalInput
                counter++; //Skip the blank line the do loop ran into before restarting for loop
            }
            string[,] splitInput = new string[cleanedInput.Length, 8]; //Create a multidimensional array to hold each potential field/value set (each entry can have up to eight fields)
            int[] countFields = new int[cleanedInput.Length]; //Array to keep track of how many fields each entry has
            for (int i = 0; i < cleanedInput.Length; i++) //Go through each cell of cleanedInput (one loop for each entry)
            {
                string[] tempArray = cleanedInput[i].Split(' '); //Split the entry into each of its field using the common space delimiter between each and store in temporary array
                for (int j = 1; j < tempArray.Length; j++) //Go through each cell of the temporary array (ignoring first cell (0) since its blank due to leading space)
                {
                    splitInput[i, j - 1] = tempArray[j]; //Store each field in a column of the splitInput array (shifting column index back one to eliminate the leading space field)
                }
                countFields[i] = tempArray.Length; //Store the number of fields the given entry has based on how many are stored in the temporary array
            }
            string[][,] finalSplit = new string[cleanedInput.Length][,]; //Create jagged array to store a multidimensional array containing the split fields and values for each entry
            for (int i = 0; i < cleanedInput.Length; i++) //Go through each cell of cleanedInput (and therefore each row of splitInput)
            {
                finalSplit[i] = new string[countFields[i], 2]; //Initialize the multidimensional array for this entry with the right number of fields and two columns (for splitting each field/value)
                for (int j = 0; j < countFields[i] - 1; j++) //Go through each field in the entry
                {
                    string[] tempArray = splitInput[i, j].Split(':'); //Split the field/value based on the : delimiter
                    finalSplit[i][j, 0] = tempArray[0]; //Store the field in the first column of this entry's finalSplit array
                    finalSplit[i][j, 1] = tempArray[1]; //Store the value in the second column of this entry's finalSplit array
                }
            }
            int[][,] fieldsCheck = new int[cleanedInput.Length][,]; //Create jagged array to keep track of whether each entry contains each given field and if the value is valid
            int requiredCount = 0; //Create variable to keep track of how many passports had the required fields
            int passCount = 0; //Create variable to keep track of how many passports had the required fields with valid entries for all
            for (int i = 0; i < cleanedInput.Length; i++) //Go through each cell of cleanedInput (and therefore each row of splitInput)
            {
                fieldsCheck[i] = new int[fields.Length + 1, 2]; //Initialize the multidimensional array for this entry with the number of fields and an extra row for a count of how many fields were passed
                                                                //and two columns (one for having the field, one for having a valid entry for the field)
                for (int j = 0; j < finalSplit[i].Length / 2; j++) //Go through each field present in the entry (length divided by two since the length accounts for two columns)
                {
                    for (int k = 0; k < fields.Length; k++) //Go through each possible field
                    {
                        if (finalSplit[i][j, 0] == fields[k]) //Compare the field present in the entry to the possible field
                        {
                            fieldsCheck[i][k, 0] = 1; //If it matches, check the field off in the fieldsCheck array as being present
                            if (k != 7) //Make sure the optional field ("cid") is not counted
                            {
                                fieldsCheck[i][8, 0]++; //Count the field as being present towards the total (seven needed for all required to be present)
                            } else //If the optional field ("cid") was the one checked off
                            {
                                fieldsCheck[i][7, 1] = 1; //Value for the optional field ("cid") does not matter, so we check it off as valid if it was present
                            }
                        }
                    } //Not going through each possible field anymore, still going through each field present in the entry
                    int tempInt;
                    int.TryParse(finalSplit[i][j, 1], out tempInt); //Temporary integer to try and parse the field value as an integer (will be 0 if not)
                    if ((finalSplit[i][j, 0] == fields[0]) && (1920 <= tempInt) && (tempInt <= 2002)) //Check against first entry ("byr") and following rules of at least 1920 and at most 2002
                    {
                        fieldsCheck[i][0, 1] = 1; //If it is a valid value for the "byr" field, then check it off as valid in the second column of this entries fieldsCheck array
                    }
                    else if ((finalSplit[i][j, 0] == fields[1]) && (2010 <= tempInt) && (tempInt <= 2020)) //Check against second entry ("iyr") and following rules of at least 2010 and at most 2020
                    {
                        fieldsCheck[i][1, 1] = 1; //If it is a valid value for the "iyr" field, then check it off as valid in the second column of this entries fieldsCheck array
                    }
                    else if ((finalSplit[i][j, 0] == fields[2]) && (2020 <= tempInt) && (tempInt <= 2030)) //Check against third entry ("eyr") and following rules of at least 2020 and at most 2030
                    {
                        fieldsCheck[i][2, 1] = 1; //If it is a valid value for the "eyr" field, then check it off as valid in the second column of this entries fieldsCheck array
                    }
                    else if (finalSplit[i][j, 0] == fields[3]) //Check against fourth entry ("hgt") of fields
                    {
                        string tempString = finalSplit[i][j, 1].Substring(finalSplit[i][j, 1].Length - 2); //Take only the last two characters of the value (should be "in" or "cm" for valid values)
                        int.TryParse(finalSplit[i][j, 1].Remove(finalSplit[i][j, 1].Length - 2), out tempInt); //Try to parse everything but the last two characters of the value as an integer (the height)
                        if ((tempString == "cm") && (150 <= tempInt) && (tempInt <= 193)) //Check that it ends in "cm" and follows the rules of at least 150 and at most 193
                        {
                            fieldsCheck[i][3, 1] = 1; //If it is a valid value for the "hgt" field, then check it off as valid in the second column of this entries fieldsCheck array (only "cm" values)
                        }
                        else if ((tempString == "in") && (59 <= tempInt) && (tempInt <= 76)) //Check that it ends in "in" and follows the rules of at least 59 and at most 76
                        {
                            fieldsCheck[i][3, 1] = 1; //If it is a valid value for the "hgt" field, then check it off as valid in the second column of this entries fieldsCheck array (only "in" values)
                        }
                    }
                    else if (finalSplit[i][j, 0] == fields[4]) //Check against fifth entry ("hcl") of fields
                    {
                        char[] tempArray = finalSplit[i][j, 1].ToCharArray(); //Turn value into temporary array of characters to be able to check each position
                        int tempCounter = 0; //Create a counter to keep track of how many of the characters following '#' are valid
                        if (tempArray[0] == '#') //Check to make sure it starts with '#' (would be invalid if not)
                        {
                            for (int l = 1; l < tempArray.Length; l++) //Go through each character of the value (stored in tempArray)
                            {
                                if (('0' <= tempArray[l] && tempArray[l] <= '9') || ('a' <= tempArray[l] && tempArray[l] <= 'f')) //Check that the character unicode falls into either of two ranges (0-9 or a-f)
                                {
                                    tempCounter++; //If the chracter unicode fell within either of the ranges, increment the counter since that character of the value was valid
                                }
                            }
                        }
                        if (tempCounter == 6 && tempArray.Length == 7) //Check that there were the right number of characters in the value and they were all valid
                        {
                            fieldsCheck[i][4, 1] = 1; //If it is a valid value for the "hcl" field, then check it off as valid in the second column of this entries fieldsCheck array
                        }
                    }
                    else if (finalSplit[i][j, 0] == fields[5]) //Check against sixth entry ("ecl") of fields
                    {
                        foreach (string color in eyeColor) //Go through each potential eye color
                        {
                            if (finalSplit[i][j, 1] == color) //Check to see if the value is that eye color
                            {
                                fieldsCheck[i][5, 1] = 1; //If it is a valid value for the "ecl" field, then check it off as valid in the second column of this entries fieldsCheck array
                            }
                        }
                    }
                    else if (finalSplit[i][j, 0] == fields[6]) //Check against seventh entry ("pid") of fields
                    {
                        char[] tempArray = finalSplit[i][j, 1].ToCharArray(); //Turn value into temporary array of characters to be able to check each positio
                        int tempCounter = 0; //Create a counter to keep track of how many of the characters are valid
                        for (int l = 0; l < tempArray.Length; l++) //Go through each character of the value (stored in tempArray)
                        {
                            if (('0' <= tempArray[l]) && (tempArray[l] <= '9')) //Check that the character unicode falls in the range 0-9
                            {
                                tempCounter++; //If the chracter unicode fell within the range, increment the counter since that character of the value was valid
                            }
                        }
                        if (tempCounter == 9 && tempArray.Length == 9) //Check that there were the right number of characters in the value and they were all valid
                        {
                            fieldsCheck[i][6, 1] = 1; //If it is a valid value for the "pid" field, then check it off as valid in the second column of this entries fieldsCheck array
                        }
                    }
                } //Done going through each field of the entry
                if (fieldsCheck[i][8, 0] == 7) //Check that the entry had all seven required fields
                {
                    requiredCount++; //If the entry had all required fields, count it towards the total requiredCount
                }
                for (int j = 0; j < fields.Length - 1; j++) //Go through each of the required seven entries (length minus one to cut off optional entry "cid")
                {
                    if (fieldsCheck[i][j, 1] == 1) //Check that the value was valid (would have been set to 1)
                    {
                        fieldsCheck[i][8, 1]++; //If the value was valid, count it towards the fields with valid values for the entry
                    }
                }
                if (fieldsCheck[i][8, 0] == 7 && fieldsCheck[i][8, 1] == 7) //Check that the entry had all required fields and they all had valid values
                {
                    passCount++; //If the entry had all required fields with valid values, count it towards the total passCount
                }
            } //Done going through each entry
            Console.WriteLine("{0} passports have all required fields", requiredCount); //Write how many passports have all required fields (values not required to be valid)
            Console.WriteLine("{0} passports have all required fields with valid entries", passCount); //Write how many passports have all required fields with valid values
        }
    }
}
