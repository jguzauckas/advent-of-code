# Read lines from file
str_inp = []
with open('2025/Inputs/02.txt', 'r') as inp:
    str_inp = inp.readline()

# Split file contents into list of ranges as strings
list_inp = str_inp.split(",")

# Split each entry into a first and last range value
from collections import namedtuple
idrange = namedtuple("idrange", ("first", "last"))
idranges = []
for str_in in list_inp:
    current_range = str_in.split("-")
    idranges.append(idrange(int(current_range[0]), int(current_range[1])))

# Part 1
invalid_ids_1 = []
invalid_ids_2 = []
for idrange in idranges:
    for i in range(idrange.first, idrange.last + 1):
        num_as_str = str(i)
        if len(num_as_str) % 2 == 0 and num_as_str[:len(num_as_str)//2] == num_as_str[len(num_as_str)//2:]:
            invalid_ids_1.append(i)
        for j in range(1, len(num_as_str)):
            if len(num_as_str) % j == 0:
                str_to_check = num_as_str[:j]
                flag = True
                for k in range(0,len(num_as_str),j):
                    if str_to_check != num_as_str[k:k+j]:
                        flag = False
                        break
                if flag:
                    invalid_ids_2.append(i)
                    break

print(sum(invalid_ids_1), sum(invalid_ids_2))

