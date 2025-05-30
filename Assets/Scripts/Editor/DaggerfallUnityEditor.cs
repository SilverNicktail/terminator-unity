// Project:         Daggerfall Unity
// Copyright:       Copyright (C) 2009-2023 Daggerfall Workshop
// Web Site:        http://www.dfworkshop.net
// License:         MIT License (http://www.opensource.org/licenses/mit-license.php)
// Source Code:     https://github.com/Interkarma/daggerfall-unity
// Original Author: Gavin Clayton (interkarma@dfworkshop.net)
// Contributors:    
// 
// Notes:
//

using UnityEngine;
using UnityEditor;
using System;
using System.Collections;
using System.Collections.Generic;
using DaggerfallConnect;
using DaggerfallConnect.Arena2;
using DaggerfallConnect.InternalTypes;
using DaggerfallConnect.Utility;
using DaggerfallWorkshop.Game;
using DaggerfallWorkshop.Utility;

namespace DaggerfallWorkshop
{
    [CustomEditor(typeof(DaggerfallUnity))]
    public class DaggerfallUnityEditor : Editor
    {
        private DaggerfallUnity dfUnity { get { return target as DaggerfallUnity; } }

        private const string showOptionsFoldout = "DaggerfallUnity_ShowOptionsFoldout";
        private static bool ShowOptionsFoldout
        {
            get { return EditorPrefs.GetBool(showOptionsFoldout, false); }
            set { EditorPrefs.SetBool(showOptionsFoldout, value); }
        }

        private const string showImportFoldout = "DaggerfallUnity_ShowImportFoldout";
        private static bool ShowImportFoldout
        {
            get { return EditorPrefs.GetBool(showImportFoldout, true); }
            set { EditorPrefs.SetBool(showImportFoldout, value); }
        }

        private const string showEnemyAdvancedFoldout = "DaggerfallUnity_ShowEnemyAdvancedFoldout";
        private static bool ShowEnemyAdvancedFoldout
        {
            get { return EditorPrefs.GetBool(showEnemyAdvancedFoldout, false); }
            set { EditorPrefs.SetBool(showEnemyAdvancedFoldout, value); }
        }

        private const string showTimeAndSpaceFoldout = "DaggerfallUnity_ShowTimeAndSpaceFoldout";
        private static bool ShowTimeAndSpaceFoldout
        {
            get { return EditorPrefs.GetBool(showTimeAndSpaceFoldout, true); }
            set { EditorPrefs.SetBool(showTimeAndSpaceFoldout, value); }
        }

        private const string showAssetExportFoldout = "DaggerfallUnity_ShowAssetExportFoldout";
        private static bool ShowAssetExportFoldout
        {
            get { return EditorPrefs.GetBool(showAssetExportFoldout, false); }
            set { EditorPrefs.SetBool(showAssetExportFoldout, value); }
        }

        SerializedProperty Prop(string name)
        {
            return serializedObject.FindProperty(name);
        }

        public override void OnInspectorGUI()
        {
            // Update
            dfUnity.EditorUpdate();

#if UNITY_EDITOR_LINUX
            // TODO: Check if this is still necessary in Linux version of Unity
            string message = string.Empty;
            message += "Linux users please set your Daggerfall installation path (i.e. parent folder of complete Daggerfall install) in Resources/defaults.ini then click 'Update Path' below.";
            message += " This is a temporary limitation to work around Inspector bugs in experimental Linux build.";
            EditorGUILayout.HelpBox(message, MessageType.Info);
            EditorGUILayout.SelectableLabel(dfUnity.Arena2Path, EditorStyles.textField, GUILayout.Height(EditorGUIUtility.singleLineHeight));
            if (GUILayout.Button("Update Path"))
            {
                dfUnity.Arena2Path = string.Empty;
                dfUnity.EditorResetArena2Path();
            }
#else
            // Get properties
            var propArena2Path = Prop("Arena2Path");

            // Browse for Arena2 path
            EditorGUILayout.Space();
            GUILayoutHelper.Horizontal(() =>
            {
                EditorGUILayout.LabelField(new GUIContent("Arena2 Path", "The local Arena2 path used for development only."), GUILayout.Width(EditorGUIUtility.labelWidth - 4));
                EditorGUILayout.SelectableLabel(dfUnity.Arena2Path, EditorStyles.textField, GUILayout.Height(EditorGUIUtility.singleLineHeight));
                if (GUILayout.Button("Browse..."))
                {
                    string path = EditorUtility.OpenFolderPanel("Locate Arena2 Path", "", "");
                    if (!string.IsNullOrEmpty(path))
                    {
                        if (!DaggerfallUnity.ValidateArena2Path(path))
                        {
                            EditorUtility.DisplayDialog("Invalid Path", "The selected Arena2 path is invalid", "Close");
                        }
                        else
                        {
                            dfUnity.Arena2Path = path;
                            propArena2Path.stringValue = path;
                            dfUnity.EditorResetArena2Path();
                        }
                    }
                }
                if (GUILayout.Button("Clear"))
                {
                    dfUnity.EditorClearArena2Path();
                    EditorUtility.SetDirty(target);
                }
            });

            // Prompt user to set Arena2 path
            if (string.IsNullOrEmpty(dfUnity.Arena2Path))
            {
                EditorGUILayout.HelpBox("Please set the Arena2 path of your Daggerfall installation.", MessageType.Info);
                return;
            }
#endif

            // Display other GUI items
            DisplayOptionsGUI();
            DisplayImporterGUI();

            // Save modified properties
            serializedObject.ApplyModifiedProperties();
            if (GUI.changed)
                EditorUtility.SetDirty(target);
        }

