﻿using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace SlavApp.Resembler.DHash
{
    public class DHash
    {
        public ulong Compute(string filename)
        {
            var bits = new Any64();
            var scaled = this.GetThumbnail(filename, 9, 8);
            var img = this.ToGrayscale(scaled);
            for (var y = 0; y < 8; y++)
            {
                for (var x = 0; x < 8; x++)
                {
                    var p1 = img.GetPixel(x, y);
                    var p2 = img.GetPixel(x + 1, y);
                    bool diff = (p1.R + p1.G + p1.B) < (p2.R + p2.G + p2.B);
                    bits[y * 8 + x] = diff;
                }
            }

            var hash = bits.UINT64;
            return hash;
        }

        private Bitmap ToGrayscale(Image imgPhoto)
        {
            var newBitmap = new Bitmap(imgPhoto.Width, imgPhoto.Height);

            var colorMatrix = new ColorMatrix(new float[][]
            {
                new float[] {.3f, .3f, .3f, 0, 0},
                new float[] {.59f, .59f, .59f, 0, 0},
                new float[] {.11f, .11f, .11f, 0, 0},
                new float[] {0, 0, 0, 1, 0},
                new float[] {0, 0, 0, 0, 1}
            });

            var attributes = new ImageAttributes();

            attributes.SetColorMatrix(colorMatrix);

            using (var g = Graphics.FromImage(newBitmap))
            {
                g.DrawImage(imgPhoto, new Rectangle(0, 0, imgPhoto.Width, imgPhoto.Height), 0, 0, imgPhoto.Width, imgPhoto.Height, GraphicsUnit.Pixel, attributes);
            }

            return newBitmap;
        }

        private Image GetThumbnail(string filename, int width, int height)
        {
            using (var fs = new FileStream(filename, FileMode.Open))
            using (var img = Image.FromStream(fs, true, false))
            {
                return img.GetThumbnailImage(width, height, null, IntPtr.Zero);
            }
        }
    }
}