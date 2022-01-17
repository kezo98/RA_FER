

SVEUČILIŠTE U ZAGREBU

FAKULTET ELEKTROTEHNIKE I RAČUNARSTVA

\3. laboratorijska vježba

Pong

Zagreb, siječanj 2022.





Sadržaj

[1.](#br3)[ ](#br3)[Uvod................................................................................................................................................1](#br3)

[2.](#br4)[ ](#br4)[Prikaz](#br4)[ ](#br4)[scena.....................................................................................................................................2](#br4)

[3.](#br6)[ ](#br6)[Pokretanje](#br6)[ ](#br6)[igre................................................................................................................................4](#br6)





\1. Uvod

Kao treća laboratorijska vježba implementirana je 2D igra „*Pong*“ u razvojnome okruženju

Unity. Budući da je cilj laboratorijske vježbe implementirati tehniku računalne animacije u

nekome od jezika, u igri su implementirane detekcije sudara te dinamika. Postoje dvije vrste

sudara. Prvi sudar je između loptice i gornjeg/donjeg ruba ekrana, dok je druga vrsta sudara

između loptice i igrača.

U igri se natječu dva igrača te igrač koji prvi dostigne 5 pogodaka postaje pobjednik.

Pobjedom jednoga od igrača završava jedna runda nakon čega igrači mogu ponovno igrati

klikom na gumb „*Play Again*“.

1





\2. Prikaz scena

Prva scena [(Slika](#br4)[ ](#br4)[1)](#br4)[ ](#br4)je ona koja se prikazuje nakon pokretanja igre. Na njoj se nalaze dva

gumba – „*Start*“ koji služi za pokretanje igre te „*Exit*“ kojim se izlazi iz igre.

*Slika 1 Početna scena*

Na drugoj sceni [(Slika](#br4)[ ](#br4)[2)](#br4)[ ](#br4)igrači igraju te se natječu koji od njih će biti bolji te osvojiti titulu

prvaka u igri.

*Slika 2 Glavna scena*

2





Nakon što je jedan od igrača ostvario 5 pogodaka, prikazuje se treća, ujedno i zadnja,

scena [(Slika](#br5)[ ](#br5)[3](#br5)[)](#br5)[ ](#br5)te ukoliko igrači žele ponovni okršaj, potrebno je kliknuti „*Play again*“, inače

izlaze iz igre klikom na „*Exit*“ ili klikom tipke „*ESC*“.

*Slika 3 Završna scena*

3





\3. Pokretanje igre

Unutar direktorija „*Pong*“ nalazi se „*Pong.exe*“ [(Slika](#br6)[ ](#br6)[4)](#br6)[ ](#br6)kojega je potrebno otvoriti. Nakon

toga igra se pokreće klikom na „*Start*“ [(Slika](#br4)[ ](#br4)[1)](#br4).

*Slika 4 Sadržaj „Pong“direktorija*

Prvi igrač se pomiče gore-dolje pritiskom na tipku „*W*“ odnosno „*S*“, dok se drugi igrač

pomiče pritiskom na strjelicu „*Up*“ odnosno „*Down*“.

4

