# Read in input into 2D list
list_inp = []
with open('2025/Inputs/09.txt', 'r') as inp:
    list_inp = inp.readlines()
list_inp = [[int(y) for y in x.strip().split(",")] for x in list_inp]

max_area = 0
max_coordinates = []
for coordinate_1 in list_inp:
    for coordinate_2 in list_inp:
        area = (abs(coordinate_2[0] - coordinate_1[0]) + 1) * (abs(coordinate_2[1] - coordinate_1[1]) + 1)
        if area > max_area:
            max_area = area
            max_coordinates = [coordinate_1, coordinate_2]
print(max_area, max_coordinates)


horizontals = {} # Dictionary storing a list of tuples of the form y:[(x1, x2), (x1, x2)]
verticals = {} # Dictionary storing a list of tuples of the form x:[(y1, y2), (y1, y2)]

for coordinate_1, coordinate_2 in zip(list_inp, list_inp[1:] + list_inp[:1]):
    if coordinate_1[0] == coordinate_2[0]:
        if coordinate_1[0] in verticals:
            verticals[coordinate_1[0]].append((min(coordinate_1[1], coordinate_2[1]), max(coordinate_1[1], coordinate_2[1])))
        else:
            verticals[coordinate_1[0]] = [(min(coordinate_1[1], coordinate_2[1]), max(coordinate_1[1], coordinate_2[1]))]
    else:
        if coordinate_1[1] in horizontals:
            horizontals[coordinate_1[1]].append((min(coordinate_1[0], coordinate_2[0]), max(coordinate_1[0], coordinate_2[0])))
        else:
            horizontals[coordinate_1[1]] = [(min(coordinate_1[0], coordinate_2[0]), max(coordinate_1[0], coordinate_2[0]))]
for row in horizontals:
    horizontals[row].sort()
for col in verticals:
    verticals[col].sort()

rows = {} # Dictionary storing a list of tuples of the form y:[(x1, x2), (x1, x2)]
# These tuples represent valid horizontal areas of the polygon per y value
for i in range(min(horizontals.keys()), max(horizontals.keys()) + 1):
    all_values = []
    if i in horizontals:
        all_values = horizontals[i]
    for col, verts in verticals.items():
        for vert in verts:
            if vert[0] <= i <= vert[1]:
                all_values.append((col, col))
                break
    final_values = [all_values[0]]
    for ranges in all_values[1:]:
        within_range = -1
        left_overlap_range = -1
        right_overlap_range = -1
        for index, final_range in zip(range(len(final_values)), final_values):
            if final_range[0] <= ranges[0] and ranges[1] <= final_range[1]:
                within_range = index
                break
            elif final_range[1] >= ranges[0] >= final_range[0]:
                left_overlap_range = index
            elif final_range[0] <= ranges[1] <= final_range[1]:
                right_overlap_range = index
        if within_range >= 0:
            continue
        elif left_overlap_range >= 0 and right_overlap_range >= 0:
            min_index, max_index = sorted([left_overlap_range, right_overlap_range])
            final_values[min_index] = (final_values[left_overlap_range][0], final_values[right_overlap_range][1])
            final_values.pop(max_index)
        elif left_overlap_range >= 0:
            final_values[left_overlap_range] = (final_values[left_overlap_range][0], ranges[1])
        elif right_overlap_range >= 0:
            final_values[right_overlap_range] = (ranges[0], final_values[right_overlap_range][1])
        else:
            final_values.append(ranges)
    final_values.sort()
    rows[i] = final_values
for index, row in rows.items():
    if len(row) % 2 == 0:
        new_row = []
        for i in range(0, len(row), 2):
            new_row.append((row[i][0], row[i + 1][1]))
        new_row.sort()
        rows[index] = new_row

max_area = 0
max_coordinates = []
for coordinate_1 in list_inp:
    for coordinate_2 in list_inp:
        area = (abs(coordinate_2[0] - coordinate_1[0]) + 1) * (abs(coordinate_2[1] - coordinate_1[1]) + 1)
        if area > max_area:
            min_x, max_x = sorted([coordinate_1[0], coordinate_2[0]])
            min_y, max_y = sorted([coordinate_1[1], coordinate_2[1]])
            flag = True
            
            for y in range(min_y, max_y + 1):
                row = rows[y]
                row_flag = False
                for ranges in row:
                    if ranges[0] <= min_x and max_x <= ranges[1]:
                        row_flag = True
                        break
                if not row_flag:
                    flag = False
                    break
            print(max_area, area, coordinate_1, coordinate_2, flag)
            if flag:
                max_area = area
                max_coordinates = [coordinate_1, coordinate_2]
print(max_area, max_coordinates)