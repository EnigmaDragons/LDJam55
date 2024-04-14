using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

namespace RoomGen
{

    [CustomEditor(typeof(PresetEditorComponent))]
    public class PresetEditor : Editor
    {

        PresetEditorComponent presetEditorComp;

        SerializedProperty preset;


        SerializedProperty roofs;
        SerializedProperty floors;
        SerializedProperty walls;
        SerializedProperty wallCorners;
        SerializedProperty doors;
        SerializedProperty windows;
        SerializedProperty characters;
        SerializedProperty roofDecorations;
        SerializedProperty floorDecorations;
        SerializedProperty wallDecorations;

        ReorderableList roofList;
        ReorderableList floorList;
        ReorderableList wallList;
        ReorderableList wallCornerList;
        ReorderableList doorList;
        ReorderableList windowList;
        ReorderableList characterList;
        ReorderableList roofDecorList;
        ReorderableList floorDecorList;
        ReorderableList wallDecorList;

        bool showRoofs;
        bool showFloors;
        bool showWalls;
        bool showWallCorners;
        bool showDoors;
        bool showWindows;
        bool showCharacters;
        bool showRoofDecor;
        bool showFloorDecor;
        bool showWallDecor;

        float lineHeight;
        float lineHeightSpace;


        void AssignProperties()
        {
            lineHeight = EditorGUIUtility.singleLineHeight;
            lineHeightSpace = EditorGUIUtility.standardVerticalSpacing;
            presetEditorComp = (PresetEditorComponent)target;

            preset = serializedObject.FindProperty("preset");

            if (presetEditorComp.preset != null)
            {
                preset.objectReferenceValue = presetEditorComp.preset;
            }

            roofs = serializedObject.FindProperty("roofs");
            floors = serializedObject.FindProperty("floors");
            walls = serializedObject.FindProperty("walls");
            wallCorners = serializedObject.FindProperty("wallCorners");
            doors = serializedObject.FindProperty("doors");
            windows = serializedObject.FindProperty("windows");

            characters = serializedObject.FindProperty("characters");
            roofDecorations = serializedObject.FindProperty("roofDecorations");
            wallDecorations = serializedObject.FindProperty("wallDecorations");
            floorDecorations = serializedObject.FindProperty("floorDecorations");


        }

        private void OnEnable()
        {


            if (target == null)
                return;

            AssignProperties();

            CreateRoofList();
            CreateFloorList();
            CreateWallList();
            CreateWallCornerList();
            CreateDoorList();
            CreateWindowList();

            CreateCharacterList();
            CreateRoofDecorList();
            CreateFloorDecorList();
            CreateWallDecorList();



        }




