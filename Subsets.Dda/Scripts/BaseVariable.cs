using System;
using System.ComponentModel;
using Godot;
using Subsets.Dda;

namespace Subsets.Dda
{
    public abstract class BaseVariable<T> : Resource, INotifyPropertyChanged, IRuntimeInitialize
    {
        [Export]
        public T Value
        {
            get
            {
                return this.value;
            }
            set
            {
                this.OldValue = this.value;
                this.value = value;
                OnPropertyChanged("Value");
            }
        }

        [Export]
        public T InitialValue
        {
            get
            {
                return this.initialValue;
            }
            set
            {
                this.initialValue = value;
                OnPropertyChanged("InitialValue");
            }
        }
        
        public event PropertyChangedEventHandler PropertyChanged;

        private T initialValue;
        private T value;
        public T OldValue;
        private string variableName;

        private void SetEnable()
        {
            this.variableName = this.ResourceName;
            RuntimeInstances.Register(this);
            
            GD.Print("BaseVariable::OnEnable: name is " + ( variableName.Length == 0 ? "dynamic_value:none": variableName));
            RuntimeInitialize();
            RaiseRuntimeInitializeEvent();
        }
        
        private void SetDisable()
        {
            RuntimeInstances.Unregister(this);
            
            GD.Print("BaseVariable::OnDisable: name is " + ( variableName.Length == 0 ? "dynamic_value:none": variableName));
            RuntimeFinalize();
        }

        private void Reset()
        {
        }

        public virtual void RuntimeInitialize()
        {
            GD.Print("BaseVariable::RuntimeInitialize: name is " + ( variableName.Length == 0 ? "dynamic_value:none" : variableName));
            if (initialValue != null)
            {
                this.value = Clone(initialValue);
            }
        }

        public virtual void RaiseRuntimeInitializeEvent()
        {
            GD.Print("BaseVariable::RaiseRuntimeInitializeEvent: name is " +
                      (variableName.Length == 0 ? "dynamic_value:none" : variableName));
            OnPropertyChanged("Value");
        }

        public virtual void RuntimeFinalize()
        {
            GD.Print("BaseVariable::RuntimeFinalize: name is " + (variableName.Length == 0 ? "dynamic_value:none" : variableName));
            PropertyChanged = null;
        }

        protected abstract T Clone(T value);

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}