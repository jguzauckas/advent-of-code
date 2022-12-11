# Import the input into a list of strings to process
with open("2022/Inputs/02.txt", "r") as input_file:
    input_str: list[str] = input_file.readlines()

# Strip the newline characters off of each input line
for pos, item in enumerate(input_str):
    input_str[pos] = item.strip()

# Take each line of the input and turn it into a two string tuple
# Make a list of all of these tuples
input: list[tuple] = []
for inp in input_str:
    temp = inp.split(" ")
    input.append(tuple(temp))

# Translate each move into RPS for parts 1 and 2:
coded_moves_p1 = {
    "A": "Rock",
    "B": "Paper",
    "C": "Scissors",
    "X": "Rock",
    "Y": "Paper",
    "Z": "Scissors",
}
part1: list[tuple] = []

coded_moves_p2 = {
    "A": "Rock",
    "B": "Paper",
    "C": "Scissors",
    "X": "Lose",
    "Y": "Draw",
    "Z": "Win",
}
part2: list[tuple] = []

for tup in input:
    opp, me = tup[0], tup[1]
    part1.append((coded_moves_p1[opp], coded_moves_p1[me]))
    part2.append((coded_moves_p2[opp], coded_moves_p2[me]))

# Function to determine point value of a round
def win_loss_value(turn: tuple) -> int:
    opp, me = turn[0], turn[1]
    win = {"Rock": "Paper", "Paper": "Scissors", "Scissors": "Rock"}
    if opp == me:
        return 3
    if win[opp] == me:
        return 6
    return 0


# Function to assign score based on move played
def move_value(turn: tuple) -> int:
    me = turn[1]
    points = {"Rock": 1, "Paper": 2, "Scissors": 3}
    return points[me]


# Function to calculate score for a round
def round_score(turn: tuple) -> int:
    return win_loss_value(turn) + move_value(turn)


# Function to determine round value for part 2
def p2_round_calc(turn: tuple) -> int:
    opp, me = turn[0], turn[1]
    lose = {"Rock": "Scissors", "Paper": "Rock", "Scissors": "Paper"}
    win = {"Rock": "Paper", "Paper": "Scissors", "Scissors": "Rock"}
    points = {"Rock": 1, "Paper": 2, "Scissors": 3}
    if me == "Lose":
        round = points[lose[opp]]
    elif me == "Draw":
        round = points[opp] + 3
    else:
        round = points[win[opp]] + 6
    return round


# Run function on all turns
total = 0
for turn in part1:
    total += round_score(turn)
print(
    f"Your total score would be {total} points with our interpretation of the strategy guide."
)
total = 0
for turn in part2:
    total += p2_round_calc(turn)
print(f"Your total score would be {total} points with what the elf says.")
