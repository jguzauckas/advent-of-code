import re

# Read String from file
str_inp = ""
with open('2024/Inputs/03.txt', 'r') as inp:
    str_inp = inp.read()

# Generate regex to isolate correct values
search_term = re.compile("mul\\(\\d+,\\d+\\)")
results = search_term.findall(str_inp)

# Parse each found value
def sum_products(muls):
    sum = 0
    number_search_term = re.compile("\\d+")
    for mul in muls:
        str_nums = number_search_term.findall(mul)
        sum += int(str_nums[0]) * int(str_nums[1])
    return sum

# Finds and returns indices of the next do, don't, and mul functions, if present
def find_functions(search_str, index):
    # Find next do() function, if present
    next_do = do_search_term.search(search_str, index)
    if next_do == None:
        next_do = len(search_str) + 1
    else:
        next_do = next_do.start()
    # Find next don't() function, if present
    next_dont = dont_search_term.search(search_str, index)
    if next_dont == None:
        next_dont = len(search_str) + 1
    else:
        next_dont = next_dont.start()
    # Find next mul(#, #) function, if present
    next_mul = mul_search_term.search(search_str, index)
    if next_mul == None:
        next_mul = len(search_str) + 1
    else:
        next_mul = next_mul.start()
    return (next_do, next_dont, next_mul)

# Set up to go through the input to find each do, don't and mul function
index = 0
enable = True
part_1_matches = []
part_2_matches = []
do_search_term = re.compile("do\\(\\)")
dont_search_term = re.compile("don't\\(\\)")
mul_search_term = re.compile("mul\\(\\d+,\\d+\\)")

# Repeat process of identifying next functions until through input String
while index < len(str_inp):
    next_do, next_dont, next_mul = find_functions(str_inp, index)
    
    # Determine lowest index of the functions, which would be the next one
    next_op = min(next_do, next_dont, next_mul)

    # If do() is next, enable and update index
    if next_do == next_op:
        enable = True
        index = next_do + 1

    # If don't() is next, disable and update index
    elif next_dont == next_op:
        enable = False
        index = next_dont + 1

    # If mul(#, #) is next, add to part 1 and conditionally to part 2 and update index
    elif next_mul == next_op:
        find = mul_search_term.search(str_inp, index)[0]
        part_1_matches.append(find)
        if enable:
            part_2_matches.append(find)
        index = next_mul + 1

print(sum_products(part_1_matches))
print(sum_products(part_2_matches))