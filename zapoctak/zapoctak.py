"""Pawn Game
Matěj Mezera I. ročník, kruh 33
zimní semestr 2021/2022
Programování 1 NPRG030""" 
class pesec:    #třída pěšec
    def __init__(self, pozice,barva):
        self.jmeno = barva
        self.radek = pozice[0]
        self.poc_radek = self.radek
        self.sloupec = pozice[1]
        sachovnice[self.radek][self.sloupec] = self.jmeno
        if self.jmeno == "W":
            self.vyherni = self.radek + 6
        else:
            self.vyherni = self.radek - 6
    def kam_tahnu(self):    #určí všechny možné tahy daného pěšce a uloží je do seznamu
        global skok_o_dva, sachovnice
        if sachovnice[self.radek][self.sloupec] == self.jmeno:
            if self.radek != self.vyherni:
                tahy = []
                if self.jmeno == "W":
                    x = 1
                else:
                    x = -1
                #vpřed o dva
                if self.radek == self.poc_radek and sachovnice[self.radek + x][self.sloupec] == 0 and sachovnice[self.radek + 2*x][self.sloupec] == 0:
                    tahy.append([self.radek + 2*x, self.sloupec])
                #vpřed o jedna
                if sachovnice[self.radek + x][self.sloupec] == 0:
                    tahy.append([self.radek + x,self.sloupec])
                #vezmi šikmo doprava
                if sachovnice[self.radek + x][self.sloupec + 1] != 0 and sachovnice[self.radek + x][self.sloupec + 1] != self.jmeno and sachovnice[self.radek + x][self.sloupec + 1] != "X":
                    tahy.append([self.radek + x,self.sloupec + 1])
                #vezmi šikmo doleva
                if sachovnice[self.radek + x][self.sloupec - 1] != 0 and sachovnice[self.radek + x][self.sloupec - 1] != self.jmeno and sachovnice[self.radek + x][self.sloupec - 1] != "X":
                    tahy.append([self.radek + x,self.sloupec - 1])
                #braní mimochodem
                if self.radek - 8*x == skok_o_dva[1]:
                    if self.sloupec + 1 == skok_o_dva[2] or self.sloupec - 1 == skok_o_dva[2]:
                        tahy.append([self.radek + x,skok_o_dva[2]])
                return tahy
            else:
                return []
        else:
            return []
    def tahni(self,kam):    #zahraje daný tah
        global skok_o_dva, pocet_bilych, pocet_cernych, ber, Vyhra, Remiza
        Lze = False
        if sachovnice[self.radek][self.sloupec] == self.jmeno:
            if self.jmeno == "W":
                x = 1
            else:
                x = -1
            radek = kam[0]
            sloupec = kam[1]
            if sloupec == self.sloupec:
                #vpřed o jedna
                if radek == self.radek + x  and sachovnice[radek][sloupec] == 0:
                    Lze = True
                    skok_o_dva = (False,None,None)
                    ber = False
                #vpřed o 2
                if radek == self.radek + 2*x and self.radek == self.poc_radek and sachovnice[radek][sloupec] == 0 and sachovnice[radek - x][sloupec] == 0:
                    Lze = True
                    skok_o_dva = (True,radek,sloupec)
                    ber = False  
            elif abs(sloupec - self.sloupec) == 1:
                if radek == self.radek + x and sachovnice[radek][sloupec] != self.jmeno and sachovnice[radek][sloupec] != "X":
                    if self.radek - 8*x == skok_o_dva[1] and sloupec == skok_o_dva[2]:
                        #braní mimochodem
                        sachovnice[radek - x][sloupec] = 0
                        Lze = True
                        skok_o_dva = (False,None,None)
                        if x > 0:
                            pocet_cernych -= 1
                        else:
                            pocet_bilych -= 1
                        ber = True
                    elif sachovnice[radek][sloupec] != 0:
                        #braní
                        Lze = True
                        skok_o_dva = (False,None,None)
                        if x > 0:
                            pocet_cernych -= 1
                        else:
                            pocet_bilych -= 1
                        ber = True
                else:
                    ber = False
        if Lze: #zahraje daný tah
            sachovnice[radek][sloupec] = self.jmeno
            sachovnice[self.radek][self.sloupec] = 0
            self.radek = radek
            self.sloupec = sloupec
            if self.radek == self.vyherni:
                Vyhra = (True,self.jmeno)
            elif pocet_bilych == 0:
                Vyhra = (True,"B")
            elif pocet_cernych == 0:
                Vyhra = (True,"W")
            p = 0
            #kontrola, jestli nenastala remíza
            if self.jmeno == "W":
                for pesec in cerny:
                    if pesec.kam_tahnu() == []:
                        p += 1
            else:
                for pesec in bily:
                    if pesec.kam_tahnu() == []:
                        p += 1
            if p == 8 and not Vyhra[0]:
                Remiza = True
        else:
            print("Na toto políčko vaše figurka táhnout nesmí!")
