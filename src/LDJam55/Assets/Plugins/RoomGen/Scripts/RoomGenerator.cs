using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;
using Random = UnityEngine.Random;

namespace RoomGen
{
    [RequireComponent(typeof(PresetEditorComponent))]
    [RequireComponent(typeof(EventSystem))]
    public class RoomGenerator : MonoBehaviour
    {
        public RoomPreset preset;
        public bool debug;

        /// The id used to identify RoomGen instances in the scene. Used with the runtime event system.
        public int id;

        [Space] [Header("Room Size")] [Range(1, 20)]
        public int gridX = 2;

        [Range(1, 20)] public int gridZ = 2;

        public List<Level> levels = new List<Level>();

        [Range(0.01f, 10f)] public float tileSize = 5f;


        [HideInInspector] public List<Roof> roofs = new List<Roof>();
        [HideInInspector] public List<Floor> floors = new List<Floor>();
        [HideInInspector] public List<Wall> walls = new List<Wall>();
        [HideInInspector] public List<Wall> wallCorners = new List<Wall>();
        [HideInInspector] public List<Door> doors = new List<Door>();
        [HideInInspector] public List<Window> windows = new List<Window>();

        [HideInInspector] public List<Decoration> characters = new List<Decoration>();
        [HideInInspector] public List<Decoration> roofDecorations = new List<Decoration>();
        [HideInInspector] public List<Decoration> floorDecorations = new List<Decoration>();
        [HideInInspector] public List<Decoration> wallDecorations = new List<Decoration>();


        [HideInInspector] public GameObject parent;


        List<Node> nodes = new List<Node>();
        public List<GameObject> tiles = new List<GameObject>();
        List<DecoratorPoint> decoratorPoints = new List<DecoratorPoint>();
        List<GameObject> generatedCharacters = new List<GameObject>();
        List<GameObject> generatedWallDecor = new List<GameObject>();
        List<GameObject> generatedFloorDecor = new List<GameObject>();

        private List<DoorPin> doorPins = new List<DoorPin>();

        [Tooltip("This is a global property that will adjust all wall decorations forward by this value. Use this if all your wall decorations " + "are clipping through the wall tiles.")]
        public float wallDecorationOffset = 0.2f;

        [Tooltip("This is a global property that will adjust all floor decorations up by this value. Use this if all your floor decorations " + "are clipping through the floor tiles.")]
        public float floorDecorationOffset = 0f;

        [Tooltip("This will give your floor props a safe area distance from the surrounding walls. Nothing will spawn within the safe area.")] [Range(0, 25)]
        public int decorSafeArea = 1;

        [HideInInspector] public int points;

        [Tooltip("Increasing this property will multiply the number of available decoration points, resulting in a less grid-like layout," + "and more dense decorations and props." + "Be aware increasing this too much can impact performance.")]
        [Range(0, 3)]
        public int pointSpacing = 1;

        [Range(0.25f, 5f)] public float floorDecorSpacing = 0.5f;

        //GenerationWeights
        int totalRoofWeight = 0;
        int totalFloorWeight = 0;
        int totalWallWeight = 0;
        int totalWallCornerWeight = 0;
        int totalDoorWeight = 0;
        int totalWindowWeight = 0;

        //int totalCharacterWeight = 0;
        //int totalRoofDecorWeight = 0;
        //int totalFloorDecorWeight = 0;
        //int totalWallDecorWeight = 0;


        //Debugging tools.
        private List<Vector3> safeAreaPoints = new List<Vector3>();

        private void Start()
        {

        }



        public void GenerateRoom(int generatorID)
        {
            if (generatorID != this.id) return;
            DestroyRoom();
            GenerateLevels();
        }


        public void GenerateLevels()
        {
            if (parent == null)
            {
                parent = new GameObject("RoomPreview");
                parent.transform.position = transform.position;
            }

            RemoveBounds();

            float offset = 0;

            foreach (Level level in levels)
            {
                if (level.preset == null)
                    continue;

                GameObject newParent = new GameObject("LevelBounds");
                newParent.transform.position = transform.position;
                newParent.transform.parent = transform;
                BoxCollider bounds = newParent.gameObject.AddComponent<BoxCollider>();

                bounds.size = new Vector3(gridX * tileSize, level.levelHeight * tileSize, gridZ * tileSize);
                bounds.center = new Vector3(0, offset + (bounds.size.y / 2), 0);
                bounds.center += level.levelOffset;
                offset += bounds.size.y;

                doorPins = FindObjectsOfType<DoorPin>().Where(x => x.roomGenerator == this).ToList();
                    
                CalculateWeights(level);
                SpawnPoints(bounds, level);
                StartCoroutine(SpawnTiles(bounds, level.preset, level));
            }
        }


