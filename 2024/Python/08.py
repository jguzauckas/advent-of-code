list_inp = [x.strip() for x in open("2024/Inputs/08.txt", "r").readlines()]

def generate_antinodes():
    antinodes = []
    for row in list_inp:
        new_row = []
        for col in row:
            new_row.append(0)
        antinodes.append(new_row)
    return antinodes

def antenna_search(y, x, antenna):
    matching_antennas = []
    for y2, row in enumerate(list_inp):
        start = 0
        while row.count(antenna, start) > 0:
            x2 = row.index(antenna, start)
            matching_antennas.append((y2, x2))
            start = x2 + 1
    matching_antennas.remove((y, x))
    return matching_antennas

antinodes1 = generate_antinodes()
antinodes2 = generate_antinodes()
def find_antinodes_1(y, x, matches):
    for match in matches:
        y2 = match[0]
        x2 = match[1]
        delta_y = y2 - y
        delta_x = x2 - x
        antinode_y = y - delta_y
        antinode_x = x - delta_x
        if (0 <= antinode_y < len(antinodes1)) and (0 <= antinode_x < len(antinodes1[antinode_y])):
            antinodes1[antinode_y][antinode_x] = 1

def find_antinodes_2(y, x, matches):
    for match in matches:
        y2 = match[0]
        x2 = match[1]
        delta_y = y2 - y
        delta_x = x2 - x
        antinode_y = y - delta_y
        antinode_x = x - delta_x
        while (0 <= antinode_y < len(antinodes2)) and (0 <= antinode_x < len(antinodes2[antinode_y])):
            antinodes2[antinode_y][antinode_x] = 1
            antinode_y -= delta_y
            antinode_x -= delta_x

for y, row in enumerate(list_inp):
    for x, col in enumerate(row):
        if col == ".":
            continue
        antinodes2[y][x] = 1
        matches = antenna_search(y, x, col)
        find_antinodes_1(y, x, matches)
        find_antinodes_2(y, x, matches)

count1 = 0
for row in antinodes1:
    for col in row:
        count1 += col
print(count1)

count2 = 0
for row in antinodes2:
    for col in row:
        count2 += col
print(count2)