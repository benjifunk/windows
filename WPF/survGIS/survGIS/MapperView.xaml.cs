using System;
using System.Windows;
using System.Windows.Controls;
using Esri.ArcGISRuntime.Data;
using Esri.ArcGISRuntime.Mapping;
using Esri.ArcGISRuntime.LocalServices;
using System.IO;
using System.Diagnostics;
using System.Reflection;
using ArcGISRuntime.Samples.Managers;
using Esri.ArcGISRuntime.Tasks;
using Esri.ArcGISRuntime.Tasks.Geoprocessing;
using System.Linq;
using System.Collections.Generic;
using Esri.ArcGISRuntime.Geometry;
using Esri.ArcGISRuntime.Symbology;
using System.Drawing;

namespace survGIS
{
    [ArcGISRuntime.Samples.Shared.Attributes.Sample(
        name: "Local Server Feature Layer",
        category: "Local Server",
        description: "Start a local server and feature service will automatically be started. if started, then added to the map",
        instructions: "Squid up",
        tags: new[] { "feature service", "local", "offline", "server", "service" })]
    [ArcGISRuntime.Samples.Shared.Attributes.OfflineData("4e94fec734434d1288e6ebe36c3c461f")]

    /// <summary>
    /// Interaction logic for MapperView.xaml
    /// </summary>
    public partial class MapperView : UserControl
    {
        private ShapefileFeatureTable sfFeatTable;
        private FeatureLayer newFeatureLayer;
        //private string _status = "";

        private string _currTaxID;

        public MapperView()
        {
            InitializeComponent();
            DataContext = new MapperViewModel();

            Initialize();
        }


        private async void Initialize()
        {
            Map myMap = new Map(BasemapType.TopographicVector, 43.3, -73.2, 8);

            try
            {
                if(LocalServer.Instance.Status == LocalServerStatus.Started)
                {
                    await LocalServer.Instance.StopAsync();
                }

                string tempDataPathRoot = Directory.GetParent(Environment.GetFolderPath(Environment.SpecialFolder.Windows)).FullName;
                string tempDataPath = System.IO.Path.Combine(tempDataPathRoot, "EsriSamples", "AppData");
                await LocalServer.Instance.StartAsync();

            }
            catch (Exception ex)
            {
                var localServerTypeInfo = typeof(LocalMapService).GetTypeInfo();
                var localServerVersion = FileVersionInfo.GetVersionInfo(localServerTypeInfo.Assembly.Location);

                MessageBox.Show($"Please ensure that local server {localServerVersion.FileVersion} is installed prior to usung this app. Thx bro.\n{ex}");
                return;
            }

            sfFeatTable = await ShapefileFeatureTable.OpenAsync("C://Users//benja//Desktop//ColumbiaCounty//ColumbiaCounty.shp");

            // Create feature layer using this feature table. Make it slightly transparent.
            newFeatureLayer = new FeatureLayer(sfFeatTable)
            {
                Opacity = 0.6,
                // Work around service setting.
                MaxScale = 10
            };

            // Create a new renderer for the States Feature Layer.
            SimpleLineSymbol lineSymbol = new SimpleLineSymbol(SimpleLineSymbolStyle.Solid, Color.Black, 1);
            SimpleFillSymbol fillSymbol = new SimpleFillSymbol(SimpleFillSymbolStyle.Solid, Color.Transparent, lineSymbol);

            // Set States feature layer renderer.
            newFeatureLayer.Renderer = new SimpleRenderer(fillSymbol);

            // Add feature layer to the map.
            myMap.OperationalLayers.Add(newFeatureLayer);

            MyMapView.Map = myMap;

            try
            {
                await MyMapView.SetViewpointGeometryAsync(newFeatureLayer.FullExtent, 50);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        public void SelectionChanged(object sender, EventArgs e)
        {
            if (MyMapView.Map != null)
            {
                DataGrid jobDG = (DataGrid)sender;
                if (jobDG.GetType() == typeof(Job))
                {
                    Job job = (Job)jobDG.SelectedItem;

                    if (job != null)
                    {
                        _currTaxID = string.Format("{0}.-{1}-{2}", job.Sec, job.Blk, job.Lot);
                    }
                }
            }
        }

        public async void UpdateExtents(object sender, RoutedEventArgs e)
        {
            statusTextBlock.Text = "Updating Extents";

            QueryParameters queryParams = new QueryParameters();

            queryParams.WhereClause = "TaxID LIKE '" + _currTaxID + "'";

            FeatureQueryResult queryResult = await sfFeatTable.QueryFeaturesAsync(queryParams);

            List<Feature> features = queryResult.ToList();

            if (features.Any())
            {
                EnvelopeBuilder envBuilder = new EnvelopeBuilder(SpatialReference.Create(102715));

                foreach (Feature feature in features)
                {
                    envBuilder.UnionOf(feature.Geometry.Extent);

                    newFeatureLayer.SelectFeature(feature);
                }

                await MyMapView.SetViewpointGeometryAsync(envBuilder.ToGeometry(), 20);
                statusTextBlock.Text = "";
            }

            else statusTextBlock.Text = "No parcel found for current query";
        }

    }
}