        public void SpawnPoints(BoxCollider boxCollider, Level level)
        {
            Vector3 max = boxCollider.bounds.max;
            Vector3 min = boxCollider.bounds.min;

            int levelIndex = levels.IndexOf(level);


            //Wall Nodes
            for (float x = min.x; x < max.x; x += tileSize)
            {
                for (float y = min.y; y < max.y; y += tileSize)
                {
                    // Z corners
                    if (x == min.x)
                    {
                        Vector3 cornerNodePos = new Vector3(min.x, y, min.z);
                        nodes.Add(new Node(cornerNodePos, Quaternion.Euler(0, 0, 0), TileType.WallCorner, levelIndex));
                    }

                    if (x == max.x - tileSize)
                    {
                        Vector3 cornerNodePos = new Vector3(max.x, y, min.z);
                        nodes.Add(new Node(cornerNodePos, Quaternion.Euler(0, -90, 0), TileType.WallCorner, levelIndex));
                    }

                    //zMin
                    Vector3 zMinWall = new Vector3(x + tileSize, y, min.z);
                    nodes.Add(new Node(zMinWall, Quaternion.Euler(0, 0, 0), TileType.Wall, levelIndex));

                    //zMax
                    Vector3 zMaxWall = new Vector3(x, y, max.z);
                    nodes.Add(new Node(zMaxWall, Quaternion.Euler(0, 180, 0), TileType.Wall, levelIndex));
                }
            }

            for (float z = min.z; z < max.z; z += tileSize)
            {
                for (float y = min.y; y < max.y; y += tileSize)
                {
                    // x corners
                    if (z == min.z)
                    {
                        Vector3 cornerNodePos = new Vector3(max.x, y, max.z);
                        nodes.Add(new Node(cornerNodePos, Quaternion.Euler(0, 180, 0), TileType.WallCorner, levelIndex));
                    }

                    if (z == max.z - tileSize)
                    {
                        Vector3 cornerNodePos = new Vector3(min.x, y, max.z);
                        nodes.Add(new Node(cornerNodePos, Quaternion.Euler(0, 90, 0), TileType.WallCorner, levelIndex));
                    }

                    //xMin
                    Vector3 xMinWall = new Vector3(min.x, y, z);
                    nodes.Add(new Node(xMinWall, Quaternion.Euler(0, 90, 0), TileType.Wall, levelIndex));

                    //xMax
                    Vector3 xMaxWall = new Vector3(max.x, y, z + tileSize);
                    nodes.Add(new Node(xMaxWall, Quaternion.Euler(0, -90, 0), TileType.Wall, levelIndex));
                }
            }

            // Floor Nodes
            for (float x = min.x; x < max.x; x += tileSize)
            {
                for (float z = min.z; z < max.z; z += tileSize)
                {
                    for (float y = min.y; y < max.y; y += tileSize * level.levelHeight)
                    {
                        Vector3 yMinFloor = new Vector3(x, y, z);
                        nodes.Add(new Node(yMinFloor, Quaternion.Euler(0, 90, 0), TileType.Floor, levelIndex));
                    }
                }
            }


            // Roof Nodes
            for (float x = min.x; x < max.x; x += tileSize)
            {
                for (float z = min.z; z < max.z; z += tileSize)
                {
                    Vector3 yMaxRoof = new Vector3(x, max.y, z);
                    nodes.Add(new Node(yMaxRoof, Quaternion.Euler(0, 90, 0), TileType.Roof, levelIndex));
                }
            }
        }

        void CalculatePoints()
        {
            int pointsMax = (int)tileSize + 1;
            int numPoints = (pointsMax * pointSpacing) - (pointSpacing - 1);
            points = numPoints;
        }


        void SpawnWallDecoratorPoints(BoxCollider boxCollider, GameObject obj, Tile tile, PointType pointType, int levelNumber)
        {
            if (boxCollider == null)
                return;

            Vector3 max = boxCollider.bounds.max;
            Vector3 min = boxCollider.bounds.min;

            CalculatePoints();
            for (int x = 0; x < points - 1; x++)
            {
                for (int y = 0; y < points; y++)
                {
                    Vector3 raypos = new Vector3(-x, y, 0);
                    Vector3 adjustedPos = Tools.AdjustedPosition(obj, -tile.positionOffset);
                    //Vector3 rayStart = obj.transform.position + (obj.transform.forward * 5f) + obj.transform.rotation * (raypos / pointSpacing);
                    Vector3 rayEnd = adjustedPos + obj.transform.rotation * (raypos / pointSpacing);
                    //Debug.DrawLine(rayStart, rayEnd, Color.yellow, 2f);

                    if (rayEnd.x == max.x && rayEnd.z == min.z)
                    {
                        continue;
                    }

                    if (rayEnd.x == max.x && rayEnd.z == max.z)
                    {
                        continue;
                    }

                    if (rayEnd.x == min.x && rayEnd.z == max.z)
                    {
                        continue;
                    }

                    if (rayEnd.x == min.x && rayEnd.z == min.z)
                    {
                        continue;
                    }


                    DecoratorPoint newPoint = new DecoratorPoint(obj, null, rayEnd + obj.transform.forward * wallDecorationOffset, pointType, false, levelNumber);
                    decoratorPoints.Add(newPoint);
                }
            }
        }


