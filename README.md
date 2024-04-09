# GameJamTemplate

Template Repository for Starting a new Game Jam Unity Project

----

### Contributor Quickstart

Welcome to the Enigma Dragons game jam squad! 

To begin work on the project, prepare yourself and your environment by following these steps:
1. Read the Guides below
2. Ensure that you can create/move cards on the Kanban board
3. Setup your Development Environment

Details on each are found below.

----

### Guides

[Game Jam Guide](./guides/game-jam-guide.md)
[Kanban Workflow Process](./guides/kanban-workflow-guide.md)
[Kanban Board Setup](./guides/kanban-board-guide.md)
[Unity Programming Guidelines](./guides/unity-design-guidelines.md)

----

### Kanban Board

[Project Kanban Board](https://zube.io/enigmadragons/ldjam55/w/workspace-1/kanban)

[Kanban Workflow Process](./guides/kanban-workflow-guide.md)
[Kanban Board Setup](./guides/kanban-board-guide.md)

----

### Development Environment Setup

Software Requirements:
- Git
- Unity 2023 (Version: 2023.2.17f1)
- Any C# IDE
- FMOD Studio v.2.02.21 (Unity Verified)

Setup:
1. Clone this repository using git
2. Install any C# IDE of your choice. [Visual Studio Code](https://code.visualstudio.com/) recommended.
3. Install [Unity Hub](https://unity3d.com/get-unity/download)
4. Launch Unity Hub and Install Unity Version 2023.2.17f1
5. In Unity Hub, click Open
6. Browse and select `../repo/src/LDJam55`
7. Click on Open

FMOD: The Fmod studio project is in src/FMOD_LD55

Creating an event and adding a sound in FMOD
In the Browser add a 2D or 3D Timeline Event and name it
Add sound in the start of the timeline
Assign the Event to the relevant bank e.g., SFX, music, etc (right click on the event in the browser). 

Build from the File menu or (F7)

In Unity
Add an FMOD Studio Event Emitter to a gameobject
Set start and stop method (OnTrigger, etc)
Add the event from the build (if the events aren't visible try refreshing the banks from the FMOD windows menu)
Note that while built files are compressed FMOD Studio accepts any file format and higher res files can grow the FMOD session quickly. Therefore 48kHz/16 or 24 bit files are recommended.

----

### Team

- Project Manager
- Lead Designer
- Lead Artist
- Lead Audio Engineer
- Lead Programmer
- Lead Tester
- UI
- Music
- Sound
- Content Design
- Writing
