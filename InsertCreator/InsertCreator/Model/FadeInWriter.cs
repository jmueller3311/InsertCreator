﻿using HgSoftware.InsertCreator.ViewModel;
using System;
using System.Drawing;
using System.IO;

namespace HgSoftware.InsertCreator.Model
{
    public class FadeInWriter
    {
        #region Private Fields

        private readonly PictureReader _pictureReader = new PictureReader();
        private readonly PositionData _positionData;

        #endregion Private Fields

        #region Public Constructors

        public FadeInWriter(PositionData positionData)
        {
            _positionData = positionData;
            CurrentFade = LoadFrame(!Properties.Settings.Default.UseGreenscreen, Properties.Settings.Default.LogoAsCornerlogo);
        }

        #endregion Public Constructors

        #region Public Events

        public event EventHandler<Bitmap> OnInsertUpdate;

        #endregion Public Events

        #region Public Properties

        public Bitmap CurrentFade { get; private set; }

        #endregion Public Properties

        #region Public Methods

        public void LoadImages()
        {
            Bitmap image = LoadFrame(!Properties.Settings.Default.UseGreenscreen, Properties.Settings.Default.LogoAsCornerlogo);
            var drawingTool = Graphics.FromImage(image);
            DrawLogo(drawingTool);
            image.Save($"{Environment.GetEnvironmentVariable("userprofile")}/InsertCreator/Insert.png", System.Drawing.Imaging.ImageFormat.Png);
            image.Save($"{Environment.GetEnvironmentVariable("userprofile")}/InsertCreator/HymnalInsert.png", System.Drawing.Imaging.ImageFormat.Png);
            image.Save($"{Environment.GetEnvironmentVariable("userprofile")}/InsertCreator/MinistryInsert.png", System.Drawing.Imaging.ImageFormat.Png);
        }

        public void ResetFade()
        {
            Bitmap image = LoadFrame(!Properties.Settings.Default.UseGreenscreen, Properties.Settings.Default.LogoAsCornerlogo);
            image.Save($"{Environment.GetEnvironmentVariable("userprofile")}/InsertCreator/Insert.png", System.Drawing.Imaging.ImageFormat.Png);
            image.Save($"{Environment.GetEnvironmentVariable("userprofile")}/InsertCreator/HymnalInsert.png", System.Drawing.Imaging.ImageFormat.Png);
            image.Save($"{Environment.GetEnvironmentVariable("userprofile")}/InsertCreator/MinistryInsert.png", System.Drawing.Imaging.ImageFormat.Png);
            image.Save($"{Environment.GetEnvironmentVariable("userprofile")}/InsertCreator/CustomInsert.png", System.Drawing.Imaging.ImageFormat.Png);
            CurrentFade = image;
            OnInsertUpdate?.Invoke(this, image);
        }

        public float TextPositionX()
        {
            if (Properties.Settings.Default.LogoOnLefthand)
                return 280;
            return 70;
        }

        public void WriteFade(IInsertData insert)
        {
            switch (insert)
            {
                case CustomInsert _:
                    WriteCustom(insert as CustomInsert);
                    break;

                case HymnalData _:
                    WriteHymnalFade(insert as HymnalData);
                    break;

                case MinistryGridViewModel _:
                    WriteMinistryFade(insert as MinistryGridViewModel);
                    break;
            }
        }

        #endregion Public Methods

        #region Private Methods

        private void CreateCustomInsertDouble(string textLaneOne, string textLaneTwo, bool transparent = true, bool useCornerBug = false)
        {
            Bitmap image = LoadFrame(transparent, useCornerBug);
            var drawingTool = Graphics.FromImage(image);
            DrawRectangle(drawingTool);

            drawingTool.DrawString(
            textLaneOne,
            _positionData.FontTextTwoRowFirstLine,
            new SolidBrush(Color.Black), _positionData.TextTwoRowFirstLinePosition);

            drawingTool.DrawString(
             textLaneTwo,
             _positionData.FontTextTwoRowSecondLine,
             new SolidBrush(Color.Black), _positionData.TextTwoRowSecondLinePosition);

            DrawLogo(drawingTool);
            image.Save($"{Environment.GetEnvironmentVariable("userprofile")}/InsertCreator/Insert.png", System.Drawing.Imaging.ImageFormat.Png);
            image.Save($"{Environment.GetEnvironmentVariable("userprofile")}/InsertCreator/CustomInsert.png", System.Drawing.Imaging.ImageFormat.Png);
            CurrentFade = image;
            OnInsertUpdate?.Invoke(this, image);
        }

