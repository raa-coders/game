# Barman Simulator 2019
Video game project for the Augmented Reality and Accessibility course at Universidad de Oviedo.

## Game Design Summary

### Overall Design

**Genre:** Arcade, Puzzle.

**Narrative and objectives:** In this game you play as a barman, who will serve the different cocktails the bar clients may order. The objective of the game is to keep our clients happy, serving top-level cocktails (or the closest we can get) without getting any order wrong.

**Characters:** The game will feature the Barman (the player) and a wide cast of non-playable characters that will act as clients of the bar.

**User Interface:** We've opted for a clean UI, with just a cursor in the centre of the screen and a tooltip with the current order displayed in a corner of the screen.


### Main Mechanics

The game plays preparing cocktails with the resorces availabe behind the bar. It will not need any kind of remote control or input other than the VR glasses (the game is dessigned to run on the cheapest VR headset).

The main game loop will have three mechanic pillars, which can be listed as follows:

 - **Mixing.** When you get an order, you will have to mix the available liquors and drinks to output the desired cocktail. This will be done directly grabbing each bottle and filling the shaker with the correct ammount of beverage. Once the shaker is full, the player will close it and shake it.
 
 - **Presentation.** Once the cocktail is mixed, the player will open the shaker and pour the content in a glass. After that, fruits, straws and other kinds of decoration would be added, if wanted.
 
 - **Happyness.** When the cocktail is ready, the player will serve it to the client. Then, the player will receive the feedback from the client, who will show if he is happy or not with his cocktail, or if we have served something that he or she didn't order at all.
 
The happier clients walk out the bar, the harder future cocktails will be. As a design decission, difficulty won't increase unless the player does a good job (so it can be more accessible to non-skilled players).
 



## Storyboard
![Storyboard](/StoryBoard.jpg)


## Planning

All coding and 2D graphic assets will be developed in-house. Ideally, 3D models will also be created in-house via C# mesh manipulation. Sound effects will be downloaded from speciallized pages that publish audio asserts for free.

If we can afford the insane amount of 10€, maybe we start talking about the possibility of thinking of a viable situation where we buy them.

### Sprint 1 (Oct 21 - Nov 3)

 - [x] Putting a _SUPER AWESOME_ team together
 - [x] Game Design
 - [x] Storyboard
 
### Sprint 2 (Nov 4 - Nov 18)

 - [ ] Basic UI
 - [ ] Basic orders
 - [ ] Grabbing and pouring bottles
 - [ ] Serving cocktails to clients
 - [ ] Client happyness
 - [ ] **First demo build**

### Sprint 3 (Nov 18 - Dec 2)

 - [ ] Mixing
 - [ ] Drink presentation
 - [ ] Difficulty system
 - [ ] Basic sounds
 - [ ] **Second demo build**

### Final Sprint (Dec 2 - Dec 15)

 - [ ] Refined UI
 - [ ] Better sounds
 - [ ] Random-generated clients
 - [ ] Special effects
 - [ ] **Final build**





## Wish List
  
  - Dialog with clients before they order a drink.
  - Sound track.
  
