import json

base_path = "C:/Users/muell/source/repos/jmueller3311/InsertCreator/InsertCreator/InsertCreator/DataSource/"

with open(base_path + "GB_Data.json", "r", encoding="utf-8") as f:
	data = json.load(f)

with open(base_path + 'International_GD.csv', 'r', encoding="utf-8") as f:
	lines = f.readlines()


for l in lines:
	parts = l.split(",")
	number = (parts[0] + parts[1]).strip()
	EN = parts[2].strip()
	WA = parts[3].strip()
	RU = parts[4].strip()
	UA = parts[5].strip()

	for d in data:
		if d["Number"] == number:
			if EN != "":
				d["Metadata"] += [{"Key": "EN", "Value": EN}]
			if RU != "":
				d["Metadata"] += [{"Key": "RU", "Value": RU}]
			if WA != "":
				d["Metadata"] += [{"Key": "WA", "Value": WA}]
			if UA != "":
				d["Metadata"] += [{"Key": "UA", "Value": UA}]
			break

with open(base_path + "GB_Data.json", "w", encoding="utf-8") as f:
	json.dump(data, f)