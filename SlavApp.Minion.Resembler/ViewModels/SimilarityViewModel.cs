﻿using Caliburn.Micro;
using ExifLib;
using SlavApp.Minion.Resembler.Messages;
using SlavApp.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace SlavApp.Minion.Resembler
{
    public class SimilarityViewModel : Caliburn.Micro.PropertyChangedBase
    {
        private string name;
        private int value;
        private ObservableCollection<ExifData> exif;
        private bool exifLoaded;

        private readonly IEventAggregator eventAggregator;
        public SimilarityViewModel(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;
        }

        public string Name
        {
            get { return name; }
            set
            {
                if (this.name != value)
                {
                    this.exifLoaded = false;
                }
                this.name = value;
                NotifyOfPropertyChange(() => Name);
                NotifyOfPropertyChange(() => FileName);
                NotifyOfPropertyChange(() => Exif);
            }
        }
        public string FileName
        {
            get { return Path.GetFileNameWithoutExtension(this.name); }
        }

        public int Value
        {
            get { return value; }
            set
            {
                this.value = value;
                NotifyOfPropertyChange(() => Value);
            }
        }

        public ObservableCollection<ExifData> Exif
        {
            get 
            { 
                if (!exifLoaded)
                {
                    exifLoaded = true;
                    UpdateExif();
                }
                return exif; 
            }
        }

        private void GetExifTag(ExifReader reader, ExifTags tag, string title, List<ExifData> list)
        {
            object value = null;
            if (reader.GetTagValue<object>(tag, out value))
            {
                list.Add(new ExifData() { Key = title, Value = value.ToString() });
            }
        }

        private void UpdateExif()
        {
            var e = new List<ExifData>();
            try
            {
                using (var reader = new ExifReader(Name))
                {
                    GetExifTag(reader, ExifTags.ImageUniqueID, "ID", e);
                    GetExifTag(reader, ExifTags.DateTimeDigitized, "Date taken", e);
                    GetExifTag(reader, ExifTags.PixelXDimension, "Width", e);
                    GetExifTag(reader, ExifTags.PixelYDimension, "Height", e);
                    GetExifTag(reader, ExifTags.XResolution, "X Resolution", e);
                    GetExifTag(reader, ExifTags.YResolution, "Y Resolution", e);
                    GetExifTag(reader, ExifTags.Make, "Device", e);
                    GetExifTag(reader, ExifTags.Model, "Device model", e);
                }
            }
            catch(ExifLibException eex)
            {

            }
            this.exif = new ObservableCollection<ExifData>(e);
        }

        public void ShowImage()
        {
            Process.Start(this.Name);
        }

        public void DeleteImage()
        {
            FileOperationAPIWrapper.Send(this.Name);
            this.eventAggregator.PublishOnUIThread(new FileDeletedMessage() { FileName = this.Name });
        }
    }

    public class ExifData
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
