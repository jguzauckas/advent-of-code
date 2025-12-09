# Read in input into 2D list
list_inp = []
with open('2025/Inputs/09.txt', 'r') as inp:
    list_inp = inp.readlines()
list_inp = [[int(y) for y in x.strip().split(",")] for x in list_inp]

max_area = 0
for coordinate_1 in list_inp:
    for coordinate_2 in list_inp:
        area = (abs(coordinate_2[0] - coordinate_1[0]) + 1) * (abs(coordinate_2[1] - coordinate_1[1]) + 1)
        if area > max_area:
            max_area = area
print(max_area)

grid = [[False for _ in range(100000)] for _ in range(100000)]
for coordinate_1, coordinate_2 in zip(list_inp, list_inp[1:] + list_inp[:1]):
    grid[coordinate_1[1]][coordinate_1[0]] = True
    grid[coordinate_2[1]][coordinate_2[0]] = True
    if coordinate_1[0] == coordinate_2[0]: # x is same, so vertical movement
        if coordinate_1[1] > coordinate_2[1]: # vertical movement is downward
            for y in range(coordinate_2[1] + 1, coordinate_1[1]):
                grid[y][coordinate_1[0]] = True
        else: # vertical movement is upward
            for y in range(coordinate_1[1] + 1, coordinate_2[1]):
                grid[y][coordinate_1[0]] = True
    else:
        if coordinate_1[0] > coordinate_2[0]:
            for x in range(coordinate_2[0] + 1, coordinate_1[0]):
                grid[coordinate_1[1]][x] = True
        else:
            for x in range(coordinate_1[0] + 1, coordinate_2[0]):
                grid[coordinate_1[1]][x] = True
to_check = [(50000, 30000), (50000, 70000)]
while len(to_check) > 0:
    x, y = to_check.pop()
    if 0 <= x < 15 and 0 <= y < 15 and grid[y][x] == False:
        grid[y][x] = True
        # Add neighbors to list
        to_check.append((x + 1, y))  # Down
        to_check.append((x - 1, y))  # Up
        to_check.append((x, y + 1))  # Right
        to_check.append((x, y - 1))  # Left

max_area = 0
for coordinate_1 in list_inp:
    for coordinate_2 in list_inp:
        x1, y1 = coordinate_1
        x2, y2 = coordinate_2
        area = (abs(x2 - x1) + 1) * (abs(y2 - y1) + 1)
        if area > max_area:
            flag = False
            x_min, x_max = min(x1, x2), max(x1, x2)
            y_min, y_max = min(y1, y2), max(y1, y2)
            for x in range(x_min, x_max + 1):
                if grid[y1][x] == False or grid[y2][x] == False:
                    flag = True
                    break
            if flag:
                break
            for y in range(y_min, y_max + 1):
                if grid[y][x1] == False or grid[y][x2] == False:
                    flag = True
                    break
            if flag:
                break
            max_area = area
print(max_area)