using System;
using System.Windows.Forms;
using KWinFramework.Views;
using KWinFramework.Views.WinForms;
using KWinFramework.Views.WinForms.Commands;
using KWinFramework.Views.WinForms.Menus;
using Subeditor.Model;
using Subeditor.Model.FileFormats;
using Subeditor.Model.StateManagement;
using Subeditor.Model.Tools;
using Subeditor.Views.Commands;
using Subeditor.Views.ContextMenu;
using Subeditor.Views.Main;
using Subeditor.Views.Menus.Encodings;
using Subeditor.Views.Subtitles;

namespace Subeditor
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

#if DEBUG
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en-US"); 
#endif

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            IViewManager viewManager = new WinFormsViewManager();

            SubtitlesManager subtitlesManager = new SubtitlesManager();

            //Określenie obsługiwanych formatów przed wczytaniem pliku.
            subtitlesManager.AddSupportedSubtitles(new AssFileFormat());
            subtitlesManager.AddSupportedSubtitles(new SSAFileFormat());
            subtitlesManager.AddSupportedSubtitles(new SubripFileFormat());
            subtitlesManager.AddSupportedSubtitles(new MicroDVDFileFormat());
            subtitlesManager.AddSupportedSubtitles(new TMPlayerFileFormat());

            MainFormView mainFormView = new MainFormView(Subeditor.Properties.Resources.NameMainView);
            viewManager.AddView(mainFormView);

            ApplicationManager appManager = new ApplicationManager(viewManager);
            MainFormPresenter mainFormPresenter = new MainFormPresenter(viewManager, mainFormView, appManager, subtitlesManager);

            UndoRedoManager undoRedoManager = new UndoRedoManager();

            ClipboardAssistant assistant = new ClipboardAssistant();
            mainFormView.ClipboardChanged += assistant.OnClipboardChanged;

            SubtitlesEditor editor = new SubtitlesEditor(subtitlesManager, undoRedoManager);
            (editor.Clipboard as SubtitlesClipboard).ClipboardAssistant = assistant;

            //<--------------------------------------------------------------Tools-------------------------------------------------------------->

            TimingAdjustmentTool timingAdjustmentTool = new TimingAdjustmentTool(subtitlesManager, editor);
            SynchronizationTool synchronizationTool = new SynchronizationTool(subtitlesManager, editor);

            //<-------------------------------------------------------SubtitlesPresenter-------------------------------------------------------->

            ISubtitlesView subtitlesFileView = (ISubtitlesView) viewManager.GetView("SubtitlesView");
            SubtitlesPresenter subtitlesFilePresenter = new SubtitlesPresenter(viewManager, subtitlesFileView, subtitlesManager, editor);

            //<-------------------------------------------------------Menu File Commands-------------------------------------------------------->

            ICommandView openFileCmdView = (ICommandView)viewManager.GetView("MenuOpenView");   
            OpenFileCommandPresenter openFileCmdPresenter = new OpenFileCommandPresenter(viewManager, openFileCmdView, subtitlesManager);

            ICommandView saveFileCmdView = (ICommandView)viewManager.GetView("MenuSaveView");
            SaveFileCommandPresenter saveFileCommandPResenter = new SaveFileCommandPresenter(viewManager, saveFileCmdView, subtitlesManager);

            ICommandView saveAsFileCmddView = (ICommandView)viewManager.GetView("MenuSaveAsView");
            SaveAsFileCommandPresenter saveAsFileCmdPResenter = new SaveAsFileCommandPresenter(viewManager, saveAsFileCmddView, subtitlesManager);

            ICommandView exitCmdView = (ICommandView)viewManager.GetView("MenuExitView");
            ExitCommandPresenter exitCmdPresenter = new ExitCommandPresenter(viewManager, exitCmdView, appManager);

            //<--------------------------------------------------------------------------------------------------------------------------------->

            //<-------------------------------------------------------Menu Edit Commands-------------------------------------------------------->

            ICommandView cutCmdView = (ICommandView)viewManager.GetView("MenuCutView");
            CutCommandPresenter cutCmdPresenter = new CutCommandPresenter(viewManager, cutCmdView, editor);

            ICommandView copyCmdView = (ICommandView)viewManager.GetView("MenuCopyView");
            CopyCommandPresenter copyCmdPresenter = new CopyCommandPresenter(viewManager, copyCmdView, editor);

            ICommandView pasteCmdView = (ICommandView)viewManager.GetView("MenuPasteView");
            PasteCommandPresenter pasteCmdPresenter = new PasteCommandPresenter(viewManager, pasteCmdView, editor);

            ICommandView selectAllCmdView = (ICommandView)viewManager.GetView("MenuSelectAllView");
            SelectAllCommandPresenter selectAllCmdPresenter = new SelectAllCommandPresenter(viewManager, selectAllCmdView, editor);

            ICommandView selectFromCmdView = (ICommandView)viewManager.GetView("MenuSelectFromView");
            SelectFromCommandPresenter selectFromCmdPresenter = new SelectFromCommandPresenter(viewManager, selectFromCmdView, editor);

            ICommandView menuUndoCmdView = (ICommandView)viewManager.GetView("MenuUndoView");
            UndoCommandPresenter menuUndoCmdPresenter = new UndoCommandPresenter(viewManager, menuUndoCmdView, undoRedoManager);

            ICommandView menuRedoCmdView = (ICommandView)viewManager.GetView("MenuRedoView");
            RedoCommandPresenter menuRedoCmdPresenter = new RedoCommandPresenter(viewManager, menuRedoCmdView, undoRedoManager);

            //<--------------------------------------------------------------------------------------------------------------------------------->

            //<-------------------------------------------------------Menu Tools Commands------------------------------------------------------->

            ICommandView adjustTimingCmdView = (ICommandView)viewManager.GetView("MenuTimingAdjustmentView");
            TimingAdjustmentCommandPresenter adjustTimingCmdPresenter = new TimingAdjustmentCommandPresenter(
                viewManager, 
                adjustTimingCmdView, 
                subtitlesManager,
                editor, 
                timingAdjustmentTool);

            ICommandView synchronizationCmdView = (ICommandView)viewManager.GetView("MenuSynchronizationView");
            SynchronizationCommandPresenter synchronizationCmdPresenter = new SynchronizationCommandPresenter(
                viewManager, 
                synchronizationCmdView, 
                subtitlesManager,
                synchronizationTool);

            //<--------------------------------------------------------------------------------------------------------------------------------->

            //<-----------------------------------------------------------Toolbar -------------------------------------------------------------->

            ICommandView toolbarOpenFileCmdView = (ICommandView)viewManager.GetView("ToolbarOpenFileView");
            OpenFileCommandPresenter toolbarOpenFileCmdPresenter = new OpenFileCommandPresenter(viewManager, toolbarOpenFileCmdView, subtitlesManager);

            ICommandView toolbarSaveFileCmdView = (ICommandView)viewManager.GetView("ToolbarSaveFileView");
            SaveFileCommandPresenter toolbarSaveFileCmdPResenter = new SaveFileCommandPresenter(viewManager, toolbarSaveFileCmdView, subtitlesManager);

            ICommandView toolbarSaveAsFileCmdView = (ICommandView)viewManager.GetView("ToolbarSaveAsFileView");
            SaveAsFileCommandPresenter toolbarSaveAsFileCmdPResenter = new SaveAsFileCommandPresenter(viewManager, toolbarSaveAsFileCmdView, subtitlesManager);

            ICommandView toolbarCutCmdView = (ICommandView)viewManager.GetView("ToolbarCutView");
            CutCommandPresenter toolbarCutCmdPresenter = new CutCommandPresenter(viewManager, toolbarCutCmdView, editor);

            ICommandView toolbarCopyCmdView = (ICommandView)viewManager.GetView("ToolbarCopyView");
            CopyCommandPresenter toolbarCopyCmdPresenter = new CopyCommandPresenter(viewManager, toolbarCopyCmdView, editor);

            ICommandView toolbarPasteCmdView = (ICommandView)viewManager.GetView("ToolbarPasteView");
            PasteCommandPresenter toolbarPasteCmdPresenter = new PasteCommandPresenter(viewManager, toolbarPasteCmdView, editor);

            ICommandView toolbarUndoCmdView = (ICommandView)viewManager.GetView("ToolbarUndoView");
            UndoCommandPresenter toolbarUndoCmdPresenter = new UndoCommandPresenter(viewManager, toolbarUndoCmdView, undoRedoManager);

            ICommandView toolbarRedoCmdView = (ICommandView)viewManager.GetView("ToolbarRedoView");
            RedoCommandPresenter toolbarRedoCmdPresenter = new RedoCommandPresenter(viewManager, toolbarRedoCmdView, undoRedoManager);

            IToolStripComboBoxView toolbarEncodingView = (IToolStripComboBoxView)viewManager.GetView("ToolbarEncodingView");
            EncodingComboBoxPresenter toolbarEncodingPresenter = new EncodingComboBoxPresenter(viewManager, toolbarEncodingView, subtitlesManager);

            //<--------------------------------------------------------------------------------------------------------------------------------->

            //<-------------------------------------------------------Context Menu-------------------------------------------------------------->
            MainContextMenuStripView mainContextMenuStripView = new MainContextMenuStripView();
            (subtitlesFileView as IHierarchicalView).AddChildView(mainContextMenuStripView);
            MainContextMenuStripPresenter mainContextMenuStripPresenter = new MainContextMenuStripPresenter(viewManager, mainContextMenuStripView, editor);

            //<--------------------------------------------------------------------------------------------------------------------------------->

            //Próbuje otworzyć plik, w sytuacji gdy użytkownik uruchomił program poprzez polecenie "otwórz za pomocą".
            bool operationResult = subtitlesManager.TryLoadFromClickOnceDeploymentSystem();
            if (operationResult == false)
            {
                subtitlesManager.TryLoadFromLineArgs();
            }

            //<--------------------------------------------------------------------------------------------------------------------------------->

            viewManager.ShowView(mainFormView);
        }
    }
}