        public override void OnInspectorGUI()
        {

            serializedObject.Update();

            GUIStyle headerStyle = new GUIStyle(EditorStyles.boldLabel);
            headerStyle.fontStyle = FontStyle.Bold;
            headerStyle.fontSize = 12;


            GUIStyle foldoutStyle = new GUIStyle(EditorStyles.foldout);
            foldoutStyle.fontStyle = FontStyle.Bold;
            foldoutStyle.fontSize = 12;

            float listSpacing = 2f;


            EditorGUILayout.Space();
            EditorGUILayout.Space();


            // Preset Editor
            GUIStyle largeHeaderStyle = new GUIStyle(EditorStyles.boldLabel);
            largeHeaderStyle.fontStyle = FontStyle.Bold;
            largeHeaderStyle.fontSize = 14;

            Color standardBGColor = GUI.backgroundColor;
            GUI.backgroundColor = new Color(0.1f, 0.1f, 0.1f, 1);
            EditorGUILayout.BeginVertical("Box");
            EditorGUILayout.Space(8);
            EditorGUILayout.BeginHorizontal();
            GUILayout.Box("Preset Editor", largeHeaderStyle);
            EditorGUILayout.Space();
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.HelpBox(new GUIContent("Drag a preset into the slot to make changes. Every change will display live in the SceneView for each level using this preset."));
            EditorGUILayout.EndVertical();
            GUI.backgroundColor = standardBGColor;


            EditorGUILayout.ObjectField(preset);

            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Tiles", headerStyle);



            #region roofs

            EditorGUILayout.BeginVertical();
            EditorGUILayout.BeginHorizontal();
            showRoofs = EditorGUILayout.Foldout(showRoofs, "Roofs", true, foldoutStyle);
            EditorGUILayout.IntField(roofList.count);
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Space(listSpacing);
            if (showRoofs)
            {
                EditorGUILayout.Space();
                roofList.DoLayoutList();
            }

            EditorGUILayout.EndVertical();

            #endregion roofs

            #region floors

            EditorGUILayout.BeginVertical();
            EditorGUILayout.BeginHorizontal();
            showFloors = EditorGUILayout.Foldout(showFloors, "Floors", true, foldoutStyle);
            EditorGUILayout.IntField(floorList.count);
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Space(listSpacing);
            if (showFloors)
            {
                EditorGUILayout.Space();
                floorList.DoLayoutList();
            }

            EditorGUILayout.EndVertical();

            #endregion floors

            #region walls

            EditorGUILayout.BeginVertical();
            EditorGUILayout.BeginHorizontal();
            showWalls = EditorGUILayout.Foldout(showWalls, "Walls", true, foldoutStyle);
            EditorGUILayout.IntField(wallList.count);
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Space(listSpacing);
            if (showWalls)
            {
                EditorGUILayout.Space();
                wallList.DoLayoutList();
            }

            EditorGUILayout.EndVertical();


            // Wall Corners
            EditorGUILayout.BeginVertical();
            EditorGUILayout.BeginHorizontal();
            showWallCorners = EditorGUILayout.Foldout(showWallCorners, "Wall Corners", true, foldoutStyle);
            EditorGUILayout.IntField(wallCornerList.count);
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Space(listSpacing);
            if (showWallCorners)
            {
                EditorGUILayout.Space();
                wallCornerList.DoLayoutList();
            }

            EditorGUILayout.EndVertical();

            #endregion walls

            #region doors

            EditorGUILayout.BeginVertical();
            EditorGUILayout.BeginHorizontal();
            showDoors = EditorGUILayout.Foldout(showDoors, "Doors", true, foldoutStyle);
            EditorGUILayout.IntField(doorList.count);
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Space(listSpacing);
            if (showDoors)
            {
                EditorGUILayout.Space();
                doorList.DoLayoutList();
            }

            EditorGUILayout.EndVertical();

            #endregion doors

            #region windows

            EditorGUILayout.BeginVertical();
            EditorGUILayout.BeginHorizontal();
            showWindows = EditorGUILayout.Foldout(showWindows, "Windows", true, foldoutStyle);
            EditorGUILayout.IntField(doorList.count);
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Space(listSpacing);
            if (showWindows)
            {
                EditorGUILayout.Space();
                windowList.DoLayoutList();
            }

            EditorGUILayout.EndVertical();

            #endregion windows


            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Decorations", headerStyle);

            #region characters

            EditorGUILayout.BeginVertical();
            EditorGUILayout.BeginHorizontal();
            showCharacters = EditorGUILayout.Foldout(showCharacters, "Characters", true, foldoutStyle);
            EditorGUILayout.IntField(characterList.count);
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Space(listSpacing);
            if (showCharacters)
            {
                EditorGUILayout.Space();
                characterList.DoLayoutList();
            }

            EditorGUILayout.EndVertical();

            #endregion characters

            #region roof decorations

            EditorGUILayout.BeginVertical();
            EditorGUILayout.BeginHorizontal();
            showRoofDecor = EditorGUILayout.Foldout(showRoofDecor, "Roof decorations", true, foldoutStyle);
            EditorGUILayout.IntField(roofDecorList.count);
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Space(listSpacing);
            if (showRoofDecor)
            {
                EditorGUILayout.Space();
                roofDecorList.DoLayoutList();
            }

            EditorGUILayout.EndVertical();

            #endregion roof decorations

            #region floor decorations

            EditorGUILayout.BeginVertical();
            EditorGUILayout.BeginHorizontal();
            showFloorDecor = EditorGUILayout.Foldout(showFloorDecor, "Floor decorations", true, foldoutStyle);
            EditorGUILayout.IntField(floorDecorList.count);
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Space(listSpacing);
            if (showFloorDecor)
            {
                EditorGUILayout.Space();
                floorDecorList.DoLayoutList();
            }

            EditorGUILayout.EndVertical();

            #endregion floor decorations

            #region wall decorations

            EditorGUILayout.BeginVertical();
            EditorGUILayout.BeginHorizontal();
            showWallDecor = EditorGUILayout.Foldout(showWallDecor, "Wall decorations", true, foldoutStyle);
            EditorGUILayout.IntField(wallDecorList.count);
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Space(listSpacing);
            if (showWallDecor)
            {
                EditorGUILayout.Space();
                wallDecorList.DoLayoutList();
            }

            EditorGUILayout.EndVertical();

            #endregion wall decorations


            serializedObject.ApplyModifiedProperties();


            if (GUI.changed)
            {
                if (presetEditorComp.preset != null)
                {
                    presetEditorComp.UpdateStoredValues();
                    EditorUtility.SetDirty(presetEditorComp.preset);

                    GameObject parent = presetEditorComp.gameObject;

                    RoomGenerator roomGenerator = parent.GetComponent<RoomGenerator>();
                    if (roomGenerator != null)
                    {
                        roomGenerator.GenerateRoom(roomGenerator.id);
                    }

                    TargetGenerator targetGenerator = parent.GetComponent<TargetGenerator>();
                    if (targetGenerator != null)
                    {
                        targetGenerator.Generate(targetGenerator.id);
                    }
                }
            }
        }