        void SpawnFloorRoofDecoratorPoints(BoxCollider boxCollider, GameObject obj, Tile tile, PointType pointType, int levelNumber)
        {
            //yield return new WaitForSeconds(0.0001f);
            if (boxCollider != null)
            {
                Vector3 max = boxCollider.bounds.max;
                Vector3 min = boxCollider.bounds.min;

                CalculatePoints();
                for (int x = 0; x < points; x++)
                {
                    for (int z = 0; z < points; z++)
                    {
                        float comparisonPoint = min.y;
                        Vector3 rayOrigin = Vector3.zero;

                        if (pointType == PointType.Floor)
                        {
                            comparisonPoint = min.y;
                            rayOrigin = Vector3.up * 3f;
                        }
                        else if (pointType == PointType.Roof)
                        {
                            comparisonPoint = max.y;
                            rayOrigin = Vector3.down * 3f;
                        }

                        Vector3 raypos = new Vector3(-x, 0, z);
                        Vector3 objPoint = obj.transform.position + obj.transform.rotation * (raypos / pointSpacing);
                        Vector3 adjustedPoint = objPoint + (obj.transform.up * floorDecorationOffset);

                        Vector3 rayStart = obj.transform.position + rayOrigin + obj.transform.rotation * (raypos / pointSpacing);


                        if (adjustedPoint.x == min.x && adjustedPoint.y == comparisonPoint)
                        {
                            continue;
                        }

                        if (adjustedPoint.x >= max.x && adjustedPoint.y == comparisonPoint)
                        {
                            continue;
                        }

                        if (adjustedPoint.z <= min.z && adjustedPoint.y == comparisonPoint)
                        {
                            continue;
                        }

                        if (adjustedPoint.z >= max.z && adjustedPoint.y == comparisonPoint)
                        {
                            continue;
                        }

                        if (!WallDistanceCheck(adjustedPoint, boxCollider))
                        {
                            continue;
                        }


                        if (tile.alignToSurface)
                        {
                            // Calculate varying height dimension for floor or roof tiles.
                            Ray ray = new Ray();

                            if (pointType == PointType.Roof)
                            {
                                //Raycast upward to check the roof level.
                                ray = new Ray(rayStart, Vector3.up);
                            }
                            else if (pointType == PointType.Floor)
                            {
                                //raycast downward to check the ground level.
                                ray = new Ray(rayStart, Vector3.down);
                            }

                            RaycastHit hit;

                            if (Physics.Raycast(ray, out hit, 1000f, tile.tileLayer))
                            {
                                DecoratorPoint newRaycastPoint = new DecoratorPoint(obj, null, hit.point, pointType, false, levelNumber);
                                decoratorPoints.Add(newRaycastPoint);
                            }
                        }
                        else
                        {
                            DecoratorPoint newPoint = new DecoratorPoint(obj, null, adjustedPoint, pointType, false, levelNumber);
                            decoratorPoints.Add(newPoint);
                        }
                    }
                }
            }
        }


        IEnumerator SpawnTiles(BoxCollider boxCollider, RoomPreset preset, Level level)
        {
            Random.InitState(level.decorSeed);
            Random.InitState(level.roomSeed);

            if (level.levelHeight > 0)
            {
                SpawnDoors(boxCollider, level);
                SpawnWindows(boxCollider, level);
            }

            SpawnWallsAndFloors(boxCollider, preset, levels.IndexOf(level));
            yield return new WaitForSeconds(0.0001f);
            Decorate(boxCollider, level, level.preset.wallDecorations, PointType.Wall, DecorationType.Wall);
            Decorate(boxCollider, level, level.preset.floorDecorations, PointType.Floor, DecorationType.Floor);
            Decorate(boxCollider, level, level.preset.roofDecorations, PointType.Roof, DecorationType.Roof);
            Decorate(boxCollider, level, level.preset.characters, PointType.Floor, DecorationType.Character);
        }


