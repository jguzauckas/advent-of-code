str_inp_list = [x.strip() for x in open("2024/Inputs/15.txt").readlines()]

middle = str_inp_list.index("")
warehouse_map = []
warehouse_map_2 = []
instructions = ""
for i in range(middle):
    row_1 = []
    row_2 = []
    for char in str_inp_list[i]:
        row_1.append(char)
        if char == "O":
            row_2.extend(["[", "]"])
        elif char == "@":
            row_2.extend(["@", "."])
        else:
            row_2.extend([char, char])
    warehouse_map.append(row_1)
    warehouse_map_2.append(row_2)
for i in range(middle + 1, len(str_inp_list)):
    instructions += str_inp_list[i]

y = -1
x = -1
for i in range(len(warehouse_map)):
    if "@" in warehouse_map[i]:
        y = i
        x = warehouse_map[i].index("@")
        break

directions = {"^":(-1, 0), ">":(0, 1), "v":(1, 0), "<":(0, -1)}

for instruction in instructions:
    new_y = y + directions[instruction][0]
    new_x = x + directions[instruction][1]
    if warehouse_map[new_y][new_x] == ".":
        warehouse_map[new_y][new_x] = "@"
        warehouse_map[y][x] = "."
        y = new_y
        x = new_x
        continue
    if warehouse_map[new_y][new_x] == "#":
        continue
    if warehouse_map[new_y][new_x] == "O":
        end_y = new_y
        end_x = new_x
        while warehouse_map[end_y][end_x] == "O":
            end_y += directions[instruction][0]
            end_x += directions[instruction][1]
        if warehouse_map[end_y][end_x] == "#":
            continue
        if warehouse_map[end_y][end_x] == ".":
            warehouse_map[end_y][end_x] = "O"
            warehouse_map[new_y][new_x] = "@"
            warehouse_map[y][x] = "."
            y = new_y
            x = new_x
            continue

sum = 0
for y, str in enumerate(warehouse_map):
    for x, char in enumerate(str):
        if char == "O":
            sum += 100 * y + x
print(f"Part 1: {sum}")

y = -1
x = -1
for i in range(len(warehouse_map_2)):
    if "@" in warehouse_map_2[i]:
        y = i
        x = warehouse_map_2[i].index("@")
        break

def check_move_boxes(instruction, y, x, all_boxes, should_move):
    new_y = y + directions[instruction][0]
    new_x = x + directions[instruction][1]
    if not should_move:
        return (all_boxes, should_move)
    if warehouse_map_2[new_y][new_x] == "[":
        all_boxes.update({(new_y, new_x, "[")})
        all_boxes.update({(new_y, new_x + 1, "]")})
        all_boxes, should_move = check_move_boxes(instruction, new_y, new_x, all_boxes, should_move)
        all_boxes, should_move = check_move_boxes(instruction, new_y, new_x + 1, all_boxes, should_move)
        return (all_boxes, should_move)
    if warehouse_map_2[new_y][new_x] == "]":
        all_boxes.update({(new_y, new_x, "]")})
        all_boxes.update({(new_y, new_x - 1, "[")})
        all_boxes, should_move = check_move_boxes(instruction, new_y, new_x, all_boxes, should_move)
        all_boxes, should_move = check_move_boxes(instruction, new_y, new_x - 1, all_boxes, should_move)
        return (all_boxes, should_move)
    if warehouse_map_2[new_y][new_x] == ".":
        return (all_boxes, should_move)
    return (all_boxes, False)

def move_all_boxes(instruction, all_boxes):
    old_locations = []
    new_locations = []
    for box in all_boxes:
        old_locations.append((box[0], box[1]))
        new_locations.append((box[0] + directions[instruction][0], box[1] + directions[instruction][1]))
    old_locations = set(old_locations)
    new_locations = set(new_locations)
    for box in all_boxes:
        new_y = box[0] + directions[instruction][0]
        new_x = box[1] + directions[instruction][1]
        warehouse_map_2[new_y][new_x] = box[2]
    for box in old_locations.difference(new_locations):
        warehouse_map_2[box[0]][box[1]] = "."


for instruction in instructions:
    new_y = y + directions[instruction][0]
    new_x = x + directions[instruction][1]
    if warehouse_map_2[new_y][new_x] == ".":
        warehouse_map_2[new_y][new_x] = "@"
        warehouse_map_2[y][x] = "."
        y = new_y
        x = new_x
        continue
    if warehouse_map_2[new_y][new_x] == "#":
        continue
    if warehouse_map_2[new_y][new_x] == "[" or warehouse_map_2[new_y][new_x] == "]":
        if instruction == ">" or instruction == "<":
            end_y = new_y
            end_x = new_x
            while warehouse_map_2[end_y][end_x] == "[" or warehouse_map_2[end_y][end_x] == "]":
                end_y += directions[instruction][0]
                end_x += directions[instruction][1]
            if warehouse_map_2[end_y][end_x] == "#":
                continue
            if warehouse_map_2[end_y][end_x] == ".":
                print(end_x, x, -directions[instruction][1])
                for i in range(end_x, x, -directions[instruction][1]):
                    warehouse_map_2[y][i] = warehouse_map_2[y][i - directions[instruction][1]]
                warehouse_map_2[y][x] = "."
                y = new_y
                x = new_x
                continue
        else:
            all_boxes = set()
            should_move = True
            all_boxes, should_move = check_move_boxes(instruction, y, x, all_boxes, should_move)
            if should_move:
                move_all_boxes(instruction, all_boxes)
                warehouse_map_2[y][x] = "."
                y = new_y
                x = new_x
                warehouse_map_2[y][x] = "@"

sum = 0
for y, str in enumerate(warehouse_map_2):
    for x, char in enumerate(str):
        if char == "[":
            sum += 100 * y + x
print(f"Part 2: {sum}")