using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
public static class ScreenshotService
{
    public static async Task CaptureScreenshot()
    {
        Bitmap captureBitmap;
        if (Screen.PrimaryScreen != null)
        {
            Rectangle primaryScreen = Screen.PrimaryScreen.Bounds;
            captureBitmap = new Bitmap(primaryScreen.Width, primaryScreen.Height, PixelFormat.Format32bppRgb);
        }
        else
        {
            captureBitmap = new Bitmap(1280, 720, PixelFormat.Format32bppRgb);
        }
        Rectangle captureRectangle = Screen.AllScreens[0].Bounds; 
        Graphics captureGraphics = Graphics.FromImage(captureBitmap);
        captureGraphics.CopyFromScreen(captureRectangle.Left, captureRectangle.Top, 0, 0, captureRectangle.Size);
        string path = Path.Combine(Path.GetTempPath(), "Capture.png");
        captureBitmap.Save(path, ImageFormat.Png);
        await FileService.GetFileByPath(path);
        File.Delete(path); // Delete the temporary file
    }
}