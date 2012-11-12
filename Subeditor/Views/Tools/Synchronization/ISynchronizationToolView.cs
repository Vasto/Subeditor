using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KWinFramework.Views;

namespace Subeditor.Views.Tools.Synchronization
{
    /// <summary>
    /// Interfejs widoku narzędzia sycnchroznizacji timingu napsiów.
    /// </summary>
    interface ISynchronizationToolView : IView
    {
        /// <summary>
        /// Zdarzenie mające miejsce gdy zmianie ulegnie aktualnie wybrany wpis na liście punktów synchronizacji.
        /// </summary>
        event EventHandler<EventArgs<int>> SelectedSynchronizationPointChanged;

        /// <summary>
        /// Zdarzenie mające miejsce gdy zmianie ulegnie wartość właściwości OrginalTiming.
        /// </summary>
        event EventHandler<EventArgs<long>> OrginalTimingChanged;

        /// <summary>
        /// Zdarzenie mające miejsce gdy zmianie zmianie ulegnie wartość właściwości CorrectTiming.
        /// </summary>
        event EventHandler<EventArgs<long>> CorrectTimingChanged;

        /// <summary>
        /// Zdarzenie mające miejsce gdy zostanie wciśnięty przycisk dodania wpsiu na liste timingów.
        /// </summary>
        event EventHandler Add;

        /// <summary>
        /// Zdarzenie mające miejsce gdy zostanie wciśnięty przycisk usunięcia wpsiu z listy timingów.
        /// </summary>
        event EventHandler Delete;

        /// <summary>
        /// Zdarzenie mające miejsce gdy zostanie wciśnięty przycisk modyfikacji wpisu z lisy timingów.
        /// </summary>
        event EventHandler Change;

        /// <summary>
        /// Zdarzenie mające miejsce gdy zostanie wciśnięty przycisk synchronizacji.
        /// </summary>
        event EventHandler Synchronize;

        /// <summary>
        /// Pozwala pobrać lub ustawić informację czy przycisk synchronizacji ma być włączony.
        /// </summary>
        bool CanSynchronize { get; set; }

        /// <summary>
        /// Pozwala pobrać lub ustawić informację czy przycisk Add ma być włączony.
        /// </summary>
        bool CanAdd { get; set; }

        /// <summary>
        /// Pozwala pobrać lub ustawić informację czy przycisk Delete ma być włączony.
        /// </summary>
        bool CanDelete { get; set; }

        /// <summary>
        /// Pozwala pobrać lub ustawić informację czy przycisk Change ma być włączony.
        /// </summary>
        bool CanChange { get; set; }

        /// <summary>
        /// Pozwala pobrać lub ustawić wartość pierwotnego timingu, dla punktu synchronizacji.
        /// </summary>
        long OrginalTiming { get; set; }

        /// <summary>
        /// Pozwala pobrać lub ustawić wartość poprawionego timingu, dla punktu synchronizacji.
        /// </summary>
        long CorrectTiming { get; set; }

        /// <summary>
        /// Zwraca lub ustawia indeks aktualnie wybranego punktu synchronizacji z listy.
        /// </summary>
        /// <remarks>Przyjmuje wartość ujemną jeżeli żaden element nie jest zaznaczony.</remarks>
        int SelectedSynchronizationPointIndex { get; set; }

        /// <summary>
        /// Zwraca liczbę punktów synchrozizacji na liście.
        /// </summary>
        int SynchronizationPointsCount { get; }

        /// <summary>
        /// Dodaje bieżącą wartość OrginalTiming i CorrectTiming jako wpis do listy punktów synchronizacji.
        /// </summary>
        void AddSynchronizationPoint();

        /// <summary>
        /// Usuwa aktualnie zaznaczony wpis z listy punktów synchronizacji.
        /// </summary>
        void DeleteSynchronizationPoint();

        /// <summary>
        /// Czyści listę punktów synchronizacji ze wszystkich wpisów.
        /// </summary>
        void ClearSynchronizationPoints();

        /// <summary>
        /// Zmienia wartość aktualnie zaznaczonego wpsiu na liście punktów synchronizacji,
        /// na zgodną z wartościami właściwości OrginalTiming i CorrectTiming.
        /// </summary>
        void ChangeSynchronizationPoint();

        /// <summary>
        /// Zmienia reprezentacje timingu wykorzystywane przez widok na klatkowe.
        /// </summary>
        void SwitchTimingToFrames();

        /// <summary>
        /// Zmienia reprezentację timingu wykorzystywane przez widok na czasowe.
        /// </summary>
        void SwitchTimingToTime();
    }
}
