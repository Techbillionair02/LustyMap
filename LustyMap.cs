// LustyMap.cs - Minimal compile-safe skeleton to fix missing types & braces
// Modified by Regime Gaming
// Original Author: k1lly0u

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Oxide.Core;
using Oxide.Core.Plugins;
using Oxide.Game.Rust.Cui;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Oxide.Plugins
{
    [Info("LustyMap", "Regime / Original: k1lly0u", "3.0.1")]
    [Description("Interactive map and minimap for Rust with local imagery handling (skeleton build).")]
    public class LustyMap : RustPlugin
    {
        #region Fields
        private ConfigData configData;
        private static LustyMap instance;
        private bool activated = true;
        private bool isNewSave = false;

        // Explicit constructor instead of target-typed new()
        private static Dictionary<string, MapMarker> customMarkers = new Dictionary<string, MapMarker>();

        private static MarkerData storedMarkers;

        private static string dataDirectory
        {
            get { return string.Format("{0}/LustyMap/", Interface.Oxide.DataDirectory); }
        }

        private static readonly Dictionary<uint, ActiveEntity> entityMarkers =
            new Dictionary<uint, ActiveEntity>();
        #endregion

        #region Initialization
        private void Init()
        {
            instance = this;
            LoadVariables();
            ValidateImages(); // Stubbed to avoid errors
            Puts("[LustyMap] Initialized successfully (skeleton build).");
        }
        #endregion

        #region Config
        protected override void LoadDefaultConfig()
        {
            configData = new ConfigData
            {
                Friends = new ConfigData.FriendOptions
                {
                    AllowCustomLists = true,
                    UseClans = true,
                    UseFriends = true
                },
                Markers = new ConfigData.MapMarkers
                {
                    ShowAllPlayers = false,
                    ShowCaves = false,
                    ShowDebris = false,
                    ShowFriends = true,
                    ShowHelicopters = true,
                    ShowMarkerNames = true,
                    ShowMonuments = true,
                    ShowPlanes = true,
                    ShowPlayer = true,
                    ShowSupplyDrops = true,
                    ShowPublicVendingMachines = true,
                    ShowCars = true,
                    ShowTanks = true
                },
                Map = new ConfigData.MapOptions
                {
                    EnableAFKTracking = true,
                    HideEventPlayers = true,
                    ShowCompass = true,
                    StartOpen = true,
                    MapImage = new ConfigData.MapOptions.MapImages
                    {
                        APIKey = "",
                        CustomMap_Filename = "",
                        CustomMap_Use = false
                    },
                    UpdateSpeed = 1f
                },
                MiniMap = new ConfigData.Minimap
                {
                    UseMinimap = true,
                    HorizontalScale = 1.0f,
                    VerticalScale = 1.0f,
                    OnLeftSide = true,
                    OffsetSide = 0f,
                    OffsetTop = 0f
                },
                ComplexOptions = new ConfigData.ComplexMap
                {
                    UseComplexMap = true,
                    ForceMapZoom = false,
                    ForcedZoomLevel = 1
                },
                Spam = new ConfigData.SpamOptions
                {
                    Enabled = true,
                    TimeBetweenAttempts = 3,
                    WarningAttempts = 5,
                    DisableAttempts = 10,
                    DisableSeconds = 120
                },
                Images = new ConfigData.ImageOptions
                {
                    EnableImageLibrary = false,
                    DownloadImagery = false,
                    UseBeancanImagery = false,
                    UseCustomImagery = false
                }
            };

            SaveConfig(configData);
        }

        private void LoadVariables()
        {
            try
            {
                configData = Config.ReadObject<ConfigData>();
                if (configData == null)
                {
                    PrintWarning("Config null, generating defaults.");
                    LoadDefaultConfig();
                }
            }
            catch (Exception)
            {
                PrintWarning("Config invalid, generating defaults.");
                LoadDefaultConfig();
            }
        }

        private void SaveConfig(ConfigData cfg)
        {
            Config.WriteObject(cfg, true);
        }
        #endregion

        #region Minimal Stubs
        private void ValidateImages()
        {
            // Placeholder — later integrate ImageLibrary
            Puts("[LustyMap] ValidateImages() skipped in skeleton build.");
        }
        #endregion

        #region Data Structures
        private class ActiveEntity : MonoBehaviour
        {
            // Skeleton only
        }

        private class MapMarker
        {
            public string name { get; set; }
            public float x { get; set; }
            public float z { get; set; }
            public float r { get; set; }
            public string icon { get; set; }
        }

        private class MarkerData
        {
            public Dictionary<string, MapMarker> data =
                new Dictionary<string, MapMarker>();
        }

        private class ConfigData
        {
            [JsonProperty(PropertyName = "Friend Options")]
            public FriendOptions Friends { get; set; }

            [JsonProperty(PropertyName = "Marker Options")]
            public MapMarkers Markers { get; set; }

            [JsonProperty(PropertyName = "Map - Main Options")]
            public MapOptions Map { get; set; }

            [JsonProperty(PropertyName = "Map - Mini Options")]
            public Minimap MiniMap { get; set; }

            [JsonProperty(PropertyName = "Map - Complex Options")]
            public ComplexMap ComplexOptions { get; set; }

            [JsonProperty(PropertyName = "Spam Options")]
            public SpamOptions Spam { get; set; }

            [JsonProperty(PropertyName = "Image Options")]
            public ImageOptions Images { get; set; }

            public class FriendOptions
            {
                public bool AllowCustomLists { get; set; }
                public bool UseClans { get; set; }
                public bool UseFriends { get; set; }
            }

            public class MapMarkers
            {
                public bool ShowAllPlayers { get; set; }
                public bool ShowCaves { get; set; }
                public bool ShowDebris { get; set; }
                public bool ShowFriends { get; set; }
                public bool ShowHelicopters { get; set; }
                public bool ShowMarkerNames { get; set; }
                public bool ShowMonuments { get; set; }
                public bool ShowPlanes { get; set; }
                public bool ShowPlayer { get; set; }
                public bool ShowSupplyDrops { get; set; }
                public bool ShowPublicVendingMachines { get; set; }
                public bool ShowCars { get; set; }
                public bool ShowTanks { get; set; }
            }

            public class MapOptions
            {
                public bool EnableAFKTracking { get; set; }
                public bool HideEventPlayers { get; set; }
                public bool StartOpen { get; set; }
                public bool ShowCompass { get; set; }
                public float UpdateSpeed { get; set; }

                public MapImages MapImage { get; set; }

                public class MapImages
                {
                    public string APIKey { get; set; }
                    public bool CustomMap_Use { get; set; }
                    public string CustomMap_Filename { get; set; }
                }
            }

            public class Minimap
            {
                public bool UseMinimap { get; set; }
                public float HorizontalScale { get; set; }
                public float VerticalScale { get; set; }
                public bool OnLeftSide { get; set; }
                public float OffsetSide { get; set; }
                public float OffsetTop { get; set; }
            }

            public class ComplexMap
            {
                public bool UseComplexMap { get; set; }
                public bool ForceMapZoom { get; set; }
                public int ForcedZoomLevel { get; set; }
            }

            public class SpamOptions
            {
                public int TimeBetweenAttempts { get; set; }
                public int WarningAttempts { get; set; }
                public int DisableAttempts { get; set; }
                public int DisableSeconds { get; set; }
                public bool Enabled { get; set; }
            }

            public class ImageOptions
            {
                public bool EnableImageLibrary { get; set; }
                public bool DownloadImagery { get; set; }
                public bool UseBeancanImagery { get; set; }
                public bool UseCustomImagery { get; set; }
            }
        }
        #endregion

        #region Utility
        private static string RemoveSpecialCharacters(string str)
        {
            var sb = new StringBuilder();
            foreach (char c in str)
            {
                if ((c >= '0' && c <= '9') ||
                    (c >= 'A' && c <= 'Z') ||
                    (c >= 'a' && c <= 'z') ||
                    (c >= 'А' && c <= 'Я') ||
                    (c >= 'а' && c <= 'я') ||
                    c == '.' || c == '_')
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }
        #endregion
    }
}