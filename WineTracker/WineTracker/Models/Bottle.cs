using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using PropertyChanged;
using SQLite;

namespace WineTracker.Models
{
    [ImplementPropertyChanged]
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
        public string Variety { get; set; }

        [NotNull]
        public int Vintage { get; set; }

        [NotNull]
        public string Producer { get; set; }

        [NotNull]
        public string Region { get; set; }

   
        public decimal AlcoholLevel { get; set; }


        public decimal Sulphites { get; set; }

        public decimal Size { get; set; }

       
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
