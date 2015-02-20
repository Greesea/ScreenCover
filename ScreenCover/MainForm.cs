using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;

namespace ScreenCover
{
    public partial class MainForm : Form
    {
        #region 字段
        DataManager DataDM = null;
        CustomTab TopTab = null;

        bool WindowSizeMode = false;
        bool Pin = true;
        Point MouseMoveOffset = new Point(0, 0);

        bool NoTitle = false;
        #endregion

        #region 事件
        #region 窗体相关
        private void MainForm_Load(object sender, EventArgs e)
        {
            //读取配置和数据
            try
            {
                DataDM = new DataManager("Data\\Data.xml");
            }
            catch (Exception)
            {
                MessageBox.Show("数据文件中有条目尚未拥有必要的属性或数据文件格式不正确", "发生异常");
                Application.ExitThread();
                return;
            }

            //绘制关闭按钮
            CustomBtn closeBTN = new CustomBtn(Properties.Resources.close_normal, Properties.Resources.close_press, Properties.Resources.close_hover);
            closeBTN.Location = new Point(this.Size.Width - 30, 0);
            closeBTN.Anchor = AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
            closeBTN.MouseClick += CloseBTN_Click;
            TopPanel.Controls.Add(closeBTN);

            //绘制置顶按钮
            PinBtn pinBTN = new PinBtn(Properties.Resources.pin_normal, Properties.Resources.pin_press, Properties.Resources.pin_hover, Properties.Resources.unpin_normal, Properties.Resources.unpin_press, Properties.Resources.unpin_hover, this.TopMost);
            pinBTN.Location = new Point(this.Size.Width - 60, 0);
            pinBTN.Anchor = AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
            pinBTN.MouseClick += pinBTN_MouseClick;
            TopPanel.Controls.Add(pinBTN);

            //绘制保存按钮
            CustomBtn saveBTN = new CustomBtn(Properties.Resources.save_normal, Properties.Resources.save_press, Properties.Resources.save_hover);
            saveBTN.Location = new Point(this.Size.Width - 90, 0);
            saveBTN.Anchor = AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
            saveBTN.MouseClick += saveBTN_MouseClick;
            TopPanel.Controls.Add(saveBTN);

            //绘制选项卡
            if (DataDM.FolderList.Count > 0)
            {
                TopTab = new CustomTab();
                TopTab.Location = new Point(0, 0);

                TopTab.MouseDown += tab_MouseDown;
                TopTab.MouseUp += tab_MouseUp;
                TopTab.MouseEnter += tab_MouseEnter;
                TopTab.MouseLeave += tab_MouseLeave;
                TopTab.MouseClick += tab_MouseClick;
                TopPanel.Controls.Add(TopTab);

                TabCMS.Items.Clear();
                for (int i = 0; i < DataDM.FolderList.Count; i++)
                {
                    ToolStripItem item = new ToolStripMenuItem(DataDM.FolderList[i].Name);
                    item.Tag = i;
                    item.Click += TabCMS_Click;
                    TabCMS.Items.Add(item);

                    if (i == 0)
                    {
                        SwapTab(i);
                    }
                }
            }

            //设置图片显示区域大小
            MainPicBox.Size = new Size(this.Size.Width, this.Size.Height - 31);
            MainPicBox.Location = new Point(0, 29);
            MainPicBox.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom;
        }

