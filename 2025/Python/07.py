# Read in input into 2D list
list_inp = []
with open('2025/Inputs/07.txt', 'r') as inp:
    list_inp = inp.readlines()
list_inp = [x.strip() for x in list_inp]

splits = 0
beams = dict.fromkeys(range(len(list_inp[0])), 0)
beams[list_inp[0].index("S")] = 1
for row in list_inp:
    if row.count("^") == 0:
        continue
    for beam in beams:
        if row[beam] == "^" and beams[beam] > 0:
            splits += 1
            beams[beam - 1] += beams[beam]
            beams[beam + 1] += beams[beam]
            beams[beam] = 0
print(splits, sum(beams.values()))