#!/usr/bin/env python3

"""Final task templater"""

import argparse
import io
import os
import sys
import re

import jinja2
import yaml
import roman

US_GALLONS = False
EXTRA_VARIABLES = {}

class ExtraVariableAction(argparse.Action):
    """Argparse action class for adding extra variables"""
    def __init__(self, option_strings, dest, nargs=None, **kwargs):
        if nargs is not None:
            raise ValueError("nargs not allowed")
        super().__init__(option_strings, dest, **kwargs)
    def __call__(self, parser, namespace, values, option_string=None):
        global EXTRA_VARIABLES
        key = values.split("=")[0]
        value = values.split("=")[1]
        EXTRA_VARIABLES[key]=value

def jinja_filter_liters_to_gallons(text):
    """funtion for converting liters to gallons"""
    global US_GALLONS
    if US_GALLONS:
        return float(text) * 0.264172
    return float(text) * 0.2199692

def jinja_filter_arabic_to_roman(arabic_number):
    """funtion for converting arabic numbers to roman numbers"""
    error_input = arabic_number
    try:
        arabic_number = int(arabic_number)
    except ValueError:
        arabic_number = -1
    if arabic_number > 0:
        return roman.toRoman(int(arabic_number))
    sys.stderr.write("Warning: arabic2roman: unable to convert %s." % error_input)
    return "NaN"

def get_jinja_environment(template_dir):
    """Function that prepares data for templating"""
    env = jinja2.Environment(loader=jinja2.FileSystemLoader(template_dir),
                             autoescape=jinja2.select_autoescape(['html', 'xml']),
                             extensions=['jinja2.ext.do'])
    env.filters['l2gal'] = jinja_filter_liters_to_gallons
    env.filters['arabic2roman'] = jinja_filter_arabic_to_roman
    return env

def load_yaml_header(data_yaml):
    """Function for loading yaml header to dict"""
    data = yaml.load(data_yaml, Loader=yaml.FullLoader)
    return data

def templater(argv):
    """Main function that does almost everything"""
    global US_GALLONS
    args = argparse.ArgumentParser(description='Templater')
    args.add_argument(
        '--template',
        dest='template',
        required=True,
        metavar='FILENAME.j2',
        help='Jinja2 template file')
    args.add_argument(
        '--input',
        dest='input',
        required=True,
        metavar='INPUT',
        help='Input filename')
    args.add_argument(
        '--use-us-gallons',
        action='store_true',
        dest='us_gallons',
        help='Option for switching into us gallons.')
    args.add_argument(
        '-V',
        action=ExtraVariableAction,
        dest='us_gallons',
        help='Option for switching into us gallons.')
    config = args.parse_args(argv)
    US_GALLONS = config.us_gallons
    save_yaml = False
    yaml_header = ""
    content = ""
    with open(config.input, 'r') as file:
        for line in file:
            if save_yaml and line != "---\n":
                yaml_header += line
            if line == "---\n":
                save_yaml = not save_yaml
            if not save_yaml and line != "---\n":
                content += line
    yaml_dir = load_yaml_header(yaml_header)
    env = get_jinja_environment(os.path.dirname(config.template))
    template = env.get_template(config.template)

    variables = {
        'content': content,
        'TEMPLATE': config.template,
        'INPUT': config.input,
    }

    variables = {**variables, **yaml_dir, **EXTRA_VARIABLES}
    # Use \n even on Windows
    sys.stdout = io.TextIOWrapper(sys.stdout.buffer, newline='\n')

    result = template.render(variables)
    if US_GALLONS:
        result = re.sub("imperial", "us", result)
    print(result)
