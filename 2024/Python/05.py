str_list_inp = [x.strip() for x in open("2024/Inputs/05.txt", "r").readlines()]

middle = str_list_inp.index("")
rules = []
for i in range(0, middle):
    rules.append(list(map(int, str_list_inp[i].split("|"))))
updates = []
for i in range(middle + 1, len(str_list_inp)):
    updates.append(list(map(int, str_list_inp[i].split(","))))

before = {}
after = {}

for rule in rules:
    if rule[0] not in after:
        after[rule[0]] = [rule[1]]
    else:
        after[rule[0]].append(rule[1])
    if rule[1] not in before:
        before[rule[1]] = [rule[0]]
    else:
        before[rule[1]].append(rule[0])

def update_check(update):
    flag = True
    for index, num in enumerate(update):
        first_part_update = set(update[:index])
        after_check = set(after[num])
        first_part_overlap = first_part_update.intersection(after_check)

        last_part_update = set(update[index + 1:])
        before_check = set(before[num])
        last_part_overlap = last_part_update.intersection(before_check)

        if len(first_part_overlap) + len(last_part_overlap) != 0:
            flag = False
    return flag

def reorder(update):
    while not update_check(update):
        for index in range(0, len(update)):
            num = update[index]

            first_part_update = set(update[:index])
            after_check = set(after[num])
            first_part_overlap = first_part_update.intersection(after_check)

            last_part_update = set(update[index + 1:])
            before_check = set(before[num])
            last_part_overlap = last_part_update.intersection(before_check)

            if len(first_part_overlap) != 0:
                for num in first_part_overlap:
                    update.insert(index + 1, num)
                    update.remove(num)
                    index -= 1
            if len(last_part_overlap) != 0:
                for num in last_part_overlap:
                    update.remove(num)
                    update.insert(index - 1, num)
                    index += 1
    return update
    
sum1 = 0
sum2 = 0
for update in updates:
    if update_check(update):
        sum1 += update[len(update) // 2]
    else:
        new_update = reorder(update)
        sum2 += new_update[len(new_update) // 2]

print(sum1, sum2)
