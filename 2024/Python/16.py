import queue

str_inp_list = [x.strip() for x in open("2024/Inputs/16.txt").readlines()]

maze_grid = []
for y, row in enumerate(str_inp_list):
    grid_row = []
    for x, char in enumerate(row):
        grid_row.append(char)
        if char == "S":
            start = (y, x, "E")
        elif char == "E":
            end = (y, x, "E")
    maze_grid.append(grid_row)

directions = {"N":(-1, 0), "E":(0, 1), "S":(1, 0), "W":(0, -1)}
def neighbors(y, x):
    neighbors = []
    for direction in directions:
        if maze_grid[y + directions[direction][0]][x + directions[direction][1]] in ".ES":
            neighbors.append((y + directions[direction][0], x + directions[direction][1], direction))
    return neighbors

turn_costs = {"NN":0, "EE":0, "SS":0, "WW":0, "NE": 1, "ES":1, "SW":1, "WN":1, "NW":1, "WS":1, "SE":1, "EN":1, "NS":2, "SN":2, "EW":2, "WE":2}
def next_cost(initial, next):
    return turn_costs[initial[2] + next[2]] * 1000 + 1

def dijkstra_search(start):
    frontier = queue.PriorityQueue(0)
    frontier.put((0, start))
    came_from = {}
    cost_so_far = {}
    came_from[start] = None
    cost_so_far[start] = 0

    while not frontier.empty():
        current = frontier.get()[1]

        # if current[0] == end[0] and current[1] == end[1]:
        #     break

        for next in neighbors(current[0], current[1]):
            new_cost = cost_so_far[current] + next_cost(current, next)
            if next not in cost_so_far or new_cost < cost_so_far[next]:
                cost_so_far[next] = new_cost
                frontier.put((new_cost, next))
                came_from[next] = current
    
    return came_from, cost_so_far

def reconstruct_path(came_from):
    current = end
    path = []
    if current not in came_from:
        return []
    while current != start:
        path.append(current)
        current = came_from[current]
    path.append(start)
    path.reverse()
    return path

def path_cost(path):
    sum = 0
    direction = "E"
    for current, next in zip(path, path[1:]):
        delta_y = next[0] - current[0]
        delta_x = next[1] - current[1]
        new_direction = ""
        if delta_y == 1:
            new_direction = "N"
        elif delta_y == -1:
            new_direction = "S"
        elif delta_x == 1:
            new_direction = "E"
        else:
            new_direction = "W"
        sum += turn_costs[direction + new_direction] * 1000 + 1
        direction = new_direction
    return sum

def part1():
    came_from, costs = dijkstra_search(start)
    return path_cost(reconstruct_path(came_from))

def part2():
    came_from, from_start = dijkstra_search(start)
    came_from, from_end = dijkstra_search((end[0], end[1], "W"))
    optimal = part1()
    flip = {"E": "W", "W": "E", "N": "S", "S": "N"}
    result = set()
    for row in range(len(maze_grid)):
        for col in range(len(maze_grid[0])):
            if maze_grid[row][col] != "#":
                for dir in "EWNS":
                    state_from_start = (row, col, dir)
                    state_from_end = (row, col, flip[dir])
                    if state_from_start in from_start and state_from_end in from_end:
                        if from_start[state_from_start] + from_end[state_from_end] == optimal:
                            result.add((row, col))
    return result

print(f"Part 1: {part1()}")
print(f"Part 2: {part2()}")
results = part2()
final_str = ""
for y, row in enumerate(maze_grid):
    for x, col in enumerate(row):
        if (y, x) in results:
            final_str += "O"
        else:
            final_str += col
    final_str += "\n"
open("2024/Inputs/16 map.txt", "w").write(final_str)
print(len(results))