        void SpawnWallsAndFloors(BoxCollider boxCollider, RoomPreset preset, int levelNumber)
        {
            foreach (Node node in nodes)
            {
                // Wall Corners
                if (node.tileType == TileType.WallCorner && node.isAvailable && node.levelNumber == levelNumber)
                {
                    Wall wall = Tools.GetWeightedWall(preset.wallCorners, totalWallCornerWeight);
                    if (wall == null || wall.prefab == null)
                        continue;
#if UNITY_EDITOR
                    GameObject obj = PrefabUtility.InstantiatePrefab(wall.prefab) as GameObject;
#else
                    GameObject obj = Instantiate(wall.prefab) as GameObject;
#endif
                    obj.transform.position = node.position;
                    obj.transform.rotation = node.rotation;
                    Tools.AdjustPosition(obj, wall.positionOffset);
                    Tools.AdjustRotation(obj, wall.rotationOffset);
                    tiles.Add(obj);
                    obj.transform.parent = parent.transform;

                    if (wall.allowDecor)
                    {
                        SpawnWallDecoratorPoints(boxCollider, obj, wall, PointType.Wall, levelNumber);
                    }
                }

                // Spawn walls
                if (node.tileType == TileType.Wall && node.isAvailable && node.levelNumber == levelNumber)
                {
                    Wall wall = Tools.GetWeightedWall(preset.wallTiles, totalWallWeight);
                    if (wall == null || wall.prefab == null)
                        continue;
#if UNITY_EDITOR
                    GameObject obj = PrefabUtility.InstantiatePrefab(wall.prefab) as GameObject;
#else
                    GameObject obj = Instantiate(wall.prefab) as GameObject;
#endif
                    obj.transform.position = node.position;
                    obj.transform.rotation = node.rotation;
                    Tools.AdjustPosition(obj, wall.positionOffset);
                    Tools.AdjustRotation(obj, wall.rotationOffset);
                    tiles.Add(obj);
                    obj.transform.parent = parent.transform;

                    if (wall.allowDecor)
                    {
                        SpawnWallDecoratorPoints(boxCollider, obj, wall, PointType.Wall, levelNumber);
                    }
                }

                // Spawn Floors
                if (node.tileType == TileType.Floor && node.isAvailable && node.levelNumber == levelNumber)
                {
                    Floor floor = Tools.GetWeightedFloor(preset.floorTiles, totalFloorWeight);
                    if (floor == null || floor.prefab == null)
                        continue;

#if UNITY_EDITOR
                    GameObject obj = PrefabUtility.InstantiatePrefab(floor.prefab) as GameObject;
#else
                    GameObject obj = Instantiate(floor.prefab) as GameObject;
#endif
                    obj.transform.position = node.position;
                    obj.transform.rotation = node.rotation;
                    int randomRotation = Random.Range(0, floor.randomRotation + 1);
                    Tools.AdjustPosition(obj, floor.positionOffset, true, randomRotation);
                    Tools.AdjustRotation(obj, floor.rotationOffset, true, randomRotation);
                    tiles.Add(obj);
                    obj.transform.parent = parent.transform;

                    if (floor.allowDecor)
                    {
                        SpawnFloorRoofDecoratorPoints(boxCollider, obj, floor, PointType.Floor, levelNumber);
                    }
                }


                // Spawn Roofs
                if (node.tileType == TileType.Roof && node.isAvailable && node.levelNumber == levelNumber)
                {
                    Roof roof = Tools.GetWeightedRoof(preset.roofTiles, totalRoofWeight);
                    if (roof == null || roof.prefab == null)
                        continue;

#if UNITY_EDITOR
                    GameObject obj = PrefabUtility.InstantiatePrefab(roof.prefab) as GameObject;
#else
                    GameObject obj = Instantiate(roof.prefab) as GameObject;
#endif
                    obj.transform.position = node.position;
                    obj.transform.rotation = node.rotation;
                    int randomRotation = Random.Range(0, roof.randomRotation + 1);
                    Tools.AdjustRotation(obj, roof.rotationOffset, true, randomRotation);
                    Tools.AdjustPosition(obj, roof.positionOffset, true, randomRotation);
                    tiles.Add(obj);
                    obj.transform.parent = parent.transform;

                    if (roof.allowDecor)
                    {
                        SpawnFloorRoofDecoratorPoints(boxCollider, obj, roof, PointType.Roof, levelNumber);
                    }
                }
            }
        }


        Node RandomNode(List<float> heights, TileType tileType)
        {
            List<Node> matchingNodes = new List<Node>();

            foreach (Node node in nodes)
            {
                foreach (float h in heights)
                {
                    if (node.isAvailable && node.position.y == h && node.tileType == tileType)
                    {
                        matchingNodes.Add(node);
                    }
                }
            }

            if (matchingNodes.Count > 0)
            {
                return matchingNodes[Random.Range(0, matchingNodes.Count)];
            }

            return null;
        }

        Node FindClosestNode(Vector3 referencePoint, float levelYMin, TileType tileType)
        {
            Node closestNode = null;
            float closestDistance = Mathf.Infinity;
            
            foreach (Node node in nodes)
            {
                float distance = Vector3.Distance(referencePoint, node.position);
                if (node.isAvailable && Math.Abs(node.position.y - levelYMin) < 0.01f && node.tileType == tileType)
                {
                    if (closestNode == null || distance < closestDistance)
                    {
                        closestNode = node;
                        closestDistance = distance;
                    }
                }
            }

            return closestNode;
        }


