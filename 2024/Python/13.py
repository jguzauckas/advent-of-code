import re
from collections import namedtuple

str_inp = [x.strip() for x in open("2024/Inputs/13.txt", "r").readlines()]

button = re.compile(r"X(\+\d+), Y(\+\d+)")
prize = re.compile(r"X=(\d+), Y=(\d+)")
claw_machine = namedtuple("claw_machine", ("ax", "ay", "bx", "by", "px", "py"))
num_inp = []
for i in range(0, len(str_inp), 4):
    ax, ay = map(int, button.search(str_inp[i]).groups())
    bx, by = map(int, button.search(str_inp[i + 1]).groups())
    px, py = map(int, prize.search(str_inp[i + 2]).groups())
    num_inp.append(claw_machine(ax, ay, bx, by, px, py))
print(num_inp)