def priprav_hru():  #Uvede šachovnici a všechny důležité proměnné do počátečního stavu
    global sachovnice, ber, bily, cerny, Vyhra, Remiza, skok_o_dva, pocet_bilych, pocet_cernych, ulozit
    sachovnice = [["X",0,0,0,0,0,0,0,0,"X"],
            ["X",0,0,0,0,0,0,0,0,"X"],
            ["X",0,0,0,0,0,0,0,0,"X"],
            ["X",0,0,0,0,0,0,0,0,"X"],
            ["X",0,0,0,0,0,0,0,0,"X"],
            ["X",0,0,0,0,0,0,0,0,"X"],
            ["X",0,0,0,0,0,0,0,0,"X"],
            ["X",0,0,0,0,0,0,0,0,"X"]]
    ber = False
    Vyhra = (False,None)
    Remiza = False
    skok_o_dva = (False,None,None)
    bily = [0]*8
    cerny = [0]*8
    ulozit = []
    bily[0] = pesec([1,1],"W")
    bily[1] = pesec([1,2],"W")
    bily[2] = pesec([1,3],"W")
    bily[3] = pesec([1,4],"W")
    bily[4] = pesec([1,5],"W")
    bily[5] = pesec([1,6],"W")
    bily[6] = pesec([1,7],"W")
    bily[7] = pesec([1,8],"W")
    cerny[0] = pesec([-2,1],"B")
    cerny[1] = pesec([-2,2],"B")
    cerny[2] = pesec([-2,3],"B")
    cerny[3] = pesec([-2,4],"B")
    cerny[4] = pesec([-2,5],"B")
    cerny[5] = pesec([-2,6],"B")
    cerny[6] = pesec([-2,7],"B")
    cerny[7] = pesec([-2,8],"B")
    pocet_bilych = 8
    pocet_cernych = 8
def ukaz_sachovnici():  #funkce, která na výstupu ukáže stav šachovnice
    global sachovnice
    print("")
    for i in range(1,9):
        print(i,"|",*sachovnice[i-1][1:9])       
    print("    _______________")
    print("    A B C D E F G H")
    print("")
def zkus_tah(barva,hloubka = 3):   #Minimaxový algoritmus, který zkusí všechny do určité hlloubky a vrátí ten nejlepší
    global skok_o_dva,ber,pocet_bilych,pocet_cernych,Vyhra,Remiza,ber,sachovnice
    vracej = [None,None]
    if barva == "W":
        if not Vyhra[0] and not Remiza:
            for pesec in bily:
                radek = pesec.radek
                sloupec = pesec.sloupec
                for tah in pesec.kam_tahnu():
                    skok = skok_o_dva
                    pole = sachovnice[tah[0]][tah[1]]
                    vyhr = Vyhra
                    rem = Remiza
                    pesec.tahni(tah)
                    beru = ber
                    if hloubka == 0:
                        vrat = ohodnot_pozici("B")
                    else:
                        vrat = zkus_tah("B",hloubka - 1)[0]
                    if vracej[0] == None or vrat > vracej[0]:
                        vracej[0] = vrat
                        vracej[1] = tah
                    sachovnice[pesec.radek][pesec.sloupec] = pole
                    sachovnice[radek][sloupec] = "W"
                    pesec.radek = radek
                    pesec.sloupec = sloupec
                    Vyhra = vyhr
                    Remiza = rem
                    if skok[0]:
                        sachovnice[skok[1]][skok[2]] = "B"
                    if beru:
                        pocet_cernych += 1
                    ber = beru
                    skok_o_dva = skok
    else:
        if not Vyhra[0] and not Remiza:
            for pesec in cerny:
                radek = pesec.radek
                sloupec = pesec.sloupec
                for tah in pesec.kam_tahnu():
                    skok = skok_o_dva
                    pole = sachovnice[tah[0]][tah[1]]
                    vyhr = Vyhra
                    rem = Remiza
                    pesec.tahni(tah)
                    beru = ber
                    if hloubka == 0:
                        vrat = ohodnot_pozici("W")
                    else:
                        vrat = zkus_tah("W",hloubka - 1)[0]
                    if vracej[0] == None or vrat < vracej[0]:
                        vracej[0] = vrat
                        vracej[1] = tah
                    sachovnice[pesec.radek][pesec.sloupec] = pole
                    sachovnice[radek][sloupec] = "B"
                    pesec.radek = radek
                    pesec.sloupec = sloupec
                    Vyhra = vyhr
                    Remiza = rem
                    if skok[0]:
                        sachovnice[skok[1]][skok[2]] = "W"
                    if beru:
                        pocet_bilych += 1
                    ber = beru
                    skok_o_dva = skok
    if Vyhra[0]:
        if Vyhra[1] == "W":
            return [10**10,None]
        else:
            return [-(10**10),None]
    elif Remiza:
        return [0,None]
    else:
        return vracej
