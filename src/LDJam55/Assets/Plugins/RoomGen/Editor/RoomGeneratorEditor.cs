using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

namespace RoomGen
{


    [CustomEditor(typeof(RoomGenerator))]
    public class RoomGeneratorEditor : Editor
    {
        RoomGenerator roomGenerator;

        SerializedProperty preset;

        SerializedProperty id;

        SerializedProperty levels;

        SerializedProperty debug;

        SerializedProperty gridX;
        SerializedProperty gridZ;

        SerializedProperty tileSize;

        SerializedProperty wallDecorationOffset;
        SerializedProperty floorDecorationOffset;
        SerializedProperty decorSafeArea;
        SerializedProperty pointSpacing;

        ReorderableList levelList;

        bool showLevels;

        float lineHeight;
        float lineHeightSpace;


        void AssignProperties()
        {
            lineHeight = EditorGUIUtility.singleLineHeight;
            lineHeightSpace = EditorGUIUtility.standardVerticalSpacing;
            roomGenerator = (RoomGenerator)target;


            preset = serializedObject.FindProperty("preset");

            id = serializedObject.FindProperty("id");

            levels = serializedObject.FindProperty("selectedLevel");
            levels = serializedObject.FindProperty("levels");
            debug = serializedObject.FindProperty("debug");

            gridX = serializedObject.FindProperty("gridX");
            gridZ = serializedObject.FindProperty("gridZ");

            tileSize = serializedObject.FindProperty("tileSize");


            wallDecorationOffset = serializedObject.FindProperty("wallDecorationOffset");
            floorDecorationOffset = serializedObject.FindProperty("floorDecorationOffset");
            decorSafeArea = serializedObject.FindProperty("decorSafeArea");
            pointSpacing = serializedObject.FindProperty("pointSpacing");





        }

        private void OnEnable()
        {
            if (target == null)
                return;

            AssignProperties();

            CreateLevelList();
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

            EditorGUILayout.PropertyField(id, new GUIContent("ID"));

            EditorGUILayout.Space();
            EditorGUILayout.Space();


            #region levels

            EditorGUILayout.BeginVertical();
            EditorGUILayout.BeginHorizontal();
            showLevels = EditorGUILayout.Foldout(showLevels, "Levels", true, foldoutStyle);
            EditorGUILayout.IntField(levelList.count);
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Space(listSpacing);
            if (showLevels)
            {
                EditorGUILayout.Space();
                levelList.DoLayoutList();
            }

            EditorGUILayout.EndVertical();

            #endregion levels


            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Room Size", headerStyle);
            EditorGUILayout.IntSlider(gridX, 1, 25);
            EditorGUILayout.IntSlider(gridZ, 1, 25);

            tileSize.floatValue = EditorGUILayout.FloatField("Tile Size", tileSize.floatValue);


            floorDecorationOffset.floatValue = EditorGUILayout.FloatField("Floor Decor Offset", floorDecorationOffset.floatValue);
            wallDecorationOffset.floatValue = EditorGUILayout.FloatField("Wall Decor Offset", wallDecorationOffset.floatValue);


            EditorGUILayout.IntSlider(decorSafeArea, 0, 25);
            EditorGUILayout.IntSlider(pointSpacing, 0, 3);


            EditorGUILayout.PropertyField(debug);


            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.Space();

            #region Buttons

            serializedObject.ApplyModifiedProperties();

            if (GUILayout.Button("Generate Room"))
            {
                //roomGenerator.GenerateRoom();
            }


            if (GUILayout.Button("Save Room"))
            {
                if (roomGenerator.levels.Count == 0)
                    return;

                string levelOneName = "RoomGen";

                if (roomGenerator.levels[0].preset != null)
                {
                    levelOneName = roomGenerator.levels[0].preset.name;
                }

                string localPath = "Assets/" + roomGenerator.levels.Count + " Floor " + levelOneName + " Room.prefab";
                localPath = AssetDatabase.GenerateUniqueAssetPath(localPath);
                PrefabUtility.SaveAsPrefabAssetAndConnect(roomGenerator.parent, AssetDatabase.GenerateUniqueAssetPath(localPath), InteractionMode.UserAction);
                roomGenerator.Save();
            }

            #endregion Buttons




            if (GUI.changed)
            {
                if (roomGenerator.tileSize <= 0)
                {
                    roomGenerator.tileSize = 0.01f;
                }


                if (roomGenerator.preset != null)
                {
                    roomGenerator.UpdateStoredValues();
                    EditorUtility.SetDirty(roomGenerator.preset);
                }

                roomGenerator.GenerateRoom(roomGenerator.id);

            }



        }


