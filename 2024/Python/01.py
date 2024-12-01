# Read lines from files
list_inp = []
with open('2024/Inputs/01.txt', 'r') as inp:
    list_inp = inp.readlines()

# Turn each line into two int values and add to lists
list_a = []
list_b = []
for inp_str in list_inp:
    str_a, str_b = inp_str.split("   ")
    list_a.append(int(str_a))
    list_b.append(int(str_b))

# Part 1
# Sort lists from least to greatest
list_a.sort()
list_b.sort()

# Get paired values and add absolute value of differences to distance
dist = 0
for a, b in zip(list_a, list_b):
    dist += abs(b - a)
print(dist)

# Part 2
# Go through list a and add product of a and number of occurences in list b
similarity = 0
for a in list_a:
    similarity += a * list_b.count(a)
print(similarity)