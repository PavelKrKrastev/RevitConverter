using Autodesk.Revit.UI;
using System.IO;
using System.Reflection;
using System.Windows.Media.Imaging;

namespace ConvertRevitFilesToPdf
{
    public class AddButtonToRibbon : IExternalApplication
    {

        public Result OnStartup(UIControlledApplication application)
        {
            AddRibbonPanel(application);
            return Result.Succeeded;
        }

        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }

        private void AddRibbonPanel(UIControlledApplication application)
        {
            string tabName = "Converter";
            application.CreateRibbonTab(tabName);

            RibbonPanel ribbonPanel = application.CreateRibbonPanel(tabName, "Convert to PDF");

            string thisAssemblyPath = Assembly.GetExecutingAssembly().Location;
            PushButtonData buttonData = new PushButtonData("cmdSaveDocumentAsPdf",
               "Convert to PDF", thisAssemblyPath, "ConvertRevitFilesToPdf.SaveDocumentAsPdf");

            PushButton pushButton = ribbonPanel.AddItem(buttonData) as PushButton;
            pushButton.ToolTip = "Convert current document to pdf";

            pushButton.LargeImage = RibbonButtonImageSource("ConvertRevitFilesToPdf.Resources.convert.png");
        }

        #region Get image for ribbon button from resources
        private System.Windows.Media.ImageSource RibbonButtonImageSource(string EmbeddedPath)
        {
            Stream ImageSream = this.GetType().Assembly.GetManifestResourceStream(EmbeddedPath);
            var PngDecoder = new PngBitmapDecoder(ImageSream, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);
            return PngDecoder.Frames[0];
        }
        #endregion Get image for ribbon button from resources
    }
}
