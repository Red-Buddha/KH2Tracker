using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows;

namespace KhTracker
{
    class ImportantCheck : INotifyPropertyChanged
    {
        public string Name;
        public int Address;
        public int Bytes = 1;
        private bool obtained = false;
        public bool Obtained
        {
            get { return obtained; }
            set
            {
                obtained = value;
                OnPropertyChanged("Obtained");
            }
        }
        public int ADDRESS_OFFSET;

        public MemoryReader memory;

        public ImportantCheck(MemoryReader mem, int address, int offset, string name)
        {
            ADDRESS_OFFSET = offset;
            Address = address;
            memory = mem;
            Name = name;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        public virtual byte[] UpdateMemory()
        {
            return memory.ReadMemory(Address + ADDRESS_OFFSET, Bytes);
        }

        public void BindLabel(ContentControl cc, string property, bool hideZero = true)
        {
            Binding binding = new Binding(property);
            binding.Source = this;
            if (hideZero)
            {
                binding.Converter = new HideZeroConverter();
            }
            cc.SetBinding(ContentControl.ContentProperty, binding);
        }

        public void BindImage(Image cc, string property)
        {
            Binding binding = new Binding(property);
            binding.Source = this;
            binding.Converter = new ObtainedConverter();
            cc.SetBinding(Image.OpacityProperty, binding);
        }
    }
}