        private void CreateCustomInsertSingle(string text, bool transparent = true, bool useCornerBug = false)
        {
            Bitmap image = LoadFrame(transparent, useCornerBug);
            var drawingTool = Graphics.FromImage(image);
            DrawRectangle(drawingTool);

            drawingTool.DrawString(
            text,
            _positionData.FontTextOneRowFirstLine,
            new SolidBrush(Color.Black), _positionData.TextOneRowFirstLinePosition);

            DrawLogo(drawingTool);
            image.Save($"{Environment.GetEnvironmentVariable("userprofile")}/InsertCreator/Insert.png", System.Drawing.Imaging.ImageFormat.Png);
            image.Save($"{Environment.GetEnvironmentVariable("userprofile")}/InsertCreator/CustomInsert.png", System.Drawing.Imaging.ImageFormat.Png);
            CurrentFade = image;
            OnInsertUpdate?.Invoke(this, image);
        }

        private void CreateHymnalInsertPicture(HymnalData hymnalData, bool transparent = true, bool useCornerBug = false)
        {
            Bitmap image = LoadFrame(transparent, useCornerBug);

            var drawingTool = Graphics.FromImage(image);

            DrawRectangle(drawingTool);

            drawingTool.DrawString(
             $"{hymnalData.Book} {hymnalData.Number}{hymnalData.SongVerses}",
             _positionData.FontTextTwoRowFirstLine,
             new SolidBrush(Color.Black), _positionData.TextTwoRowFirstLinePosition);

            drawingTool.DrawString(
                hymnalData.Name,
                _positionData.FontTextTwoRowSecondLine,
                new SolidBrush(Color.Black), _positionData.TextTwoRowSecondLinePosition);

            DrawLogo(drawingTool);

            image.Save($"{Environment.GetEnvironmentVariable("userprofile")}/InsertCreator/Insert.png", System.Drawing.Imaging.ImageFormat.Png);
            image.Save($"{Environment.GetEnvironmentVariable("userprofile")}/InsertCreator/HymnalInsert.png", System.Drawing.Imaging.ImageFormat.Png);
            CurrentFade = image;
            OnInsertUpdate?.Invoke(this, image);
        }

        private void CreateHymnalInsertPictureMeta(HymnalData hymnalData, bool transparent, bool useCornerBug)
        {
            Bitmap image = LoadFrame(transparent, useCornerBug);

            var drawingTool = Graphics.FromImage(image);

            DrawRectangle(drawingTool);

            drawingTool.DrawString(
             $"{hymnalData.Book} {hymnalData.Number}{hymnalData.SongVerses}",
             _positionData.FontTextFourRowFirstLine,
             new SolidBrush(Color.Black), _positionData.TextFourRowFirstLinePosition);

            drawingTool.DrawString(
                hymnalData.Name,
                _positionData.FontTextFourRowSecondLine,
                new SolidBrush(Color.Black), _positionData.TextFourRowSecondLinePosition);

            drawingTool.DrawString(
               hymnalData.TextAutor,
               _positionData.FontTextFourRowThirdLine,
               new SolidBrush(Color.Black), _positionData.TextFourRowThirdLinePosition);

            drawingTool.DrawString(
               hymnalData.MelodieAutor,
               _positionData.FontTextFourRowFourthLine,
               new SolidBrush(Color.Black), _positionData.TextFourRowFourthLinePosition);

            DrawLogo(drawingTool);
            image.Save($"{Environment.GetEnvironmentVariable("userprofile")}/InsertCreator/Insert.png", System.Drawing.Imaging.ImageFormat.Png);
            image.Save($"{Environment.GetEnvironmentVariable("userprofile")}/InsertCreator/HymnalInsert.png", System.Drawing.Imaging.ImageFormat.Png);
            CurrentFade = image;
            OnInsertUpdate?.Invoke(this, image);
        }

        private void CreateMinistrieInsert(MinistryGridViewModel ministry, bool transparent = true, bool useCornerBug = false)
        {
            Bitmap image = LoadFrame(transparent, useCornerBug);

            var drawingTool = Graphics.FromImage(image);
            DrawRectangle(drawingTool);

            drawingTool.DrawString(
             $"{ministry.ForeName} {ministry.SureName}",
             _positionData.FontTextTwoRowFirstLine,
             new SolidBrush(Color.Black), _positionData.TextTwoRowFirstLinePosition);

            drawingTool.DrawString(
             ministry.Function,
             _positionData.FontTextTwoRowSecondLine,
             new SolidBrush(Color.Black), _positionData.TextTwoRowSecondLinePosition);

            DrawLogo(drawingTool);
            image.Save($"{Environment.GetEnvironmentVariable("userprofile")}/InsertCreator/Insert.png", System.Drawing.Imaging.ImageFormat.Png);
            image.Save($"{Environment.GetEnvironmentVariable("userprofile")}/InsertCreator/MinistryInsert.png", System.Drawing.Imaging.ImageFormat.Png);
            CurrentFade = image;
            OnInsertUpdate?.Invoke(this, image);
        }

