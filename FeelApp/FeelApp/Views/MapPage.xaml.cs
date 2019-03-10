using Esri.ArcGISRuntime.Data;
using Esri.ArcGISRuntime.Geometry;
using Esri.ArcGISRuntime.Mapping;
using Esri.ArcGISRuntime.Symbology;
using Esri.ArcGISRuntime.UI;
using FeelApp.Helpers;
using FeelApp.Model;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;


namespace FeelApp
{
    public partial class MapPage : ContentPage
    {
        public string ShapeFile;
        public string PathFile;
        public string LabelPath;
        public ObservableCollection<MapLabel> mapLabels;
        GraphicsOverlay overlay;
        public MapPage(string shapeFile, string pathFile, bool help, bool showHelp)
        {
            InitializeComponent();

            overlay = new GraphicsOverlay();
            mapLabels = new ObservableCollection<MapLabel>();
            ShapeFile = shapeFile;
            PathFile = pathFile;

            if(shapeFile == "GroundFloor.shp")
            {
                Title = "Ground Floor";
                LoadGroundFloorLabels();
            }

            if (shapeFile == "SecondFloor.shp")
            {
                Title = "Second Floor";
                LoadSecondFloorLabels();
            }

            if (shapeFile == "ThirdFloor.shp")
            {
                Title = "Third Floor";
                 
                LoadThirdFloorLabels();
              
            }

            if (shapeFile == "FourthFloor.shp")
            {
                Title = "Fourth Floor";

                LoadFourthFloorLabels();

            }

            MainMapView.InteractionOptions = new MapViewInteractionOptions
            {
                IsRotateEnabled = false,
                //IsPanEnabled = false
            };

            var map = new Map(Basemap.CreateOpenStreetMap());
            MainMapView.Map = map;
            var lp = MainMapView.LocationDisplay.Location;

            
            permission();

            if (help)
            {
                CheckCoord();
            }
            if (showHelp)
            {
                AddSampleMarker(map);
            }

            GotoLocation();

         
      
            //GotoLocation();

            //AddSampleMarker(map);


            //  permission();


        }

        private async Task CheckCoord()
        {
            try
            {
                var id = Globals.UserID;

                var response = await Api.GetCoord(id);
                if (response.data != null)
                {

                    //Update
                    UpdateCoord();

                }
                else
                {

                    //Create
                    var lon = MainMapView.LocationDisplay.Location.Position.X;
                    var lat = MainMapView.LocationDisplay.Location.Position.Y;
                    var createResponse = await Api.CreateCoord(lon, lat);

                    UpdateCoord();
                }
            }
            catch
            {

            }
        }

        private async Task UpdateCoord()
        {
            Device.StartTimer(TimeSpan.FromSeconds(5), () =>
            {
                Task.Run(async () =>
                {
                    try
                    {
                        var lon = MainMapView.LocationDisplay.Location.Position.X;
                        var lat = MainMapView.LocationDisplay.Location.Position.Y;
                        var createResponse = await Api.UpdateCoord(lon, lat);
                    }
                    catch
                    {

                    }
                });
                return true;
            });

             
        }

        public async void permission()
        {
            try
            {
                var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Location);
                if (status != PermissionStatus.Granted)
                {
                    if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Location))
                    {
                        await DisplayAlert("Need location", "Gunna need that location", "OK");
                    }

