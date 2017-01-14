# SampleEngine
A friend of mine were asking for a simple 2d engine, that he could learn from, so this is it. You're welcome!

If you'd like to help me, please build upon this; as of right now it's pretty crappy :D

## Build status
[![Build Status](https://travis-ci.org/Mobilpadde/SampleEngine.svg?branch=master)](https://travis-ci.org/Mobilpadde/SampleEngine)

Well, apparently Travis can't build windows-dependant packages, trying to figure something.

## Known Bugs
 * ~~If a teleport to a new level is in the bottom right corner, the hero won't be able to use it.~~
 * ~~The program doesn't close after clicking `x`~~
 * ~~Enemies doesn't remember their last location~~
 * ~~Sometimes the Game won't start~~ (Hacky-fix)

## Changes
 * 14-01-17
  * Added custom key support + rewrote movement
  * Added comments to new and improved classes

 * 12-01-17
  * Initial Version
  * Added a new loading animation
  * Cleaned `using`-statements
  * Added enemies
  * Updated grid
  *	Added comments
  * Fixed closing of the Engine
  * Added a bit of intelligence of the Enemies
  * Fixed omni-bug for enemies, now they cannot see the whole board
  * Fixed enemies' memory
  * Game now opens when asked
