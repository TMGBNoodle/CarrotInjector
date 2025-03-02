# Echo of Nine Lives

## Game Intent
Echo of Nine Lives is a 2D platformer that explores themes of self-discovery, memory, and connection. Set in a dreamlike state during the player's final moments, the game follows a journey to recover the fragments of the self that have been embodied as cats (the player's most beloved thing) scattered throughout the world. By seeking out and conversing with the manifestations of the player's mind, they gradually rebuild their sense of identity and belonging before their time comes to an end.

## Narrative Structure
The game takes place in a surreal, dreamscape environment representing the protagonist's consciousness, the player wakes up in a massively inescapable forest. As the player traverses the world, they encounter cats that each represent different aspects of their former self - memories, emotions, personality traits, and relationships. Each interaction with a cat unlocks a fragment of the player's identity through dialog, gradually piecing together who they once were and what mattered to them. The journey concludes when all fragments are collected, allowing the player to return from their dream with a new sense of belonging.

## Mechanics
- **Platforming Movement**: Standard 2D platformer controls allow players to run, jump, and navigate the world they are thrown into.
- **Cat Interactions**: When near a cat, players can press 'E' to initiate dialog.
- **Dialog System**: Text-based conversations with cats reveal little pieces of personality, and more importantly hints to why the player is here.
- **Parallax Backgrounds**: Multiple layered backgrounds create depth and reinforce the dreamlike atmosphere.
- **Death and Respawn**: Players respawn at the beginning if they fall below a certain point.
- **Portal Interaction**: A stone portal at the end of the journey leads to the credits, symbolizing waking back up from the dream.

## Aesthetics
The game features a melancholic yet peaceful visual style with:
- Multiple parallax background layers creating depth and movement
- Diverse cat sprites representing different aspects of the self
- Custom UI elements for dialog boxes featuring cat portraits
- Pixel art tiles and props constructing a surreal landscape

The overall aesthetic balances between nostalgia, introspection, and the bittersweet acceptance that comes with finding oneself.

## Joys and Struggles
### Joys
- Implementing the parallax background system created a satisfying sense of depth
- Designing dialogues for the unique cats to represent fragments of identity was creatively fulfilling
- Successfully implementing the dialog system with character portraits added personality to interactions
- Creating a cohesive visual experience that supported the emotional tone of the game felt awesome

### Struggles
- Configuring the collisions and platforming physics required a lot of fine-tuning
- Managing the dialog system to properly display and hide UI elements took many tries
- Implementing a seamless parallax effect that worked with both horizontal and vertical camera movement
- Creating a UI that worked well across different screen resolutions took a long time

## Asset Attributions
- **Tileset and Background Art**: [Karsiori - Pixel Art Woods Tileset and Background]
- **Cat Sprites**: [Luiz Melo - Pet Cats Pixel Art Pack]
- **UI Elements**: [Black Hammer - Fantasy Wooden GUI]
- **Font**: [Lady Liefy - Olde Tome]
- **Player Model**: [Luiz Melo - Martial Hero]

## Team Contributions
Joseph - worked on the level design, background/parallax, dialog system, and created the Main Menu / Credits scenes.
I also helped gather the components for most of the assets other than sounds. My main goal and role throughout this
project was to focus on the 'art', style, and story of the game. So I spent a lot of time writing dialog and configuring
the style of the different scenes to feel as cohesive as possible and look well made.

Tucker - Worked to get the physics and platforming mechanics working for the player. Created a movement system which
allows for the player to jump twice, and dynamic collisions to ensure that the player is always correctly matching the
platformer aesthetic. Additionally, Tucker created the animation logic for the player model, including jumping, falling,
and idle animations (Which look awesome). Tucker also added in sound elements for the player's jump.
