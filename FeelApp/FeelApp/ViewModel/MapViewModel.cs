using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using Esri.ArcGISRuntime.Data;
using Esri.ArcGISRuntime.Geometry;
using Esri.ArcGISRuntime.Location;
using Esri.ArcGISRuntime.Mapping;
using Esri.ArcGISRuntime.Security;
using Esri.ArcGISRuntime.Symbology;
using Esri.ArcGISRuntime.Tasks;
using Esri.ArcGISRuntime.UI;
using FeelApp.ViewModel.Base;
using Esri.ArcGISRuntime.Xamarin.Forms;
using Android.Content.Res;
using System.IO;
using System.Reflection;

namespace FeelApp
{
    /// <summary>
    /// Provides map data to an application
    /// </summary>
    public class MapViewModel : BaseViewModel
    {
        public MapViewModel()
        {
            //_mapView = mapView;
            //_mainMapView = new MapView();
            _mainMap = new Map(Basemap.CreateOpenStreetMap());
            GotoLocation();

        }
        public async void AddMarker()
        {
            GraphicsOverlay overlay = new GraphicsOverlay();
          
        }

        public async void GotoLocation()
        {
            // Create a new map to display in the map view with a streets basemap
            MainMapView.Map = MainMap;



            // Get the path to the downloaded shapefile
            string fileName = "GroundFloor.cpg";
            var filePath = CopyShapeFile(fileName);

            fileName = "GroundFloor.dbf";
            filePath = CopyShapeFile(fileName);

            fileName = "GroundFloor.prj";
            filePath = CopyShapeFile(fileName);

            fileName = "GroundFloor.qpj";
            filePath = CopyShapeFile(fileName);

            fileName = "GroundFloor.shx";
            filePath = CopyShapeFile(fileName);

            fileName = "GroundFloor.shp";
            filePath = CopyShapeFile(fileName);


            if (File.Exists(filePath))
            {
                // Open the shapefile
                ShapefileFeatureTable myShapefile = await ShapefileFeatureTable.OpenAsync(filePath);

                // Create a feature layer to display the shapefile
                FeatureLayer newFeatureLayer = new FeatureLayer(myShapefile);

                // Add the feature layer to the map
                MainMap.OperationalLayers.Add(newFeatureLayer);
                //var fullextent = newFeatureLayer.FullExtent;
                //MainMap.InitialViewpoint =new Viewpoint( fullextent);
                // Zoom the map to the extent of the shapefile
                await MainMapView.SetViewpointGeometryAsync(newFeatureLayer.FullExtent);

                
            }

         
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
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }



            if (Directory.Exists(outFileName))
            {
              
            }
            return outFileName;
        }



        /// <summary>
        /// Gets or sets the map
        /// </summary>
        private Map _mainMap;
        public Map MainMap
        {
            get { return _mainMap; }
            set { _mainMap = value; OnPropertyChanged(); }
        }

        private MapView _mainMapView;
        public MapView MainMapView
        {
            get { return _mainMapView; }
            set { SetProperty(ref _mainMapView, value, "MainMapView"); }
        }

    

        /// <summary>
        /// Raises the <see cref="MapViewModel.PropertyChanged" /> event
        /// </summary>
        /// <param name="propertyName">The name of the property that has changed</param>
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var propertyChangedHandler = PropertyChanged;
            if (propertyChangedHandler != null)
                propertyChangedHandler(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
