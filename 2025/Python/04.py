# Read in input into 2D list
list_inp = []
with open('2025/Inputs/04.txt', 'r') as inp:
    list_inp = inp.readlines()
roll_map = [[c for c in s] for s in list_inp]

# Go through map and count up adjacents
total = 0
current_count = 1
while current_count > 0:
    adjacent_counts = [[0 for c in s] for s in roll_map]
    for i in range(len(roll_map)):
        for j in range(len(roll_map[i])):
            if roll_map[i][j] == "@":
                if i > 0:
                    adjacent_counts[i - 1][j] += 1
                    if j > 0:
                        adjacent_counts[i - 1][j - 1] += 1
                    if j < len(roll_map[i]) - 1:
                        adjacent_counts[i - 1][j + 1] += 1     
                if i < len(roll_map) - 1:
                    adjacent_counts[i + 1][j] += 1
                    if j > 0:
                        adjacent_counts[i + 1][j - 1] += 1
                    if j < len(roll_map[i]) - 1:
                        adjacent_counts[i + 1][j + 1] += 1
                if j > 0:
                    adjacent_counts[i][j - 1] += 1
                if j < len(roll_map[i]) - 1:
                    adjacent_counts[i][j + 1] += 1
    current_count = 0
    for i in range(len(adjacent_counts)):
        for j in range(len(adjacent_counts[i])):
            if roll_map[i][j] == "@" and adjacent_counts[i][j] < 4:
                current_count += 1
                roll_map[i][j] = "X"
    total += current_count
                
print(total)