        void CreateRoofList()
        {
            roofList = new ReorderableList(serializedObject, roofs, true, false, true, true);

            roofList.drawElementCallback = DrawRoofTileItems;
            roofList.onAddCallback = AddMainTileElement;
            roofList.elementHeightCallback = (index) =>
            {
                SerializedProperty element = roofList.serializedProperty.GetArrayElementAtIndex(index);
                SerializedProperty isExpanded = element.FindPropertyRelative("isExpanded");

                if (isExpanded.boolValue)
                {
                    return (lineHeight + lineHeightSpace) * 9;
                }

                return lineHeight;
            };
        }

        void CreateFloorList()
        {
            floorList = new ReorderableList(serializedObject, floors, true, false, true, true);

            floorList.drawElementCallback = DrawFloorTileItems;
            floorList.onAddCallback = AddMainTileElement;
            floorList.elementHeightCallback = (index) =>
            {
                SerializedProperty element = floorList.serializedProperty.GetArrayElementAtIndex(index);
                SerializedProperty isExpanded = element.FindPropertyRelative("isExpanded");

                if (isExpanded.boolValue)
                {
                    return (lineHeight + lineHeightSpace) * 9;
                }

                return lineHeight;
            };
        }

        void CreateWallList()
        {
            wallList = new ReorderableList(serializedObject, walls, true, false, true, true);

            wallList.drawElementCallback = DrawWallTileItems;
            wallList.onAddCallback = AddMainTileElement;
            wallList.elementHeightCallback = (index) =>
            {
                SerializedProperty element = wallList.serializedProperty.GetArrayElementAtIndex(index);
                SerializedProperty isExpanded = element.FindPropertyRelative("isExpanded");

                if (isExpanded.boolValue)
                {
                    return (lineHeight + lineHeightSpace) * 6;
                }

                return lineHeight;
            };
        }

        void CreateWallCornerList()
        {
            wallCornerList = new ReorderableList(serializedObject, wallCorners, true, false, true, true);

            wallCornerList.drawElementCallback = DrawWallCornerTileItems;
            wallCornerList.onAddCallback = AddMainTileElement;
            wallCornerList.elementHeightCallback = (index) =>
            {
                SerializedProperty element = wallCornerList.serializedProperty.GetArrayElementAtIndex(index);
                SerializedProperty isExpanded = element.FindPropertyRelative("isExpanded");

                if (isExpanded.boolValue)
                {
                    return (lineHeight + lineHeightSpace) * 6;
                }

                return lineHeight;
            };
        }

        void CreateDoorList()
        {
            doorList = new ReorderableList(serializedObject, doors, true, false, true, true);

            doorList.drawElementCallback = DrawDoorTileItems;
            doorList.onAddCallback = AddSecondaryTileElement;
            doorList.elementHeightCallback = (index) =>
            {
                SerializedProperty element = doorList.serializedProperty.GetArrayElementAtIndex(index);
                SerializedProperty isExpanded = element.FindPropertyRelative("isExpanded");

                if (isExpanded.boolValue)
                {
                    return (lineHeight + lineHeightSpace) * 6;
                }

                return lineHeight;
            };
        }

