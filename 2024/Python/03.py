import re, math

# Read String from file
str_inp = open('2024/Inputs/03.txt', 'r').read()

# Regex flags
number_search_term = re.compile("\\d+") # Find any single digit or larger number
# Find any mul, do, or don't operation, with grouping for numbers from mul
all_function_search = re.compile(r"(mul\((\d+),(\d+)\)|do\(\)|don't\(\))")

part_1, part_2, enable = 0, 0, True # Set up sums for solutions and flag for part 2

# Go through every instruction found, with numbers as strings for mul() operations
for instruction, num_a, num_b in all_function_search.findall(str_inp):
    enable = True if instruction == "do()" else False if instruction == "don't()" else enable
    # Map each of the two numbers from strings to ints, then multiply them
    result = math.prod(list(map(int, [num_a, num_b]))) if instruction[:3] == "mul" else 0
    part_1 += result
    part_2 += result if enable else 0
print(f"Part 1: {part_1}\nPart 2: {part_2}")