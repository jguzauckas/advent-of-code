# Read lines from file
list_inp = []
with open('2025/Inputs/01.txt', 'r') as inp:
    list_inp = inp.readlines()

# Split each entry into a pair of direction and value
from collections import namedtuple
instruction = namedtuple("instruction", ("direction", "amount"))
instructions = []
for str in list_inp:
    instructions.append(instruction(str[:1], int(str[1:])))

# Go through each instruction and modify the dial
dial = 50 # Initial dial position
count1, count2 = 0 # count1 for Part 1, count2 for Part 2
for instruction in instructions:
    if instruction.direction == "R":
        dial += instruction.amount # Adjust dial right
        count2 += dial // 100 # Number of times crossed zero
    else:
        count2 -= dial % 100 == 0 # Account for off-by-one error when dial started at 0.
        dial -= instruction.amount # Adjust dial left
        count2 -= dial // 100 - (dial % 100 == 0) # Number of times crossed 0, +1 if land on zero
    dial %= 100
    count1 += dial == 0

# Report out Part 1 and Part 2 answers
print(count1, count2)