                    var results = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Location);
                    status = results[Permission.Location];
                }

                if (status == PermissionStatus.Granted)
                {
                    //Permission granted, do what you want do.
                
                    MainMapView.LocationDisplay.AutoPanMode = LocationDisplayAutoPanMode.CompassNavigation;
                    MainMapView.LocationDisplay.IsEnabled = true;
                    x.Text = MainMapView.LocationDisplay.Location.Position.X.ToString();
                    y.Text = MainMapView.LocationDisplay.Location.Position.Y.ToString();
                   // MainMapView.SetViewpointRotationAsync(0);
                    MainMapView.LocationDisplay.LocationChanged += LocationDisplay_LocationChanged;
                    
                }
                else if (status != PermissionStatus.Unknown)
                {
                    await DisplayAlert("Location Denied", "Can not continue, try again.", "OK");
                }
            }
            catch (Exception ex)
            {
                //...
            }
        }

        private void LocationDisplay_LocationChanged(object sender, Esri.ArcGISRuntime.Location.Location e)
        {
            x.Text = e.Position.X.ToString();
            y.Text = e.Position.Y.ToString();
        }

        private async void AddSampleMarker(Map map)
        {
            Device.StartTimer(TimeSpan.FromSeconds(10), () =>
            {
                Task.Run(async () =>
                {
                    overlay.Graphics.Clear();
                    var helpId = Globals.HelpAccountId;
                    var respone = await Api.GetCoord(helpId);
                    var data = respone.data;
                    if(respone.data != null)
                    {
                        //var mapPoint = new Esri.ArcGISRuntime.Geometry.MapPoint(item.LongX, item.LatY);
                        MapPoint centralLocation = new MapPoint(respone.data[0].Longitude, respone.data[0].Latitude);

                        //Viewpoint initialViewpoint = new Viewpoint(7.06849, 125.59565, 7500);

                        // Set initial viewpoint
                        //map.InitialViewpoint = initialViewpoint;

                        // Provide used Map to the MapView
                        MainMapView.Map = map;

                        // Create overlay to where graphics are shown
                        overlay = new GraphicsOverlay();

                        // Add created overlay to the MapView
                        MainMapView.GraphicsOverlays.Add(overlay);

                        // Create a simple marker symbol
                        SimpleMarkerSymbol simpleSymbol = new SimpleMarkerSymbol()
                        {
                            Color = Color.Red,
                            Size = 10,
                            Style = SimpleMarkerSymbolStyle.Circle
                        };

                        // Add a new graphic with a central point that was created earlier
                        Graphic graphicWithSymbol = new Graphic(centralLocation, simpleSymbol);

                        overlay.Graphics.Add(graphicWithSymbol);


                    }

                    //GotoLocation();
                });
                return true;
            });

                  
        }

        public async Task TurnOnLocation()
        {

            try
            {
                var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Location);
                if (status != PermissionStatus.Granted)
                {
                    if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Location))
                    {
                        await DisplayAlert("Need location", "Gunna need that location", "OK");
                    }

                    var results = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Location);
                    status = results[Permission.Location];
                }

                if (status == PermissionStatus.Granted)
                {
                    //Permission granted, do what you want do.
                    MainMapView.LocationDisplay.AutoPanMode = LocationDisplayAutoPanMode.CompassNavigation;
                    MainMapView.LocationDisplay.IsEnabled = true;
                    x.Text = MainMapView.LocationDisplay.Location.Position.X.ToString();
                    y.Text = MainMapView.LocationDisplay.Location.Position.Y.ToString();
                 
                    MainMapView.LocationDisplay.LocationChanged += LocationDisplay_LocationChanged;
                    MainMapView.SetViewpointRotationAsync(0);
                }
                else if (status != PermissionStatus.Unknown)
                {
                    await DisplayAlert("Location Denied", "Can not continue, try again.", "OK");
                }
            }
            catch (Exception ex)
            {
                //...
            }

            //TODO Remove this IsStarted check https://github.com/Esri/arcgis-runtime-samples-xamarin/issues/182

            //var vp = new Viewpoint(MainMapView.LocationDisplay.MapLocation);
            //await MainMapView.SetViewpointAsync(vp);



        }

        public async void Rotate(object sender, ValueChangedEventArgs e)
        {
            await MainMapView.SetViewpointRotationAsync(e.NewValue);
            x.Text = e.NewValue.ToString();
        }

        public async void GotoLocation()
        {
            // Create a new map to display in the map view with a streets basemap
            var map = new Map(Basemap.CreateOpenStreetMap());
            MainMapView.Map = map;



            // Get the path to the downloaded shapefile
            //string fileName = "GroundFloor.cpg";
            //var filePath = CopyShapeFile(fileName);

            //fileName = "GroundFloor.dbf";
            //filePath = CopyShapeFile(fileName);

            //fileName = "GroundFloor.prj";
            //filePath = CopyShapeFile(fileName);

            //fileName = "GroundFloor.qpj";
            //filePath = CopyShapeFile(fileName);

            //fileName = "GroundFloor.shx";
            //filePath = CopyShapeFile(fileName);

            //fileName = "GroundFloor.shp";
            //filePath = CopyShapeFile(fileName);
            var folder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var filePath = Path.Combine(folder, "Library", "ShapeFiles", ShapeFile);
            var pathFilePath = Path.Combine(folder, "Library", "ShapeFiles", PathFile);
            
            if (File.Exists(filePath))
            {
               
                //var path = Path.Combine("/data/user/0/FeelApp.FeelApp/files/Library/ShapeFiles/GroundFloor.cpg"
                // Open the shapefile
                ShapefileFeatureTable myShapefile = await ShapefileFeatureTable.OpenAsync(filePath);
                ShapefileFeatureTable myPathfile = await ShapefileFeatureTable.OpenAsync(pathFilePath);
                
                // Create a feature layer to display the shapefile
                FeatureLayer newFeatureLayer = new FeatureLayer(myShapefile);
                FeatureLayer newFeatureLayerPath = new FeatureLayer(myPathfile);
                


                //Change Color Direction
                SimpleLineSymbol symbol = new SimpleLineSymbol()
                {
                    Color = Color.Green,
                    Width = 2,
                    Style = SimpleLineSymbolStyle.Solid
                };

                // Create a new renderer using the symbol just created
                SimpleRenderer renderer = new SimpleRenderer(symbol);

                // Assign the new renderer to the feature layer
                newFeatureLayerPath.Renderer = renderer;

                // Add the feature layer to the map
                //map.OperationalLayers.Add(newFeatureLayerLabel);
                map.OperationalLayers.Add(newFeatureLayerPath);
                map.OperationalLayers.Add(newFeatureLayer);
               

                var fullextent = newFeatureLayer.FullExtent;
                //MainMap.InitialViewpoint =new Viewpoint( fullextent);
                // Zoom the map to the extent of the shapefile

                FloorMarker();


                await MainMapView.SetViewpointGeometryAsync(newFeatureLayer.FullExtent);

            


                MainMapView.SetViewpointRotationAsync(0);
                //await TurnOnLocation();

               
            }


        }

        public void FloorMarker()
        {
            
            GraphicsOverlay graphicsLayer = new GraphicsOverlay();
            // define the location for the point (lower left of text)
            foreach(var item in mapLabels)
            {
                var mapPoint = new Esri.ArcGISRuntime.Geometry.MapPoint(item.LongX, item.LatY);

                // create a text symbol: define color, font, size, and text for the label
                var textSym = new Esri.ArcGISRuntime.Symbology.TextSymbol();
                if(item.Name =="EXIT")
                {
                    textSym.Color = Color.Red;
                    //textSym.FontStyle = 
                    textSym.Text = item.Name;
                }
                else
                {
                    textSym.Color = Color.Blue;
                    //textSym.FontStyle = 
                    textSym.Text = item.Name;
                }
               
                //textSym.Angle = -60;

                // create a marker symbol
                var markerSym = new Esri.ArcGISRuntime.Symbology.SimpleMarkerSymbol();
                markerSym.Color = Color.Blue;
                
                markerSym.Style = SimpleMarkerSymbolStyle.Circle;
                markerSym.Size = 1;

                // create a graphic for the text (apply TextSymbol)
                var textGraphic = new Graphic(mapPoint, textSym);

                // create a graphic for the marker (apply SimpleMarkerSymbol)
                var markerGraphic = new Graphic(mapPoint, markerSym);

                // add text and marker graphics


                graphicsLayer.Graphics.Add(textGraphic);
                graphicsLayer.Graphics.Add(markerGraphic);
               
            }

            MainMapView.GraphicsOverlays.Add(graphicsLayer);
        }

        public void LoadGroundFloorLabels()
        {
            mapLabels = new ObservableCollection<MapLabel>{
                new MapLabel { LongX = 13981266.104, LatY = 788841.006, Name = "H1A" },
                new MapLabel { LongX = 13981272.063, LatY = 788833.470, Name = "H1B" },
                new MapLabel { LongX = 13981277.994, LatY = 788825.790, Name = "H1C" },
                new MapLabel { LongX = 13981284.818, LatY = 788817.004, Name = "H1D" },
                new MapLabel { LongX = 13981299.640, LatY = 788798.587, Name = "H1E" },
                new MapLabel { LongX = 13981283.838, LatY = 788836.430, Name = "H1F" },
                new MapLabel { LongX = 13981290.970, LatY = 788842.217, Name = "H1G" },
                new MapLabel { LongX = 13981299.544, LatY = 788829.635, Name = "H1H" },
                new MapLabel { LongX = 13981305.504, LatY = 788821.983, Name = "H1I" },
                new MapLabel { LongX = 13981311.656, LatY = 788814.140, Name = "H1J" },
                new MapLabel { LongX = 13981317.654, LatY = 788806.873, Name = "H1K" },
                new MapLabel { LongX = 13981327.112, LatY = 788814.678, Name = "H1L" },
                new MapLabel { LongX = 13981320.806, LatY = 788822.425, Name = "H1M" },
                new MapLabel { LongX = 13981314.616, LatY = 788830.173, Name = "H1N" },
                new MapLabel { LongX = 13981308.964, LatY = 788837.478, Name = "H1O" },
                new MapLabel { LongX = 13981294.469, LatY = 788855.337, Name = "H1P" },
                new MapLabel { LongX = 13981287.356, LatY = 788864.296, Name = "H1Q" },
                new MapLabel { LongX = 13981281.012, LatY = 788872.639, Name = "H1R" },
                new MapLabel { LongX = 13981274.168, LatY = 788881.406, Name = "H1S" },
                new MapLabel { LongX = 13981260.711, LatY = 788897.669, Name = "H1T" },

                new MapLabel { LongX = 13981259.942, LatY = 788868.448, Name = "H1V" },
                new MapLabel { LongX = 13981254.175, LatY = 788855.645, Name ="STAIR A"},
                new MapLabel { LongX = 13981295.344, LatY = 788805.239, Name ="STAIR B"},
                new MapLabel { LongX = 13981263.883, LatY = 788892.671, Name ="STAIR C"},
                new MapLabel { LongX = 13981303.312, LatY = 788842.822, Name ="STAIR D"},
                new MapLabel { LongX = 13981262.730, LatY = 788852.723, Name ="FHC"},
                new MapLabel { LongX = 13981268.689, LatY = 788888.653, Name ="FHC"},
                new MapLabel { LongX = 13981300.640, LatY = 788848.590, Name ="FHC"},
                new MapLabel { LongX = 13981328.304, LatY = 788804.335, Name ="TO GET BUILDING"},
                new MapLabel { LongX = 13981237.642, LatY = 788881.771, Name ="AUDITORIUM"},
                new MapLabel { LongX = 13981274.860, LatY = 788854.549, Name ="ATRIUM"},
                new MapLabel { LongX = 13981303.620, LatY = 788810.180, Name ="ATRIUM"},
                new MapLabel { LongX = 13981315.693, LatY = 788795.262, Name ="EXIT"},
                new MapLabel { LongX = 13981239.435, LatY = 788858.947, Name = "EXIT" },
                new MapLabel { LongX = 13981251.508, LatY = 788902.547, Name = "EXIT" }                
                };
            

        }

        public void LoadSecondFloorLabels()
        {
            mapLabels = new ObservableCollection<MapLabel>
            {
                new MapLabel { LongX = 13981283.189, LatY = 788835.916, Name = "H2A"},
                new MapLabel { LongX = 13981292.071, LatY = 788842.279, Name = "H2A"},
                new MapLabel { LongX = 13981299.434, LatY = 788829.707, Name = "H2B"},
                new MapLabel { LongX = 13981305.701, LatY = 788821.940, Name = "H2C"},
                new MapLabel { LongX = 13981311.718, LatY = 788814.346, Name = "H2D"},
                new MapLabel { LongX = 13981317.774, LatY = 788806.811, Name = "H2E"},
                new MapLabel { LongX = 13981327.386, LatY = 788814.308, Name = "H2F"},
                new MapLabel { LongX = 13981321.292, LatY = 788821.863, Name = "H2G"},
                new MapLabel { LongX = 13981315.178, LatY = 788829.553, Name = "H2H"},
                new MapLabel { LongX = 13981309.084, LatY = 788837.146, Name = "H2I"},
                new MapLabel { LongX = 13981292.859, LatY = 788857.774, Name = "H2J"},
                new MapLabel { LongX = 13981282.593, LatY = 788870.385, Name = "H2K"},
                new MapLabel { LongX = 13981273.096, LatY = 788882.458, Name = "H2L"},
                new MapLabel { LongX = 13981260.408, LatY = 788898.299, Name = "H2M"},
                new MapLabel { LongX = 13981263.715, LatY = 788871.539, Name = "H2N"},
                new MapLabel { LongX = 13981241.684, LatY = 788876.845, Name = "H2O"},
                new MapLabel { LongX = 13981248.220, LatY = 788862.503, Name = "H2P"},
                new MapLabel { LongX = 13981257.755, LatY = 788864.695, Name = "H2Q"},
                new MapLabel { LongX = 13981265.330, LatY = 788841.856, Name = "H2R"},
                new MapLabel { LongX = 13981271.482, LatY = 788833.974, Name = "H2S"},
                new MapLabel { LongX = 13981278.787, LatY = 788824.785, Name = "H2T"},
                new MapLabel { LongX = 13981284.708, LatY = 788816.749, Name = "H2U"},
                new MapLabel { LongX = 13981299.876, LatY = 788798.702, Name = "H2V"},
      
                new MapLabel { LongX = 13981290.129, LatY = 788808.868, Name = "FHC"},
                new MapLabel { LongX = 13981256.294, LatY = 788849.546, Name = "FHC"},
                new MapLabel { LongX = 13981268.713, LatY = 788888.725, Name = "FHC"},
                new MapLabel { LongX = 13981301.049, LatY = 788848.816, Name = "FHC"},

                new MapLabel { LongX = 13981258.351, LatY = 788857.832, Name = "STAIR A"},               
                new MapLabel { LongX =  13981290.836, LatY = 788801.859, Name = "STAIR B"},
                new MapLabel { LongX = 13981260.495, LatY = 788890.350, Name = "STAIR C"},
                new MapLabel { LongX = 13981300.059, LatY = 788840.597, Name = "STAIR D"},

                new MapLabel { LongX = 13981327.991, LatY = 788804.456, Name = "TO GET BLDG"},
                new MapLabel { LongX = 13981303.231, LatY = 788810.223, Name = "ATRIUM"},
                new MapLabel { LongX = 13981274.702, LatY = 788855.054, Name = "ATRIUM"}
            };
        }

        public void LoadThirdFloorLabels()
        {
            mapLabels = new ObservableCollection<MapLabel>
            {
                new MapLabel { LongX = 13981265.229, LatY = 788841.740, Name = "H3A"},
                new MapLabel { LongX = 13981272.236, LatY = 788832.765, Name = "H3B"},
                new MapLabel { LongX = 13981278.436, LatY = 788824.921, Name = "H3C"},
                new MapLabel { LongX = 13981285.213, LatY = 788816.501, Name = "H3D"},
                new MapLabel { LongX = 13981299.717, LatY = 788801.506, Name = "H3E"},
                new MapLabel { LongX = 13981284.030, LatY = 788836.340, Name = "H3F"},
                new MapLabel { LongX = 13981290.634, LatY = 788842.079, Name = "H3G"},
                new MapLabel { LongX = 13981299.573, LatY = 788829.463, Name = "H3H"},
                new MapLabel { LongX = 13981305.499, LatY = 788821.893, Name = "H3I"},
                new MapLabel { LongX = 13981311.598, LatY = 788814.280, Name = "H3J"},
                new MapLabel { LongX = 13981317.639, LatY = 788806.841, Name = "H3K"},
                new MapLabel { LongX = 13981327.242, LatY = 788814.439, Name = "H3L"},
                new MapLabel { LongX = 13981320.898, LatY = 788822.340, Name = "H3M"},
                new MapLabel { LongX = 13981315.246, LatY = 788829.492, Name = "H3N"},
                new MapLabel { LongX = 13981315.246, LatY = 788829.492, Name = "H3O"},
                new MapLabel { LongX = 13981294.512, LatY = 788855.560, Name = "H3P"},
                new MapLabel { LongX = 13981287.822, LatY = 788863.836, Name = "H3Q"},
                new MapLabel { LongX = 13981281.767, LatY = 788871.362, Name = "H3R"},
                new MapLabel { LongX = 13981274.558, LatY = 788880.301, Name = "H3S"},
                new MapLabel { LongX = 13981259.188, LatY = 788901.294, Name = "H3T"},
                new MapLabel { LongX = 13981265.359, LatY = 788873.669, Name = "H3U"},
                new MapLabel { LongX = 13981258.524, LatY = 788867.931, Name = "H3V"},
                new MapLabel { LongX = 13981243.933, LatY = 788876.207, Name = "H3W"},

                new MapLabel { LongX = 13981268.862, LatY = 788888.880, Name = "STAIR A"},
                new MapLabel { LongX = 13981288.832, LatY = 788800.237, Name = "STAIR B"},
                new MapLabel { LongX = 13981269.006, LatY = 788896.839, Name = "STAIR C"},
                new MapLabel { LongX = 13981308.743, LatY = 788847.212, Name = "STAIR D"},

                new MapLabel { LongX = 13981258.337, LatY = 788848.379, Name = "FHC"},
                new MapLabel { LongX = 13981290.129, LatY = 788808.888, Name = "FHC"},
                new MapLabel { LongX = 13981300.921, LatY = 788848.884, Name = "FHC"},
                new MapLabel { LongX = 13981268.740, LatY = 788888.707, Name = "FHC"}
            };
        }

        public void LoadFourthFloorLabels()
        {
            mapLabels = new ObservableCollection<MapLabel>
            {
                new MapLabel { LongX = 13981264.760, LatY = 788842.122, Name = "H4A"},
                new MapLabel { LongX = 13981271.681, LatY = 788833.356, Name = "H4B"},
                new MapLabel { LongX = 13981277.794, LatY = 788825.628, Name = "H4C"},
                new MapLabel { LongX = 13981284.888, LatY = 788816.977, Name = "H4D"},
                new MapLabel { LongX = 13981299.133, LatY = 788798.867, Name = "H4E"},
                new MapLabel { LongX = 13981284.023, LatY = 788836.585, Name = "H4F"},
                new MapLabel { LongX = 13981290.886, LatY = 788841.488, Name = "H4G"},
                new MapLabel { LongX = 13981298.960, LatY = 788830.472, Name = "H4H"},
                new MapLabel { LongX = 13981304.728, LatY = 788822.167, Name = "H4I"},
                new MapLabel { LongX = 13981311.072, LatY = 788815.016, Name = "H4J"},
                new MapLabel { LongX = 13981317.243, LatY = 788806.884, Name = "H4K"},
                new MapLabel { LongX = 13981324.308, LatY = 788815.506, Name = "H4L"},
                new MapLabel { LongX = 13981320.818, LatY = 788822.369, Name = "H4M"},
                new MapLabel { LongX = 13981314.301, LatY = 788830.357, Name = "H4N"},
                new MapLabel { LongX = 13981308.649, LatY = 788838.402, Name = "H4O"},
                new MapLabel { LongX = 13981294.087, LatY = 788856.165, Name = "H4P"},
                new MapLabel { LongX = 13981286.878, LatY = 788864.614, Name = "H4Q"},
                new MapLabel { LongX = 13981281.255, LatY = 788872.371, Name = "H4R"},
                new MapLabel { LongX = 13981274.046, LatY = 788880.907, Name = "H4S"},
                new MapLabel { LongX = 13981258.647, LatY = 788899.881, Name = "H4T"},
                new MapLabel { LongX = 13981265.222, LatY = 788872.948, Name = "H4U"},
                new MapLabel { LongX = 13981259.022, LatY = 788867.556, Name = "H4V"},
                new MapLabel { LongX = 13981246.219, LatY = 788863.692, Name = "H4W"},

                new MapLabel { LongX = 13981248.583, LatY = 788851.551, Name = "STAIR A"},
                new MapLabel { LongX = 13981284.254, LatY = 788804.173, Name = "STAIR B"},
              
                new MapLabel { LongX =  13981270.185, LatY = 788897.668, Name = "STAIR C"},
                new MapLabel { LongX = 13981311.475, LatY = 788848.942, Name = "STAIR D"},

                new MapLabel { LongX = 13981257.930, LatY = 788848.214, Name = "FHC"},
                new MapLabel { LongX = 13981290.198, LatY = 788809.025, Name = "FHC"},
                new MapLabel { LongX = 13981301.156, LatY = 788849.021, Name = "FHC"},
                new MapLabel { LongX = 13981268.686, LatY = 788888.700, Name = "FHC"}


            };
        }

        private string CopyShapeFile(string filename)
        {

            var folder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var path = Path.Combine(folder, "Library", "ShapeFiles");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            string outFileName = Path.Combine(path, filename);

            var myInput = Android.App.Application.Context.Assets.Open(filename);

            var myOutput = new FileStream(outFileName, FileMode.Create);

            byte[] buffer = new byte[1024];
            int length;
            try
            {
                while ((length = myInput.Read(buffer, 0, 1024)) > 0)
                    myOutput.Write(buffer, 0, length);

                myOutput.Flush();
                myOutput.Close();
                myInput.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }



            if (Directory.Exists(outFileName))
            {

            }
            return outFileName;
        }

    }
}
