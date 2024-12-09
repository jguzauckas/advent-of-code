str_inp = open("2024/Inputs/09.txt", "r").readline()

nums_inp = [int(x) for x in str_inp]

# Build Disk
def build_disk():
    disk = []
    current_file_id = 0
    file = True
    for num in nums_inp:
        if file:
            for i in range(num):
                disk.append(current_file_id)
            current_file_id += 1
            file = False
        else:
            for i in range(num):
                disk.append(".")
            file = True
    return disk

# Defrag Disk Part 1
disk = build_disk()
file_index = len(disk) - 1
empty_index = disk.index(".")
while file_index > empty_index:
    disk[empty_index] = disk[file_index]
    disk[file_index] = "."
    file_index -= 1
    while disk[file_index] == ".":
        file_index -= 1
    empty_index = disk.index(".")

total = 0
for index, num in enumerate(disk):
    if num == ".":
        break
    total += index * num
print(f"Part 1: {total}")

# Defrag Disk Part 2
disk = build_disk()
file_index = len(disk) - 1
file_id = disk[file_index]
while file_id > 10:
    file_size = 1
    read_head = file_index - 1
    while disk[read_head] == disk[file_index]:
        file_size += 1
        read_head -= 1
    max_gap = 0
    max_index = -1
    current_gap = 0
    for i in range(0, file_index):
        if disk[i] == ".":
            current_gap += 1
        else:
            if current_gap > max_gap:
                max_gap = current_gap
                max_index = i - current_gap
            if max_gap >= file_size:
                break
            current_gap = 0
    if max_index > -1 and max_gap >= file_size:
        for i in range(file_size):
            disk[max_index + i] = file_id
            disk[file_index - i] = "."
    file_index -= file_size
    file_id -= 1
    while disk[file_index] != file_id:
        file_index -= 1

total = 0
for index, num in enumerate(disk):
    if num == ".":
        continue
    total += index * num
print(f"Part 2: {total}")