        void CreateLevelList()
        {
            levelList = new ReorderableList(serializedObject, levels, true, false, true, true);

            levelList.drawElementCallback = DrawLevelItems;
            levelList.onAddCallback = AddLevelElement;
            levelList.elementHeightCallback = (index) =>
            {
                SerializedProperty element = levelList.serializedProperty.GetArrayElementAtIndex(index);
                SerializedProperty isExpanded = element.FindPropertyRelative("isExpanded");

                if (isExpanded.boolValue)
                {
                    return (lineHeight + lineHeightSpace) * 10;
                }

                return lineHeight;
            };
            //levelList.onSelectCallback = OnSelectLevel;
        }



        void DrawLevelItems(Rect rect, int index, bool isActive, bool isFocused)
        {
            SerializedProperty element = levelList.serializedProperty.GetArrayElementAtIndex(index);
            CreateLevelList(element, rect, index);
        }



        void CreateLevelList(SerializedProperty element, Rect rect, int index)
        {

            SerializedProperty preset = element.FindPropertyRelative("preset");
            SerializedProperty levelHeight = element.FindPropertyRelative("levelHeight");
            SerializedProperty levelOffset = element.FindPropertyRelative("levelOffset");
            SerializedProperty numDoors = element.FindPropertyRelative("numDoors");
            SerializedProperty numWindows = element.FindPropertyRelative("numWindows");
            SerializedProperty windowHeight = element.FindPropertyRelative("windowHeight");
            SerializedProperty characterSeed = element.FindPropertyRelative("characterSeed");
            SerializedProperty roomSeed = element.FindPropertyRelative("roomSeed");
            SerializedProperty decorSeed = element.FindPropertyRelative("decorSeed");
            SerializedProperty isExpanded = element.FindPropertyRelative("isExpanded");

            isExpanded.boolValue = EditorGUI.Foldout(new Rect(rect.x + 8f, rect.y, rect.width, lineHeight), isExpanded.boolValue, "Level " + index.ToString());

            if (isExpanded.boolValue)
            {
                float indent = 10f;
                rect.y += lineHeight + lineHeightSpace + 2f;
                EditorGUI.PropertyField(new Rect(rect.x + indent, rect.y, rect.width - indent, lineHeight), preset);
                rect.y += lineHeight + lineHeightSpace;
                EditorGUI.PropertyField(new Rect(rect.x + indent, rect.y, rect.width - indent, lineHeight), levelHeight);
                rect.y += lineHeight + lineHeightSpace;
                EditorGUI.PropertyField(new Rect(rect.x + indent, rect.y, rect.width - indent, lineHeight), levelOffset);
                rect.y += lineHeight + lineHeightSpace;
                EditorGUI.PropertyField(new Rect(rect.x + indent, rect.y, rect.width - indent, lineHeight), numDoors);
                rect.y += lineHeight + lineHeightSpace;
                EditorGUI.PropertyField(new Rect(rect.x + indent, rect.y, rect.width - indent, lineHeight), numWindows);
                rect.y += lineHeight + lineHeightSpace;
                EditorGUI.PropertyField(new Rect(rect.x + indent, rect.y, rect.width - indent, lineHeight), windowHeight);
                rect.y += lineHeight + lineHeightSpace;
                EditorGUI.PropertyField(new Rect(rect.x + indent, rect.y, rect.width - indent, lineHeight), characterSeed);
                rect.y += lineHeight + lineHeightSpace;
                EditorGUI.PropertyField(new Rect(rect.x + indent, rect.y, rect.width - indent, lineHeight), roomSeed);
                rect.y += lineHeight + lineHeightSpace;
                EditorGUI.PropertyField(new Rect(rect.x + indent, rect.y, rect.width - indent, lineHeight), decorSeed);
            }

            rect.y += lineHeight + lineHeightSpace;
        }




        //Presets and levels
        void AddLevelElement(ReorderableList list)
        {
            int index = list.serializedProperty.arraySize;
            list.serializedProperty.arraySize++;
            list.index = index;

            SerializedProperty element = list.serializedProperty.GetArrayElementAtIndex(index);
            element.FindPropertyRelative("isExpanded").boolValue = true;
            element.FindPropertyRelative("levelHeight").intValue = 1;
            element.FindPropertyRelative("numDoors").intValue = 1;
            element.FindPropertyRelative("numWindows").intValue = 1;
            element.FindPropertyRelative("windowHeight").intValue = 1;
        }



        public void OnInspectorUpdate()
        {
            this.Repaint();
        }

    }
}