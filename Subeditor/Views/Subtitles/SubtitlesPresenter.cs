using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KWinFramework;
using KWinFramework.Presenters;
using KWinFramework.Views;
using Subeditor.Model;
using Subeditor.Model.Modifications;
using Subeditor.Utilities;

namespace Subeditor.Views.Subtitles
{
    /// <summary>
    /// Prezenter napisów.
    /// </summary>
    class SubtitlesPresenter : PresenterBase<ISubtitlesView>
    {
        private SubtitlesManager manager;
        private SubtitlesEditor editor;
        private SubtitlesFile subtitles;
        private ModificationComposer composer;
        private int previousLineNumberingDigits;

        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="viewManager">Obiekt menadżera widoków.</param>
        /// <param name="view">Obiekt zarządzanego przez prezentera widoku.</param>
        /// <param name="subtitlesManager">Obiekt modelu menadżera plików.</param>
        /// <param name="subtitlesEditor">Edytor napisów.</param>
        public SubtitlesPresenter(IViewManager viewManager, ISubtitlesView view, SubtitlesManager subtitlesManager, SubtitlesEditor subtitlesEditor)
            : base(viewManager, view)
        {
            this.manager = subtitlesManager;
            this.manager.CurrentSubtitlesChanged += new EventHandler<SubtitlesChangedEventArgs>(CurrentSubtitlesChangedHandler);

            this.subtitles = subtitlesManager.CurrentSubtitles;
            this.subtitles.ContentChanged += new EventHandler<SubtitlesContentChangedEventArgs>(SubtitlesContentChangedHandler);

            this.View.Content = this.subtitles.Content;
            this.View.SelectionChanged += new EventHandler<SelectionChangedEventArgs>(ViewSelectionChangedHandler);
            this.View.ContentChanged += new EventHandler(ViewContentChangedHandler);
            this.View.Scrolled += new EventHandler<ScrolledEventArgs>(ViewScrolledHandler);

            this.editor = subtitlesEditor;
            this.editor.EditStateChanged += new EventHandler<SubtitlesEditStateChangedEventArgs>(EditorEditStateChangedHandler);

            this.composer = new ModificationComposer();

            this.previousLineNumberingDigits = 1;
        }

        private void CurrentSubtitlesChangedHandler(object sender, SubtitlesChangedEventArgs e)
        {
            if (e.OldSubtitles != null)
            {
                e.OldSubtitles.ContentChanged -= new EventHandler<SubtitlesContentChangedEventArgs>(SubtitlesContentChangedHandler);
            }
            subtitles = e.NewSubtitles;
            subtitles.ContentChanged += new EventHandler<SubtitlesContentChangedEventArgs>(SubtitlesContentChangedHandler);

            View.ScrollToBegin();
        }

        private void SubtitlesContentChangedHandler(object sender, EventArgs e)
        {
            int firstVisible = View.FirstVisibleLine;

            SwitchViewContentChangedHandling(false);
            View.Content = subtitles.Content;
            SwitchViewContentChangedHandling(true);

            //Przewinięcie do widocznej lini jest konieczne z tego powodu,
            //że zmiana wartości właściowści View.Content, powoduje przeskok na sam początek tekstu,
            //przez co edytowanie ręczne teksyu jest nie możliwe.
            View.ScrollByLine(firstVisible);
        }

        private void ViewContentChangedHandler(object sender, EventArgs e)
        {
            SwitchSubtitlesContentChangedHandling(false);

            composer.Begin();
            composer.Add(new SubtitlesContentModification(View.Content, SubtitlesContentModificationArea.CaretOrSelectionEnd));

            SwitchSubtitlesContentChangedHandling(true);
        }

        private void ViewScrolledHandler(object sender, ScrolledEventArgs e)
        {
            int lastVisibleLineNumber = e.NewLineNumber + View.VisibleLinesCount;
            int charsCount = lastVisibleLineNumber.ToString().Length;
            int widthBase = 25;
            int widthIncrement = 9;

            if (previousLineNumberingDigits != charsCount)
            {
                if (charsCount > 3)
                {
                    View.LineNumberColumnWidth = widthBase + (widthIncrement * (charsCount - 3));
                }
                else
                {
                    View.LineNumberColumnWidth = widthBase;
                }
            }

            previousLineNumberingDigits = charsCount;
        }

