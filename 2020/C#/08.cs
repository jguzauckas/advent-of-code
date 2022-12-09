using System;

namespace Day_8
{
    class Day8
    {
        static void Main(string[] args)
        {
            string[] originalInput = System.IO.File.ReadAllLines(@"E:\OneDrive\Documents\Personal\Learning\Advent of Code\2020\Day 8\input.txt"); //Reading each line of the input to the originalInput array
            string[] instruction = new string[originalInput.Length]; //Create a string array to store the type of instruction for each entry
            int[,] intInstruction = new int[originalInput.Length, 2]; //Create an integer array store the associated integer with each entry
            for (int i = 0; i < originalInput.Length; i++) //Go through each entry of the input
            {
                string[] tempArray = originalInput[i].Split(' '); //Split the entry into the type of instruction and the associated integer using the space delimiter
                instruction[i] = tempArray[0]; //Set the type of instruction for the entry
                intInstruction[i, 0] = int.Parse(tempArray[1]); //Set the associated integer of the entry
            }
            int accumulator1 = 0; //Create an accumulator counting variable for part 1
            int counter = 0; //Create a counter for the do loop
            do
            {
                if (instruction[counter] == "acc") //Check if the instruction is accumulator
                {
                    accumulator1 += intInstruction[counter, 0]; //Add the associated integer to the accumulator counter for part 1
                    intInstruction[counter, 1] = 1; //Mark this instruction as being done already
                    counter++; //Increment the counter to move onto the next instruction in the next loop
                }
                else if (instruction[counter] == "jmp") //Check if the instruction is jumping
                {
                    intInstruction[counter, 1] = 1; //Mark this instruction as being done already
                    counter += intInstruction[counter, 0]; //Move the counter based on the associated integer
                }
                else if (instruction[counter] == "nop") //Check if the instruction is no operation
                {
                    intInstruction[counter, 1] = 1; //Mark this instruction as being done already
                    counter++; //Increment the counter to move onto the next instruction in the next loop
                }
            } while (intInstruction[counter, 1] == 0); //Exit loop once the next instruction is a repeated instruction (already marked as done)
            int accumulator2 = 0; //Create an accumulator counting variable for part 2
            for (int i = 0; i < originalInput.Length; i++) //Go through each entry of the instructions
            {
                if (instruction[i] == "jmp") //Check if the current instruction is jump, which we will want to change to no operation
                {
                    instruction[i] = "nop"; //Change the instruction to no operation
                } else if (instruction[i] == "nop") //Check if the current instruction is no operation, which we will want to change to jump
                {
                    instruction[i] = "jmp"; //Change the instruction to jump
                }
                for (int j = 0; j < originalInput.Length; j++) //Go through each instruction
                {
                    intInstruction[j, 1] = 0; //Mark each instruction is not done
                }
                counter = 0; //Reset counter to 0 to go through instructions from beginning again
                accumulator2 = 0; //Reset accumulator for part 2 to 0 for new run of instructions
                do
                {
                    if (instruction[counter] == "acc") //Check if the instruction is accumulator
                    {
                        accumulator2 += intInstruction[counter, 0]; //Add the associated integer to the accumulator counter for part 1
                        intInstruction[counter, 1] = 1; //Mark this instruction as being done already
                        counter++; //Increment the counter to move onto the next instruction in the next loop
                    }
                    else if (instruction[counter] == "jmp") //Check if the instruction is jumping
                    {
                        intInstruction[counter, 1] = 1; //Mark this instruction as being done already
                        counter += intInstruction[counter, 0]; //Move the counter based on the associated integer
                    }
                    else if (instruction[counter] == "nop") //Check if the instruction is no operation
                    {
                        intInstruction[counter, 1] = 1; //Mark this instruction as being done already
                        counter++; //Increment the counter to move onto the next instruction in the next loop
                    }
                } while ((counter >= 0 && counter <= originalInput.Length - 1) && intInstruction[counter, 1] == 0); //Continue going through the instructions as long as the counter is within the index,
                                                                                                                    //and the current instruction has not already been done (meaning it has not looped)
                if (counter == originalInput.Length) //Check to see if the modified set of instructions finished the program (made it to the step after the last instruction)
                {
                    goto done; //Exit the loop, since we have our working set of instructions now
                }
                //Reset the modified instructions so in the next step of the loop we can modify the next step with the otherwise original instruction
                if (instruction[i] == "jmp") //Check if the modified instruction was changed to jump
                {
                    instruction[i] = "nop"; //Reset the modified instruction back to no operation
                }
                else if (instruction[i] == "nop") //Check if the modified instruction was changed to no operation
                {
                    instruction[i] = "jmp"; //Reset the modified instruction back to jump
                }
            }
            done: //Location where we exit out of the loop with the right answer
            Console.WriteLine("The accumulator value is {0} by the time it gets to the first looped instruction", accumulator1); //Write out part 1 answer
            Console.WriteLine("With the right change, the program terminates with an accumulator value of {0}", accumulator2); //Write out part 2 answer
        }
    }
}
