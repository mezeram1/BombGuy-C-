# ***Zápočtový program SUDOKU solver - dokumentace***
## **Předmět: Neprocedurální programování**
## **Kód předmětu: NPRG005**
## **Student: Matěj Mezera**
### **Popis programu**
Program slouží jako řešič sudoku 9x9. Program umí nalézt všechna řešení pro dané sudoku a pro konkrétní dvojici sudoku řešení odpoví, zda řešení je řešením sudoku, které jsme zadali.
### **Predikáty**
Hlavním preikátem je predikát solve(+Sudoku,?Solution), který jako první argument bere sudoku a jako druhý argument jeho řešení. Jak sudoku, tak jeho řešení je ve formátu pole polí a při zadávání sudoku se pro prázdné pole použije symbol '_'. Predikát je splněný, pokud Solution je řešením Sudoku. Predikát pro dané Sudoku nalezne všechna jeho řešení. Predikát solve používá další pomocné predikáty:
- sudokuFormat(+Sudoku) - je splněný, pokud je Sudoku ve formátu pole devíti polí o velikosti devět
- contains_valid_numbers(+Sudoku) - je splněný, pokud každé políčko obsahuje číslo mezi 1 a 9
- all_rows_different(+Sudoku) - je splněný, pokud v každém řádku jsou různá čísla
- all_columns_different(+Sudoku) - je splněný, pokud v každém sloupci jsou různá čísla
- all_blocks_different(+Blocks) - je splněný, pokud v každém čtverci 3x3 ve kterém mají být podle pravidel sudoku různá čísla, jsou různá čísla
- get_blocks(+Sudoku,-Blocks) - každý block převede na pole a vrátí všechny bloky ve formátu pole polí
### **Rozšíření**
Program řeší pouze sudoku 9x9 v číselném formátu a proto by bylo možné a vhodné ho rozšířit.
Možná rozšíření:
- Řešení libovolně velkého sudoku (4x4, 16x16, ...)
- Místo čísel používat speciální abecedu, kterou si uživatel nadefinuje jako třetí predikát. Bylo by tedy možné řešit sudoku, kde místo čísel jsou v polích zvířata atd.