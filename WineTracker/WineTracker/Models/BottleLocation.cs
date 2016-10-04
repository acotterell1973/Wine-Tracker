using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using SQLite;

namespace WineTracker.Models
{

    [Table("BottleLocations")]
    public class BottleLocation : INotifyPropertyChanged
    {

        #region Properties
        [PrimaryKey, AutoIncrement]
        public int LocationId
        {
            get
            {
                int value = _locationId;
                return value;
            }
            set
            {
                if (_locationId != value)
                {
                    _locationId = value;
                    OnPropertyChanged();
                }
            }
        }
        private int _locationId;



        public int Lat
        {
            get
            {
                int value = _lat;
                return value;
            }
            set
            {
                if (_lat != value)
                {
                    _lat = value;
                    OnPropertyChanged();
                }
            }
        }
        private int _lat;




        public int Lng
        {
            get
            {
                int value = _lng;
                return value;
            }
            set
            {
                if (_lng != value)
                {
                    _lng = value;
                    OnPropertyChanged();
                }
            }
        }
        private int _lng;




        public int? PlaceId
        {
            get
            {
                int? value = _placeId;
                return value;
            }
            set
            {
                if (_placeId != value)
                {
                    _placeId = value;
                    OnPropertyChanged();
                }
            }
        }
        private int? _placeId;


        public string FormattedAddress
        {
            get
            {
                string value = _formattedAddress;
                return value;
            }
            set
            {
                if (_formattedAddress != value)
                {
                    _formattedAddress = value;
                    OnPropertyChanged();
                }
            }
        }
        private string _formattedAddress;


   
        public DateTime CreatedDate
        {
            get
            {
                DateTime value = _createdDate;
                return value;
            }
            set
            {
                if (_createdDate != value)
                {
                    _createdDate = value;
                    OnPropertyChanged();
                }
            }
        }
        private DateTime _createdDate;

        #endregion
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}