        private void EditorEditStateChangedHandler(object sender, SubtitlesEditStateChangedEventArgs e)
        {
            //    int caretPosition = newState.CaretPosition;
            //    int correctedCaretPosition = TextUtilities.GetByteCount(subtitles.Content, 0, caretPosition);

            //    View.CaretPosition = correctedCaretPosition;

            var newState = e.NewState;

            //Karetka sama sie ustawia po ustawieniu zaznaczenia.
            Selection selection = newState.Selection;
            int correctedSelectionStart = TextUtilities.GetByteCount(subtitles.Content, 0, selection.Start);
            int correctedSelectionLength = TextUtilities.GetByteCount(subtitles.Content, selection.Start, selection.Length);

            SwitchViewSelectionChangedHandling(false);
            View.Select(correctedSelectionStart, correctedSelectionLength);
            SwitchViewSelectionChangedHandling(true);

            EnsureCaretVisible();
        }


        /// <remarks>
        /// Obsługa zdarzenia o zmianie zaznaczenia w widoku. W przypadku zmiany tekstu w widoku,
        /// to zdarzenie następuje po zdarzeniu o zmianie tekstu.
        /// </remarks>
        private void ViewSelectionChangedHandler(object sender, SelectionChangedEventArgs e)
        {
            int correctedSelectionStart = TextUtilities.GetCharCount(View.RawContent, 0, e.Start);
            int correctedSelectionLength = TextUtilities.GetCharCount(View.RawContent, e.Start, e.Length);

            //Jeśli karetka znajduje się na początku zazaczenia to jej pozycja jest wyznaczona przez pozycjie początku zaznaczenia.
            //Jeśli nie to karetka znajduje się na końcu zaznaczenia.
            int caretPosition = (View.CaretPosition == e.Start) ? (correctedSelectionStart) : (correctedSelectionStart + correctedSelectionLength);

            Selection selection = new Selection(correctedSelectionStart, correctedSelectionLength);

            SubtitlesEditState newEditState = new SubtitlesEditState(caretPosition, selection);

            SubtitlesEditStateModification stateModification = new SubtitlesEditStateModification(newEditState);

            if (composer.IsComposing)
            {
                composer.Add(stateModification);
                var modification = composer.End();

                editor.PerformModification(modification);
            }
            else
            {
                //Trzeba wyłączyć obsługe zdarzenia zwrotnego, ponieważ edytor zwraca informacje o zaznaczeniu, która jest nieaktualne dla widoku.
                //To do: sprawdzić czy nie da sie jakoś tego wyeliminować
                SwitchEditorEditStateChangedHandling(false);
                editor.PerformModification(stateModification);
                SwitchEditorEditStateChangedHandling(true);
            }
        }

        private void CaretPositionChangedHandler(object sender, EventArgs<int> e)
        {
            int correctedCaretPosition = TextUtilities.GetByteCount(subtitles.Content, 0, e.Value);
            View.CaretPosition = correctedCaretPosition;

            EnsureCaretVisible();
        }

        private void EnsureCaretVisible()
        {
            if (View.FirstVisibleLine + View.VisibleLinesCount < View.CaretLine)
            {
                View.ScrollToCaret();
            }
        }

        private void SwitchViewContentChangedHandling(bool isHandling)
        {
            if (isHandling)
            {
                View.ContentChanged += new EventHandler(ViewContentChangedHandler);
            }
            else
            {
                View.ContentChanged -= new EventHandler(ViewContentChangedHandler);
            }
        }

        private void SwitchSubtitlesContentChangedHandling(bool isHandling)
        {
            if (isHandling)
            {
                subtitles.ContentChanged += new EventHandler<SubtitlesContentChangedEventArgs>(SubtitlesContentChangedHandler);
            }
            else
            {
                subtitles.ContentChanged -= new EventHandler<SubtitlesContentChangedEventArgs>(SubtitlesContentChangedHandler);
            }
        }

        private void SwitchViewSelectionChangedHandling(bool isHandling)
        {
            if (isHandling)
            {
                View.SelectionChanged += new EventHandler<SelectionChangedEventArgs>(ViewSelectionChangedHandler);
            }
            else
            {
                View.SelectionChanged -= new EventHandler<SelectionChangedEventArgs>(ViewSelectionChangedHandler);
            }
        }

        private void SwitchEditorEditStateChangedHandling(bool isHandling)
        {
            if (isHandling)
            {
                editor.EditStateChanged += new EventHandler<SubtitlesEditStateChangedEventArgs>(EditorEditStateChangedHandler);
            }
            else
            {
                editor.EditStateChanged -= new EventHandler<SubtitlesEditStateChangedEventArgs>(EditorEditStateChangedHandler);
            }
        }

    }
}
