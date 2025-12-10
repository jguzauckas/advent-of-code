list_inp = []
with open('2025/Inputs/10.txt', 'r') as inp:
    list_inp = inp.readlines()
lights = []
buttons = []
joltages = []
for row in list_inp:
    temp = row.strip().split(" ")
    lights.append([True if x == "#" else False for x in temp[0][1:-1]])
    buttons.append([[int(x) for x in y[1:-1].split(',')] for y in temp[1:-1]])
    joltages.append([int(x) for x in temp[-1][1:-1].split(',')])
answer = 0
for index in range(len(list_inp)):
    light_key = lights[index]
    button_options = buttons[index]
    results = []
    solution = 0
    for option in button_options:
        row_lights = [False for _ in range(len(light_key))]
        score = 0
        for light in option:
            row_lights[light] = not row_lights[light]
        for light, check in zip(row_lights, light_key):
            if light == check:
                score += 1
        if score == len(light_key):
            solution = 1
            break
        results.append((score, [option], row_lights))
    results.sort()
    results.reverse()
    while(solution == 0):
        results_keep = []
        for result in results:
            score_to_beat, options, current_row_lights = result
            new_results = []
            flag = False
            for option in button_options:
                if option not in options:
                    row_lights = current_row_lights[:]
                    score = 0
                    for light in option:
                        row_lights[light] = not row_lights[light]
                    for light, check in zip(row_lights, light_key):
                        if light == check:
                            score += 1
                    if score >= score_to_beat:
                        flag = True
                    new_results.append((score, options + [option], row_lights))
                    if score == len(light_key):
                        solution = len(options) + 1
                        break
            if flag:
                results_keep.extend(new_results)
            if solution > 0:
                break
        results = results_keep[:]
        results.sort()
        results.reverse()
    print(index, ":", solution)
    answer += solution
print(answer)

for button, joltage in zip(buttons, joltages):
    matrix = []
    for i in range(len(joltage)):
        row = []
        for option in button:
            if i in option:
                row.append(1)
            else:
                row.append(0)
        row.append(joltage[i])
        matrix.append(row)
    leader = -1
    index = 0
    while leader < 0:
        for i in range(len(matrix)):
            if 