        void SpawnDoors(BoxCollider boxCollider, Level level)
        {
            int levelNumber = levels.IndexOf(level);
            float yMin = boxCollider.bounds.min.y;

            List<float> floorHeights = new List<float>();

            floorHeights.Add(yMin);
            
            
            List<DoorPin> doorPinsForLevel = doorPins.Where(x => x.transform.position.y <= boxCollider.bounds.max.y && x.transform.position.y >= boxCollider.bounds.min.y).ToList();

            //Create a door for each door pin in the scene.
            for (int x = 0; x < doorPinsForLevel.Count; x++)
            {
                DoorPin doorPin = doorPinsForLevel[x];
                Node node = FindClosestNode(doorPin.transform.position, yMin, TileType.Wall);
                Door door = Tools.GetWeightedDoor(level.preset.doorTiles, totalDoorWeight);

                if (door == null || door.prefab == null)
                    continue;
#if UNITY_EDITOR
                GameObject doorObj = PrefabUtility.InstantiatePrefab(door.prefab) as GameObject;
#else
                 GameObject doorObj = Instantiate(door.prefab) as GameObject;
#endif
                doorObj.transform.position = node.position;
                doorObj.transform.rotation = node.rotation;
                Tools.AdjustPosition(doorObj, door.positionOffset);
                Tools.AdjustRotation(doorObj, door.rotationOffset);
                tiles.Add(doorObj);
                doorObj.transform.parent = parent.transform;
                node.isAvailable = false;

                if (door.allowDecor)
                {
                    SpawnWallDecoratorPoints(boxCollider, doorObj, door, PointType.Door, levelNumber);
                }
            }

            //Check to see if there are any remaining doors that should be placed randomly.
            int remainingDoors = level.numDoors - doorPinsForLevel.Count;
            if (remainingDoors <= 0) return;
            
            for (int i = 0; i < remainingDoors; i++)
            {

                Node node = RandomNode(floorHeights, TileType.Wall);

                if (node == null)
                {
                    continue;
                }

                Door door = Tools.GetWeightedDoor(level.preset.doorTiles, totalDoorWeight);

                if (door == null || door.prefab == null)
                    continue;
#if UNITY_EDITOR
                GameObject doorObj = PrefabUtility.InstantiatePrefab(door.prefab) as GameObject;
#else
                 GameObject doorObj = Instantiate(door.prefab) as GameObject;
#endif
                doorObj.transform.position = node.position;
                doorObj.transform.rotation = node.rotation;
                Tools.AdjustPosition(doorObj, door.positionOffset);
                Tools.AdjustRotation(doorObj, door.rotationOffset);
                tiles.Add(doorObj);
                doorObj.transform.parent = parent.transform;
                node.isAvailable = false;

                if (door.allowDecor)
                {
                    SpawnWallDecoratorPoints(boxCollider, doorObj, door, PointType.Door, levelNumber);
                }
             }
        }

        void SpawnWindows(BoxCollider boxCollider, Level level)
        {
            int levelNumber = levels.IndexOf(level);
            float yMin = boxCollider.bounds.min.y;

            List<float> floorHeights = new List<float>();

            float wHeight = yMin + ((level.windowHeight - 1) * tileSize);
            floorHeights.Add(wHeight);

            for (int i = 0; i < level.numWindows; i++)
            {
                Node node = RandomNode(floorHeights, TileType.Wall);

                if (node == null)
                {
                    continue;
                }

                Window window = Tools.GetWeightedWindow(level.preset.windowTiles, totalWindowWeight);

                if (window == null || window.prefab == null)
                    continue;

#if UNITY_EDITOR
                GameObject windowObj = PrefabUtility.InstantiatePrefab(window.prefab) as GameObject;
#else
                GameObject windowObj = Instantiate(window.prefab) as GameObject;
#endif
                windowObj.transform.position = node.position;
                windowObj.transform.rotation = node.rotation;
                Tools.AdjustPosition(windowObj, window.positionOffset);
                Tools.AdjustRotation(windowObj, window.rotationOffset);
                tiles.Add(windowObj);
                windowObj.transform.parent = parent.transform;
                node.isAvailable = false;

                if (window.allowDecor)
                {
                    SpawnWallDecoratorPoints(boxCollider, windowObj, window, PointType.Window, levelNumber);
                }
            }
        }


