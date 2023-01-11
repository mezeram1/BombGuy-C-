#!/usr/bin/env python3

import sys
from .templater import templater

def main():
    templater(sys.argv[1:])

if __name__ == '__main__':
    main(sys.argv[1:])
