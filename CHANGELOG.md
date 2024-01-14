## Release v1.1.1

### Gameplay changes

- fix: added missing sound effects for pickups and power-up transitions
- added better icons for the game

## Release v1.1.0

### Gameplay changes

- changed the number of lemons required to open secret to 100 (previous 500). this hopefully makes the game easier to "complete"
- camera adjusted to better support taller devices (1:2 aspect ratio)
- removes credits tab on the landing screen

### Technical changes

- upgrade unity version to latest LTS (2022)
- refactored the audio manager
  - now audio plays both in the editor and android devices
  - changed how music is played on android by utilising ANAMusic
- now targetting android 12+ to comply with google play policy changes
- removed unnecessary assets
- removed unnecessary plugins