        void Decorate(BoxCollider boxCollider, Level level, List<Decoration> decorList, PointType pointType, DecorationType decorType)
        {
            int levelNumber = levels.IndexOf(level);
            if (boxCollider == null)
                return;

            if (decorList.Count == 0)
            {
                return;
            }

            switch (decorType)
            {
                case DecorationType.Character:
                    Random.InitState(level.characterSeed);
                    break;
                case DecorationType.Wall:
                    Random.InitState(level.decorSeed);
                    break;
                case DecorationType.Floor:
                    Random.InitState(level.decorSeed);
                    break;
                case DecorationType.Roof:
                    Random.InitState(level.decorSeed);
                    break;
            }

            Bounds bounds = boxCollider.bounds;

            // For every tile in our preset, do the following.
            for (int i = 0; i < decorList.Count; i++)
            {
                // Grab the tile from our decoration collection.
                Decoration decoration = decorList[i];

                // Get a random amount based on the provided min/max specified.
                int randomAmount = Random.Range(decoration.amountRange.x, decoration.amountRange.y);


                float minNodeHeight = decoration.verticalRange.x + bounds.min.y;
                float maxNodeHeight = decoration.verticalRange.y + bounds.min.y;
                List<DecoratorPoint> validPoints = new List<DecoratorPoint>(Tools.GetValidPoints(decoratorPoints, false, pointType, minNodeHeight, maxNodeHeight, levelNumber, bounds));

                // Spawn the amount of wallDecorations required to meet our min/max spawning requirement.
                for (int decorCount = 0; decorCount < randomAmount; decorCount++)
                {
                    //Get a random point to spawn the decoration.
                    DecoratorPoint randomPoint = Tools.RandomPoint(validPoints, false, pointType, decoration.prefab);

                    // If the decorSafeArea is too large, decor points won't be spawned, so we won't have anywhere to create characters/decor prefabs.
                    if (randomPoint == null || decoration.prefab == null)
                    {
                        continue;
                    }

                    // The Spawning buffer will prevent items of the same type from spawning too close together.
                    for (int pointIndex = validPoints.Count - 1; pointIndex >= 0; pointIndex--)
                    {
                        DecoratorPoint point = validPoints[pointIndex];
                        if (Vector3.Distance(point.point, randomPoint.point) <= decoration.spacingBuffer)
                        {
                            validPoints.Remove(point);
                        }
                    }

                    randomPoint.occupied = true;

#if UNITY_EDITOR
                    GameObject decor = PrefabUtility.InstantiatePrefab(decoration.prefab) as GameObject;
#else
GameObject decor = Instantiate(decoration.prefab) as GameObject;
#endif
                    decor.transform.position = randomPoint.point;

                    if (randomPoint.tileObject != null)
                    {
                        decor.transform.rotation *= randomPoint.tileObject.transform.rotation;
                    }


                    decor.transform.rotation *= Quaternion.Euler(decoration.rotationOffset);
                    Tools.AdjustPosition(decor, decoration.positionOffset);

                    switch (pointType)
                    {
                        case PointType.Wall:
                            decor.transform.Rotate(transform.forward * Random.Range(0, decoration.randomRotation), Space.Self);
                            break;
                        default:
                            decor.transform.Rotate(Vector3.up * Random.Range(0, decoration.randomRotation), Space.World);
                            break;
                        //case PointType.Character:
                        //    decor.transform.Rotate(Vector3.up * Random.Range(0, decoration.randomRotation), Space.World);
                        //    break;
                        //case PointType.Roof:
                        //    decor.transform.Rotate(Vector3.up * Random.Range(0, decoration.randomRotation), Space.World);
                        //    break;
                    }

                    decor.transform.localScale *= Random.Range(decoration.scaleRange.x, decoration.scaleRange.y);
                    decor.transform.parent = parent.transform;
                    tiles.Add(decor);
                }
            }
        }


        bool WallDistanceCheck(Vector3 gridPoint, BoxCollider boxCollider)
        {
            safeAreaPoints.Clear();
            float stepSize = (1 / (float)pointSpacing);
            float XoffsetPoints = decorSafeArea * stepSize;
            float ZoffsetPoints = decorSafeArea * stepSize;
            float halfTileSize = tileSize / 2;
            //   float offset = offsetPoints * stepSize;

            float xMax = boxCollider.bounds.max.x - XoffsetPoints;
            float xMin = boxCollider.bounds.min.x + XoffsetPoints;
            float zMax = boxCollider.bounds.max.z - ZoffsetPoints;
            float zMin = boxCollider.bounds.min.z + ZoffsetPoints;

            safeAreaPoints.Add(new Vector3(xMax, 0, 0));
            safeAreaPoints.Add(new Vector3(xMin, 0, 0));
            safeAreaPoints.Add(new Vector3(0, 0, zMax));
            safeAreaPoints.Add(new Vector3(0, 0, zMin));

            if (gridPoint.x <= xMax + 0.1f && gridPoint.x >= xMin - 0.1f && gridPoint.z <= zMax + 0.1f && gridPoint.z >= zMin - 0.1f)
            {
                return true;
            }

            return false;
        }