        private void DisplayOptionsGUI()
        {
            EditorGUILayout.Space();
            ShowOptionsFoldout = GUILayoutHelper.Foldout(ShowOptionsFoldout, new GUIContent("Options"), () =>
            {
                // Performance options
                var propSetStaticFlags = Prop("Option_SetStaticFlags");
                var propCombineRMB = Prop("Option_CombineRMB");
                var propCombineRDB = Prop("Option_CombineRDB");
                var propBatchBillboards = Prop("Option_BatchBillboards");
                EditorGUILayout.Space();
                EditorGUILayout.LabelField("Performance");
                if (!propSetStaticFlags.boolValue ||
                    !propCombineRMB.boolValue ||
                    !propCombineRDB.boolValue ||
                    !propBatchBillboards.boolValue)
                {
                    EditorGUILayout.HelpBox("Below settings will impact runtime performance. Only change when necessary.", MessageType.Warning);
                }
                GUILayoutHelper.Indent(() =>
                {
                    propSetStaticFlags.boolValue = EditorGUILayout.Toggle(new GUIContent("Set Static Flags", "Apply static flag where appropriate when building scenes. Billboards and dynamic objects are not marked static."), propSetStaticFlags.boolValue);
                    propCombineRMB.boolValue = EditorGUILayout.Toggle(new GUIContent("Combine RMB", "Combine city-block meshes together."), propCombineRMB.boolValue);
                    propCombineRDB.boolValue = EditorGUILayout.Toggle(new GUIContent("Combine RDB", "Combine dungeon-block meshes together."), propCombineRDB.boolValue);
                    propBatchBillboards.boolValue = EditorGUILayout.Toggle(new GUIContent("Batch Billboards", "Combine billboards into batches. Some billboards are never batched, such as editor markers."), propBatchBillboards.boolValue);
                });

                // Import options
                var propAddMeshColliders = Prop("Option_AddMeshColliders");
                var propRMBGroundPlane = Prop("Option_RMBGroundPlane");
                //var propCloseCityGates = Prop("Option_CloseCityGates");
                EditorGUILayout.Space();
                EditorGUILayout.LabelField("Import Options");
                GUILayoutHelper.Indent(() =>
                {
                    propAddMeshColliders.boolValue = EditorGUILayout.Toggle(new GUIContent("Add Colliders", "Add colliders where appropriate when building scenes. Decorative billboards will not receive colliders."), propAddMeshColliders.boolValue);
                    propRMBGroundPlane.boolValue = EditorGUILayout.Toggle(new GUIContent("RMB Ground Plane", "Adds RMB ground plane to imported city block (ignored by terrain system)."), propRMBGroundPlane.boolValue);
                });

                // Prefab options
                EditorGUILayout.Space();
                EditorGUILayout.LabelField("Prefabs");
                GUILayoutHelper.Indent(() =>
                {
                // Lights
                var propImportLightPrefabs = Prop("Option_ImportLightPrefabs");
                var propCityLightPrefab = Prop("Option_CityLightPrefab");
                var propDungeonLightPrefab = Prop("Option_DungeonLightPrefab");
                var propInteriorLightPrefab = Prop("Option_InteriorLightPrefab");
                propImportLightPrefabs.boolValue = EditorGUILayout.Toggle(new GUIContent("Import Light Prefabs", "Import light prefabs into scene."), propImportLightPrefabs.boolValue);
                GUILayoutHelper.EnableGroup(propImportLightPrefabs.boolValue, () =>
                {
                    GUILayoutHelper.Indent(() =>
                    {
                        propCityLightPrefab.objectReferenceValue = EditorGUILayout.ObjectField(new GUIContent("City Lights", "Prefab for city lights."), propCityLightPrefab.objectReferenceValue, typeof(Light), false);
                        propDungeonLightPrefab.objectReferenceValue = EditorGUILayout.ObjectField(new GUIContent("Dungeon Lights", "Prefab for dungeon lights."), propDungeonLightPrefab.objectReferenceValue, typeof(Light), false);
                        propInteriorLightPrefab.objectReferenceValue = EditorGUILayout.ObjectField(new GUIContent("Interior Lights", "Prefab for building interior lights."), propInteriorLightPrefab.objectReferenceValue, typeof(Light), false);
                    });
                });

                // Doors
                var propImportDoorPrefabs = Prop("Option_ImportDoorPrefabs");
                var propDungeonDoorPrefab = Prop("Option_DungeonDoorPrefab");
                var propInteriorDoorPrefab = Prop("Option_InteriorDoorPrefab");
                EditorGUILayout.Space();
                propImportDoorPrefabs.boolValue = EditorGUILayout.Toggle(new GUIContent("Import Door Prefabs", "Import door prefabs into scene."), propImportDoorPrefabs.boolValue);
                GUILayoutHelper.EnableGroup(propImportDoorPrefabs.boolValue, () =>
                {
                    GUILayoutHelper.Indent(() =>
                    {
                        propDungeonDoorPrefab.objectReferenceValue = EditorGUILayout.ObjectField(new GUIContent("Dungeon Doors", "Prefab for dungeon doors."), propDungeonDoorPrefab.objectReferenceValue, typeof(DaggerfallActionDoor), false);
                        propInteriorDoorPrefab.objectReferenceValue = EditorGUILayout.ObjectField(new GUIContent("Interior Doors", "Prefab for building interior doors."), propInteriorDoorPrefab.objectReferenceValue, typeof(DaggerfallActionDoor), false);
                    });
                });

                // Enemies
                var propImportEnemyPrefabs = Prop("Option_ImportEnemyPrefabs");
                var propEnemyPrefab = Prop("Option_EnemyPrefab");
                EditorGUILayout.Space();
                propImportEnemyPrefabs.boolValue = EditorGUILayout.Toggle(new GUIContent("Import Enemy Prefabs", "Import enemy prefabs into scene."), propImportEnemyPrefabs.boolValue);
                GUILayoutHelper.EnableGroup(propImportEnemyPrefabs.boolValue, () =>
                {
                    GUILayoutHelper.Indent(() =>
                    {
                        propEnemyPrefab.objectReferenceValue = EditorGUILayout.ObjectField(new GUIContent("Enemies", "Prefab for enemies."), propEnemyPrefab.objectReferenceValue, typeof(DaggerfallEnemy), false);
                    });
                });

                // Random treasure
                var propImportRandomTreasure = Prop("Option_ImportRandomTreasure");
                var propLootContainerPrefab = Prop("Option_LootContainerPrefab");
                EditorGUILayout.Space();
                propImportRandomTreasure.boolValue = EditorGUILayout.Toggle(new GUIContent("Import Random Treasure", "Import random treasure piles into scene."), propImportRandomTreasure.boolValue);
                GUILayoutHelper.EnableGroup(propImportRandomTreasure.boolValue, () =>
                {
                    GUILayoutHelper.Indent(() =>
                    {
                        propLootContainerPrefab.objectReferenceValue = EditorGUILayout.ObjectField(new GUIContent("Loot Container", "Prefab for random treasure loot containers."), propLootContainerPrefab.objectReferenceValue, typeof(DaggerfallLoot), false);
                    });
                });

                // Dungeon Water
                var propDungeonWaterPrefab = Prop("Option_DungeonWaterPrefab");
                var propDungeonWaterPlaneSize = Prop("Option_DungeonWaterPlaneSize");
                var propDungeonWaterPlaneOffset = Prop("Option_DungeonWaterPlaneOffset");
                EditorGUILayout.Space();
                EditorGUILayout.LabelField("Water");
                GUILayoutHelper.Indent(() =>
                {
                    propDungeonWaterPrefab.objectReferenceValue = EditorGUILayout.ObjectField(new GUIContent("Dungeon Water", "Prefab for dungeon water."), propDungeonWaterPrefab.objectReferenceValue, typeof(GameObject), false);
                    propDungeonWaterPlaneSize.vector3Value = EditorGUILayout.Vector3Field(new GUIContent("Plane Size", "Size of water plane in Unity units, used for scaling target relative to Daggerfall geometry import (i.e. MeshReader.GlobalScale)."), propDungeonWaterPlaneSize.vector3Value, null);
                    propDungeonWaterPlaneOffset.vector3Value = EditorGUILayout.Vector3Field(new GUIContent("Plane Offset", "Amount to offset water plane so origin aligns with block origin and top aligns with water surface."), propDungeonWaterPlaneOffset.vector3Value, null);
                });

                // Optional
                var propCityBlockPrefab = Prop("Option_CityBlockPrefab");
                var propDungeonBlockPrefab = Prop("Option_DungeonBlockPrefab");
                var propMobileNPCPrefab = Prop("Option_MobileNPCPrefab");
                EditorGUILayout.Space();
                EditorGUILayout.LabelField("Optional");
                GUILayoutHelper.Indent(() =>
                {
                    propCityBlockPrefab.objectReferenceValue = EditorGUILayout.ObjectField(new GUIContent("City Blocks", "Prefab for city blocks."), propCityBlockPrefab.objectReferenceValue, typeof(DaggerfallRMBBlock), false);
                    propDungeonBlockPrefab.objectReferenceValue = EditorGUILayout.ObjectField(new GUIContent("Dungeon Blocks", "Prefab for dungeon blocks."), propDungeonBlockPrefab.objectReferenceValue, typeof(DaggerfallRDBBlock), false);
                    propMobileNPCPrefab.objectReferenceValue = EditorGUILayout.ObjectField(new GUIContent("Mobile NPCs", "Prefab for mobile NPCs in locations."), propMobileNPCPrefab.objectReferenceValue, typeof(MobilePersonMotor), false);
                });
            });

            // Time & Space options
            var propAutomateTextureSwaps = Prop("Option_AutomateTextureSwaps");
                var propAutomateSky = Prop("Option_AutomateSky");
                var propAutomateCityLights = Prop("Option_AutomateCityLights");
                //var propAutomateCityGates = Prop("Option_AutomateCityGates");
                EditorGUILayout.Space();
                EditorGUILayout.LabelField("Time & Space");
                GUILayoutHelper.Indent(() =>
                {
                    propAutomateTextureSwaps.boolValue = EditorGUILayout.Toggle(new GUIContent("Automate Textures", "Assign textures for climate, season, and windows at runtime based on player world position and world time."), propAutomateTextureSwaps.boolValue);
                    propAutomateSky.boolValue = EditorGUILayout.Toggle(new GUIContent("Automate Sky", "Assign seasonal skies and full day/night cycle based on player world position and world time."), propAutomateSky.boolValue);
                    propAutomateCityLights.boolValue = EditorGUILayout.Toggle(new GUIContent("Automate City Lights", "Turn city lights on/off based on world time."), propAutomateCityLights.boolValue);
                    //propAutomateCityGates.boolValue = EditorGUILayout.Toggle(new GUIContent("Automate City Gates", "Open/close city gates based on world time. Not implemented."), propAutomateCityGates.boolValue);
                });
            });
        }