def uzivatel_tahni(jmeno_hrace,barva): #Funkce, která od uživatele zjistí kam chce hrát a tento tah zahraje 
    global bily,cerny,ulozit
    proved = True
    while proved:
        print("Hraje",jmeno_hrace)
        odkud = input("Zadejte z jakého pole na jaké chcete hrát (př.: E4 E5): ").upper()
        pozice = odkud.split()
        try:
            if len(pozice[1]) != 2 or len(pozice[1]) != 2:
                raise NameError
            if barva == "W":
                kam = [int(pozice[1][1]) - 1,ord(pozice[1][0]) - 64]
                policko = [int(pozice[0][1]) - 1,ord(pozice[0][0]) - 64]
                fig = None
                for pesec in bily:
                    if policko[0] == pesec.radek and policko[1] == pesec.sloupec and sachovnice[policko[0]][policko[1]] == pesec.jmeno:
                        fig = pesec
                        break
            else:
                kam = [int(pozice[1][1]) - 9,ord(pozice[1][0]) - 64]
                policko = [int(pozice[0][1]) - 9,ord(pozice[0][0]) - 64]
                fig = None
                for pesec in cerny:
                    if policko[0] == pesec.radek and policko[1] == pesec.sloupec and sachovnice[policko[0]][policko[1]] == pesec.jmeno:
                        fig = pesec
                        break
            if fig != None:
                poc_sloupec = chr(pesec.sloupec + 64)
                poc_radek = pesec.radek
                fig.tahni(kam)
                if fig.radek != policko[0] or fig.sloupec != policko[1]:
                    proved = False
                    if barva == "W":
                        poc_radek += 1
                        kon_sloupec = chr(kam[1] + 64)
                        kon_radek = kam[0] + 1
                    else:
                        poc_radek += 9
                        kon_sloupec = chr(kam[1] + 64)
                        kon_radek = kam[0] + 9
                    ulozit.append(str(poc_sloupec) + str(poc_radek) + " " + str(kon_sloupec) + str(kon_radek))                   
            else:
                print("Na výchozím políčku nestojí vaše figurka! Zkuste tah zadat znovu.")
        except:
            print("Chybný vstup! Dodržte formát viz. příklad!!!")
