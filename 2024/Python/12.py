from collections import defaultdict

list_str = [x.strip() for x in open("2024/Inputs/12.txt", "r").readlines()]

garden_map = []
garden_map_utilization = []
for x in list_str:
    row = []
    row_u = []
    for col in x:
        row.append(col)
        row_u.append(False)
    garden_map.append(row)
    garden_map_utilization.append(row_u)

def find_adjacent_plots(y, x, char):
    finds = []
    # North
    if 0 <= y - 1 and garden_map[y - 1][x] == char:
        finds.append((y - 1, x))
        garden_map_utilization[y - 1][x] = True
    # East        
    if x + 1 < len(garden_map[y]) and garden_map[y][x + 1] == char:
        finds.append((y, x + 1))
        garden_map_utilization[y][x + 1] = True
    # South
    if y + 1 < len(garden_map) and garden_map[y + 1][x] == char:
        finds.append((y + 1, x))
        garden_map_utilization[y + 1][x] = True
    # West
    if 0 <= x - 1 and garden_map[y][x - 1] == char:
        finds.append((y, x - 1))
        garden_map_utilization[y][x - 1] = True
    return set(finds)

def plot_perimeter(plots, plot):
    perimeter = 4
    y = plot[0]
    x = plot[1]
    perimeter -= (y - 1, x) in plots
    perimeter -= (y, x + 1) in plots
    perimeter -= (y + 1, x) in plots
    perimeter -= (y, x - 1) in plots
    return perimeter

def region_perimeter(plots):
    perimeter = 0
    for plot in plots:
        perimeter += plot_perimeter(plots, plot)
    return perimeter

def mins_maxs(plots):
    min_y = plots[0][0]
    max_y = plots[0][0]
    min_x = plots[0][1]
    max_x = plots[0][1]
    for plot in plots:
        if plot[0] < min_y:
            min_y = plot[0]
        if plot[0] > max_y:
            max_y = plot[0]
        if plot[1] < min_x:
            min_x = plot[1]
        if plot[1] > max_x:
            max_x = plot[1]
    return (min_y, max_y, min_x, max_x)

def sides_grid(plots):
    min_y, max_y, min_x, max_x = mins_maxs(plots)
    new_grid = []
    for i in range(min_y, max_y + 2):
        for j in range(min_x, max_x + 2):
            new_grid.append((i, j))
    return new_grid

def region_sides(plots):
    if len(plots) == 1:
        return 4
    grid = sides_grid(plots)
    min_y, max_y, min_x, max_x = mins_maxs(plots)
    sides = 0
    for i in range(min_y, max_y + 2):
        for j in range(min_x, max_x + 2):
            sum = ((i, j) in plots) + ((i - 1, j) in plots) + ((i, j - 1) in plots) + ((i - 1, j - 1) in plots)
            if sum == 1 or sum == 3:
                sides += 1
            # Diagonal pairs!
            sum2 = ((i, j) in plots) + ((i - 1, j - 1) in plots) + ((i - 1, j) not in plots) + ((i, j - 1) not in plots)
            sum3 = ((i - 1, j) in plots) + ((i, j - 1) in plots) + ((i, j) not in plots) + ((i - 1, j - 1) not in plots)
            if sum2 == 4 or sum3 == 4:
                sides += 2
    return sides

y = 0
x = 0
def next_region():
    global y, x
    while garden_map_utilization[y][x]:
        x += 1
        if x == len(garden_map_utilization[y]):
            x = 0
            y += 1
        if y == len(garden_map_utilization):
            return False
    current_region_char = garden_map[y][x]
    current_region = []
    unchecked_plots = [(y, x)]
    while len(unchecked_plots) != 0:
        current_plot = unchecked_plots[0]
        finds = find_adjacent_plots(current_plot[0], current_plot[1], current_region_char)
        garden_map_utilization[current_plot[0]][current_plot[1]] = True
        current_region.append(current_plot)
        unchecked_plots.pop(0)
        unchecked_plots.extend(finds.difference(current_region, unchecked_plots))
    return current_region

region = next_region()
part1 = 0
part2 = 0
while region != False:
    part1 += region_perimeter(region) * len(region)
    part2 += region_sides(region) * len(region)
    region = next_region()
print(f"Part 1: {part1}")
print(f"Part 2: {part2}")