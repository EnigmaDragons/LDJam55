using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RoomGen {

    [CreateAssetMenu(menuName = "Room Generator/Room Preset")]
    public class RoomPreset : ScriptableObject
    {


        [Header("Room Prefabs")]
        [Space]
        public List<Roof> roofTiles = new List<Roof>();
        public List<Floor> floorTiles = new List<Floor>();
        public List<Wall> wallTiles = new List<Wall>();
        public List<Wall> wallCorners = new List<Wall>();
        public List<Window> windowTiles = new List<Window>();
        public List<Door> doorTiles = new List<Door>();
        public List<Decoration> characters = new List<Decoration>();
        public List<Decoration> roofDecorations = new List<Decoration>();
        public List<Decoration> wallDecorations = new List<Decoration>();
        public List<Decoration> floorDecorations = new List<Decoration>();

    }

}
