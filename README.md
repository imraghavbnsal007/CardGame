# CardGame
This is an implementation of two single-player cardgames, Klondike Solitaire and Blackjack, using Unity game engine achieved with three seperate scenes: MainMenu, Solitaire, and Spider.

### MainMenu
A simple main menu that allows access to the games using a simple yet intuitive design. There are four buttons available. Two for the games, one for the sound control and one to exit the game.

### Solitaire
A simple implementation of Klondike Solitaire with minimal features.

### Spider
An implementation of Blackjack


## Blackjack
When we started working on Blackjack it was difficult, since we had never worked with Unity before. However, after some time, a lot of tries and doing research we managed and were able to work faster. The main difficulties were creating the shuffle functionality as well as counting the cards values and determining the winner of the game. We also had some issues with displaying everything correctly, which also took some time. What helped us was creating the GameManager script, this way we had a way better overview over the game. In the end we were able to create the game as we had imagined to. <br />

### Script:
**GameManager:** In this class all the functionalities concerning buttons, text etc. are included. Besides this it is the basis for the game. For instance, the points from the PlayingScript are checked to find out if the game is over and who won. <br />
**PlayingScript:** The PlayingScript serves the player and the dealer. Functionalities like counting the points and the money are part of it. <br />
**CardScript:** It is used to set the cards and return to the Deck class. <br />
**Deck:** With the Deck script new cards are assigned to the player and the dealer. <br />

## Solitaire
A simple implementation of Klondike Solitaire using Unity game engine.
The features available to the user are: an auto-stack function that brings a selected card up to the top deck by double clicking said card, a reset button in case the player gets stuck, a sound control button available throughout the game, and a victory screen. Selected cards are highlighted.
There is no undo ctrl+z funtion available nor a loose condition.
The toughest parts of this project was game testing, then addressing those issues and bugs. Due to our unfamiliarity with the Unity game engine and the fact that nearly all bugs were due to a misconfiguration of the existing game objects (such as layering) finding and fixing said bugs proved to be quite challenging. All known bugs were found and fixed and the latest version is available on Github.

### Scripts
**UserInput.cs**: Responsible for receiving and processing user input during runtime. GetMouseClick() reads where on the screen the user clicked, plays appropriate soundFX for each action, and increments the clickCount variable used to detect a double click in the Update() function. Deck() calls the DealFromDeck() function which cycles through cards in threes. Card() turns facedown cards if not blocked, checks validity then implements AutoStack, and allows cards to be stacked during gameplay. Top() allows Aces to be added to the final decks. Bottom() allows Kings to be moved to an empty slot. Stackable() returns a boolean after checking whether cards are blocked or not and the suits, values of the cards. Stack() stacks cards and adds card of lower value as a children to cards of higher value. Allows movements of children with parent. Blocked(), DoubleClick(), HasNoChildren() return booleans that aid in the execution of the forementioned functions.

**UpdateSprite.cs**: Renders cardfaces and highlights selected cards.

**UIButtons.cs**: Code that allows the reset button to function.

**SoundManager.cs**: Controls and updates the sprite on the mute/unmute button then saves/loads user settings.

**Solitaire.cs**: Responsible for the setup, deck display, deck shuffling, deck restacking, deck sorting, deck dealing, deck generation and holds all the necessary lists and strings necessary to run the game. For more information check comments.

**Selectable**: Assigns appropriate values to card game objects.

**ScoreKeeper.cs**: Checks if winning condition is met.

**SFX.cs**: Holds all necessary sound effects. Doesn't get destroyed on Load.
