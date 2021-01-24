# KH2Tracker
A Kingdom Hearts 2 item tracker for use with the Garden of Assemblage Randomizer mod

![Screenshot](KH2Tracker.png)

## Options
* Save Progress
 * Saves the current tracker state to a file. If hints are loaded they will be saved as well
* Load Progress
 * Loads a saved tracker file.
* Load Hints
  * Loads hint files generated and saved by [JSmartee's hint system](https://jsmartee.github.io/kh2fm-hints-demo/)
  * This will apply the three strikes anti cheat and auto update world check counts when tracking reports
* Run Auto Tracker
  * Connects the tracker to pcsx2 and automatically tracks important checks as they are found

***

* Broadcast Window
  * Opens a window with a more typical tracker display for streaming. Everything in this window updates when tracking checks from the default window
  
***
  
* Reset Window
 * Resets the main and broadcast windows to their default size
* Reset Tracker
  * Resets the tracker to its default state 
  
## Toggles
* Loading Hints will automatically toggle the settings that the hint file was made with

* Promise Charm
  * Toggles on/off the promise charm as an important check
* Ansem Reports
  * Toggles on/off the Ansem reports as important checks
* Once More / Second Chance
  * Toggles on/off once more and second chance as important checks
* Torn Pages
  * Toggles on/off the torn pages as important checks
* Cure
  * Toggles on/off the cure spells as important checks
* Final Form
  * Toggles on/off final form as an important check
  
***
  
* Sora's Heart
  * Toggles on/off Levels as a place that important checks can be found
* Simulated Twilight Town
  * Toggles on/off Simulated Twilight Town as a place that important checks can be found
* 100 Acre Wood
  * Toggles on/off 100 Acre Wood as a place that important checks can be found
* Atlantica
  * Toggles on/off Atlantica as a place that important checks can be found
  
***

* Simple Icons
  * Simplistic important check icons for both the main window and broadcast window
* Orb Icons
  * Orb-like important check icons for both the main window and broadcast window
* Third Option
  * Orb-like important check icons for the main window with more detailed icons for the broadcast window
  
***
* World Icons

  * Toggles between simplistic icons and abbreviations for worlds
  
***

* Drag and Drop
  * Toggles between drag and dropping items + selecting a world and double clicking an item or just selecting a world and single clicking an item to track items
  
## How To Use

Drag an item to the location that you found it in. Alternatively highlight worlds by clicking on them and then double click on items to mark them as collected in that world. Clicking on a marked item will return it to the item pool. (if not using drag and drop controls then only a single click on an item is required)

The question marks connected to each world denote the number of important checks in a world. If hints are loaded these will be set automatically as reports are tracked. If hints are not loaded they can be increased or decreased with the scroll wheel or by selecting a world and using page up / page down

If a hint file is loaded into the tracker reports must be tracked correctly. Incorrectly tracking a report 3 times will lock you out of tracking that report and receiving its hint. Hovering over already tracked reports will also display their hint text.

## Auto Tracker

The auto tracker functionality works by reading pcsx2's memory. Trying to run the auto tracker when pcsx2 is closed will not work and closing pcsx2 while the auto tracker is running will stop it.

In addition to automatically tracking the important checks, the auto tracker tracks your stats as well as the starting weapon you chose. One thing to keep in mind for stats is that during cutscenes and some fights they values they show will not be correct. If using the broadcast window the auto tracker will also track drive form levels and growth abilities. Currently there isn't a way to track these yourself or see them in the main window.

Valor form is not being automatically tracked due to the flag for it being obtained being set anytime you open the summon command menu. Final form will only be tracked automatically if it is found before being forced (both normally or through light and dark).

## Thanks

* Tommadness
  * Created the broadcast window and the framework from which the auto tracker was built. Spent a ton of time helping figure out bugs and solutions to get the auto tracker working.
* Televo
  * Made all of the icons the tracker uses that weren't taken straight from the game (some modified very slightly by me)
* Sonicshadowsilver2
  * Made the GoA mod that the randomizer itself is built on and provided tons of useful information to help create the auto tracker
