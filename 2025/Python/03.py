# Read lines from file
list_inp = []
with open('2025/Inputs/03.txt', 'r') as inp:
    list_inp = inp.readlines()
stripped_list = [s.strip() for s in list_inp]

# Parse into lists of numbers
banks = []
for line in stripped_list:
    bank = []
    for char in line:
        bank.append(int(char))
    banks.append(bank)

# Part 1 Calculation
joltages = []
for bank in banks:
    tens = -1
    tens_index = -1
    for index in range(len(bank) - 1):
        if bank[index] > tens:
            tens = bank[index]
            tens_index = index
    ones = -1
    for index in range(tens_index + 1, len(bank)):
        if bank[index] > ones:
            ones = bank[index]
    joltages.append(tens * 10 + ones)

print(sum(joltages))

# Part 2 Calculation
joltages = []
for bank in banks:
    joltage = 0
    last_index = 0
    for i in range(11, -1, -1):
        place = -1
        place_index = -1
        for index in range(last_index, len(bank) - i):
            if bank[index] > place:
                place = bank[index]
                place_index = index
        last_index = place_index + 1
        joltage += place * 10 ** i
    joltages.append(joltage)

print(sum(joltages))