def ohodnot_pozici(na_tahu): #Statická funkce minimaxu, která ohodnotí pozici na šachovnici
    global Vyhra, Remiza, bily, cerny, pocet_bilych, pocet_cernych
    if Vyhra[0]:
        if Vyhra[1] == "W":
            return 10**10
        else:
            return -(10**10)
    if Remiza:
        return 0
    bily_skore = 0
    cerny_skore = 0
    if na_tahu == "W":
        bily_skore += 3
    else:
        cerny_skore -= 3
    for pesec in bily:
        pridej = True
        p = 0
        if sachovnice[pesec.radek][pesec.sloupec] == pesec.jmeno:
            for i in range(pesec.radek + 1,pesec.vyherni):
                if sachovnice[i][pesec.sloupec] == "B":
                    pridej = False
                    break
            if pridej:
                for i in range(pesec.radek + 1,pesec.vyherni + 1):
                    if sachovnice[i][pesec.sloupec + 1] == "B":
                        p -= 1
                        break
                p += 1
                for i in range(pesec.radek + 1,pesec.vyherni + 1):
                    if sachovnice[i][pesec.sloupec - 1] == "B":
                        p -= 1
                        break
                p += 1
            if p == 2:
                bily_skore += (pesec.radek)**5
            else:
                bily_skore += (p+1)*(pesec.radek)
    for pesec in cerny:
        pridej = True
        p = 0
        if sachovnice[pesec.radek][pesec.sloupec] == pesec.jmeno:
            for i in range(pesec.radek - 1,pesec.vyherni - 1,-1):
                if sachovnice[i][pesec.sloupec] == "W":
                    pridej = False
                    break
            if pridej:
                for i in range(pesec.radek - 1,pesec.vyherni - 1,-1):
                    if sachovnice[i][pesec.sloupec + 1] == "W":
                        p -= 1
                        break
                p += 1
                for i in range(pesec.radek - 1,pesec.vyherni,-1):
                    if sachovnice[i][pesec.sloupec - 1] == "W":
                        p -= 1
                        pridej = False
                        break
                p += 1
            if p == 2:
                cerny_skore += (pesec.radek + 1)**5
            else:
                cerny_skore += (p+1)*(pesec.radek + 1)
    return (bily_skore + cerny_skore + 3*(pocet_bilych - pocet_cernych)) + 0.5
def uzivatel_vs_uzivatel(): #Rozhraní pro hru uživatele proti uživateli
    bily_jmeno = input("Zadejte přezdívku bílému hráči: ")
    cerny_jmeno = input("Zadejte přezdívku černému hráči: ")
    cislo = 1
    ukaz_sachovnici()
    while not Remiza and not Vyhra[0]:
        if cislo == 1:
            uzivatel_tahni(bily_jmeno,"W")
        else:
            uzivatel_tahni(cerny_jmeno,"B")
        ukaz_sachovnici()
        cislo *= -1
    print("KONEC HRY")
    if Vyhra[0]:
        if Vyhra[1] == "W":
            print(bily_jmeno,"vyhrál, gratulujeme.",cerny_jmeno,"nezoufej, příště ho porazíš.")
        else:
            print(cerny_jmeno,"vyhrál, gratulujeme.",bily_jmeno,"nezoufej, příště ho porazíš.")
    else:
        print("REMÍZA")
    return [bily_jmeno,cerny_jmeno]
def pocitac_tahni(barva,tah): #Funkce, která zahraje "nejlepší" tah počítače
    global ulozit
    print("Hraje počítač")
    delej = True
    i = 0
    if barva == "W":
        while delej:
            pesec = bily[i]
            for tahni in pesec.kam_tahnu():
                if tah == tahni:
                    poc_sloupec = chr(pesec.sloupec + 64)
                    poc_radek = pesec.radek + 1
                    pesec.tahni(tah)
                    kon_sloupec = chr(tah[1] + 64)
                    kon_radek = tah[0] + 1
                    delej = False
                    break
            i += 1
    else:
        while delej:
            pesec = cerny[i]
            for tahni in pesec.kam_tahnu():
                if tah == tahni:
                    poc_sloupec = chr(pesec.sloupec + 64)
                    poc_radek = pesec.radek + 9
                    pesec.tahni(tah)
                    kon_sloupec = chr(tah[1] + 64)
                    kon_radek = tah[0] + 9
                    delej = False
                    break
            i += 1
    print(poc_sloupec,poc_radek," ",kon_sloupec,kon_radek,sep = "")
    ulozit.append(str(poc_sloupec) + str(poc_radek) + " " + str(kon_sloupec) + str(kon_radek))
