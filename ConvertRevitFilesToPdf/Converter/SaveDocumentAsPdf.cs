using Autodesk.Revit.UI;
using System;
using Autodesk.Revit.DB;
using System.Windows.Forms;
using System.IO;
using Autodesk.Revit.Attributes;

namespace ConvertRevitFilesToPdf
{
    [Transaction(TransactionMode.Manual)]
    class SaveDocumentAsPdf : IExternalCommand
    {
        #region Constants
        private const string _saveDialogFilter = "PDF|*.pdf";
        private const string _defaultPdfNameFormat = " PDF";
        #endregion Constants

        #region Private Properties
        private string _saveAsPdfPath { get; set; }
        private SaveFileDialog _saveDialog = new SaveFileDialog() { Filter = _saveDialogFilter };
        #endregion Private Properties

        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            try
            {
                Document document = commandData.Application.ActiveUIDocument.Document;
                string name = document.Title;
                _saveDialog.FileName = Path.GetFileNameWithoutExtension(name);

                if (_saveDialog.ShowDialog() == DialogResult.OK)
                {
                    // TO DO Implement convert logic (rvt->pdf)
                }

                return Result.Succeeded;
            }
            // This is where we "catch" potential errors and define how to deal with them
            catch (Autodesk.Revit.Exceptions.OperationCanceledException)
            {
                // If user decided to cancel the operation return Result.Canceled
                return Result.Cancelled;
            }
            catch (Exception ex)
            {
                // If something went wrong return Result.Failed
                message = ex.Message;
                return Result.Failed;
            }
        }
    }
}
