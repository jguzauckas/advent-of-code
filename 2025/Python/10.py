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

light_key = lights[0]
button_options = buttons[0]
results = []
for option in button_options:
    lights = [False for _ in range(len(light_key))]
    score = 0
    for light in option:
        lights[light] = not lights[light]
    for light, check in zip(lights, light_key):
        if light == check:
            score += 1
    results.append((score, [option], lights))
results.sort()
results.reverse()
print(results)
for result in results:
    score_to_beat, options, current_lights = result
    new_results = []
    flag = False
    for option in button_options:
        lights = current_lights[:]
        score = 0
        for light in option:
            lights[light] = not lights[light]
        for light, check in zip(lights, light_key):
            if light == check:
                score += 1
        if score >= score_to_beat:
            flag = True
        new_results.append((score, options + [option], lights))
        if score == len(light_key):
            solution = len(options) + 1
            print(solution)
            break
    if flag:
        results.extend(new_results)
    if score > 0:
        break
    
results.sort()
results.reverse()
print(results)