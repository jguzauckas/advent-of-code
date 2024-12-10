import copy

str_inp = [x.strip() for x in open("2024/Inputs/10.txt", "r").readlines()]

topo_map = []
for line in str_inp:
    map_line = []
    for col in line:
        map_line.append(int(col))
    topo_map.append(map_line)

def trail(y, x):
    current_nodes = [(y, x)]
    next_nodes = []
    for i in range(1, 10):
        for node in current_nodes:
            results = check_directions(node[0], node[1], i)
            if results[0]:
                next_nodes.append((node[0] - 1, node[1]))
            if results[1]:
                next_nodes.append((node[0], node[1] + 1))
            if results[2]:
                next_nodes.append((node[0] + 1, node[1]))
            if results[3]:
                next_nodes.append((node[0], node[1] - 1))
        current_nodes = copy.deepcopy(next_nodes)
        next_nodes.clear()
    return (len(set(current_nodes)), len(current_nodes))

def check_directions(y, x, h):
    results = [0, 0, 0, 0] # N, E, S, W
    if 0 <= y - 1 < len(topo_map) and topo_map[y - 1][x] == h:
        results[0] = 1
    if 0 <= x + 1 < len(topo_map[y]) and topo_map[y][x + 1] == h:
        results[1] = 1
    if 0 <= y + 1 < len(topo_map) and topo_map[y + 1][x] == h:
        results[2] = 1
    if 0 <= x - 1 < len(topo_map[y]) and topo_map[y][x - 1] == h:
        results[3] = 1
    return results

trail_y = 0
trail_x = -1
def next_trail_head():
    global trail_y, trail_x
    if trail_x == len(topo_map[trail_y]) - 1:
        trail_x = -1
        trail_y += 1
    if trail_y == len(topo_map):
        return False
    trail_x += 1
    while topo_map[trail_y][trail_x] != 0:
        if trail_x == len(topo_map[trail_y]) - 1:
            trail_x = -1
            trail_y += 1
        if trail_y == len(topo_map):
            return False
        trail_x += 1
    return True

total1 = 0
total2 = 0
while next_trail_head():
    part1, part2 = trail(trail_y, trail_x)
    total1 += part1
    total2 += part2
print(f"Part 1: {total1}")
print(f"Part 2: {total2}")