        void CreateWindowList()
        {
            windowList = new ReorderableList(serializedObject, windows, true, false, true, true);

            windowList.drawElementCallback = DrawWindowTileItems;
            windowList.onAddCallback = AddSecondaryTileElement;
            windowList.elementHeightCallback = (index) =>
            {
                SerializedProperty element = windowList.serializedProperty.GetArrayElementAtIndex(index);
                SerializedProperty isExpanded = element.FindPropertyRelative("isExpanded");

                if (isExpanded.boolValue)
                {
                    return (lineHeight + lineHeightSpace) * 6;
                }

                return lineHeight;
            };
        }

        void CreateCharacterList()
        {
            characterList = new ReorderableList(serializedObject, characters, true, false, true, true);

            characterList.drawElementCallback = DrawCharacterItems;
            characterList.onAddCallback = AddDecorElement;
            characterList.elementHeightCallback = (index) =>
            {
                SerializedProperty element = characterList.serializedProperty.GetArrayElementAtIndex(index);
                SerializedProperty isExpanded = element.FindPropertyRelative("isExpanded");

                if (isExpanded.boolValue)
                {
                    return (lineHeight + lineHeightSpace) * 8;
                }

                return lineHeight;
            };
        }

        void CreateRoofDecorList()
        {
            roofDecorList = new ReorderableList(serializedObject, roofDecorations, true, false, true, true);

            roofDecorList.drawElementCallback = DrawRoofDecorItems;
            roofDecorList.onAddCallback = AddDecorElement;
            roofDecorList.elementHeightCallback = (index) =>
            {
                SerializedProperty element = roofDecorList.serializedProperty.GetArrayElementAtIndex(index);
                SerializedProperty isExpanded = element.FindPropertyRelative("isExpanded");

                if (isExpanded.boolValue)
                {
                    return (lineHeight + lineHeightSpace) * 9;
                }

                return lineHeight;
            };
        }

        void CreateFloorDecorList()
        {
            floorDecorList = new ReorderableList(serializedObject, floorDecorations, true, false, true, true);

            floorDecorList.drawElementCallback = DrawFloorDecorItems;
            floorDecorList.onAddCallback = AddDecorElement;
            floorDecorList.elementHeightCallback = (index) =>
            {
                SerializedProperty element = floorDecorList.serializedProperty.GetArrayElementAtIndex(index);
                SerializedProperty isExpanded = element.FindPropertyRelative("isExpanded");

                if (isExpanded.boolValue)
                {
                    return (lineHeight + lineHeightSpace) * 9;
                }

                return lineHeight;
            };
        }

        void CreateWallDecorList()
        {
            wallDecorList = new ReorderableList(serializedObject, wallDecorations, true, false, true, true);

            wallDecorList.drawElementCallback = DrawWallDecorItems;
            wallDecorList.onAddCallback = AddDecorElement;
            wallDecorList.elementHeightCallback = (index) =>
            {
                SerializedProperty element = wallDecorList.serializedProperty.GetArrayElementAtIndex(index);
                SerializedProperty isExpanded = element.FindPropertyRelative("isExpanded");

                if (isExpanded.boolValue)
                {
                    return (lineHeight + lineHeightSpace) * 9;
                }

                return lineHeight;
            };
        }



        void DrawRoofTileItems(Rect rect, int index, bool isActive, bool isFocused)
        {
            SerializedProperty element = roofList.serializedProperty.GetArrayElementAtIndex(index);
            CreateFloorRoofTileList(element, rect, index);
        }

        void DrawFloorTileItems(Rect rect, int index, bool isActive, bool isFocused)
        {
            SerializedProperty element = floorList.serializedProperty.GetArrayElementAtIndex(index);
            CreateFloorRoofTileList(element, rect, index);
        }

        void DrawWallTileItems(Rect rect, int index, bool isActive, bool isFocused)
        {
            SerializedProperty element = wallList.serializedProperty.GetArrayElementAtIndex(index);
            CreateTileList(element, rect, index);
        }

        void DrawWallCornerTileItems(Rect rect, int index, bool isActive, bool isFocused)
        {
            SerializedProperty element = wallCornerList.serializedProperty.GetArrayElementAtIndex(index);
            CreateTileList(element, rect, index);
        }

        void DrawDoorTileItems(Rect rect, int index, bool isActive, bool isFocused)
        {
            SerializedProperty element = doorList.serializedProperty.GetArrayElementAtIndex(index);
            CreateTileList(element, rect, index);
        }

