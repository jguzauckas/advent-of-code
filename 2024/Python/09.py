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
while file_id > 0:
    file_size = 1
    read_head = file_index - 1
    while disk[read_head] == disk[file_index]:
        file_size += 1
        read_head -= 1
    try:
        new_location = disk.index("." * file_size)
        for i in range(file_size):
            disk[new_location + i] = file_id
        for i in range(file_size):
            disk[file_index - i] = "."
    except:
        pass
    file_index -= file_size
    file_id -= 1
    while disk[file_index] != file_id:
        file_index -= 1
print(disk)
total = 0
for index, num in enumerate(disk):
    if num == ".":
        continue
    total += index * num
print(f"Part 2: {total}")