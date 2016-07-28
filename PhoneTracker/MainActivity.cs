using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using PokemonGo.RocketAPI;
using Android.Locations;
using System.Collections.Generic;
using System.Threading.Tasks;
using PogoLib;

namespace PhoneTracker
{
    [Activity(Label = "PhoneTracker", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity, ILocationListener
    {
        LocationManager _locationManager;
        string _locationProvider;
        Location _currentLocation;
        PogoClient _client;
        EditText _textUser;
        EditText _textPass;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            Button button = FindViewById<Button>(Resource.Id.logIn);
            button.Click += OnButtonClicked;
            _textUser = FindViewById<EditText>(Resource.Id.editText1);
            _textPass = FindViewById<EditText>(Resource.Id.editText2);
            InitializeLocationManager();
        }

        /// <summary>
        /// When the user clicks the button, log in.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">e.</param>
        async void OnButtonClicked(object sender, EventArgs e)
        {
            if (_currentLocation == null)
            {
                _currentLocation.Altitude = 378;
                _currentLocation.Latitude = 52.6000;
                _currentLocation.Longitude = -2.0000;
            }
            _client = new PogoClient(_currentLocation.Altitude, _currentLocation.Latitude, _currentLocation.Longitude, _textUser.Text, _textPass.Text);
            await _client.LogIn();
            var player = await _client.GetPlayerInformation();
            string test = player.Username;
        }

        void InitializeLocationManager()
        {
            _locationManager = (LocationManager)GetSystemService(LocationService);
            Criteria criteriaForLocationService = new Criteria
            {
                Accuracy = Accuracy.Fine
            };
            IList<string> acceptableLocationProviders = _locationManager.GetProviders(criteriaForLocationService, true);

            if (acceptableLocationProviders.Count != 0)
            {
                _locationProvider = acceptableLocationProviders[0];
            }
            else
            {
                _locationProvider = string.Empty;
            }
        }

        public async void OnLocationChanged(Location location)
        {
            _currentLocation = location;
            OnPause();
        }
        public void OnProviderDisabled(string provider)
        {
            throw new NotImplementedException();
        }

        public void OnProviderEnabled(string provider)
        {
            throw new NotImplementedException();
        }

        public void OnStatusChanged(string provider, [GeneratedEnum] Availability status, Bundle extras)
        {
            throw new NotImplementedException();
        }

        protected override void OnPause()
        {
            base.OnPause();
            _locationManager.RemoveUpdates(this);
        }

        protected override void OnResume()
        {
            base.OnResume();
            _locationManager.RequestLocationUpdates(_locationProvider, 0, 0, this);
        }
    }
}

