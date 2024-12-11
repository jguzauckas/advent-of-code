from collections import defaultdict

str_inp = open("2024/Inputs/11.txt", "r").readline()

stones = defaultdict(int)
for str_num in str_inp.split(" "):
    stones[int(str_num)] += 1

def str_to_int(s):
    exponent = 0
    num = 0
    for x in reversed(s):
        num += int(x) * 10 ** exponent
        exponent += 1
    return num

def blink(current_stones):
    next_stones = defaultdict(int)

    for stone, count in current_stones.items():
        if stone == 0:
            next_stones[1] += count
        elif len(str(stone)) % 2 == 0:
            next_stones[str_to_int(str(stone)[:len(str(stone)) // 2])] += count
            next_stones[str_to_int(str(stone)[len(str(stone)) // 2:])] += count
        else:
            next_stones[stone * 2024] += count
    
    return next_stones

for i in range(75):
    stones = blink(stones)
    if i == 24:
        print(f"Part 1: {sum(stones.values())}")
print(f"Part 2: {sum(stones.values())}")