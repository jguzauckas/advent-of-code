# Import the input into a list of strings to process
with open("2022/Inputs/03.txt", "r") as input_file:
    input_str: list[str] = input_file.readlines()

# Strip the newline characters off of each input line
for pos, item in enumerate(input_str):
    input_str[pos] = item.strip()

# Split each entry into a tuple of the first and second halves
input: list[tuple] = []
for item in input_str:
    str1 = item[: len(item) // 2]
    str2 = item[len(item) // 2 :]
    input.append((str1, str2))

# Function to get the priority of a given character
def char_val(inp: str) -> int:
    if ord(inp) > 95:
        return ord(inp) - 96
    return ord(inp) - 38


# Function to find the shared character in two strings in a tuple
def find_similar_p1(inp: tuple[str, ...]) -> str:
    for chr in inp[0]:
        if chr in inp[1]:
            return chr
    return ""


# Function to find the shared character in three tuples of two strings
def find_similar_p2(inp: tuple[str, ...]) -> str:
    for chr in inp[0]:
        if chr in inp[1] and chr in inp[2]:
            return chr
    return ""


# Go through split input and calculate part 1
sum = 0
for tup in input:
    sum += char_val(find_similar_p1(tup))
print(f"The total of the priorities in each rucksack is {sum}.")

# Go through split input and calculate part 2
sum = 0
for i in range(0, len(input_str), 3):
    sum += char_val(find_similar_p2((input_str[i], input_str[i + 1], input_str[i + 2])))
print(f"The total of the priorities in each group of 3 elves is {sum}.")
