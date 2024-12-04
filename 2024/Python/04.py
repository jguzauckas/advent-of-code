list_inp = open('2024/Inputs/04.txt', 'r').readlines()
list_inp = [i.strip() for i in list_inp]

    # (y, x)     N        NE      E       SE      S       SW        W        NW
directions = [(-1, 0), (-1, 1), (0, 1), (1, 1), (1, 0), (1, -1), (0, -1), (-1, -1)]
y_min, y_max = 0, len(list_inp) - 1
x_min, x_max = 0, len(list_inp[0]) - 1
str_check = "XMAS"
distance = len(str_check) - 1

def boundary_check(axis, index, direction):
    final_index = index + distance * direction
    if axis == "x":
        return (x_min <= final_index <= x_max)
    else:
        return (y_min <= final_index <= y_max)

def xmas_check(y_index, x_index):
    count = 0
    for direction in directions:
        if boundary_check("y", y_index, direction[0]) and boundary_check("x", x_index, direction[1]):
            str_new = "X" + "".join([list_inp[y_index + i * direction[0]][x_index + i * direction[1]] for i in range(1, distance + 1)])
            count += str_new == str_check
    return count

    # (y, x)       NW        NE      SE      SW
directions_2 = [(-1, -1), (-1, 1), (1, 1), (1, -1)]
def x_mas_check(y_index, x_index):
    if y_index == y_min or y_index == y_max or x_index == x_min or x_index == x_max:
        return False
    new_chars = [list_inp[y_index + y][x_index + x] for y, x in directions_2]
    if new_chars.count("M") != 2 or new_chars.count("S") != 2:
        return False
    for char_1, char_2 in zip(new_chars, new_chars[2:]):
        if char_1 == char_2:
            return False
    return True
                    
total1 = 0
total2 = 0
for y_index, str in enumerate(list_inp):
    for x_index, char in enumerate(str):
        if char == "X":
            total1 += xmas_check(y_index, x_index)
        if char == "A":
            total2 += x_mas_check(y_index, x_index)
print(total1)
print(total2)
