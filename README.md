🏖️ VacationSystem
VacationSystem er et administrasjonsverktøy bygget med ASP.NET Core MVC og SQLite for å:

Beregne antall feriedager mellom to datoer

Hente helligdager automatisk basert på valgt land

Vise oversiktlig kalender med planleggingsdager og helligdager

La brukere foreslå planleggingsdager som admin kan godkjenne

📦 Nedlasting og kjøring
1. Last ned prosjektet
Klikk på Code-knappen øverst og velg Download ZIP, eller bruk git:

bash
Kopier
Rediger
git clone https://github.com/Aikon74/VacationSystem.git
2. Åpne i Visual Studio 2022
Åpne VacationSystem.sln i Visual Studio 2022.

Sørg for at vacation.db ligger i db/-mappen.

Trykk F5 eller klikk Start-knappen for å kjøre prosjektet.

3. Førstegangsbruk
Ved første kjøring opprettes databasen automatisk hvis den mangler.

🔐 Innlogging
Admin-bruker:

Brukernavn: admin

Passord: admin123

Du kan legge til flere brukere via adminpanelet.

🌍 Funksjoner
Beregning av feriedager:

Tar hensyn til helg, røde dager og godkjente planleggingsdager.

Støtter flere lands helligdager (NO, SE, DK, DE, US, GB, FR).

Kalenderoversikt:

Fargekoder for helg og helligdager.

Viser navn på røde dager og institusjon for planleggingsdag.

Adminpanel:

Godkjenning av planleggingsdager.

Legge til/slette brukere.

