# Read in input into 2D list
list_inp = []
with open('2025/Inputs/09.txt', 'r') as inp:
    list_inp = inp.readlines()
list_inp = [[int(y) for y in x.strip().split(",")] for x in list_inp]
grid = [["."] * 100000] * 100000
def flood_fill(x, y, marker):
    if (x >= 100000 or y >= 100000):
        return
    if (x < 0 or y < 0):
        return
    print(grid[x][y])
    if (grid[x][y] != "."):
        return
    grid[x][y] = marker
    flood_fill(x - 1, y - 1, marker)
    flood_fill(x - 1, y, marker)
    flood_fill(x - 1, y + 1, marker)
    flood_fill(x, y - 1, marker)
    flood_fill(x, y + 1, marker)
    flood_fill(x + 1, y - 1, marker)
    flood_fill(x + 1, y, marker)
    flood_fill(x + 1, y + 1, marker)

max_area = 0
for coordinate_1 in list_inp:
    for coordinate_2 in list_inp:
        area = (abs(coordinate_2[0] - coordinate_1[0]) + 1) * (abs(coordinate_2[1] - coordinate_1[1]) + 1)
        if area > max_area:
            max_area = area
print(max_area)

print("Test")
for coordinate_1, coordinate_2 in zip(list_inp, list_inp[1:] + list_inp[:1]):
    grid[coordinate_1[0]][coordinate_1[1]] = "R"
    grid[coordinate_2[0]][coordinate_2[1]] = "R"
    if coordinate_1[0] == coordinate_2[0]:
        if coordinate_1[1] > coordinate_2[1]:
            for y in range(coordinate_2[1] + 1, coordinate_1[1]):
                grid[coordinate_1[0]][y] = "G"
        else:
            for y in range(coordinate_1[1] + 1, coordinate_2[1]):
                grid[coordinate_1[0]][y] = "G"
    else:
        if coordinate_1[0] > coordinate_2[0]:
            for x in range(coordinate_2[0] + 1, coordinate_1[0]):
                grid[x][coordinate_1[1]] = "G"
        else:
            for x in range(coordinate_1[0] + 1, coordinate_2[0]):
                grid[x][coordinate_1[1]] = "G"
flood_fill(list_inp[0][0] + 1, list_inp[0][1] + 1, "1")
flood_fill(list_inp[0][0] + 1, list_inp[0][1] - 1, "2")
flood_fill(list_inp[0][0] - 1, list_inp[0][1] - 1, "3")
flood_fill(list_inp[0][0] - 1, list_inp[0][1] + 1, "4")
found = set()
for i in range(4):
    if i == 0:
        for j in range(100000):
            found.add(grid[0][j])
    elif i == 1:
        for j in range(100000):
            found.add(grid[-1][j])
    elif i == 2:
        for j in range(100000):
            found.add(grid[j][0])
    else:
        for j in range(100000):
            found.add(grid[j][-1])
print(found)
# max_area = 0
# for coordinate_1 in list_inp:
#     for coordinate_2 in list_inp:
#         area = (abs(coordinate_2[0] - coordinate_1[0]) + 1) * (abs(coordinate_2[1] - coordinate_1[1]) + 1)
#         if area > max_area:
#             if grid[coordinate_2[0]][coordinate_1[1]] != "." and grid[coordinate_1[0]][coordinate_2[1]] != ".":
#                 max_area = area
# print(max_area)