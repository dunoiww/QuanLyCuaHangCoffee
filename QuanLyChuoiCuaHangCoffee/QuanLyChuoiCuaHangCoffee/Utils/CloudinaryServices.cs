﻿using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace QuanLyChuoiCuaHangCoffee.Utils
{
    public class CloudinaryServices
    {
        private static CloudinaryServices _ins;
        public static CloudinaryServices Ins
        {
            get
            {
                if (_ins == null)
                {
                    _ins = new CloudinaryServices();
                }
                return _ins;
            }
            private set => _ins = value;
        }

        private Account account;
        private Cloudinary cloudinary;
        private CloudinaryServices()
        {
            account = new Account("dg0uneomp", "924294962494475", "Ahrb-2beUzb0TEJpKjHck2IYCGI");
            cloudinary = new Cloudinary(account);
            cloudinary.Api.Secure = true;
        }

        public async Task<string> UploadImage(string filePath)
        {
            try
            {
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(filePath),
                };

                var uploadResult = await cloudinary.UploadAsync(uploadParams);

                return uploadResult.SecureUrl.AbsoluteUri;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<BitmapImage> LoadImageFromURL(string imageURL)
        {
            if (string.IsNullOrEmpty(imageURL))
            {
                return null;
            }

            System.Net.WebRequest request = System.Net.WebRequest.Create(imageURL);
            System.Net.HttpWebResponse response;
            try
            {
                response = (await request.GetResponseAsync()) as System.Net.HttpWebResponse;
            }
            catch (System.Net.WebException)
            {
                return null;
            }

            System.IO.Stream responseStream = response.GetResponseStream();

            var bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.StreamSource = responseStream;
            bitmap.CacheOption = BitmapCacheOption.OnLoad;
            bitmap.EndInit();

            return bitmap;
        }
    }
}
