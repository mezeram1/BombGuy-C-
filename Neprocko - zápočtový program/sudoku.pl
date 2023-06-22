/**
 * Zápočtový program SUDOKU solver
 * Předmět: Neprocedurální programování
 * Kód předmětu: NPRG005
 * Student: Matěj Mezera 
 **/
:- use_module(library(clpfd)).
/**
 * main function that solves sudoku
 * INPUT: sudoku in matrix format (array of arrays)
 *        and alphabeth used in sudoku (array of symbols)
 *        (in real world we mostly use alphabeth = [1,2,...,8,9])
 * OUTPUT: single solution of sudoku in matrix format
 **/
solve(Sudoku,Solution) :- 
    sudokuFormat(Sudoku),
    contains_valid_numbers(Sudoku),
    all_rows_different(Sudoku),
    all_columns_different(Sudoku),
    all_blocks_different(Sudoku),
    maplist(label,Sudoku),
    Solution = Sudoku.

% check if sudoku contains only numbers between 1 and 9
contains_valid_numbers(Sudoku) :- flatten(Sudoku, L),L ins 1..9.

% check if sudoku has valid format
sudokuFormat([FirstRow|Rows]) :- length(FirstRow,9),length([FirstRow|Rows],9).

% true if all rows contain different numbers
all_rows_different(Sudoku) :- maplist(all_distinct, Sudoku).
    
% true if all columns contain different numbers
all_columns_different(Sudoku) :- 
    transpose(Sudoku, Transpose),
    all_rows_different(Transpose).

% true if all sudoku blocks contains different numbers
all_blocks_different(Sudoku) :- 
    get_blocks(Sudoku,Blocks),
    all_rows_different(Blocks).

% returns list of sudoku blocks
get_blocks([A1,A2,A3,A4,A5,A6,A7,A8,A9],Blocks) :- 
    get_blocks(A1,A2,A3,B1),
    get_blocks(A4,A5,A6,B2),
    get_blocks(A7,A8,A9,B3),
    append([B1,B2,B3],Blocks).

% takes 3 rows and cut them into 3 blocks
get_blocks([], [], [], []).
get_blocks([A1,A2,A3|B1],[A4,A5,A6|B2],[A7,A8,A9|B3], [Block|Blocks]) :-
    Block = [A1,A2,A3,A4,A5,A6,A7,A8,A9],
    get_blocks(B1, B2, B3, Blocks).