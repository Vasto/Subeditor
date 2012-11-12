using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KWinFramework.Views;

namespace Subeditor.Views.Tools.TimingAdjusts
{
    /// <summary>
    /// Interfejs widoku narzędzia służącego do dostosowywania timingu napisów.
    /// </summary>
    interface ITimingAdjustmentToolView : IView
    {
        /// <summary>
        /// Zdarzenie zmiany wartości właściwości TimingChange.
        /// </summary>
        event EventHandler<EventArgs<long>> TimingChangeChanged;

        /// <summary>
        /// Zdarzenie zmiany wartości właściwości TimingFrom.
        /// </summary>
        event EventHandler<EventArgs<long>> TimingFromChanged;

        /// <summary>
        /// Zdarzenie zmiany wartości właściwości TimingTo.
        /// </summary>
        event EventHandler<EventArgs<long>> TimingToChanged;

        /// <summary>
        /// Zdarzenie mające miejsce gdy wartość właściwości ApplyToSelection ulegnie zmianie.
        /// </summary>
        event EventHandler<EventArgs<bool>> ApplyToSelectionChanged;

        /// <summary>
        /// Zdarzenie mające miejsce gdy wartość właściwości ApplyToAll ulegnie zmianie.
        /// </summary>
        event EventHandler<EventArgs<bool>> ApplyToAllChanged;

        /// <summary>
        /// Zdarzenie mające miejsce gdy wartość właściwości ApplyToRange ulegnie zmianie.
        /// </summary>
        event EventHandler<EventArgs<bool>> ApplyToRangeChanged;

        /// <summary>
        /// Zdarzenie mające miejsce gdy wartość właściwości UseFromRange ulegnie zmianie.
        /// </summary>
        event EventHandler<EventArgs<bool>> UseFromRangeChanged;

        /// <summary>
        /// Zdarzenie mające miejsce gdy wartość właściwości UseToRange ulegnie zmianie.
        /// </summary>
        event EventHandler<EventArgs<bool>> UseToRangeChanged;

        /// <summary>
        /// Zdarzenie mające miejsce gdy zostanie wciśnięty przycisk dodawania timingu.
        /// </summary>
        event EventHandler Add;

        /// <summary>
        /// Zdarzenie mające miejsce gdy zostanie wciśnięty przycisk odejmowania timingu.
        /// </summary>
        event EventHandler Substract;

        /// <summary>
        /// Pozwala pobrać lub ustawić wartość zmiany timingu.
        /// </summary>
        long TimingChange { get; set; }

        /// <summary>
        /// Pozwala pobrać lub ustawić wartość od jakiej ma zostać zmieniony timing.
        /// </summary>
        long TimingFrom { get; set; }

        /// <summary>
        /// Pozwala pobrać lub ustawić wartość, do której ma zostać zmieniony timing.
        /// </summary>
        long TimingTo { get; set; }

        /// <summary>
        /// Pozwala pobrać lub ustawić informacje czy zmiana timingu ma zostać zastosowana do całości.
        /// </summary>
        bool ApplyToAll { get; set; }

        /// <summary>
        /// Pozwala pobrać lub ustawić informacje czy zmiana timingu ma zostać zastosowana do zaznaczenia.
        /// </summary>
        bool ApplyToSelection { get; set; }

        /// <summary>
        /// Pozwala pobrać lub ustawić informacje czy zmiana timingu ma zostać zastosowana do wskazanego zakresu.
        /// </summary>
        bool ApplyToRange { get; set; }

        /// <summary>
        /// Pozwala pobrać lub ustawić informacje czy zakres ma określoną dolną wartość.
        /// </summary>
        bool UseRangeFrom { get; set; }

        /// <summary>
        /// Pozwala pobrać lub ustawić informacje czy zakres ma określoną górną wartość. 
        /// </summary>
        bool UseRangeTo { get; set; }

        /// <summary>
        /// Pozwala pobrać lub ustawić informacje czy element określający użycie dolnego zakresu, jest włączony.
        /// </summary>
        bool IsRangeFromEnabled { get; set; }

        /// <summary>
        /// Pozwala pobrać lub ustawić informacje czy element określający użycie górnego zakresu, jest włączony.
        /// </summary>
        bool IsRangeToEnabled { get; set; }

        /// <summary>
        /// Pozwala pobrać lub ustawić informacje czy element przechowujący dolną wartość zakresu, jest włączony.
        /// </summary>
        bool IsTimingFromEnabled { get; set; }

        /// <summary>
        /// Pozwala pobrać lub ustawić informacje czy element przechowujący górną wartość zakresu, jest włączony.
        /// </summary>
        bool IsTimingToEnabled { get; set; }
             
        /// <summary>
        /// Zmienia reprezentacje timingu na klatkową.
        /// </summary>
        void SwitchTimingToFrames();

        /// <summary>
        /// Zmienia reprezentację timingu na czasową.
        /// </summary>
        void SwitchTimingToTime();
    }
}
