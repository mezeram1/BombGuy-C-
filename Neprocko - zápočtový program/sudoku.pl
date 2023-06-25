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
solve(Sudoku,Solution,Size) :- 
    sudokuFormat(Sudoku,Size),
    contains_valid_numbers(Sudoku,Size),
    all_rows_different(Sudoku),
    all_columns_different(Sudoku),
    all_blocks_different(Sudoku,Size),
    maplist(label,Sudoku),
    Solution = Sudoku.

% check if sudoku contains only numbers between 1 and 9
contains_valid_numbers(Sudoku,Size) :- integer(Size),flatten(Sudoku, L),L ins 1..Size.

% check if sudoku has valid format
sudokuFormat([FirstRow|Rows],Size) :- length(FirstRow,Size),length([FirstRow|Rows],Size).

% true if all rows contain different numbers
all_rows_different(Sudoku) :- maplist(all_distinct, Sudoku).
    
% true if all columns contain different numbers
all_columns_different(Sudoku) :- 
    transpose(Sudoku, Transpose),
    all_rows_different(Transpose).

% true if all sudoku blocks contains different numbers
all_blocks_different(Sudoku,Size) :-
    K is round(sqrt(Size)),    
    get_blocks(Sudoku,K,Blocks),
    all_rows_different(Blocks).

% returns first K elements of list and rest of it
get_first_K(Row,0,[],Row) :- !.
get_first_K([H|Row],K,[H|KRow],Rest) :-
    L is K-1,
    get_first_K(Row,L,KRow,Rest).

% takes K rows and return first block of them
get_block([],_,[],[]) :- !.
get_block([Row|Rows],K,Bl,[]) :-
    length(Row,K),
    get_first_K(Row,K,B,[]),
    get_block(Rows,K,Block,[]),
    append(B,Block,Bl),!.

get_block([Row|Rows],K,Bl,[R|Rest]) :-
    get_first_K(Row,K,B,R),
    get_block(Rows,K,Block,Rest),
    append(B,Block,Bl).

% takes K rows of length K*K and returns K blocks from them
get_blocks_from_Rows([],_,[]) :- !.
get_blocks_from_Rows(Rows,K,[Block|Blocks]) :-
    get_block(Rows,K,Block,Rest),
    get_blocks_from_Rows(Rest,K,Blocks).

% returns first K rows of sudoku and rest of it
get_K_rows(Sudoku,0,[],Sudoku) :- !.
get_K_rows([Row|Sudoku],K,[Row|KRows],Rest) :-
    L is K-1,
    get_K_rows(Sudoku,L,KRows,Rest).

% returns list of sudoku blocks
get_blocks([],_,[]) :- !.
get_blocks(Sudoku,K,Blocks) :- 
    get_K_rows(Sudoku,K,KRows,Rest),
    get_blocks_from_Rows(KRows,K,Blocks1),
    get_blocks(Rest,K,Blocks2),
    append(Blocks1,Blocks2,Blocks). 