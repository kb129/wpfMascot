using System;
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
    public partial class Form1 : Form
    {
        private int modelHandle;
        private int attachIndex;
        private float totalTime;
        private float playTime;
        private float playSpeed;

        public Form1()
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

            modelHandle = DX.MV1LoadModel("Data/rin_sour/miku.pmx"); // load model
            attachIndex = DX.MV1AttachAnim(modelHandle, 31, -1, DX.FALSE); // set animation number
            totalTime = DX.MV1GetAttachAnimTime(modelHandle, attachIndex);
            playTime = 0.0f;
            playSpeed = 3.0f;

            DX.SetCameraNearFar(0.1f, 1000.0f); // 奥行0.1～1000を描画範囲とする
            DX.SetCameraPositionAndTarget_UpVecY(DX.VGet(0.0f, 10.0f, -20.0f), DX.VGet(0.0f, 10.0f, 0.0f));

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

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            DX.DxLib_End();
        }

        private void Form1_Sown(object sender, EventArgs e)
        {
            FormBorderStyle = FormBorderStyle.None; // フォームの枠非表示
            TransparencyKey = Color.FromArgb(1, 1, 1);
        }
    }
}
