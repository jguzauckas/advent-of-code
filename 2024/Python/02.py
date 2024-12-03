# Read lines from files
list_inp = []
with open('2024/Inputs/02.txt', 'r') as inp:
    list_inp = inp.readlines()

# Separate each line into individual values
list_line_str = []
for line in list_inp:
    list_line_str.append(line.split(" "))

# Interpret all values as integers
reports = []
for report in list_line_str:
    current_report = []
    for level in report:
        current_report.append(int(level))
    reports.append(current_report)

# Check if report is all increasing or all decreasing
def report_ordered_check(report):
    return report == sorted(report) or report == sorted(report)[::-1]

# Find any and all single indices that can be removed and ordered check is passed
def report_ordered_remove(report):
    potential_removals = []
    for index, elem in enumerate(report):
        report_copy = report.copy()
        report_copy.pop(index)
        if report_ordered_check(report_copy):
            potential_removals.append(index)
    return potential_removals

# Check if report levels have a gap of 1 to 3
def report_levels_gap_check(report):
    for level_1, level_2 in zip(report, report[1:]):
        diff = abs(level_2 - level_1)
        if diff < 1 or diff > 3:
            return False
    return True

# Find any and all single indices that can be removed and levels check is passed
def report_levels_remove(report):
    potential_removals = []
    for index, elem in enumerate(report):
        report_copy = report.copy()
        report_copy.pop(index)
        if report_levels_gap_check(report_copy):
            potential_removals.append(index)
    return potential_removals

# Part 1
count = 0
for report in reports:
    count += (report_ordered_check(report) and report_levels_gap_check(report))
print(count)

# Part 2
count = 0
for report in reports:
    ordered_pass = report_ordered_check(report)
    levels_pass = report_levels_gap_check(report)
    if not (ordered_pass and levels_pass): # If either failed, check for removal options
        ordered_removals = set(report_ordered_remove(report))
        levels_removals = set(report_levels_remove(report))
        if ordered_removals.intersection(levels_removals) != set(): # If any removal options are shared
            ordered_pass = True
            levels_pass = True
    count += ordered_pass and levels_pass
print(count)