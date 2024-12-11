import math

str_inp = open("2024/Inputs/11.txt", "r").readline()

stones = []
for str_num in str_inp.split(" "):
    stones.append(int(str_num))

for i in range(25):
    stone_index = 0
    while stone_index < len(stones):
        current_stone = stones[stone_index]
        if current_stone == 0:
            stones[stone_index] = 1
        elif int(math.log10(current_stone)) % 2 + 1 == 0:
            str_stone = str(current_stone)
            print(str_stone)
            stones.insert(stone_index, int(str_stone[:len(str_stone) // 2]))
            stone_index += 1
            stones[stone_index] = int(str_stone[len(str_stone) // 2 + 1:])
        else:
            stones[stone_index] *= 2024
        stone_index += 1

print(stones)
print(len(stones))