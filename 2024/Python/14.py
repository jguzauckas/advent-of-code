import re
from collections import namedtuple
from statistics import variance

str_inp_list = [x.strip() for x in open("2024/Inputs/14.txt", "r").readlines()]

pattern = re.compile(r"p=(\d+),(\d+) v=(\-*\d+),(\-*\d+)")
robots = []
robot = namedtuple("robot", ("px", "py", "vx", "vy"))
for row in str_inp_list:
    values = pattern.search(row).groups()
    robots.append(robot(int(values[0]), int(values[1]), int(values[2]), int(values[3])))

final_positions = []
for i in range(103):
    row = []
    for j in range(101):
        row.append(0)
    final_positions.append(row)

def robot_position(robot, i):
    final_px = (robot.px + i * robot.vx) % 101
    final_py = (robot.py + i * robot.vy) % 103
    final_positions[final_py][final_px] += 1
    return (final_py, final_px)

def quadrant_count(y, x, fy, fx):
    sum = 0
    for i in range(y, fy):
        for j in range(x, fx):
            sum += final_positions[i][j]
    return sum

def quadrants_count():
    ylen = len(final_positions)
    xlen = len(final_positions[0])
    halfylen = ylen // 2
    halfxlen = xlen // 2
    product = 1
    print(halfylen, halfxlen)
    product *= quadrant_count(0, 0,  halfylen, halfxlen)
    product *= quadrant_count(0, halfxlen + 1,  halfylen, xlen)
    product *= quadrant_count(halfylen + 1, 0,  ylen, halfxlen)
    product *= quadrant_count(halfylen + 1, halfxlen + 1, ylen, xlen)
    return product

for robot in robots:
    robot_position(robot, 100)
print(f"Part 1: {quadrants_count()}")

min_x = 10 ** 10
min_x_loc = -1
min_y = 10 ** 10
min_y_loc = -1
for i in range(103):
    ys, xs = [], []
    for robot in robots:
        y, x = robot_position(robot, i)
        ys.append(y)
        xs.append(x)
    if variance(ys) < min_y:
        min_y = variance(ys)
        min_y_loc = i
    if variance(xs) < min_x:
        min_x = variance(xs)
        min_x_loc = i
print("Part 2:", min_x_loc+((pow(101, -1, 103)*(min_y_loc-min_x_loc)) % 103)*101)