        void DrawWindowTileItems(Rect rect, int index, bool isActive, bool isFocused)
        {
            SerializedProperty element = windowList.serializedProperty.GetArrayElementAtIndex(index);
            CreateTileList(element, rect, index);
        }

        void DrawCharacterItems(Rect rect, int index, bool isActive, bool isFocused)
        {
            SerializedProperty element = characterList.serializedProperty.GetArrayElementAtIndex(index);
            CreateDecorList(element, rect, index);
        }

        void DrawRoofDecorItems(Rect rect, int index, bool isActive, bool isFocused)
        {
            SerializedProperty element = roofDecorList.serializedProperty.GetArrayElementAtIndex(index);
            CreateDecorList(element, rect, index);
        }

        void DrawFloorDecorItems(Rect rect, int index, bool isActive, bool isFocused)
        {
            SerializedProperty element = floorDecorList.serializedProperty.GetArrayElementAtIndex(index);
            CreateDecorList(element, rect, index);
        }

        void DrawWallDecorItems(Rect rect, int index, bool isActive, bool isFocused)
        {
            SerializedProperty element = wallDecorList.serializedProperty.GetArrayElementAtIndex(index);
            CreateDecorList(element, rect, index);
        }




        void CreateFloorRoofTileList(SerializedProperty element, Rect rect, int index)
        {

            SerializedProperty prefab = element.FindPropertyRelative("prefab");
            SerializedProperty weight = element.FindPropertyRelative("weight");
            SerializedProperty positionOffset = element.FindPropertyRelative("positionOffset");
            SerializedProperty rotationOffset = element.FindPropertyRelative("rotationOffset");
            SerializedProperty randomRotation = element.FindPropertyRelative("randomRotation");
            SerializedProperty allowDecor = element.FindPropertyRelative("allowDecor");
            SerializedProperty alignToSurface = element.FindPropertyRelative("alignToSurface");
            SerializedProperty tileLayer = element.FindPropertyRelative("tileLayer");
            SerializedProperty isExpanded = element.FindPropertyRelative("isExpanded");



            if (prefab.objectReferenceValue == null)
            {
                isExpanded.boolValue = EditorGUI.Foldout(new Rect(rect.x + 8f, rect.y, rect.width, lineHeight), isExpanded.boolValue, "Element " + index.ToString());
            }
            else
            {
                string name = prefab.objectReferenceValue.name;
                string trimmedString = "";

                if (name.Length > 30)
                {
                    trimmedString = name.Substring(0, 30);
                }
                else
                {
                    trimmedString = name;
                }

                isExpanded.boolValue = EditorGUI.Foldout(new Rect(rect.x + 8f, rect.y, rect.width, lineHeight), isExpanded.boolValue, trimmedString);

            }

            float indent = 10f;
            float trailingSpace = 80;

            if (isExpanded.boolValue)
            {

                if (prefab.objectReferenceValue != null)
                {
                    Texture2D assetPreview = AssetPreview.GetAssetPreview(prefab.objectReferenceValue);
                    EditorGUI.DrawPreviewTexture(new Rect(rect.width - 15, rect.y + 25, 50, 50), assetPreview);
                }

                EditorGUIUtility.labelWidth = 120;
                rect.y += lineHeight + lineHeightSpace + 2f;
                EditorGUI.PropertyField(new Rect(rect.x + indent, rect.y, rect.width - trailingSpace, lineHeight), prefab);
                rect.y += lineHeight + lineHeightSpace;
                EditorGUI.PropertyField(new Rect(rect.x + indent, rect.y, rect.width - trailingSpace, lineHeight), weight);
                rect.y += lineHeight + lineHeightSpace;
                EditorGUI.PropertyField(new Rect(rect.x + indent, rect.y, rect.width - trailingSpace, lineHeight), positionOffset);
                rect.y += lineHeight + lineHeightSpace;
                EditorGUI.PropertyField(new Rect(rect.x + indent, rect.y, rect.width - trailingSpace, lineHeight), rotationOffset);
                rect.y += lineHeight + lineHeightSpace;
                EditorGUI.PropertyField(new Rect(rect.x + indent, rect.y, rect.width - trailingSpace, lineHeight), randomRotation);
                rect.y += lineHeight + lineHeightSpace;
                EditorGUI.PropertyField(new Rect(rect.x + indent, rect.y, rect.width - trailingSpace, lineHeight), allowDecor);
                rect.y += lineHeight + lineHeightSpace;
                EditorGUI.PropertyField(new Rect(rect.x + indent, rect.y, rect.width - trailingSpace, lineHeight), alignToSurface);

                if (alignToSurface.boolValue == true)
                {
                    rect.y += lineHeight + lineHeightSpace;
                    EditorGUI.PropertyField(new Rect(rect.x + indent, rect.y, rect.width - trailingSpace, lineHeight), tileLayer);
                }

                EditorGUIUtility.labelWidth = 0;
            }
        }


