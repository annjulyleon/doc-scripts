import os
import json
import matplotlib.pyplot as plt
import numpy as np
import argparse


def get_classes_from_folders(path):
    list_classes = []
    for f in os.scandir(path):
        if f.is_dir():
            d = {"name": f.name}
            d["amount"] = len(next(os.walk(f))[2])
            d["subclasses"] = []
            for s in os.scandir(f):
                if s.is_dir():
                    d["subclasses"].append({"name":s.name, "amount": len(next(os.walk(s))[2])})
            sub_amount = sum(item['amount'] for item in d['subclasses'])
            d["total"] = sub_amount + d["amount"]
            list_classes.append(d)
    json_classes = {"classes": list_classes}

    return json_classes


def write_to_json(json_classes):
    with open('result.json', 'w', encoding='utf-8') as f:
        json.dump(json_classes, f, ensure_ascii=False, indent=4)


def build_histogram(path, rounded, bins_edges = None):
    try:
        with open(path, encoding='utf-8') as json_file:
            data = json.load(json_file)

    except FileNotFoundError:
        print("File not found in current directory")

    amount_per_class = [i['total'] for i in data['classes']]
    print(f'Ammount of images per class: {amount_per_class}')

    if rounded:
        print(f'Rounded is selected. Getting list of edges from user input')
        calc_bins_rounded = bins_edges
    else:
        print(f'Rounded is not selected, calculate bins automatically')
        calc_bins = np.histogram_bin_edges(amount_per_class, bins='auto').tolist()
        calc_bins_rounded = [round(num, 0) for num in calc_bins]
        print(f'Calculated edges: {calc_bins_rounded}')

    plt.hist(amount_per_class, bins=calc_bins_rounded, edgecolor='black')
    plt.xlabel('Images')
    plt.ylabel('Classes')
    plt.xticks(calc_bins_rounded)
    plt.savefig('histogram.png', bbox_inches='tight')
    plt.show()


if __name__ == '__main__':
    parser = argparse.ArgumentParser(description="Images per class analyzer",
                                     formatter_class=argparse.ArgumentDefaultsHelpFormatter)
    parser.add_argument("-p","--path", type=str, default="_filtred_locations_pics",
                        help="Directory where images are stored or json to build histogram", required=True)
    parser.add_argument("-gc", "--getClasses", action="store_true",
                        help="Creates json in current dir with images per class")
    parser.add_argument("-bh", "--buildHisto", action="store_true",
                        help="Create histogram png in curren dir from json in path")
    parser.add_argument("-ub", "--user_bin", action="store_true",
                        help="Create histogram with user bin edges passed in edges list")
    parser.add_argument("-e", "--edges", type=json.loads, default="[10,20,40,60,80,100,120]",
                        help='Pass list of edges as "[10,20,40,60,80,100,120]"')
    args = vars(parser.parse_args())

    if args["getClasses"]:
        json_classes = get_classes_from_folders(args["path"])
        write_to_json(json_classes)

    if args["buildHisto"]:
        if args["user_bin"]:
            build_histogram(args["path"],1,args["edges"])
        else:
            build_histogram(args["path"], 0)
