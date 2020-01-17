﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DxLibDLL;


namespace Mascot
{
    public partial class Viewer : Form
    {
        private int modelHandle;
        private int attachIndex;
        private float totalTime;
        private float playTime;
        private float playSpeed;
        private Point lStartPoint;
        private Point rStartPoint;


        public Viewer(string modelPath)
        {
            InitializeComponent();

            ClientSize = new Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            Text = "DesktopMascot";


            DX.SetOutApplicationLogValidFlag(DX.FALSE);
            DX.SetUserWindow(Handle);
            DX.SetZBufferBitDepth(24);
            DX.SetCreateDrawValidGraphZBufferBitDepth(24);
            DX.SetFullSceneAntiAliasingMode(4, 2);
            DX.DxLib_Init();
            DX.SetDrawScreen(DX.DX_SCREEN_BACK);
            modelHandle = DX.MV1LoadModel(modelPath); // load model
            attachIndex = DX.MV1AttachAnim(modelHandle, 2, -1, DX.FALSE); // set animation number
            totalTime = DX.MV1GetAttachAnimTotalTime(modelHandle, attachIndex);
            playTime = 0.0f;

            SetSpeed(0.5f);

            DX.SetCameraNearFar(0.1f, 1000.0f); // 奥行0.1～1000を描画範囲とする
            DX.SetCameraPositionAndTarget_UpVecY(DX.VGet(0.0f, 10.0f, -20.0f), DX.VGet(0.0f, 10.0f, 20.0f));

        }

        public void MainLoop()
        {
            DX.ClearDrawScreen();
            DX.DrawBox(0, 0, Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height, DX.GetColor(1, 1, 1), DX.TRUE);

            playTime += playSpeed;

            // アニメーションが最後まで行ったら先頭に戻る
            if(playTime >= totalTime)
            {
                playTime = 0.0f;
            }

            DX.MV1SetAttachAnimTime(modelHandle, attachIndex, playTime);

            DX.MV1DrawModel(modelHandle);

            // Press ESC to quit
            if(DX.CheckHitKey(DX.KEY_INPUT_ESCAPE) != 0)
            {
                Close();
            }

            DX.ScreenFlip();
        }

        public void SetSpeed(float speed)
        {
            this.playSpeed = speed;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            DX.DxLib_End();
        }

        private void Form1_Sown(object sender, EventArgs e)
        {
            FormBorderStyle = FormBorderStyle.None; // フォームの枠非表示
            TransparencyKey = Color.FromArgb(1, 1, 1);
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                lStartPoint = new Point(e.X, e.Y);
            }
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                rStartPoint = new Point(e.X, e.Y);
            }
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                this.Location = new Point(
                    this.Location.X + e.X - lStartPoint.X,
                    this.Location.Y + e.Y - lStartPoint.Y);
            }
            if ((e.Button & MouseButtons.Right) == MouseButtons.Left)
            {
                
            }
        }
    }
}
