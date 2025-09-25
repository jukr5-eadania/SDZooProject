General notes:
Gerne Kig på UI. Det er ikke så visuelt pænt. Takker :D
Alt er sat op i en core/Content struktur, hvorefter det er sorteret op i diverse minigames som er.
Alle relevante filer for MMD burde ligge tilgængelige under content mappen.



General Minigame UI:
Der er lavet et generelt UI til alle minigames så de er ensartet. De burde allerede ligge i scenerne der skal bruge dem. i inspectoren på dem er den vigtige fil det scriptable object som er tillagt under data. Her ændres billedet af det klistermærke spilleren vinder når de gennemfører minigamet, samt den introtekst der er til minigamet.
For at ændre de visuelle dele, bruges Minigame_UI_Document. Hvis noget ændres i den fil ændres det på tværs af alle minigames. bemærk at navne på diverse elementer som allerede eksistere i documentet skal gerne forblive så koden kan genkende dem og gøre de nødvendige handlinger.

Main Menu:
Hovedmenuen består af nogle ikoner i en scroll-menu. Alle minigames, samt en quiz har allerede et ikon. Ikonernes billeder kan ændres i deres "Image" komponent under "Source Image".
For at tilføje flere quizzer, skal der først laves en "AnimalData" fil (beskrevet i ReadMe Quiz dokumentet). Derefter skal der tilføjes et nyt ikon til listen.
Inde i Assets -> Content -> MainMenu er der en prefab der hedder "Ikon", dette prefab skal tilføjes under Canvas -> ScrollMenu -> View -> Content, sammen med de andre ikoner.
Disse ikoner har to vigtige komponenter, "Win Checker" og "Click". Win Checker har et "Trophy Name" field, den bruges til at gemme at man har klaret et quiz/minigame, den skal have samme navn som "Animal Name" i jeres animal data. SaveSO burde allerede være sat og kan derfor ignoreres.
I click er der "Scene Name" og "Animal Data", scene name bruges ikke til quizzer og skal derfor forblive tom. Animal data skal gives dataet for den quiz i har lavet.



Hawk Minigame:
Dette spil består af en ørn og nogle fisk. Ørnens sprite kan ændres under "Hawk"s spriterenderer. Fiskenes sprites kan ændres i JumpingFish prefab'et. De har et komponent der hedder "Fish Logic", herunder en liste af sprites.
Denne liste kan gives ligeså mange sprites som der ønskes, og hver fisk får en tilfældig sprite når den bliver lavet.
I "GameManager"'en kan der ændres på hvor ofte fisk kommer flyvende under "Time Between Fish", og hvor mange fisk der skal fanges i "Fish Goal".

Bear Minigame:
Hidden Object Spil

Om objekterne:
Der er to typer af objekter, dem du kan klikkes på (kaldet ClickableObjects) og dem du skal finde (kaldet HiddenObjects).

ClickableObjects er en base Prefab der bruges til at lave flere varianter af de objekter der bliver gemt ting bagved. 

For at lave en ny ClickableObject (f.eks træer) skal du:
1. Gå ind i Assets/Content/BearGame/Prefabs
2. Højre klik på ClickableObjectBase
3. Gå ind under Create og vælg Prefab Variant


HiddenObjects er base Prefaben der bruges til at lave varianter af de objekter spilleren skal finde.

For at lave en ny HiddenObject (f.eks træer) skal du:
1. Gå ind i Assets/Content/BearGame/Prefabs
2. Højre klik på HiddenObjectBase
3. Gå ind under Create og vælg Prefab Variant.

Når du sætter objektet ind i scenen skal du huske at deaktivere den. Du kan også deaktiver prefaben i stedet for. 


Hvordan gemmer man bjørne?
ClickableObject har et script kaldet Bear Finder. Med dette script styre du hvem der gemmer på hvad, hvilket HiddenObject de gemmer og objektets animationer og lyde.

1. Vælg det ClickableObject (f.eks en busk) der skal gemme på en bjørn.
2. Check af Has Bear
3. Træk det HiddenObject (bjørn) den skal gemme på ned i Bear Cub feltet (For at kunne gøre dette skal du allerede havde sat en bjørn ind i scenen)

Animationer og Lydeffekter
For at lave animationer der kan indikere hvor der gemmer sig en bjørn, skal du lave et Scriptable Object.

