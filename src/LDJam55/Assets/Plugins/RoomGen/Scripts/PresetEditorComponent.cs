using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RoomGen
{
    public class PresetEditorComponent : MonoBehaviour
    {

        public RoomPreset preset;


        [HideInInspector]
        public List<Roof> roofs = new List<Roof>();
        [HideInInspector]
        public List<Floor> floors = new List<Floor>();
        [HideInInspector]
        public List<Wall> walls = new List<Wall>();
        [HideInInspector]
        public List<Wall> wallCorners = new List<Wall>();
        [HideInInspector]
        public List<Door> doors = new List<Door>();
        [HideInInspector]
        public List<Window> windows = new List<Window>();

        [HideInInspector]
        public List<Decoration> characters = new List<Decoration>();
        [HideInInspector]
        public List<Decoration> roofDecorations = new List<Decoration>();
        [HideInInspector]
        public List<Decoration> wallDecorations = new List<Decoration>();
        [HideInInspector]
        public List<Decoration> floorDecorations = new List<Decoration>(); 


        #region inspectorValidations;


        public void UpdateStoredValues()
        {
            if (preset == null)
                return;

            roofs = preset.roofTiles;
            walls = preset.wallTiles;
            wallCorners = preset.wallCorners;
            doors = preset.doorTiles;
            floors = preset.floorTiles;
            windows = preset.windowTiles;
            characters = preset.characters;
            roofDecorations = preset.roofDecorations;
            wallDecorations = preset.wallDecorations;
            floorDecorations = preset.floorDecorations;

            // save them to the preset.
            UpdatePreset();
        }

        public void UpdatePreset()
        {
            preset.roofTiles = roofs;
            preset.wallTiles = walls;
            preset.wallCorners = wallCorners;
            preset.doorTiles = doors;
            preset.floorTiles = floors;
            preset.windowTiles = windows;
            preset.characters = characters;
            preset.roofDecorations = roofDecorations;
            preset.wallDecorations = wallDecorations;
            preset.floorDecorations = floorDecorations;
        }

        #endregion


    }
}
