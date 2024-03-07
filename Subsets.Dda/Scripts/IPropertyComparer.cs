using System.ComponentModel;

namespace Subsets.Dda
{
    public interface IPropertyComparer
    {
         bool IsMatch();
         void RegisterValueChangeEvent(PropertyChangedEventHandler handler);
         void UnregisterValueChangeEvent(PropertyChangedEventHandler handler);
    }
}