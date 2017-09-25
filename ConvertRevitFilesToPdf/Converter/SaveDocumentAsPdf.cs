using Autodesk.Revit.UI;
using System;
using Autodesk.Revit.DB;
using Autodesk.Revit.Attributes;

namespace ConvertRevitFilesToPdf
{
    [Transaction(TransactionMode.Manual)]
    class SaveDocumentAsPdf : IExternalCommand
    {
        #region Print Current Revit Document to PDF
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            try
            {
                Document document = commandData.Application.ActiveUIDocument.Document;
                string name = document.Title;
                PrintManager printManager = document.PrintManager;
                printManager.SelectNewPrintDriver("Microsoft Print to PDF");
                printManager.PrintToFile = true;
                printManager.CombinedFile = true;
                printManager.SubmitPrint();

                return Result.Succeeded;
            }
            catch (Autodesk.Revit.Exceptions.OperationCanceledException)
            {
                return Result.Cancelled;
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return Result.Failed;
            }
        }
        #endregion Print Current Revit Document to PDF
    }
}
