using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace anket_wpf
{
    public class Person : INotifyPropertyChanged
    {
        private string? name;
        public string? Name
        {
            get => name; set
            {
                name = value;
                PropertyChanging();
            }
        }
        public string? Surname { get; set; }
        public string? Email { get; set; }
        public string? Number { get; set; }
        public string? Date { get; set; }
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void PropertyChanging([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}