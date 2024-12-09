guard_map = [x.strip() for x in open("2024/Inputs/06.txt", "r").readlines()]

# Tracking map for directions gone through each location
def generate_map_track():
    guard_map_track = []
    for row in guard_map:
        track_row = []
        for col in row:
            track_row.append({"^":False, ">":False, "v":False, "<":False})
        guard_map_track.append(track_row)
    return guard_map_track

guard_y = 0
guard_x = 0
guard_directions_cycle = {"^":">", ">":"v", "v":"<", "<":"^"}
guard_directions_shift = {"^":(-1, 0), ">":(0, 1), "v":(1, 0), "<":(0, -1)}
guard_direction = "<"

for y_index, row in enumerate(guard_map):
    for x_index, col in enumerate(row):
        if row.count(guard_directions_cycle[guard_direction]) == 1:
            guard_y = y_index
            guard_x = row.index(guard_directions_cycle[guard_direction])

def border_detect(map, y, x):
    return not (0 <= y < len(map) and 0 <= x < len(map[0]))

def replace_with(map, y, x, str):
    row = map[y]
    row = row[:x] + str + row[x + 1:]
    map[y] = row

def update_tracker(track, y, x, dir):
    if track[y][x][dir]:
        return True   
    track[y][x][dir] = True
    return False

def original_guard_course(map, y, x, dir):
    while not border_detect(map, y, x):
        replace_with(map, y, x, "X")
        y_shift, x_shift = guard_directions_shift[guard_directions_cycle[dir]]
        if border_detect(map, y + y_shift, x + x_shift):
            break
        while map[y + y_shift][x + x_shift] == "#":
            dir = guard_directions_cycle[dir]
            y_shift, x_shift = guard_directions_shift[guard_directions_cycle[dir]]
            if border_detect(map, y + y_shift, x + x_shift):
                break
        if border_detect(map, y + y_shift, x + x_shift):
            break
        y += y_shift
        x += x_shift
        replace_with(map, y, x, guard_directions_cycle[dir])
    return map

def check_map_for_loop(map, track, y, x, dir):
    while not border_detect(map, y, x):
        replace_with(map, y, x, "X")
        y_shift, x_shift = guard_directions_shift[guard_directions_cycle[dir]]
        if border_detect(map, y + y_shift, x + x_shift):
            break
        while map[y + y_shift][x + x_shift] == "#":
            dir = guard_directions_cycle[dir]
            y_shift, x_shift = guard_directions_shift[guard_directions_cycle[dir]]
            if border_detect(map, y + y_shift, x + x_shift):
                break
        if border_detect(map, y + y_shift, x + x_shift):
            break
        if update_tracker(track, y, x, guard_directions_cycle[dir]):
            return True
        y += y_shift
        x += x_shift
        replace_with(map, y, x, guard_directions_cycle[dir])
    return False

guard_course = original_guard_course(guard_map, guard_y, guard_x, guard_direction)
print(f"Part 1: {guard_course.count("X")}")
obs_y = 0
obs_x = -1
def next_obstacle():
    global obs_y, obs_x
    if obs_x == len(guard_course[obs_y]) - 1:
        obs_x = -1
        obs_y += 1
    if obs_y == len(guard_course):
        return False
    obs_x += 1
    while guard_course[obs_y][obs_x] != "X":
        if obs_x == len(guard_course[obs_y]) - 1:
            obs_x = -1
            obs_y += 1
        if obs_y == len(guard_course):
            return False
        obs_x += 1
    return True

count = 0  
while next_obstacle():
    next_map = guard_map.copy()
    replace_with(next_map, obs_y, obs_x, "#")
    next_map_track = generate_map_track()
    if check_map_for_loop(next_map, next_map_track, guard_y, guard_x, guard_direction):
        count += 1
print(f"Part 2: {count}")