        private void DisplayImporterGUI()
        {
            // Hide importer GUI when not active in hierarchy
            if (!dfUnity.gameObject.activeInHierarchy)
                return;

            EditorGUILayout.Space();
            ShowImportFoldout = GUILayoutHelper.Foldout(ShowImportFoldout, new GUIContent("Importer"), () =>
            {
                GUILayoutHelper.Indent(() =>
                {
                    EditorGUILayout.Space();
                    var propModelID = Prop("ModelImporter_ModelID");
                    EditorGUILayout.LabelField(new GUIContent("ModelID", "Enter numeric ID of model."));
                    GUILayoutHelper.Horizontal(() =>
                    {
                        propModelID.intValue = EditorGUILayout.IntField(propModelID.intValue);
                        if (GUILayout.Button("Import"))
                        {
                            GameObjectHelper.CreateDaggerfallMeshGameObject((uint)propModelID.intValue, null);
                        }
                    });

                    EditorGUILayout.Space();
                    var propBlockName = Prop("BlockImporter_BlockName");
                    EditorGUILayout.LabelField(new GUIContent("Block Name", "Enter name of block. Accepts .RMB and .RDB blocks."));
                    GUILayoutHelper.Horizontal(() =>
                    {
                        propBlockName.stringValue = EditorGUILayout.TextField(propBlockName.stringValue.Trim().ToUpper());
                        if (GUILayout.Button("Import"))
                        {
                            // Create block
                            if (propBlockName.stringValue.EndsWith(".RMB"))
                            {
                                GameObjectHelper.CreateRMBBlockGameObject(propBlockName.stringValue, 0, 0, 0, 0, dfUnity.Option_RMBGroundPlane, dfUnity.Option_CityBlockPrefab);
                            }
                            else if (propBlockName.stringValue.EndsWith(".RDB"))
                            {
                                GameObjectHelper.CreateRDBBlockGameObject(
                                    propBlockName.stringValue,
                                    null,
                                    true,
                                    DFRegion.DungeonTypes.HumanStronghold,
                                    0.5f,
                                    4,
                                    (int)DateTime.Now.Ticks,
                                    dfUnity.Option_DungeonBlockPrefab);
                            }
                        }
                    });

                    EditorGUILayout.Space();
                    var propCityName = Prop("CityImporter_CityName");
                    EditorGUILayout.LabelField(new GUIContent("City Name", "Enter exact city name in format RegionName/CityName. Case-sensitive."));
                    GUILayoutHelper.Horizontal(() =>
                    {
                        propCityName.stringValue = EditorGUILayout.TextField(propCityName.stringValue.Trim());
                        if (GUILayout.Button("Import"))
                        {
                            GameObjectHelper.CreateDaggerfallLocationGameObject(propCityName.stringValue, null);
                        }
                    });

                    EditorGUILayout.Space();
                    var propDungeonName = Prop("DungeonImporter_DungeonName");
                    EditorGUILayout.LabelField(new GUIContent("Dungeon Name", "Enter exact dungeon name in format RegionName/DungeonName. Case-sensitive."));
                    GUILayoutHelper.Horizontal(() =>
                    {
                        propDungeonName.stringValue = EditorGUILayout.TextField(propDungeonName.stringValue.Trim());
                        if (GUILayout.Button("Import"))
                        {
                            GameObjectHelper.CreateDaggerfallDungeonGameObject(propDungeonName.stringValue, null);
                        }
                    });
                });
            });
        }

