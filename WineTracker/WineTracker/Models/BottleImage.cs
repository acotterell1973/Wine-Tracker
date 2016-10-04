using System.ComponentModel;
using System.Runtime.CompilerServices;
using SQLite;

namespace WineTracker.Models
{

    [Table("BottleImags")]
    public class BottleImage : INotifyPropertyChanged
    {
        #region Properties
        [PrimaryKey, AutoIncrement]
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


        public string Base64ImageData
        {
            get
            {
                string value = _base64ImageData;
                return value;
            }
            set
            {
                if (_base64ImageData != value)
                {
                    _base64ImageData = value;
                    OnPropertyChanged();
                }
            }
        }
        private string _base64ImageData;




        public byte[] ImageData
        {
            get
            {
                byte[] value = _imageData;

                return value;
            }
            set
            {
                if (_imageData != value)
                {
                    _imageData = value;
                    OnPropertyChanged();

                }
            }
        }
        private byte[] _imageData;


        #endregion
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}