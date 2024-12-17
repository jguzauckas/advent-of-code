str_inp = [x.strip() for x in open("2024/Inputs/17.txt", "r").readlines()]

a = int(str_inp[0][12:])
b = int(str_inp[1][12:])
c = int(str_inp[2][12:])
instructions = list(map(int, str_inp[4][9:].split(",")))

def print_result(num_list, str):
    print(str, end="")
    for index, num in enumerate(num_list):
        if index < len(num_list) - 1:
            print(f"{num},", end="")
        else:
            print(num)

result = []
while a != 0:
    result.append(((((a % 8) ^ 1) ^ 4) ^ (a // (2 ** ((a % 8) ^ 1)))) % 8)
    b = (((a % 8) ^ 1) ^ 4) ^ (a // (2 ** ((a % 8) ^ 1)))
    a = a // 8
print_result(result, "Part 1: ")

def place_options(num, a):
    options = []
    i = 0
    temp_a = a
    while i < 8:
        temp_a = i + (2 ** 3) * a
        result_calc = ((((temp_a % 8) ^ 1) ^ 4) ^ (temp_a // (2 ** ((temp_a % 8) ^ 1)))) % 8
        if result_calc == num:
            options.append(temp_a)
        i += 1
    return options

def test_a_values(index, options):
    good_options = []
    for i, option in enumerate(options):
        temp_index = -(index + 1)
        good_value = True
        while option != 0 and good_value:
            output = ((((option % 8) ^ 1) ^ 4) ^ (option // (2 ** ((option % 8) ^ 1)))) % 8
            if output != instructions[temp_index]:
                good_value = False
            option = option // 8
            temp_index += 1
        if good_value:
            good_options.append(options[i])
    return good_options

options = [0]
for index, num in enumerate(reversed(instructions)):
    new_options = []
    for option in options:
        options = new_options.extend(test_a_values(index, place_options(num, option)))
    options = new_options
print(f"Part 2: {min(options)}")

a = min(options)
result = []
while a != 0:
    result.append(((((a % 8) ^ 1) ^ 4) ^ (a // (2 ** ((a % 8) ^ 1)))) % 8)
    b = (((a % 8) ^ 1) ^ 4) ^ (a // (2 ** ((a % 8) ^ 1)))
    a = a // 8
print_result(result, "Part 2 Program Output: ")