using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using PokemonGo.RocketAPI;
using PokemonGo.RocketAPI.Enums;

namespace PhoneTracker
{
    class Settings : ISettings
    {
        double defaultAltitude;
        double defaultLatitude;
        double defaultLongitude;
        string ptcPassword;
        string ptcUsername;
        string refreshToken;

        public Settings(double alt, double lat, double longt, string password, string username)
        {
            defaultAltitude = alt;
            defaultLatitude = lat;
            defaultLongitude = longt;
            ptcPassword = password;
            ptcUsername = username;
        }

        public AuthType AuthType
        {
            get
            {
                return AuthType.Google;
            }
        }

        public double DefaultAltitude
        {
            get
            {
                return defaultAltitude;
            }
        }

        public double DefaultLatitude
        {
            get
            {
                return defaultLatitude;
            }
        }

        public double DefaultLongitude
        {
            get
            {
                return defaultLongitude;
            }
        }

        public string GoogleRefreshToken
        {
            get
            {
                return refreshToken;
            }
            set
            {
                refreshToken = value;
            }
        }

        public string PtcPassword
        {
            get
            {
                return ptcPassword;
            }
        }

        public string PtcUsername
        {
            get
            {
                return ptcUsername;
            }
        }
    }
}