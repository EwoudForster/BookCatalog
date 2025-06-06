﻿using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BookCatalog.ViewModels;

public class BaseViewModel : INotifyPropertyChanged
{
		bool _isBusy = false;

		bool IsBusy
		{
			get { return _isBusy; }
			set { SetProperty(ref _isBusy, value); }
		}

    string _title = string.Empty;

    public string Title
    {
        get { return _title; }
        set { SetProperty(ref _title, value); }
    }
    protected bool SetProperty<T>(ref T backingStore, T value,
        [CallerMemberName] string propertyName ="",
        Action? onChanged = null)
    {
        if (EqualityComparer<T>.Default.Equals(backingStore, value))
            return false;
        backingStore = value;
        onChanged?.Invoke();
        OnPropertyChanged(propertyName);
        return true;
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
    {
        var changed = PropertyChanged;
        if (changed == null)
            return;
        changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
