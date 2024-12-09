list_inp = open("2024/Inputs/07.txt").readlines()

list_eqs = []
for row in list_inp:
    vals = row.split(" ")
    nums = []
    nums.append(int(vals[0][:-1]))
    nums.append(tuple(map(int, vals[1:])))
    list_eqs.append(nums)

# Creates all options in the given numerical base
def operations(equation_values, base):
    options = []
    for i in range(0, base ** (len(equation_values) - 1)):
        option = []
        for j in range(0, len(equation_values) - 1):
            option.append(i % (base ** (j + 1)) // (base ** j))
        options.append(option)
    return options

# Part 1
count = 0
for equation in list_eqs:
    answer = equation[0]
    values = equation[1]
    options = operations(values, 2)
    solution = False
    for option in options:
        total = values[0]
        for i in range(0, len(option)):
            if option[i] == 1:
                total += values[i + 1]
            else:
                total *= values[i + 1]
        if total == answer:
            solution = True
    print(answer, solution)
    count += answer if solution else 0
print(f"Part 1: {count}")

# Part 2
count = 0
for equation in list_eqs:
    answer = equation[0]
    values = equation[1]
    options = operations(values, 3)
    solution = False
    for option in options:
        total = values[0]
        for i in range(0, len(option)):
            if option[i] == 0:
                total += values[i + 1]
            elif option[i] == 1:
                total *= values[i + 1]
            else:
                total = int(str(total) + str(values[i + 1]))
            if total > answer:
                break
        if total == answer:
            solution = True
            break
    print(answer, solution)
    count += answer if solution else 0
print(f"Part 2: {count}")