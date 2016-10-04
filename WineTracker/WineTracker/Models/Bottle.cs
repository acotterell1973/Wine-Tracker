using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using SQLite;

namespace WineTracker.Models
{
    [Table("Bottles")]
    public class Bottle : INotifyPropertyChanged
    {
        #region Properties   
        [PrimaryKey, AutoIncrement]
        public int Id
        {
            get
            {
                int value = _id;
                return value;
            }
            set
            {
                if (_id != value)
                {
                    _id = value;
                    OnPropertyChanged();
                }
            }
        }
        private int _id;

        [NotNull]
        public int WineCategoriesCategoryId
        {
            get
            {
                int value = _wineCategoriesCategoryId;
                return value;
            }
            set
            {
                if (_wineCategoriesCategoryId != value)
                {
                    _wineCategoriesCategoryId = value;
                    OnPropertyChanged();
                }
            }
        }
        private int _wineCategoriesCategoryId;

        [NotNull]
        public int LocationId
        {
            get
            {
                int value = _locationsId;
                return value;
            }
            set
            {
                if (_locationsId != value)
                {
                    _locationsId = value;
                    OnPropertyChanged();
                }
            }
        }
        private int _locationsId;

        [NotNull]
        public string OccasionsCode
        {
            get
            {
                string value = _occasionsCode;
                return value;
            }
            set
            {
                if (_occasionsCode != value)
                {
                    _occasionsCode = value;
                    OnPropertyChanged();
                }
            }
        }
        private string _occasionsCode;

        [NotNull]
        public int ImageId
        {
            get
            {
                int value = _imageId;
                return value;
            }
            set
            {
                if (_imageId != value)
                {
                    _imageId = value;
                    OnPropertyChanged();
                }
            }
        }
        private int _imageId;

        [NotNull]
        public string Upc
        {
            get
            {
                string value = _upc;
                return value;
            }
            set
            {
                if (_upc != value)
                {
                    _upc = value;
                    OnPropertyChanged();
                }
            }
        }
        private string _upc;


        [NotNull]
        public string Name
        {
            get
            {
                string value = _name;
                return value;
            }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged();
                }
            }
        }
        private string _name;

        [NotNull]
        public string UserName
        {
            get
            {
                string value = _userName;
                return value;
            }
            set
            {
                if (_userName != value)
                {
                    _userName = value;
                    OnPropertyChanged();
                }
            }
        }
        private string _userName;

        [NotNull]
        public int Year
        {
            get
            {
                int value = _year;
                return value;
            }
            set
            {
                if (_year != value)
                {
                    _year = value;
                    OnPropertyChanged();
                }
            }
        }
        private int _year;

        [NotNull]
        public string Winery
        {
            get
            {
                string value = _winery;
                return value;
            }
            set
            {
                if (_winery != value)
                {
                    _winery = value;
                    OnPropertyChanged();
                }
            }
        }
        private string _winery;

        [NotNull]
        public bool Sync
        {
            get
            {
                bool value = _sync;
                return value;
            }
            set
            {
                if (_sync != value)
                {
                    _sync = value;
                    OnPropertyChanged();
                }
            }
        }
        private bool _sync;

        [NotNull]
        public string DeviceId
        {
            get
            {
                string value = _deviceId;
                return value;
            }
            set
            {
                if (_deviceId != value)
                {
                    _deviceId = value;
                    OnPropertyChanged();
                }
            }
        }
        private string _deviceId;

        [NotNull]
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
