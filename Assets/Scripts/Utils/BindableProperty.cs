using System;
using System.Collections.Generic;

public class BindableProperty<T>
{
    private T value;
    private Action<T> onValueChanged;

    public T Value
    {
        get => value;
        set
        {
            if (!EqualityComparer<T>.Default.Equals(this.value, value))
            {
                this.value = value;
                onValueChanged?.Invoke(value);
            }
        }
    }

    public void BindTo(Action<T> callback)
    {
        onValueChanged += callback;
    }

    public void UnbindFrom(Action<T> callback)
    {
        onValueChanged -= callback;
    }
}