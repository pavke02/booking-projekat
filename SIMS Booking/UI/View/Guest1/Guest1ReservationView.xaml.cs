using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.TeamFoundation.Build.WebApi;
using SIMS_Booking.Model;
using SIMS_Booking.Model.Relations;
using SIMS_Booking.Service;
using SIMS_Booking.Service.RelationsService;
using SIMS_Booking.UI.ViewModel.Guest1;
using SIMS_Booking.UI.ViewModel.Owner;

namespace SIMS_Booking.UI.View.Guest1;

public partial class Guest1ReservationView : UserControl
{
    
    public Guest1ReservationView()
    {
        InitializeComponent();
        DataContextChanged += SubscribeToBlackoutDatesChangedEvent;
        DataContextChanged += SubscribeToEndDateChangedEvent;
        startDateDp.DisplayDateStart = DateTime.Today.AddDays(1);
    }


    private void SubscribeToBlackoutDatesChangedEvent(object sender, DependencyPropertyChangedEventArgs e)
    {
        var viewModel = (Guest1ReservationViewModel)DataContext;
        if (viewModel != null)
        {
            viewModel.BlackoutDatesChangedEvent += UpdateBlackoutDates;
            viewModel.DisableReservedDates();
        }
    }

    private void SubscribeToEndDateChangedEvent(object sender, DependencyPropertyChangedEventArgs e)
    {
        var viewModel = (Guest1ReservationViewModel)DataContext;
        if (viewModel != null)
        {
            viewModel.EndDpDateStartChangedEvent += UpdateEndDates;
        }
    }

    private void UpdateEndDates(int minDays)
    {
        if (startDateDp.SelectedDate.HasValue)
        {
            endDateDp.IsEnabled = true;
            endDateDp.DisplayDateStart = startDateDp.SelectedDate.Value.AddDays(minDays);
            endDateDp.SelectedDate = startDateDp.SelectedDate.Value.AddDays(minDays);
        }
    }

    //metoda koja onemogucuje rezervisane datume na kalendaru
    private void UpdateBlackoutDates(List<CalendarDateRange> blackoutDates)
    {
        startDateDp.BlackoutDates.Clear();
        foreach (var blackoutDate in blackoutDates)
        {
            startDateDp.BlackoutDates.Add(blackoutDate);
        }
    }

    //metoda koja kao opcije za kranji datum daje niz datuma od (selektovanog+1) do prvog (Blackout-ovanog-1)
    /*private void StartDateSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (startDateDp.SelectedDate.HasValue)
        {
            endDateDp.IsEnabled = true;
            endDateDp.DisplayDateEnd = startDateDp.BlackoutDates.FirstOrDefault(d => d.Start > startDateDp.SelectedDate.Value)?.Start.AddDays(-1);
        }
    }*/

    private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

    private static bool IsTextAllowed(string text)
    {
        // Allow only numeric input
        Regex regex = new Regex("[^0-9]+");
        return !regex.IsMatch(text);
    }
}