        void CalculateWeights(Level level)
        {
            totalRoofWeight = 0;
            totalFloorWeight = 0;
            totalWallWeight = 0;
            totalWallCornerWeight = 0;
            totalDoorWeight = 0;
            totalWindowWeight = 0;

            foreach (Roof roof in level.preset.roofTiles)
            {
                totalRoofWeight += roof.weight;
            }

            foreach (Floor floor in level.preset.floorTiles)
            {
                totalFloorWeight += floor.weight;
            }

            foreach (Wall wall in level.preset.wallTiles)
            {
                totalWallWeight += wall.weight;
            }

            foreach (Wall wallCorner in level.preset.wallCorners)
            {
                totalWallCornerWeight += wallCorner.weight;
            }

            foreach (Door door in level.preset.doorTiles)
            {
                totalDoorWeight += door.weight;
            }

            foreach (Window window in level.preset.windowTiles)
            {
                totalWindowWeight += window.weight;
            }
        }


        #region events

        void SubscribeToEvents()
        {
            EventSystem.instance.OnGenerate += GenerateRoom;
            EventSystem.instance.OnSetRoomPreset += SetRoomPreset;
            EventSystem.instance.OnSetGridSize += SetGridSize;
            EventSystem.instance.OnSetRoomSeed += SetRoomSeed;
            EventSystem.instance.OnSetDecorSeed += SetDecorSeed;
            EventSystem.instance.OnSetCharacterSeed += SetCharacterSeed;
            EventSystem.instance.OnSetDoorCount += SetDoorCount;
            EventSystem.instance.OnSetWindowCount += SetWindowCount;
            EventSystem.instance.OnSetLevelHeight += SetLevelHeight;
            EventSystem.instance.OnSetLevelOffset += SetLevelOffset;
        }

        void UnsubscribeFromEvents()
        {
            EventSystem.instance.OnGenerate -= GenerateRoom;
            EventSystem.instance.OnSetRoomPreset -= SetRoomPreset;
            EventSystem.instance.OnSetGridSize -= SetGridSize;
            EventSystem.instance.OnSetRoomSeed -= SetRoomSeed;
            EventSystem.instance.OnSetDecorSeed -= SetDecorSeed;
            EventSystem.instance.OnSetCharacterSeed -= SetCharacterSeed;
            EventSystem.instance.OnSetDoorCount -= SetDoorCount;
            EventSystem.instance.OnSetWindowCount -= SetWindowCount;
            EventSystem.instance.OnSetLevelHeight -= SetLevelHeight;
            EventSystem.instance.OnSetLevelOffset -= SetLevelOffset;
        }

        private void SetRoomPreset(int generatorID, RoomPreset newPreset, int levelNumber)
        {
            if (generatorID != this.id) return;
            
            if (levels.Count >= levelNumber)
            {
                this.levels[levelNumber].preset = newPreset;
            }
            else
            {
                Debug.LogWarning($"Attempted to set a new preset on level: {levelNumber}, but that level didn't exist.", this.gameObject);
            }
        }

        private void SetGridSize(int generatorID, int x, int z)
        {
            if (generatorID != this.id) return;
            if (x <= 0 || z <= 0)
            {
                Debug.LogWarning("RoomGen grid size values must be greater than or equal to 1.");
                return;
            }

            gridX = x;
            gridZ = z;
        }

        private void SetRoomSeed(int generatorID, int levelNumber, int seed)
        {
            if (generatorID != this.id) return;
            if (LevelIndexCheck("room seed", levelNumber))
            {
                levels[levelNumber].roomSeed = seed;
            }
        }
        
        

        private void SetDecorSeed(int generatorID, int levelNumber, int seed)
        {
            if (generatorID != this.id) return;
            if (LevelIndexCheck("decor seed", levelNumber))
            {
                levels[levelNumber].decorSeed = seed;
            }
        }

        private void SetCharacterSeed(int generatorID, int levelNumber, int seed)
        {
            if (generatorID != this.id) return;
            if (LevelIndexCheck("character seed", levelNumber))
            {
                levels[levelNumber].characterSeed = seed;
            }
        }

        private void SetDoorCount(int generatorID, int levelNumber, int count)
        {
            if (generatorID != this.id) return;
            if (LevelIndexCheck("door count", levelNumber))
            {
                levels[levelNumber].numDoors = count;
            }
        }

        private void SetWindowCount(int generatorID, int levelNumber, int count)
        {
            if (generatorID != this.id) return;
            if (LevelIndexCheck("window count", levelNumber))
            {
                levels[levelNumber].numWindows = count;
            }
        }

        private void SetLevelHeight(int generatorID, int levelNumber, int height)
        {
            if (generatorID != this.id) return;
            if (LevelIndexCheck("level height", levelNumber))
            {
                levels[levelNumber].levelHeight = height;
            }
        }