        private void DrawLogo(Graphics drawingTool)
        {
            if (File.Exists($"{Environment.GetEnvironmentVariable("userprofile")}/InsertCreator/Logo.png"))
            {
                var image = _pictureReader.ResizePicture(new Bitmap($"{Environment.GetEnvironmentVariable("userprofile")}/InsertCreator/Logo.png"), _positionData.SizeLogo);
                LogoWriter(drawingTool, image, _positionData.LogoPosition, _positionData.SizeLogo);
            }
        }

        private void DrawRectangle(Graphics drawingTool)
        {
            System.Drawing.SolidBrush myBrush = new System.Drawing.SolidBrush(System.Drawing.Color.White);
            drawingTool.FillRectangle(myBrush, new Rectangle(_positionData.RectanglePosition.X, _positionData.RectanglePosition.Y, _positionData.SizeRectangle.Width, _positionData.SizeRectangle.Height));
            myBrush.Dispose();
        }

        private Bitmap LoadFrame(bool transparent, bool useCornerBug)
        {
            var transparentFrame = $"{Directory.GetCurrentDirectory()}/DataSource/InsertFrameTrans.png";
            var greenFrame = $"{Directory.GetCurrentDirectory()}/DataSource/InsertFrameGreen.png";
            Bitmap image;

            if (transparent)
                image = new Bitmap(transparentFrame);
            else
                image = new Bitmap(greenFrame);

            if (useCornerBug && File.Exists($"{Environment.GetEnvironmentVariable("userprofile")}/InsertCreator/Logo.png"))
            {
                var drawingTool = Graphics.FromImage(image);

                var logoImage = _pictureReader.ResizePicture(new Bitmap($"{Environment.GetEnvironmentVariable("userprofile")}/InsertCreator/Logo.png"), _positionData.SizeCornerbug);
                LogoWriter(drawingTool, logoImage, _positionData.CornerbugPosition, _positionData.SizeCornerbug);
            }
            return image;
        }

        private void LogoWriter(Graphics drawingTool, Image image, PointF position, float size)
        {
            if (image.Width == image.Height)
            {
                drawingTool.DrawImage(image, new PointF(position.X, position.Y));
                return;
            }
            if (image.Width > image.Height)
            {
                drawingTool.DrawImage(image, new PointF(position.X, ((size - image.Height) / 2) + position.Y));
                return;
            }
            if (image.Width < image.Height)
            {
                drawingTool.DrawImage(image, new PointF(((size - image.Width) / 2) + position.X, position.Y));
            }
        }

        private void WriteCustom(CustomInsert insert)
        {
            var greenScreen = !Properties.Settings.Default.UseGreenscreen;
            var cornerbug = Properties.Settings.Default.LogoAsCornerlogo;

            if (String.IsNullOrEmpty(insert.LineOne))
            {
                CreateCustomInsertSingle(insert.LineTwo, greenScreen, cornerbug);
                return;
            }

            if (String.IsNullOrEmpty(insert.LineTwo))
            {
                CreateCustomInsertSingle(insert.LineOne, greenScreen, cornerbug);
                return;
            }
            CreateCustomInsertDouble(insert.LineOne, insert.LineTwo, greenScreen, cornerbug);
        }

        private void WriteHymnalFade(HymnalData hymnalData)
        {
            var greenScreen = !Properties.Settings.Default.UseGreenscreen;
            var cornerbug = Properties.Settings.Default.LogoAsCornerlogo;

            if (Properties.Settings.Default.ShowComponistAndAutor)
                CreateHymnalInsertPictureMeta(hymnalData, greenScreen, cornerbug);
            else
                CreateHymnalInsertPicture(hymnalData, greenScreen, cornerbug);
        }

        private void WriteMinistryFade(MinistryGridViewModel ministry)
        {
            var greenScreen = !Properties.Settings.Default.UseGreenscreen;
            var cornerbug = Properties.Settings.Default.LogoAsCornerlogo;

            CreateMinistrieInsert(ministry, greenScreen, cornerbug);
        }

        #endregion Private Methods
    }
}