        void CreateTileList(SerializedProperty element, Rect rect, int index)
        {

            SerializedProperty prefab = element.FindPropertyRelative("prefab");
            SerializedProperty weight = element.FindPropertyRelative("weight");
            SerializedProperty positionOffset = element.FindPropertyRelative("positionOffset");
            SerializedProperty rotationOffset = element.FindPropertyRelative("rotationOffset");
            SerializedProperty allowDecor = element.FindPropertyRelative("allowDecor");
            SerializedProperty isExpanded = element.FindPropertyRelative("isExpanded");



            if (prefab.objectReferenceValue == null)
            {
                isExpanded.boolValue = EditorGUI.Foldout(new Rect(rect.x + 8f, rect.y, rect.width, lineHeight), isExpanded.boolValue, "Element " + index.ToString());
            }
            else
            {
                string name = prefab.objectReferenceValue.name;
                string trimmedString = "";

                if (name.Length > 30)
                {
                    trimmedString = name.Substring(0, 30);
                }
                else
                {
                    trimmedString = name;
                }

                isExpanded.boolValue = EditorGUI.Foldout(new Rect(rect.x + 8f, rect.y, rect.width, lineHeight), isExpanded.boolValue, trimmedString);

            }

            float indent = 10f;
            float trailingSpace = 80;

            if (isExpanded.boolValue)
            {

                if (prefab.objectReferenceValue != null)
                {
                    Texture2D assetPreview = AssetPreview.GetAssetPreview(prefab.objectReferenceValue);
                    EditorGUI.DrawPreviewTexture(new Rect(rect.width - 15, rect.y + 25, 50, 50), assetPreview);
                }

                EditorGUIUtility.labelWidth = 120;
                rect.y += lineHeight + lineHeightSpace + 2f;
                EditorGUI.PropertyField(new Rect(rect.x + indent, rect.y, rect.width - trailingSpace, lineHeight), prefab);
                rect.y += lineHeight + lineHeightSpace;
                EditorGUI.PropertyField(new Rect(rect.x + indent, rect.y, rect.width - trailingSpace, lineHeight), weight);
                rect.y += lineHeight + lineHeightSpace;
                EditorGUI.PropertyField(new Rect(rect.x + indent, rect.y, rect.width - trailingSpace, lineHeight), positionOffset);
                rect.y += lineHeight + lineHeightSpace;
                EditorGUI.PropertyField(new Rect(rect.x + indent, rect.y, rect.width - trailingSpace, lineHeight), rotationOffset);
                rect.y += lineHeight + lineHeightSpace;
                EditorGUI.PropertyField(new Rect(rect.x + indent, rect.y, rect.width - trailingSpace, lineHeight), allowDecor);
                EditorGUIUtility.labelWidth = 0;
            }
        }


