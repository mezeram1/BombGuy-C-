# ***Zápočtový program SUDOKU solver - dokumentace***
## **Předmět: Neprocedurální programování**
## **Kód předmětu: NPRG005**
## **Student: Matěj Mezera**
### **Popis programu**
Program slouží jako řešič sudoku. Program umí nalézt všechna řešení pro dané sudoku a pro konkrétní dvojici sudoku řešení odpoví, zda řešení je řešením sudoku, které jsme zadali.
### **Spuštění programu**
Program lze pustit v libovolném prostředí pro Prolog (např SWI-Prolog). Program lze spustit z terminálu příkazem ``swipl $PATH``, kde $PATH je cesta k souboru s programem. Poté stačí zadat predikát s argumenty, tedy solve(+Sudoku,?Solution,+Size).
### **Predikáty**
Hlavním predikátem je predikát solve(+Sudoku,?Solution,+Size), který jako první argument bere sudoku, jako druhý argument jeho řešení a jako třetí argument velikost sudoku (např pro sudoku 9x9 je Size 9). Jak sudoku, tak jeho řešení je ve formátu pole polí a při zadávání sudoku se pro prázdné pole použije symbol '_'. Predikát je splněný, pokud Solution je řešením Sudoku. Predikát pro dané Sudoku nalezne všechna jeho řešení. Predikát solve používá další pomocné predikáty:
- sudokuFormat(+Sudoku,+Size) - je splněný, pokud je Sudoku ve formátu pole polí o velikosti Size x Size
- contains_valid_numbers(+Sudoku,+Size) - je splněný, pokud každé políčko obsahuje číslo mezi 1 a Size
- all_rows_different(+Sudoku) - je splněný, pokud v každém řádku jsou různá čísla
- all_columns_different(+Sudoku) - je splněný, pokud v každém sloupci jsou různá čísla
- all_blocks_different(+Sudoku,+Size) - je splněný, pokud v každém bloku ve kterém mají být podle pravidel sudoku různá čísla, jsou různá čísla
- get_blocks(+Sudoku,+K,-Blocks) - každý block převede na pole a vrátí všechny bloky (velikosti K x K) ve formátu pole polí
- get_first_K(+Row,+K,-FirstK,-Rest) - seznam Row roztrhne na dva seznamy FirstK délky K a Rest, což je zbytek seznamu
- get_blocks_from_Rows(+Rows,+K,-Blocks) - z K řádků vyrobí K bloků velikosti K x K a ty vrátí ve formátu pole polí, kde každé pole představuje jeden block
- get_K_rows(+Sudoku,+K,-KRows,-Rest) - matici sudoku roztrhne na matici KRows obsahující prvních K řádků a zbytek Rest
- get_block(Rows,K,Block,Rest) - z K řádků vrátí první blok Block a zbytek řádků Rest
### **Rozšíření**
Program řeší pouze sudoku v číselném formátu a proto by bylo možné a vhodné ho rozšířit tak, aby bylo možné místo čísel používat speciální abecedu, kterou si uživatel nadefinuje jako čtvrtý argument. Bylo by tedy možné řešit sudoku, kde místo čísel jsou v polích zvířata atd.