        private void MainForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SwapStyle(NoTitle);
            }
            else if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
            else if (e.KeyCode == Keys.PageUp)
            {
                OpacityUpDown(1);
            }
            else if (e.KeyCode == Keys.PageDown)
            {
                OpacityUpDown(-1);
            }
        }
        #endregion

        #region 拖拽和大小调整
        void MouseMove_Move(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (WindowSizeMode)
                {
                    this.Size = new Size(this.PointToClient(MousePosition).X, this.PointToClient(MousePosition).Y);
                }
                else if (!Pin)
                {
                    this.Location = new Point(this.Location.X + (e.X - MouseMoveOffset.X), this.Location.Y + (e.Y - MouseMoveOffset.Y));
                }
            }
        }

        void MouseMove_Up(object sender, MouseEventArgs e)
        {
            WindowSizeMode = false;
        }

        void MouseMove_Down(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (e.X < this.Size.Width - 11 && e.Y < this.Size.Height - 42)
                {
                    MouseMoveOffset = new Point(e.X, e.Y);
                    WindowSizeMode = false;
                }
                else if (!Pin)
                {
                    WindowSizeMode = true;
                }
            }
        }
        #endregion

        #region 标签页处理
        void TabCMS_Click(object sender, EventArgs e)
        {
            SwapTab(Convert.ToInt32((sender as ToolStripMenuItem).Tag));
        }

        void tab_MouseClick(object sender, MouseEventArgs e)
        {
            TabCMS.Show(this.Location.X, this.Location.Y + 29);
        }

        void tab_MouseUp(object sender, MouseEventArgs e)
        {
            (sender as CustomTab).BackColor = TopPanel.BackColor;
            (sender as CustomTab).ForeColor = Color.Black;
        }

        void tab_MouseDown(object sender, MouseEventArgs e)
        {
            (sender as CustomTab).BackColor = Color.FromArgb(9, 59, 76);
            (sender as CustomTab).ForeColor = Color.White;
        }

        void tab_MouseLeave(object sender, EventArgs e)
        {
            (sender as CustomTab).BackColor = TopPanel.BackColor;
        }

        void tab_MouseEnter(object sender, EventArgs e)
        {
            (sender as CustomTab).BackColor = Color.FromArgb(99, 174, 206);
        }
        #endregion

        #region 其它
        void MainForm_MouseWheel(object sender, MouseEventArgs e) //窗体滚轮事件
        {
            OpacityUpDown(e.Delta);
        }

        private void pinBTN_MouseClick(object sender, MouseEventArgs e) //Pin按钮按下并释放事件
        {
            if (this.TopMost)
            {
                this.TopMost = false;
                Pin = false;
            }
            else
            {
                this.TopMost = true;
                Pin = true;
            }

            (sender as PinBtn).SwapState(this.TopMost);
        }

        private void CloseBTN_Click(object sender, MouseEventArgs e) //关闭按钮按下并释放事件
        {
            this.Close();
        }

        void saveBTN_MouseClick(object sender, MouseEventArgs e) //保存按钮按下并释放事件
        {
            if (MessageBox.Show("确定要保存 \"窗体位置 大小 透明度\" 到文件吗？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Folder f = new Folder(DataDM.FolderList[TopTab.Tag.ToString()].Name);
                f.WindowX = this.Location.X;
                f.WindowY = this.Location.Y;
                f.ImageW = this.Width;
                f.ImageH = this.Height;
                f.Transparent = this.Opacity;

                if (DataDM.UpdateFolderSettings(f))
                {
                    MessageBox.Show("保存成功", "提示");
                }
                else
                {
                    MessageBox.Show("保存失败", "提示");
                }
            }
        }

        void item_Click(object sender, EventArgs e)
        {
            SwapImage((TopTab.Tag as Folder).Childs[Convert.ToInt32((sender as ToolStripItem).Tag)].Value);
        }
        #endregion
        #endregion

        public MainForm()
        {
            InitializeComponent();

            //检查Data目录是否存在 不存在则创建
            if (!Directory.Exists("Data"))
            {
                Directory.CreateDirectory("Data");
            }

            //绑定滚轮
            this.MouseWheel += MainForm_MouseWheel;

            //绑定拖拽和大小调整
            MainPicBox.MouseDown += MouseMove_Down;
            MainPicBox.MouseMove += MouseMove_Move;
            MainPicBox.MouseUp += MouseMove_Up;
            TopPanel.MouseDown += MouseMove_Down;
            TopPanel.MouseMove += MouseMove_Move;
            TopPanel.MouseUp += MouseMove_Up;
        }

        void OpacityUpDown(int delta)
        {
            if (delta > 0)
            {
                if (this.Opacity + 0.05 > 1)
                {
                    this.Opacity = 1;
                }
                else
                {
                    this.Opacity += 0.05;
                }
            }
            else if (delta < 0)
            {
                if (this.Opacity - 0.05 < 0.05)
                {
                    this.Opacity = 0.05;
                }
                else
                {
                    this.Opacity -= 0.05;
                }
            }
        }

        void SwapImage(string path)
        {
            path = "Data\\" + path;
            if (File.Exists(path))
            {
                MainPicBox.Image = Image.FromFile(path);
                return;
            }
            MainPicBox.Image = null;
        }

        void SwapStyle(bool state)
        {
            if (state)
            {
                TopPanel.Visible = true;
                NoTitle = false;
            }
            else
            {
                TopPanel.Visible = false;
                NoTitle = true;
            }
        }

        void SwapTab(int index)
        {
            Folder f = DataDM.FolderList[index];
            TopTab.Tag = f;
            TopTab.Text = f.Name;
            TopTab.ResizeControl();

            MainCMS.Items.Clear();
            for (int i = 0; i < f.Childs.Count; i++)
            {
                ToolStripItem item = new ToolStripMenuItem(f.Childs[i].Name);
                item.Tag = i;
                item.Click += item_Click;
                MainCMS.Items.Add(item);
            }
            MainCMS.Items[0].PerformClick();

            int x = (f.WindowX != -1) ? f.WindowX : this.Location.X;
            int y = (f.WindowY != -1) ? f.WindowY : this.Location.Y;
            int w = (f.ImageW != -1) ? f.ImageW : this.Width;
            int h = (f.ImageH != -1) ? f.ImageH + 31 : this.Height;
            double t = (f.Transparent != -1) ? f.Transparent : this.Opacity;

            this.Location = new Point(x, y);
            this.Size = new Size(w, h);
            this.Opacity = t;
        }
    }
}
