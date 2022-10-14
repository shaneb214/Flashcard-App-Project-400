# Flashcard-App-Project-400

This is a 4th year project I worked on which is a mobile flashcard app made with Unity. My aim was to create as many essential features that are found in other mobile flashcard apps within the deadline. My idea was to create a more game focused flashcard app which offers a wider selection of mini games the user could play to help them learn vocabulary in a more interesting way. 

At the moment to register / login to this app you have to run the API that’s located in the “WebApplication” folder. I have not deployed the web api to azure yet so you can only register or login when the local API is running on your computer.

## Features Implemented So Far
-	Can register and login to the app using a web API.
-	Can create language profiles that are used to separate the users created flashcards.
-	Can create flashcards and sets / folders which the flashcards are stored in. 
-	Auto translate option when creating flashcards.
-	Mini game called Wall Dash (inspired by a Fall Guys game mode involving fake walls).

## Features To Be Worked On
-	Deployment of web API to azure. 
-	Ability to delete and rename sets and flashcards.
-	More minigames.
-	UI rework. 

## App Showcase
Here are some of the features / screens I've made so far!

### Login / Register
The user can login to an existing account or create a new account where they will be directed to create their default language profile.

![login](https://user-images.githubusercontent.com/47157867/195874001-04a849f7-a2fc-400f-9fbb-0a09f7aa4b65.gif)

### Flashcard & Set Creation
Flashcards and sets can be created. Any flashcard made will be added to the users current default set they want to add flashcards to. Sets can also be created within sets. An auto translate option is also available which translates the users native word and inserts it into the text field for the learning side of the flashcard. 

![flashcard](https://user-images.githubusercontent.com/47157867/195878964-df7e85e0-5739-4c9b-9025-773b45401453.gif)

### Language Profiles
If the user wants to create flashcards for multiple languages, they can easily create a language profile and switch between any profiles they’ve created.  Any sets or flashcards they create in these profiles will be saved. 

![lang profile](https://user-images.githubusercontent.com/47157867/195874044-b4de4883-49ee-4b1c-9a73-2e5843075fb3.gif)

### Wall Dash Mini Game
Wall Dash is the only mini game implemented so far and provides some customisable settings such as selecting which language will be prompted to the user. The game involves trying to get to the finish line by choosing the correct translation of the word they are prompted with by choosing a wall / path that each represent a possible answer. The wall showing the correct answer will be a fake wall that you can break through while the others will make you fall over and you'll be given the opportunity to try again.

![wall dash](https://user-images.githubusercontent.com/47157867/195875098-8c6c5086-d5c3-4bf7-a16b-2c5cc13e4e98.gif)

![wall dash settings screenshot](https://user-images.githubusercontent.com/47157867/195875124-dda4139b-0dd7-4293-b000-a61a890ffb1c.png)
