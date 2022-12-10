# CardGame

## Blackjack
When we started working on Blackjack it was difficult, since we had never worked with Unity before. However, after some time, a lot of tries and doing research we managed and were able to work faster. The main difficulties were creating the shuffle functionality as well as counting the cards values and determining the winner of the game. We also had some issues with displaying everything correctly, which also took some time. What helped us was creating the GameManager script, this way we had a way better overview over the game. In the end we were able to create the game as we had imagined to. <br />

#### Script:
**GameManager:** In this class all the functionalities concerning buttons, text etc. are included. Besides this it is the basis for the game. For instance, the points from the PlayingScript are checked to find out if the game is over and who won. <br />
**PlayingScript:** The PlayingScript serves the player and the dealer. Functionalities like counting the points and the money are part of it. <br />
**CardScript:** It is used to set the cards and return to the Deck class. <br />
**Deck:** With the Deck script new cards are assigned to the player and the dealer. <br />