        private void SetLevelOffset(int generatorID, int levelNumber, Vector3 offset)
        {
            if (generatorID != this.id) return;
            if (LevelIndexCheck("level offset", levelNumber))
            {
                levels[levelNumber].levelOffset = offset;
            }
        }

        private bool LevelIndexCheck(string detail, int levelNumber)
        {
            if (levels.Count - 1 < levelNumber)
            {
                Debug.LogWarning("Attempted to adjust " + detail + " for level " + levelNumber + " but couldn't find a matching level at that index. Levels start at index 0.");
                return false;
            }

            return true;
        }

        #endregion
        
        
        private void OnEnable()
        {
            SubscribeToEvents();
        }

        private void OnDisable()
        {
           
        }


        public void RemoveBounds()
        {
            //Destroy box collider bounds
            foreach (Transform child in transform)
            {
                DestroyImmediate(child.gameObject);
            }
        }


        public void DestroyRoom()
        {
            foreach (GameObject tile in tiles)
            {
                DestroyImmediate(tile);
            }


            nodes.Clear();
            decoratorPoints.Clear();
            generatedWallDecor.Clear();
            generatedFloorDecor.Clear();
            generatedCharacters.Clear();
            tiles.Clear();
            RemoveBounds();
        }


        #region inspectorValidations;

        public void UpdateStoredValues()
        {
            walls = preset.wallTiles;
            doors = preset.doorTiles;
            floors = preset.floorTiles;
            windows = preset.windowTiles;
            characters = preset.characters;
            wallDecorations = preset.wallDecorations;
            floorDecorations = preset.floorDecorations;

            // save them to the preset.
            UpdatePreset();
        }

        public void UpdatePreset()
        {
            preset.wallTiles = walls;
            preset.doorTiles = doors;
            preset.floorTiles = floors;
            preset.windowTiles = windows;
            preset.characters = characters;
            preset.wallDecorations = wallDecorations;
            preset.floorDecorations = floorDecorations;
        }

        #endregion


        private void OnDrawGizmos()
        {
            if (!debug)
                return;
            foreach (BoxCollider boxCollider in GetComponentsInChildren<BoxCollider>())
            {
                Vector3 max = boxCollider.bounds.max;
                Vector3 min = boxCollider.bounds.min;

                //foreach (Node point in nodes)
                //{
                //    Gizmos.color = Color.green;
                //    Gizmos.DrawSphere(point.position, 0.125f);
                //}


                //Gizmos.color = Color.red;
                //Gizmos.DrawSphere(max, 0.21f);

                //Gizmos.color = Color.blue;
                //Gizmos.DrawSphere(min, 0.21f);
            }

            foreach (var point in safeAreaPoints)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawSphere(point, 0.125f);
            }

            foreach (Node node in nodes)
            {
                //if (node.tileType == TileType.WallCorner)
                //{
                //    Gizmos.color = Color.red;
                //    Gizmos.DrawSphere(node.position, 0.125f);
                //}

                //if (node.tileType == TileType.Floor)
                //{
                //    Gizmos.color = Color.green;
                //    Gizmos.DrawSphere(node.position, 0.125f);
                //}

                //if (node.tileType == TileType.Roof)
                //{
                //    Gizmos.color = Color.yellow;
                //    Gizmos.DrawSphere(node.position, 0.125f);
                //}
            }

            foreach (DecoratorPoint point in decoratorPoints)
            {
                if (point.pointType == PointType.Wall)
                {
                    Gizmos.color = Color.white;
                    Gizmos.DrawWireSphere(point.point, 0.125f);
                }

                if (point.pointType == PointType.Floor)
                {
                    Gizmos.color = Color.green;
                    Gizmos.DrawWireSphere(point.point, 0.125f);
                }

                if (point.pointType == PointType.Window)
                {
                    Gizmos.color = Color.red;
                    Gizmos.DrawWireSphere(point.point, 0.125f);
                }

                if (point.pointType == PointType.Door)
                {
                    Gizmos.color = Color.red;
                    Gizmos.DrawWireSphere(point.point, 0.125f);
                }

                if (point.pointType == PointType.Roof)
                {
                    Gizmos.color = Color.blue;
                    Gizmos.DrawWireSphere(point.point, 0.125f);
                }
            }
        }


        public void Save()
        {
            generatedFloorDecor.Clear();
            generatedWallDecor.Clear();
            decoratorPoints.Clear();
            DestroyImmediate(parent);


            foreach (Transform child in transform)
            {
                DestroyImmediate(child.gameObject);
            }

            Debug.Log("room prefab saved.");
        }
    }

    [System.Serializable]
    public enum TileType
    {
        Floor,
        Wall,
        WallCorner,
        Roof
    }

    [System.Serializable]
    public enum DecorationType
    {
        Door,
        Window,
        Wall,
        Floor,
        Character,
        Roof
    }
}