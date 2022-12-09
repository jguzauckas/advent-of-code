# Read information out of file into list
with open("2022/Inputs/01.txt", "r") as input_file:
    input_str: list[str] = input_file.readlines()

# Strip newline characters from input
for pos, item in enumerate(input_str):
    input_str[pos] = item.strip()

# Create list of integers instead of strings
input: list[int] = []
for num in input_str:
    if num == "":
        input.append(-1)
    else:
        input.append(int(num))

# Look for maximum sums between each set of numbers
sums: list[int] = []
sum = 0
for num in input:
    if num == -1:
        sums.append(sum)
        sum = 0
        continue
    sum += num
sums.sort(reverse=True)
print(f"The elf carrying the most calories was carrying {sums[0]} calories.")
print(
    f"The top three elves combined are carrying {sums[0] + sums[1] + sums[2]} calories."
)
