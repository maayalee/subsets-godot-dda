using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Godot;
using Subsets.Dda;

namespace Subsets.Dda
{
    public abstract class Variable<TValue> : Resource, INotifyPropertyChanged, IRuntimeInitialize, IVariable
    {
        public TValue Value
        {
            get
            {
                OnEnable();
                return this.value;
            }
            set
            {
                OnEnable();
                this.OldValue = this.value;
                this.value = value;
                OnPropertyChanged("Value");
            }
        }

        protected abstract TValue InitialValue { get; set; }
        public event PropertyChangedEventHandler PropertyChanged = null;

        private TValue value;
        public TValue OldValue;
        protected string valueName = "";
        private bool initialized = false;

        protected Variable()
        {
            //GD.Print("BaseVariable::BaseVariable");
            
            if (!Engine.EditorHint)
            {
                RuntimeInstances.Register(this);
            }

            initialized = false;
        }

        public void OnEnable()
        {
            if (initialized == true)
                return;
            initialized = true;
            this.valueName = this.ResourcePath;
            //GD.Print("BaseVariable::Enable: name is " + ( valueName.Length == 0 ? "none": valueName));
            RuntimeInitialize();
            RaiseRuntimeInitializeEvent();
        }
        
        protected override void Dispose(bool disposing)
        {
            //GD.Print("BaseVariable::Dispose: name is " + ( valueName.Length == 0 ? "none": valueName));
            if (!Engine.EditorHint)
            {
                OnDisable();
                RuntimeInstances.Unregister(this);
            }
            base.Dispose(disposing);
        }
        
        public void OnDisable()
        {
            //GD.Print("BaseVariable::Disable: name is " + ( valueName.Length == 0 ? "none": valueName));
            RuntimeFinalize();
        }
        
        public virtual void RuntimeInitialize()
        {
            //GD.Print("BaseVariable::RuntimeInitialize: name is " + ( valueName.Length == 0 ? "none" : valueName));
            if (InitialValue != null)
            {
                this.value = DuplicateValue(InitialValue);
            }
        }

        public virtual void RaiseRuntimeInitializeEvent()
        {
            //GD.Print("BaseVariable::RaiseRuntimeInitializeEvent: name is " + (valueName.Length == 0 ? "none" : valueName));
            OnPropertyChanged("Value");
        }

        public virtual void RuntimeFinalize()
        {
            //GD.Print("BaseVariable::RuntimeFinalize: name is " + (valueName.Length == 0 ? "none" : valueName));
            PropertyChanged = null;
        }

        protected abstract TValue DuplicateValue(TValue value);

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void NotifyChange()
        {
            OnPropertyChanged("Value");
        }
    }
}