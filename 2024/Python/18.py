import queue

str_inp = [x.strip() for x in open("2024/Inputs/18.txt", "r").readlines()]
coordinates = [(int(x), int(y)) for x, y in [str.split(",") for str in str_inp]]

start = (0, 0)
end = (70, 70)

map = []
for i in range(71):
    row = []
    for j in range(71):
        row.append(".")
    map.append(row)

directions = [(-1, 0), (0, 1), (1, 0), (0, -1)]
def neighbors(y, x):
    neighbors = []
    for direction in directions:
        new_y = y + direction[0]
        new_x = x + direction[1]
        if 0 <= new_x <= 70 and 0 <= new_y <= 70:
            if map[new_y][new_x] == ".":
                neighbors.append((new_y, new_x))
    return neighbors

def mapweighting():
    frontier = queue.PriorityQueue()
    frontier.put((0, start))
    came_from = dict()
    cost_so_far = dict()
    came_from[start] = None
    cost_so_far[start] = 0

    while not frontier.empty():
        current = frontier.get()[1]
        
        for next in neighbors(current[0], current[1]):
            new_cost = cost_so_far[current] + 1
            if next not in cost_so_far or new_cost < cost_so_far[next]:
                cost_so_far[next] = new_cost
                priority = new_cost
                frontier.put((priority, next))
                came_from[next] = current
    
    return cost_so_far

for i in range(1024):
    map[coordinates[i][0]][coordinates[i][1]] = "#"

print(f"Part 1: {mapweighting()[end]}")

count = 1024
for i in range(1024, len(coordinates)):
    map[coordinates[i][0]][coordinates[i][1]] = "#"
    count += 1
    if end not in mapweighting():
        print(f"Part 2: {coordinates[i]}")
        break
