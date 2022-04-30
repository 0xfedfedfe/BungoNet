using System;
using System.Reflection;

using GenHTTP.Api.Protocol;
using GenHTTP.Modules.Webservices;

using SkiaSharp;

namespace BungoNet.Services.Storage.Resource
{
    public class HopperMatchmakingNightmapResource
    {
        [ResourceMethod("dynamic_matchmaking_nightmap.jpg")]
        public Stream? GetMatchmakingMap(IRequest request)
        {
            SKBitmap outputBitmap = new(463, 152, true);
            SKCanvas canvas = new(outputBitmap);
            canvas.Clear(new SKColor(30, 40, 75));

            SKBitmap sabaBitmap;
            Assembly assembly = GetType().GetTypeInfo().Assembly;

            using (Stream stream = assembly.GetManifestResourceStream("BungoNet.Services.Storage.Content.saba_comorensis.jpg"))
            {
                sabaBitmap = SKBitmap.Decode(stream);
            }
            canvas.DrawBitmap(sabaBitmap, new SKRect(367.0f, 16.0f, 447.0f, 120.8f));

            SKPaint fontPaint;
            using (Stream stream = assembly.GetManifestResourceStream("BungoNet.Services.Storage.Content.Conduit_ITC.ttf"))
            {
                fontPaint = new(new SKFont(SKTypeface.FromStream(stream), 28));
            }
            fontPaint.IsAntialias = true;
            fontPaint.Color = new SKColor(90, 100, 135);

            canvas.DrawText("BungoNet, generated @", 16, 32, fontPaint);
            canvas.DrawText($"{DateTime.Now:yyyy-MM-dd HH:mm:ss}", 16, 60, fontPaint);

            fontPaint.TextSize = 8;
            canvas.DrawText($"{request["User-Agent"]}", 16, 138, fontPaint);

            MemoryStream memStream = new MemoryStream();

            using (SKManagedWStream wstream = new SKManagedWStream(memStream, false))
            {
                outputBitmap.Encode(wstream, SKEncodedImageFormat.Jpeg, 95);
            }

            return memStream;
        }
    }
}
