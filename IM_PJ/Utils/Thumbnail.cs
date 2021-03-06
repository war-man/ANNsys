﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;

namespace IM_PJ.Utils
{
    public class Thumbnail
    {
        public enum Size
        {
            Source,
            Micro,
            Small,
            Normal,
            Large,
            XLarge
        }

        public static string getURL(string image, Size size)
        {
            if (String.IsNullOrEmpty(image))
            {
                return "/App_Themes/Ann/image/placeholder.png";
            }

            var directory = String.Empty;
            switch (size)
            {
                case Size.Micro:
                    directory = "85x113/";
                    break;
                case Size.Small:
                    directory = "159x212/";
                    break;
                case Size.Normal:
                    directory = "240x320/";
                    break;
                case Size.Large:
                    directory = "350x467/";
                    break;
                case Size.XLarge:
                    directory = "600/";
                    break;
                default:
                    directory = String.Empty;
                    break;
            }

            return String.Format("/uploads/images/{0}{1}", directory, image);
        }

        public static bool create(string path_file, int ideal_width, int ideal_height)
        {
            if (!File.Exists(path_file))
            {
                return false;
            }

            var directory = Path.GetDirectoryName(path_file);
            var filename = Path.GetFileName(path_file);
            var image = Image.FromFile(path_file);
            var width = image.Width;
            var height = image.Height;

            var resize = new Rectangle();

            var dir_thumb_1 = "";

            if (ideal_height > 0)
            {
                var aspect = width / (height * 1.0);
                var ideal_aspect = ideal_width / (ideal_height * 1.0);

                var offset = 0;
                

                if (aspect > ideal_aspect)
                {
                    // Then crop the left and right edges:
                    var new_width = Convert.ToInt32(ideal_aspect * height);
                    offset = (width - new_width) / 2;
                    resize = new Rectangle(offset, 0, new_width, height);
                }
                else
                {
                    // ... crop the top and bottom:
                    var new_height = Convert.ToInt32(width / ideal_aspect);
                    offset = (height - new_height) / 2;
                    resize = new Rectangle(0, offset, width, new_height);
                }

                dir_thumb_1 = String.Format("{0}/{1}x{2}", directory, ideal_width, ideal_height);
            }
            else
            {
                ideal_height = ideal_width * height / width;
                resize = new Rectangle(0, 0, width, height);

                dir_thumb_1 = String.Format("{0}/{1}", directory, ideal_width);
            }

            
            if (!Directory.Exists(dir_thumb_1))
            {
                Directory.CreateDirectory(dir_thumb_1);
            }

            var path_thumb_1 = String.Format("{0}/{1}", dir_thumb_1, filename);
            if (!File.Exists(path_thumb_1))
            {
                var bmpImage = new Bitmap(image);
                var thumb = bmpImage
                    .Clone(resize, bmpImage.PixelFormat)
                    .GetThumbnailImage(ideal_width, ideal_height, () => false, IntPtr.Zero);

                thumb.Save(path_thumb_1);
            }

            return true;
        }
    }
}