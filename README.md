# 2DPlatformGame

______

Detailed information will be published soon...

______

## 2D Platform Game Project
In this game project, our aim was to design the mechanics of a simple 2D platform game. Unity game engine was used in this project, which was designed to be easily expandable.

![2D Platform Game Project](/Screenshots/Main.png "2D Platform Game Project")

### Slime System
Slimes are dynamic. If there is an obstacle at ground level where they are placed, they turn back and continue their path. If there is a gap ahead on the path they are walking on, they also turn back and continue their way.
When they come into contact with the player's character, they inflict damage.

![Slime System](/Screenshots/Slime.png "Slime")
![Slime System 2](/Screenshots/Slime2.png "Slime 2")

### Saw System
There are two types of saws in the game.
One of them is a saw that moves vertically or horizontally and doesn't oscillate. It only goes back and forth between two specific points.
The other one is an oscillating/swinging saw. It swings like it's attached to a chain, moving back and forth between two points.

![Saw System](/Screenshots/Saw1.png "Saw")
![Saw System 2](/Screenshots/Saw2.png "Saw 2")

### Stake System
Stakes are placed in specific positions. They inflict damage to the character when touched in any way.

![Stake System](/Screenshots/Stake.png "Stake")

### Responsive Jump
Jumping is responsive. It allows for jumping to certain heights based on how much Mana is available and how much the jump button is pressed.

![Responsive Jump](/Screenshots/Jump.png "Jump")

### Mana System
Mana decreases based on the character's movements. When there is no movement, the mana bar starts to slowly fill up. (It is customizable.)

![Mana System](/Screenshots/Mana.png "Mana")

### Health System
The character's health is set to 6. When the health reaches 0 due to damage from enemies, the level resets.

![Health System](/Screenshots/Damage.png "Damage")

### Third Party Assets
The packages in the list below were used in the project;
* Pixel Adventure 1 by Pixel Frog (https://assetstore.unity.com/packages/2d/characters/pixel-adventure-1-155360)
* Pixel Adventure 2 by Pixel Frog (https://assetstore.unity.com/packages/2d/characters/pixel-adventure-2-155418)
