using System.ComponentModel;
using System.Runtime.CompilerServices;
using SQLite;

namespace WineTracker.Models
{
    [Table("BottleWineCategories")]
    public class BottleWineCategory : INotifyPropertyChanged
    {
        #region Properties
        [PrimaryKey, AutoIncrement]
        public int CategoryId
        {
            get
            {
                int value = _categoryId;
                return value;
            }
            set
            {
                if (_categoryId != value)
                {
                    _categoryId = value;
                    OnPropertyChanged();
                }
            }
        }
        private int _categoryId;

        [MaxLength(50)]
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


        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}