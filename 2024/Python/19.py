from collections import defaultdict
import queue

str_inp = [x.strip() for x in open("2024/Inputs/19.txt", "r").readlines()]

towels = [x.strip() for x in str_inp[0].split(", ")]

patterns = []
for i in range(2, len(str_inp)):
    patterns.append(str_inp[i])

attempts = defaultdict(int)
def match_towels(matches, pattern, index):
    if index == len(pattern):
        return True
    for towel in towels:
        end_index = index + len(towel)
        if end_index <= len(pattern) and pattern[index:end_index] == towel:
            attempts[(towel, index)] += 1
            if attempts[(towel, index)] == 1 and match_towels(matches, pattern, end_index):
                matches.append(towel)
                return True
    return False

count = 0
for pattern in patterns[:]:
    matches = []
    flag = match_towels(matches, pattern, 0)
    attempts = defaultdict(int)
    if not flag:
        patterns.remove(pattern)
    count += flag
print(f"Part 1: {count}")

def find_full_sets(pattern):
    count = 0
    matches = queue.SimpleQueue()
    uniques = []
    for towel in towels:
        if pattern[:len(towel)] == towel:
            path = []
            path.append(towel)
            matches.put((len(towel), path))
    
    while not matches.empty():
        index, path = matches.get()
        print(index, path)

        if index == len(pattern):
            count += 1
            continue

        for towel in towels:
            end_index = index + len(towel)
            if end_index <= len(pattern) and pattern[index:end_index] == towel:
                new_path = path[:]
                new_path.append(towel)
                if new_path not in uniques:
                    uniques.append(new_path)
                    matches.put((end_index, new_path))

    return count

print(find_full_sets(patterns[0]))

# sum = 0
# count = 0
# for pattern in patterns:
#     num = find_full_sets(pattern)
#     if num > 0:
#         count += 1
#     sum += num
# print(count)
# print(f"Part 2: {sum}")