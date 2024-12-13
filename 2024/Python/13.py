import re
from collections import namedtuple

str_inp = [x.strip() for x in open("2024/Inputs/13.txt", "r").readlines()]

button = re.compile(r"X(\+\d+), Y(\+\d+)")
prize = re.compile(r"X=(\d+), Y=(\d+)")
claw_machine = namedtuple("claw_machine", ("ax", "ay", "bx", "by", "px", "py"))
machines = []
for i in range(0, len(str_inp), 4):
    ax, ay = map(int, button.search(str_inp[i]).groups())
    bx, by = map(int, button.search(str_inp[i + 1]).groups())
    px, py = map(int, prize.search(str_inp[i + 2]).groups())
    machines.append(claw_machine(ax, ay, bx, by, px, py))

def solve_claw(c):
    a_numerator = c.by * c.px - c.bx * c.py
    denominator = c.ax * c.by - c.ay * c.bx
    a = a_numerator / denominator
    if a % 1 != 0:
        return False
    b_numerator = c.ax * c.py - c.ay * c.px
    b = b_numerator / denominator
    if b % 1 != 0:
        return False
    return (int(a), int(b))

part1 = 0
part2 = 0
for claw in machines:
    solution1 = solve_claw(claw)
    if solution1:
        a, b = solution1
        if a <= 100 and b <= 100:
            part1 += 3 * a + b
    solution2 = solve_claw(claw._replace(px = claw.px + 10000000000000)._replace(py = claw.py + 10000000000000))
    if solution2:
        a, b = solution2
        part2 += 3 * a + b 
print(f"Part 1: {part1}")
print(f"Part 2: {part2}")