1. Gå til Assaets/Content/BearGame/Scriptable Objects
2. Højre klik i Project
3. Vælg Create -> Scriptable Objects -> HiddenObjectData_SO
4. Giv din Scriptable Object et navn (f.eks. Bush_SO)

I dens Inspector kan du definer dens Idle Animation og Hint Animation, sætte tid på hvor længe dens hint animations skal køre samt hvor lang en pause der er imellem hintsne, samt give den lydeffekter til hint animationer (hint sound) og når man finder bjørnen (found sound).

Du skal være opmærksom på at du i Animation mappen ikke sletter HintAnimationController, Idle_anim og Hint_anim. Hvis en bliver slettet skal der laves en ny med præcis samme navn, og hvis det er en af animationerne skal animationen være tom. Åben animations controlleren og sæt Idle_anim til at være Default State. Hint_anim skal bare være inde i layeret uden en transition.   

Når du er færdig med at sætte din Scriptable Object op, skal du gå tilbage til den prefab den passer til og sætte din Scriptable Object in i Object Data feltet under Bear Finder scripted.

I Animation Settings er der en Animator og Base Controller. Base Controller skal bare bruge HintAnimationController, mens Animator skal bruge objektets egen Animator komponent, ved at trække dens prefab op i Animator feltet.


GameManager
I B_GameManager skal du skrive hvor mange bjørne der er i alt.


UI tæller
Du kan ændre teksten der viser hvor mange bjørne er fundet inde i:
Assets/Content/BearGame/UI. Åben BearGameUI (Visual Tree Asset) og ændre teksten til det du ønsker. 

HUSK at skrive {found} for at vise hvor mange der er fundet, og {total} for at vise hvor mange der er i alt. 


UI Popup Regler
For at sætte dens regler op skal du inde i Hierarchy vælge Minigame_UI_Prefab. Her skriver du reglerne for spillet samt sætter spriten for Sticker ind.



Wolf Minigame:
Spillet er et simon siger spil, hvor spilleren skal gentage rækkefølgen af hyl som ulvene laver.
UI:
Der er to dele til spillets UI. Den ene er HR_GameUI som holder styr på runde antallet samt om det er spillerens tur eller ej. UI documentet kan ændres efter behov, bare behold navne på de to labels så koden kan finde dem.
Den anden del af UI er den generelle minigameUI. Den er tidligere beskrevet.

GameManager:
HR_GameManager er ansvarlig for at styre spillets logik. Her er nogle fields i inspectoren som kan justeres efter behov, det er bare at ændre tallet.
yderligere kan der tilføjes flere wolves til listen af wolves i inspectoren, brug plusset og minus til at ændre størrelsen af listen og træk de kreeret wolves i scene hirakiet over i hver element. (Et element for hver ulv)

Wolf Prefab:
Wolf Prefab er de interaktive objekter spilleren interagere med. Der kan laves eller fjernes fra scene hirakiet for at tilpasse mængden af ulve spilleren skal holde styr på. det er drag and drop wolf_prefab fra assets.
Hver ulv kan tilpasses individuelt via et scriptable object som kan laves ved at højreklikke asset folderen, og så "Create -> ScriptableObjects -> HR_Wolf_SO". inspectoren af et scriptable object består af sprites, det lydklip der skal afspilles samt længen af tid ulven hyler på (Målt i sekunder).
Det kreeret scriptable object skal derefter lægges ind på ulv objektet i Scenen under settings.
Yderligere er der lavet events til når en ulv hyler eller når spilleren fejler, som kan tilpasses individuelt efter behov.



PolarBear Minigame:
Dette spil består af en isbjørn og nogle vandhuller med fisk. Isbjørnen "BearObject" har et komponent "Polar Bear Logic", her kan der tilføjes forskellige sprites for når den står stille, og for når den angriber fiskene.
Vandhullerne, under "FishingHoleList" har et komponent "Fishing Hole Logic", her kan der tilføjes en separat sprite for vandhullet og for fisken.
I "GameManager"'en kan der andres på hvor ofte fiskene kommer op af hullerne "Timer Length", og hvor mange der skal fanges for at vinde "Fish Goal".

Quiz Minigame:
Check "ReadMe Quiz.docx"