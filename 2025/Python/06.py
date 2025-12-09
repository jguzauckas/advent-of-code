# Read in input into 2D list
list_inp = []
with open('2025/Inputs/06.txt', 'r') as inp:
    list_inp = inp.readlines()
list_inp = [x.strip() for x in list_inp]
values = [[int(y) for y in x.strip().split()] for x in list_inp[:-1]]
operators = list_inp[-1].strip().split()

total = 0
for i in range(len(operators)):
    problem_answer = 0
    if operators[i] == "+":
        for j in range(len(values)):
            problem_answer += values[j][i]
    else:
        problem_answer = 1
        for j in range(len(values)):
            problem_answer *= values[j][i]
    total += problem_answer
print(total)
        
spaces = [-1]
for i in range(len(list_inp[0])):
    is_space = True
    for j in range(len(list_inp)):
        if list_inp[j][i] != " ":
            is_space = False
            break
    if is_space:
        spaces.append(i)
spaces.append(len(list_inp[0]))
values = []
for i, j in zip(spaces[:-1], spaces[1:]):
    numbers = []
    for col in range(j - 1, i, -1):
        current_num_as_str = ""
        for row in list_inp[:-1]:
            current_num_as_str += row[col] if row[col].isdigit() else ""
        numbers.append(int(current_num_as_str))
    values.append(numbers)

total = 0
for i in range(len(operators)):
    problem_answer = 0
    if operators[i] == "+":
        for value in values[i]:
            problem_answer += value
    else:
        problem_answer = 1
        for value in values[i]:
            problem_answer *= value
    total += problem_answer
print(total)