        void CreateDecorList(SerializedProperty element, Rect rect, int index)
        {

            SerializedProperty prefab = element.FindPropertyRelative("prefab");
            SerializedProperty positionOffset = element.FindPropertyRelative("positionOffset");
            SerializedProperty rotationOffset = element.FindPropertyRelative("rotationOffset");
            SerializedProperty spacingBuffer = element.FindPropertyRelative("spacingBuffer");
            SerializedProperty placementHeightRange = element.FindPropertyRelative("verticalRange");
            SerializedProperty amountRange = element.FindPropertyRelative("amountRange");
            SerializedProperty randomRotation = element.FindPropertyRelative("randomRotation");
            SerializedProperty scaleRange = element.FindPropertyRelative("scaleRange");
            SerializedProperty isExpanded = element.FindPropertyRelative("isExpanded");



            if (prefab.objectReferenceValue == null)
            {
                isExpanded.boolValue = EditorGUI.Foldout(new Rect(rect.x + 8f, rect.y, rect.width, lineHeight), isExpanded.boolValue, "Element " + index.ToString());
            }
            else
            {
                string name = prefab.objectReferenceValue.name;
                string trimmedString = "";

                if (name.Length > 30)
                {
                    trimmedString = name.Substring(0, 30);
                }
                else
                {
                    trimmedString = name;
                }

                isExpanded.boolValue = EditorGUI.Foldout(new Rect(rect.x + 8f, rect.y, rect.width, lineHeight), isExpanded.boolValue, trimmedString);

            }

            float indent = 10f;
            float trailingSpace = 80;

            if (isExpanded.boolValue)
            {

                if (prefab.objectReferenceValue != null)
                {
                    Texture2D assetPreview = AssetPreview.GetAssetPreview(prefab.objectReferenceValue);
                    EditorGUI.DrawPreviewTexture(new Rect(rect.width - 15, rect.y + 25, 50, 50), assetPreview);
                }

                EditorGUIUtility.labelWidth = 120;
                rect.y += lineHeight + lineHeightSpace + 2f;
                EditorGUI.PropertyField(new Rect(rect.x + indent, rect.y, rect.width - trailingSpace, lineHeight), prefab);
                rect.y += lineHeight + lineHeightSpace;
                EditorGUI.PropertyField(new Rect(rect.x + indent, rect.y, rect.width - trailingSpace, lineHeight), positionOffset);
                rect.y += lineHeight + lineHeightSpace;
                EditorGUI.PropertyField(new Rect(rect.x + indent, rect.y, rect.width - trailingSpace, lineHeight), rotationOffset);
                rect.y += lineHeight + lineHeightSpace;
                EditorGUI.PropertyField(new Rect(rect.x + indent, rect.y, rect.width - trailingSpace, lineHeight), spacingBuffer);
                rect.y += lineHeight + lineHeightSpace;
                EditorGUI.PropertyField(new Rect(rect.x + indent, rect.y, rect.width - trailingSpace, lineHeight), placementHeightRange);
                rect.y += lineHeight + lineHeightSpace;
                EditorGUI.PropertyField(new Rect(rect.x + indent, rect.y, rect.width - trailingSpace, lineHeight), amountRange);
                rect.y += lineHeight + lineHeightSpace;
                EditorGUI.PropertyField(new Rect(rect.x + indent, rect.y, rect.width - trailingSpace, lineHeight), randomRotation);
                rect.y += lineHeight + lineHeightSpace;
                EditorGUI.PropertyField(new Rect(rect.x + indent, rect.y, rect.width - trailingSpace, lineHeight), scaleRange);
            }
        }


        //Roofs, Floors, and walls
        void AddMainTileElement(ReorderableList list)
        {
            int index = list.serializedProperty.arraySize;
            list.serializedProperty.arraySize++;
            list.index = index;

            SerializedProperty element = list.serializedProperty.GetArrayElementAtIndex(index);
            //Set Floors and walls by default to show decorations. Saves users a click.
            element.FindPropertyRelative("allowDecor").boolValue = true;
            element.FindPropertyRelative("isExpanded").boolValue = true;
        }

        //Windows and doors
        void AddSecondaryTileElement(ReorderableList list)
        {
            int index = list.serializedProperty.arraySize;
            list.serializedProperty.arraySize++;
            list.index = index;

            SerializedProperty element = list.serializedProperty.GetArrayElementAtIndex(index);
            //Set windows and doors by default to not show decorations. Saves users a click.
            element.FindPropertyRelative("allowDecor").boolValue = false;
            element.FindPropertyRelative("isExpanded").boolValue = true;
        }



        void AddDecorElement(ReorderableList list)
        {
            int index = list.serializedProperty.arraySize;
            list.serializedProperty.arraySize++;
            list.index = index;

            SerializedProperty element = list.serializedProperty.GetArrayElementAtIndex(index);
            element.FindPropertyRelative("prefab").objectReferenceValue = null;
            element.FindPropertyRelative("positionOffset").vector3Value = Vector3.zero;
            element.FindPropertyRelative("rotationOffset").vector3Value = Vector3.zero;
            element.FindPropertyRelative("verticalRange").vector2Value = new Vector2(0, 100);
            element.FindPropertyRelative("randomRotation").floatValue = 0;
            element.FindPropertyRelative("scaleRange").vector2Value = new Vector2(1, 1);
            element.FindPropertyRelative("isExpanded").boolValue = true;

            list.serializedProperty.serializedObject.ApplyModifiedProperties();
        }


        public void OnInspectorUpdate()
        {
            this.Repaint();
        }
    }
}

