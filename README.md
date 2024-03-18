# MenuRestaurantWebAPP
**Developers:** Luca Falappa (118387), Maggye Salvucci (120065), Matteo Morelli (118804), Igor Casadidio (113905)

## Introduzione
Abbiamo sviluppato un’applicazione web che consente di gestire il menù di un ristorante, con la possibilità di svolgere le seguenti azioni: 
- Creare ed eliminare tipo di portata (es. antipasto, primo, secondo, ecc…) 
- Creare, eliminare e modificare una pietanza (es. Spaghetti alla carbonara) 

## ASP.NET Core MVC Web App
Secondo il pattern architetturale MVC (Model-View-Controller), il quale suddivide le responsabilità di risposta alle richieste degli utenti in più sezioni, abbiamo così operato: 

### Models 
Abbiamo definito i seguenti modelli, allocati in una libreria di classi esterna:
1. La classe Portata con le seguenti proprietà: Id, Tipologia 
2. La classe Pietanza con le seguenti proprietà: Id, Nome, Prezzo, Portata Id, Tipologia (riferita alla portata associata) 

### Controller
Abbiamo sfruttato il controller di default, ovvero HomeController, per gestire le operazioni CRUD (Create, Read, Update, Delete) sulle portate e pietanze. I metodi per manipolare l’insieme dei dati memorizzati sono stati implementati in una libreria di classi, esterna al progetto principale.

### ViewModels 
Un modello di una vista contiene solo i dati necessari per la visualizzazione o la modifica sulla pagina, semplificandone la gestione e la validazione, consentendo di mantenere la logica di business separata dall’interfaccia grafica. 

### Views
Le visualizzazioni Razor da noi create consentono di visionare i dati elaborati dai metodi contenuti nel controller. 

## Stile e Layout: 
Abbiamo sviluppato le nostre pagine web applicando i layout generati automaticamente al momento della creazione del progetto.  Per quanto riguarda le viste da noi realizzate, abbiamo seguito una linea implementativa condivisa, sfruttando i componenti di Bootstrap. 

## Validazione: 
1. Nell’inserimento dei prezzi delle pietanze, nel rispetto dei parametri prestabiliti
2. Nell’inserimento di una portata affinché non possa essere creata una portata già esistente
3. Nell’inserimento di una pietanza affinché non possa essere creata una pietanza avente lo stesso nome di una esistente

## Autenticazione: 
Abbiamo implementato un sistema di autenticazione per consentire soltanto agli utenti registrati alla piattaforma di poter interagire con essa. Per realizzare ciò, abbiamo utilizzato il pacchetto Microsoft.AspNetCore.Identity. Tramite la creazione di un nuovo elemento di scaffolding, sono stati generati automaticamente i vari componenti forniti dal pacchetto stesso, tra cui il contesto del database, il prototipo di utente e le pagine web per la gestione dell’account. Inoltre, Identity offre di default la crittografia delle password, secondo l’algoritmo di hashing PBKDF2 (Password-Based Key Derivation Function 2).  

## Database
La scelta è ricaduta su un database relazionale: Microsoft SQL Server. Per poter memorizzare le informazioni relative ai modelli da noi definiti (pietanza, portata, utente), abbiamo implementato i DbContext su cui eseguire le migrazioni, le quali consentono di creare direttamente le varie tabelle su database senza eseguire alcuno script SQL. Le classi relative ai due contesti presenti (quello per l’autenticazione e quello per il menù), e alle migrazioni verso il database, sono state inserite all’interno di una libreria di classi, esterna al progetto principale. 
La configurazione della base di dati avviene tramite dependency injection, al momento dell’esecuzione di Program.cs, includendo anche le stringhe di connessione al database (memorizzate nel file di impostazioni dell’applicazione “appsettings.json”). Abbiamo inoltre aggiunto il vincolo di integrità referenziale tra pietanze e portate, in particolare l’attributo PortataId di una pietanza si riferisce all’Id della portata associata. Questo vincolo garantisce che le relazioni tra le pietanze e le portate siano coerenti e rispettate, evitando situazioni inconsistenti nei dati. 

## Docker
Con l’obiettivo di ospitare il nostro applicativo web all’interno di un container Docker, abbiamo innanzitutto aggiunto il supporto Docker (Dockerfile) al progetto MVC, che ha la funzione di generare l’immagine da predisporre nel container. Tra le altre informazioni presenti in Dockerfile, abbiamo impostato la porta esposta dal container. 

Successivamente per far comunicare tra loro l’applicazione e il database, facciamo uso dell’agente di orchestrazione dei container (Docker Compose). Docker Compose utilizza il formato YAML, caratterizzato da flessibilità e leggibilità, e che rende intuitiva la definizione di dati strutturati e configurazioni. All’interno di docker-compose.yml abbiamo dichiarato, tra l’altro: 
- Le immagini (applicazione e base di dati) di riferimento
- Le porte da esporre, sia dal computer host sia dai container
- Le variabili d’ambiente del database
- Il network tramite cui comunicano il database e l’applicazione

## manifest.json, service-worker.js e PWA
### manifest.json
La funzione principale di un manifesto dell’applicazione web è quella di fornire informazioni necessarie al browser per installare una Progressive Web App (PWA) su un dispositivo. Le PWA offrono un’esperienza simile a quella delle app native, ma possono essere eseguite direttamente dal browser. Grazie a manifest.json, il browser sa quali risorse scaricare e come integrare l’applicazione nel sistema operativo del dispositivo, avvalendosi di alcune proprietà, tra cui:  
- Il nome dell’applicazione
- L’indirizzo URL iniziale
- Le icone
- Gli screenshots dell’applicazione

### service-worker.js
Un service worker è uno script JavaScript che il browser avvia in background. Esso è separato dalla pagina, pertanto non può modificarne gli elementi, ma può comunicare con essi mediante “messaggi” scambiati. Le informazioni chiave relative al nostro service worker sono: 
- Definizione della cache del sito web 
- L’evento di installazione della PWA
- Eventi di recupero della richiesta eseguita dal browser
L’implementazione di service-worker.js non è stata effettuata integralmente, a causa di conflitti generabili quando viene avviato in un ambiente HTTP; inoltre è stata realizzata mediante lo strumento open source PWABuilder di Microsoft. 

### PWA
A differenza delle app tradizionali, le PWA sono un ibrido tra le normali pagine web e le applicazioni mobili. Esse presentano alcuni vantaggi: 
- Responsiveness 
- Di veloce implementazione
- Non necessitano di duplicazione del codice in base al sistema operativo 
- Scaricabili accedendo al sito web
È possibile verificare che il progetto risulti essere una PWA, avvalendosi per esempio della composizione dei container Docker.