def uzivatel_vs_pocitac(): #Rozhraní pro hru uživatele proti počítači
    uroven = input("Zvolte si úroveň počítače (snadný/obtížný/velmi obtížný): ").upper()
    delej = True
    while delej:
        if uroven == "SNADNÝ":
            uroven_pocitace = 1
            delej = False
        elif uroven == "OBTÍŽNÝ":
            uroven_pocitace = 2
            delej = False
        elif uroven == "VELMI OBTÍŽNÝ":
            uroven_pocitace = 3
            delej = False
        else:
            uroven = input("Zadejte snadný, obtížný nebo velmi obtížný: ").upper()
    barva_hrace = input("Zvolte si barvu - W(white)/ B(black): ").upper()
    while barva_hrace != "W" and barva_hrace != "B":
        print("Chybný vstup. Zadejte W nebo B")
        barva_hrace = input("Zvolte si barvu - W(white)/ B(black): ").upper()
    prezdivka = input("Zvolte si přezdívku: ")
    if barva_hrace == "W":
        barva_pocitace = "B"
        cislo = 1
        print(prezdivka,", hrajete za bílé.",sep = "")
    else:
        barva_pocitace = "W"
        cislo = -1
        print(prezdivka,", hrajete za černé.",sep = "")
    ukaz_sachovnici()
    while not Vyhra[0] and not Remiza:
        if cislo == 1:
            uzivatel_tahni(prezdivka,barva_hrace)
        elif cislo == -1:
            pocitac_tahni(barva_pocitace,zkus_tah(barva_pocitace,uroven_pocitace)[1])
        ukaz_sachovnici()
        cislo *= -1
    print("KONEC HRY")
    if Vyhra[0]:
        if Vyhra[1] == barva_hrace:
            print(prezdivka,"vyhrál jste, gratulujeme.")
        else:
            print("Vyhrává počítač. Nezoufej ",prezdivka,", příště ho porazíš.",sep = "")
    else:
        print("REMÍZA")
    if barva_hrace == "W":
        return [prezdivka,"počítač " + str(uroven)]
    else:
        return ["počítač " + str(uroven),prezdivka]
def uloz_hru(bily_barva,cerny_barva): #Funkce, ktrerá uloží poslední hru do souboru "Posledni_hra.txt"
    global ulozit,Vyhra,Remiza
    ukladej = open("Posledni_hra.txt","w")
    if len(ulozit) % 2 == 1:
        ulozit.append(" ")
    print("Bílý -",bily_barva,file = ukladej)
    print("Černý -",cerny_barva,file = ukladej)
    for i in range(len(ulozit)//2):
        print(i+1,") ",ulozit[2*i]," | ",ulozit[2*i + 1],sep = "",file = ukladej)
    if Vyhra[0]:
        if Vyhra[1] == "W":
            print("1-0",file = ukladej)
        else:
            print("0-1",file = ukladej)
    if Remiza:
        print("0,5-0,5",file = ukladej)
    print("Tato hra byla uložena do souboru: Posledni_hra.txt")
#Začátek hry
print("AHOJ, POJĎME SI ZAHRÁT PAWN GAME")
#Herní menu
chci = input("Chcete hrát? ")
CHCI = True
hral = False
while CHCI:
    if chci.upper() == "ANO":
        priprav_hru()       
        hra = input("Chceteli hrát proti počítači zadejte: PC\nChceteli hrát proti druhému hráči zadejte: PL_PL\nZadejte vaši volbu: ")
        delej = True
        while delej:
            delej = False
            if hra.upper() == "PL_PL":
                barvy = uzivatel_vs_uzivatel()
            elif hra.upper() == "PC":
                barvy = uzivatel_vs_pocitac()
            else:
                delej = True
                hra = input("Zadejte Pl_Pl nebo PC: ")
        uloz = input("Chcete uložit tuto hru? ").upper()
        while uloz != "ANO" and uloz != "NE":
            uloz = input("Zadejte ANO nebo NE: ").upper()
        if uloz == "ANO":
            save = input("Uložením hry přepíšete poslední uloženou hru. Opravdu chcete hru uložit? ").upper()
            while save != "ANO" and save != "NE":
                save = input("Zadejte ANO nebo NE: ").upper() 
            if save == "ANO":
                uloz_hru(barvy[0],barvy[1])
        hral = True
        chci = input("Chcete hrát znovu? ")
    elif chci.upper() == "NE":
        if hral:
            print("Děkuji za hru. Zase si někdy přijďte zahrát.")
        else:
            print("Nevadí, třeba si zahrajete jindy.")
        CHCI = False
    else:
        chci = input("Zadejte ANO nebo NE: ")