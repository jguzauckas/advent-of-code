import math
# Read in input into 2D list
list_inp = []
with open('2025/Inputs/08.txt', 'r') as inp:
    list_inp = inp.readlines()
list_inp = [[int(y) for y in x.strip().split(",")] for x in list_inp]
distances = {}
for index_1, point_1 in zip(range(len(list_inp)), list_inp):
    for index_2, point_2 in zip(range(len(list_inp)), list_inp):
        if index_1 == index_2:
            continue
        if (index_1, index_2) not in distances and (index_2, index_1) not in distances:
            distances[(index_1, index_2)] = math.sqrt((point_1[0] - point_2[0]) ** 2 + (point_1[1] - point_2[1]) ** 2 + (point_1[2] - point_2[2]) ** 2)
distances_1 = distances.copy()
connections = []
for i in range(1000):
    minimum_distance = list(distances.values())[0]
    minimum_location = list(distances.keys())[0]
    for key, value in distances.items():
        if value < minimum_distance:
            minimum_location = key
            minimum_distance = value
    del distances[minimum_location]
    join_connection_0 = -1
    join_connection_1 = -1
    for index, connection in zip(range(len(connections)), connections):
        if minimum_location[0] in connection:
            join_connection_0 = index
        if minimum_location[1] in connection:
            join_connection_1 = index
    if join_connection_0 >= 0 and join_connection_1 >= 0:
        if join_connection_0 == join_connection_1:
            continue
        connections[join_connection_0].extend(connections[join_connection_1])
        connections.pop(join_connection_1)
    elif join_connection_0 >= 0:
        connections[join_connection_0].append(minimum_location[1])
    elif join_connection_1 >= 0:
        connections[join_connection_1].append(minimum_location[0])
    else:
        connections.append(list(minimum_location))
answer = 1
for i in range(3):
    length = len(connections[0])
    connector = connections[0]
    for connection in connections:
        if len(connection) > length:
            length = len(connection)
            connector = connection
    answer *= length
    connections.remove(connector)
print(answer)

distances = distances_1
connections = []
final_pairing = []
while len(distances) > 0:
    minimum_distance = list(distances.values())[0]
    minimum_location = list(distances.keys())[0]
    for key, value in distances.items():
        if value < minimum_distance:
            minimum_location = key
            minimum_distance = value
    final_pairing = minimum_location
    join_connection_0 = -1
    join_connection_1 = -1
    for index, connection in zip(range(len(connections)), connections):
        if minimum_location[0] in connection:
            join_connection_0 = index
        if minimum_location[1] in connection:
            join_connection_1 = index
    if join_connection_0 >= 0 and join_connection_1 >= 0:
        if join_connection_0 == join_connection_1:
            del distances[minimum_location]
            continue
        connections[join_connection_0].extend(connections[join_connection_1])
        connections.pop(join_connection_1)
        join_connection_0 = join_connection_0 - 1 if join_connection_1 < join_connection_0 else join_connection_0
        for index_1 in connections[join_connection_0]:
            for index_2 in connections[join_connection_0]:
                if index_1 != index_2:
                    if (index_1, index_2) in distances.keys():
                        del distances[(index_1, index_2)]
                    elif (index_2, index_1) in distances.keys():
                        del distances[(index_2, index_1)]
    elif join_connection_0 >= 0:
        connections[join_connection_0].append(minimum_location[1])
        for index in connections[join_connection_0]:
            if index != minimum_location[1]:
                if (index, minimum_location[1]) in distances.keys():
                    del distances[(index, minimum_location[1])]
                elif (minimum_location[1], index) in distances.keys():
                    del distances[(minimum_location[1], index)]
    elif join_connection_1 >= 0:
        connections[join_connection_1].append(minimum_location[0])
        for index in connections[join_connection_1]:
            if index != minimum_location[0]:
                if (index, minimum_location[0]) in distances.keys():
                    del distances[(index, minimum_location[0])]
                elif (minimum_location[0], index) in distances.keys():
                    del distances[(minimum_location[0], index)]
    else:
        connections.append(list(minimum_location))
        del distances[minimum_location]
print(list_inp[final_pairing[0]][0] * list_inp[final_pairing[1]][0])