        private void DisplayAssetExporterGUI()
        {
            EditorGUILayout.Space();
            ShowAssetExportFoldout = GUILayoutHelper.Foldout(ShowAssetExportFoldout, new GUIContent("Asset Exporter (Beta)"), () =>
            {
                EditorGUILayout.HelpBox("Export pre-built assets to specified Resources folder and subfolder.", MessageType.Info);

                // Parent Resources path
                var propMyResourcesFolder = Prop("Option_MyResourcesFolder");
                propMyResourcesFolder.stringValue = EditorGUILayout.TextField(new GUIContent("My Resources Folder", "Path to Resources folder for asset export."), propMyResourcesFolder.stringValue);

                // Terrain atlases
                GUILayoutHelper.Horizontal(() =>
                {
                    var propTerrainAtlasesSubFolder = Prop("Option_TerrainAtlasesSubFolder");
                    propTerrainAtlasesSubFolder.stringValue = EditorGUILayout.TextField(new GUIContent("Terrain Atlas SubFolder", "Sub-folder for terrain atlas textures."), propTerrainAtlasesSubFolder.stringValue);
                    if (GUILayout.Button("Update"))
                    {
//#if UNITY_EDITOR && !UNITY_WEBPLAYER
//                        dfUnity.ExportTerrainTextureAtlases();
//#endif
                    }
                });
            });
        }

    }
}
