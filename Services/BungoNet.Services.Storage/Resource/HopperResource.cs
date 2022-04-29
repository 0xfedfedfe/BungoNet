using System;
using System.IO;

using GenHTTP.Modules.Webservices;

using SkiaSharp;

//using BungoNet.Services.Storage.Model;

namespace BungoNet.Services.Storage.Resource
{
    public class HopperResource
    {
        [ResourceMethod("dynamic_matchmaking_nightmap.jpg")]
        public Stream? GetMatchmakingMap()
        {
            SKBitmap bitmap = new(463, 152, true);
            SKCanvas canvas = new(bitmap);
            canvas.Clear(new SKColor(30, 40, 75));

            SKPaint paint = new(new SKFont(SKTypeface.FromFamilyName("Consolas", 400, 32, SKFontStyleSlant.Upright)));
            paint.TextSize = 24;
            paint.IsAntialias = true;
            paint.Color = new SKColor(90, 100, 135);
            canvas.DrawText("BungoNet, generated @", 16, 32, paint);
            canvas.DrawText($"{DateTime.Now:yyyy-MM-dd HH:mm:ss}", 16, 55, paint);

            MemoryStream memStream = new MemoryStream();

            using (SKManagedWStream wstream = new SKManagedWStream(memStream, false))
            {
                bitmap.Encode(wstream, SKEncodedImageFormat.Jpeg, 75);
            }

            return memStream;
        }
    }
}
