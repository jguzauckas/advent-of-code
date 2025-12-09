# Read in input into 2D list
list_inp = []
with open('2025/Inputs/05.txt', 'r') as inp:
    list_inp = inp.readlines()
middle = list_inp.index("\n")
ranges = [[int(y[0]), int(y[1])] for y in (x.strip().split("-") for x in list_inp[:middle])]
ids = [int(x.strip()) for x in list_inp[middle + 1:]]

count = 0
for x in ids:
    for gap in ranges:
        if gap[0] <= x <= gap[1]:
            count += 1
            break
print(count)

final_ranges = [ranges[0]]
for gap in ranges:
    min = gap[0]
    max = gap[1]
    range_within = False
    ranges_left_overlap = []
    ranges_right_overlap = []
    ranges_within_overlap = []
    for final_gap in final_ranges:
        if min >= final_gap[0] and max <= final_gap[1]:
            range_within = True
            break
        if min >= final_gap[0] and min <= final_gap[1]:
            ranges_left_overlap.append(final_gap)
        if max >= final_gap[0] and max <= final_gap[1]:
            ranges_right_overlap.append(final_gap)
        if min < final_gap[0] and max > final_gap[1]:
            ranges_within_overlap.append(final_gap)
    if range_within:
        continue
    elif len(ranges_left_overlap) == 0 and len(ranges_right_overlap) == 0:
        final_ranges.append(gap)
    elif len(ranges_right_overlap) == 0:
        new_min = min([x[0] for x in ranges_left_overlap]) if len(ranges_left_overlap) > 1 else ranges_left_overlap[0][0]
        new_gap = [new_min, max]
        for temp in ranges_left_overlap:
            final_ranges.remove(temp)
        final_ranges.append(new_gap)
    elif len(ranges_left_overlap) == 0:
        new_max = max([x[1] for x in ranges_right_overlap]) if len(ranges_right_overlap) > 1 else ranges_right_overlap[0][1]
        new_gap = [min, new_max]
        for temp in ranges_right_overlap:
            final_ranges.remove(temp)
        final_ranges.append(new_gap)
    else:
        new_min = min([x[0] for x in ranges_left_overlap]) if len(ranges_left_overlap) > 1 else ranges_left_overlap[0][0]
        new_max = max([x[1] for x in ranges_right_overlap]) if len(ranges_right_overlap) > 1 else ranges_right_overlap[0][1]
        new_gap = [new_min, new_max]
        for temp in ranges_right_overlap:
            final_ranges.remove(temp)
        for temp in ranges_left_overlap:
            final_ranges.remove(temp)
        final_ranges.append(new_gap)
    if len(ranges_within_overlap) > 0:
        for temp in ranges_within_overlap:
            final_ranges.remove(temp)
distance = 0
for final_range in final_ranges:
    distance += final_range[1] - final_range[0] + 1
print(distance)